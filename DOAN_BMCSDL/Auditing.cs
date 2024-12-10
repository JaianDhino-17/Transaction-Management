using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
namespace DOAN_BMCSDL
{
    public partial class Auditing : Form
    {
        private OracleConnection CONN;
        public Auditing()
        {
            InitializeComponent();
            CenterToScreen(); 
            CONN = Database.Get_connect();


        }

        private void Auditing_Load(object sender, EventArgs e)
        {
            load_Cbo_User(CONN);
        }

        private void load_Cbo_User(OracleConnection conn)
        {
            try
            {
                // Thủ tục pro_select_all_users đã được tạo ở lab đầu tiên bài OLS
                using (OracleCommand command = new OracleCommand("PRO_SYS_SELECT_USER_DML", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // Tạo tham số output
                    OracleParameter outParam = new OracleParameter("CUR", OracleDbType.RefCursor);
                    outParam.Direction = ParameterDirection.Output;
                    command.Parameters.Add(outParam);

                    // Thực thi thủ tục
                    command.ExecuteNonQuery();

                    // Lấy dữ liệu từ tham số output
                    using (OracleDataReader reader = command.ExecuteReader())
                    {
                        comboBox1.Items.Clear();
                        while (reader.Read())
                        {
                            string userName = reader.GetString(0);
                            comboBox1.Items.Add(userName);
                            comboBox1.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Error Select user: " + ex.Message);
            }
        }
        public void LoadAuditUser(string user, DataGridView dataGridView, OracleConnection conn)
        {
            try
            {
                using (OracleCommand command = new OracleCommand("SYS.pro_select_audit_trail_user", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.Add("username", OracleDbType.Varchar2).Value = user;
                    command.Parameters.Add("cur", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        dataGridView.DataSource = dataTable;
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi không xem bảng audit user được: " + e.Message);
            }
        }

        private void cbo_User_SelectedIndexChanged(object sender, EventArgs e)
        {
            string user = comboBox1.SelectedItem.ToString();
            LoadAuditUser(user, dataGridView1, CONN);
            load_checkListBox_audit_opts(user, CONN);
        }

        // Hàm xử lý nút refresh:
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            string user = comboBox1.SelectedItem.ToString();
            LoadAuditUser(user, dataGridView1, CONN);
            load_checkListBox_audit_opts(user, CONN);
        }

        private void load_checkListBox_audit_opts(string user, OracleConnection conn)
        {
            try
            {
                flowLayoutPanel1.Controls.Clear();
                using (OracleCommand command = new OracleCommand("SYS.PRO_SELECT_STMT_AUDIT_OPTS", conn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("USERNAME", OracleDbType.Varchar2).Value = user;
                    command.Parameters.Add("CUR", OracleDbType.RefCursor).Direction = ParameterDirection.Output;

                    using (OracleDataAdapter adapter = new OracleDataAdapter(command))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        // Tạo một HashSet để lưu trữ các AUDIT_OPTION duy nhất từ cơ sở dữ liệu
                        HashSet<string> uniqueOptions = new HashSet<string>();

                        foreach (DataRow row in dt.Rows)
                        {
                            string auditOption = row["AUDIT_OPTION"].ToString().ToUpper();
                            uniqueOptions.Add(auditOption);
                        }
                        foreach (string option in uniqueOptions)
                        {
                            CheckBox cb = new CheckBox
                            {
                                Text = option,
                                Checked = true,
                                AutoSize = true
                            };
                            cb.CheckedChanged += Checkbox_CheckedChanged; // Thêm sự kiện CheckedChanged
                            flowLayoutPanel1.Controls.Add(cb);
                        }
                        // Nếu bạn muốn thêm các tùy chọn bổ sung không có trong kết quả từ Oracle
                        //string[] additionalOptions = { "CREATE ANY TABLE", "DROP ANY TABLE", "DELETE TABLE", "INSERT TABLE", "SELECT TABLE", "UPDATE TABLE", "DELETE ANY TABLE", "INSERT ANY TABLE", "SELECT ANY TABLE", "UPDATE ANY TABLE" };
                        string[] additionalOptions = { "CREATE ANY TABLE", "DROP ANY TABLE", "DELETE TABLE", "INSERT TABLE", "SELECT TABLE", "UPDATE TABLE", "DELETE ANY TABLE", "INSERT ANY TABLE", "SELECT ANY TABLE", "UPDATE ANY TABLE" };
                        foreach (string option in additionalOptions)
                        {
                            if (!flowLayoutPanel1.Controls.Cast<CheckBox>().Any(cb => cb.Text == option))
                            {
                                CheckBox cb = new CheckBox
                                {
                                    Text = option,
                                    Checked = false,
                                    AutoSize = true
                                };
                                cb.CheckedChanged += Checkbox_CheckedChanged; // Thêm sự kiện CheckedChanged
                                flowLayoutPanel1.Controls.Add(cb);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Lỗi không xem bảng audit user được: " + e.Message);
            }
        }

        private void Checkbox_CheckedChanged(object sender, EventArgs e)
        {
            string userName = comboBox1.SelectedItem.ToString();
            CheckBox cb = sender as CheckBox;
            if (cb != null)
            {
                if (cb.Checked)
                {
                    // Gọi thủ tục tạo audit với 2 tham số là câu lệnh và user name
                    ExecuteAuditProcedure("pro_create_audit", cb.Text, userName);
                }
                else
                {
                    // Gọi thủ tục xóa audit (noaudit) với 2 tham số là câu lệnh và user name
                    ExecuteAuditProcedure("pro_drop_audit", cb.Text, userName);
                }
            }
        }

        private void ExecuteAuditProcedure(string procedure, string statement, string username)
        {
            try
            {
                using (OracleCommand cmd = new OracleCommand(procedure, CONN))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Thêm các tham số
                    cmd.Parameters.Add("p_statement", OracleDbType.Varchar2).Value = statement;
                    cmd.Parameters.Add("p_username", OracleDbType.Varchar2).Value = username;

                    // Thực thi thủ tục
                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Audit command executed successfully for user " + username,
                                    "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (OracleException ex)
            {
                MessageBox.Show("Error executing audit procedure: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Administrator administrator = new Administrator();
            administrator.ShowDialog();
            this.Hide();
        }
    }
}
