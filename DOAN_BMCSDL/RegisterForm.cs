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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using BCrypt.Net;
using System.Text.RegularExpressions;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DOAN_BMCSDL
{
    public partial class RegisterForm : Form
    {
        private string connectionString = @"Data Source=localhost:1521/orcl;Persist Security Info=True;User ID=DoAn_Nhom4;Password=thedung4";
        public RegisterForm()
        {
            InitializeComponent();
        }
        public bool checkAccount(string account)
        {
            return Regex.IsMatch(account, "^[a-z0-9]{6,24}$");
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text;
            string password = txtPassword.Text;
            string confirmPassword = txtConfirmPassword.Text;

            // Kiểm tra các trường hợp rỗng
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                MessageBox.Show("Hãy điền hết những ô trống.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (checkAccount(username) != true)
            {
                MessageBox.Show("Vui lòng nhập tên tài khoản dài 6-24 ký tự, với ký tự chữ và số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (checkAccount(password) != true)
            {
                MessageBox.Show("Vui lòng nhập mật khẩu dài 6-24 ký tự, với ký tự chữ và số!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (password != confirmPassword)
            {
                MessageBox.Show("Mật khẩu không khớp, hãy kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try {
                Database.Set_Database("localhost", "1521", "orcl", "create_user_account01", "123");
                Database.Connect();
                OracleCommand cmd = new OracleCommand("CreateUser", Database.conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("username", OracleDbType.NVarchar2).Value = username;
                cmd.Parameters.Add("password", OracleDbType.NVarchar2).Value = password;

                cmd.ExecuteNonQuery();
                MessageBox.Show("Đăng ký thành công!");
                Database.conn.Close();
                LoginForm loginForm = new LoginForm();
                loginForm.ShowDialog();
                this.Close();
            }
            catch
            {
                MessageBox.Show("Lỗi Sql!");
            }
            

            // Mã hóa mật khẩu
            //string passwordHash = BCrypt.Net.BCrypt.HashPassword(password);

            //using (OracleConnection conn = new OracleConnection(connectionString))
            //{
            //    conn.Open();



            //    // Thêm người dùng mới vào cơ sở dữ liệu
            //    string query = "INSERT INTO LOGIN VALUES ('"+ username +"','"+ passwordHash +"')";

            //    using (OracleCommand cmd = new OracleCommand(query, conn))
            //    {


            //        try
            //        {
            //            cmd.ExecuteNonQuery();

            //            MessageBox.Show("Đăng ký thành công!. Bạn có thể đăng nhập.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            LoginForm loginForm = new LoginForm();
            //            loginForm.ShowDialog();
            //            this.Close(); // Đóng form đăng ký sau khi thành công
            //        }
            //        catch (Exception ex)
            //        {
            //            MessageBox.Show("Tên đăng nhập đã tồn tại. Hãy chọn tên khác!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //    }
            //}
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtConfirmPassword_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
