using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;

namespace DOAN_BMCSDL
{
    public partial class AccountInforForm : Form
    {
        private string connectionString = @"Data Source=localhost:1521/orcl;Persist Security Info=True;User ID=DoAnNhom4;Password=thedung4";
        private System.Timers.Timer timer;

        public AccountInforForm()
        {
            InitializeComponent();
            HiBox.Text = "Hi, " + LoginForm.username;
            LoadAccountInfo("SELECT * FROM Users");
            InitializeSessionCheckTimer();
        }
        private void InitializeSessionCheckTimer()
        {
            // Tạo timer với khoảng thời gian 3 giây (3000 milliseconds)
            timer = new System.Timers.Timer(3000);
            timer.Elapsed += OnSessionCheck;
            timer.AutoReset = true;
            timer.Enabled = true;    // Bắt đầu chạy timer
        }

        private void OnSessionCheck(Object source, ElapsedEventArgs e)
        {
            // Kiểm tra session trong Oracle
            if (IsSessionKilled())
            {
                // Session đã bị kill, thực thi lệnh cần thiết, ví dụ: đăng xuất người dùng
                timer.Stop();
                MessageBox.Show("Tài khoản đã đăng xuất. Vui lòng đăng nhập lại!");
                this.Invoke(new Action(() => {
                    LoginForm loginForm = new LoginForm();
                    loginForm.Show();
                    this.Hide(); // Đóng form hoặc thực thi hành động khác
                }));
            }
        }

        private bool IsSessionKilled()
        {
            try
            {
                string connectionString2 = @"Data Source=localhost:1521/orcl;Persist Security Info=True;User ID=KillSession;Password=123";
                using (OracleConnection connection = new OracleConnection(connectionString2))
                {
                    connection.Open();

                   
                    string checkSessionQuery = $"SELECT COUNT(*) FROM v$session WHERE username = '{LoginForm.username.ToUpper()}'"; // Điều chỉnh theo nhu cầu
                    OracleCommand command = new OracleCommand(checkSessionQuery, connection);

                    int sessionCount = Convert.ToInt32(command.ExecuteScalar());

                    return sessionCount == 0; // Nếu không còn session, tức là đã bị kill
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi khi kết nối Oracle thất bại
                MessageBox.Show("Lỗi khi check Session: " + ex.Message);
                timer.Stop();
                return false;
            }
        }
        
        private void LoadAccountInfo(string query)
        {
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

               

                using (OracleDataAdapter adapter = new OracleDataAdapter(query, conn))
                {  
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
        }



        private void btnClose_Click(object sender, EventArgs e)
        {
            DialogResult Exit = MessageBox.Show("Bạn muốn thoát?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (Exit == DialogResult.Yes)
                Application.Exit();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult logOut = MessageBox.Show("Bạn có chắc chắn muốn đăng xuất?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (logOut == DialogResult.Yes)
            {
                try
                {

                    Database.Set_Database("localhost", "1521", "orcl", "KillSession", "123");
                    Database.Connect();


                    using (OracleCommand cmd = new OracleCommand("kill_all_sessions_other", Database.conn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("p_username", OracleDbType.NVarchar2).Value = LoginForm.username;

                        cmd.ExecuteNonQuery();
                        Database.conn.Close();
                        this.Close();
                        LoginForm loginForm = new LoginForm();
                        loginForm.ShowDialog();
                    }

                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi: {ex.Message}");
                }


                Database.conn.Close();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string accountID = txtAccountID.Text.Trim();
            string userID = txtUserID.Text.Trim();

            string query = "SELECT a.AccountID, a.UserID, a.AccountNumber, a.AccountType, a.Balance, a.EncryptedDetails, u.FullName " +
                           "FROM Accounts a " +
                           "JOIN Users u ON a.UserID = u.UserID " +
                           "WHERE 1=1"; 
            if (!string.IsNullOrEmpty(accountID))
            {
                query += " AND a.AccountID LIKE :accountID";
            }

            
            if (!string.IsNullOrEmpty(userID))
            {
                query += " AND u.UserID LIKE :userID";
            }

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();

                    using (OracleDataAdapter adapter = new OracleDataAdapter(query, conn))
                    {
                        
                        if (!string.IsNullOrEmpty(accountID))
                        {
                            adapter.SelectCommand.Parameters.Add(new OracleParameter("accountID", "%" + accountID + "%"));
                        }
                        if (!string.IsNullOrEmpty(userID))
                        {
                            adapter.SelectCommand.Parameters.Add(new OracleParameter("userID", "%" + userID + "%"));
                        }

                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView1.DataSource = dataTable;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi tìm kiếm: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }






        private void txtUserID_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtAccountID_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnAdd_Click_Click(object sender, EventArgs e)
        {

        }

        private void btnAddAccount_Click(object sender, EventArgs e)
        {
            Add addForm = new Add();
            addForm.ShowDialog();
            LoadAccountInfo("SELECT * FROM Accounts");
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            UpdateForm update = new UpdateForm();
            update.ShowDialog();
            LoadAccountInfo("SELECT * FROM Accounts");
        }

        private void btn_Delete_Click(object sender, EventArgs e)
        {
            using (DeleteForm deleteForm = new DeleteForm())
            {
                if (deleteForm.ShowDialog() == DialogResult.OK)
                {
                    LoadAccountInfo("SELECT a.AccountID, a.AccountNumber, a.AccountType, a.Balance, a.EncryptedDetails, u.FullName FROM Accounts a JOIN Users u ON a.UserID = u.UserID");
                }
            }
        }




        private void button2_Click(object sender, EventArgs e)
        {
            TransactionManagement transactionManagement = new TransactionManagement();
            transactionManagement.Show();
            this.Hide();
        }

        private void HiBox_Click(object sender, EventArgs e)
        {

        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            refreshData();
        }

        void refreshData()
        {
            string query = "SELECT * FROM USERS";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();

                using (OracleDataAdapter data = new OracleDataAdapter(query, conn))
                {
                    DataTable dataTable = new DataTable();
                    data.Fill(dataTable);

                    dataGridView1.DataSource = dataTable;
                }
            }
        }
    }
}
