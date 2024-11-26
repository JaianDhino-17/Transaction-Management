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
using Guna.UI2.WinForms;

namespace DOAN_BMCSDL
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            DialogResult Exit = MessageBox.Show("Bạn muốn thoát?","Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(Exit == DialogResult.Yes)
                 Application.Exit();
        }

        public static string username;
        //public static string password;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            //username = txtUsername.Text;
            //password = txtPassword.Text;
            string host = textBox1.Text;
            string port = textBox2.Text;
            string sid = textBox3.Text;
            string user = txtUsername.Text;
            string password = txtPassword.Text;
            username = user;
            Database.Set_Database(host, port, sid, user, password);
            if (Database.Connect())
            {
                SimpleAuthentication simpleAuthentication = new SimpleAuthentication(password);
                simpleAuthentication.ShowDialog();
                OracleConnection conn = Database.Get_connect();
                MessageBox.Show("Đăng nhập thành công!");
                AccountInforForm accountInforForm = new AccountInforForm();
                accountInforForm.Show();
                this.Hide();
            }
            else
                MessageBox.Show("Đăng nhập không thành công!\nVui lòng kiểm tra lại tài khoản");
        }
        private void btnRegister_Click_1(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void btn_admin_login_Click(object sender, EventArgs e)
        {
            Administrator administrator = new Administrator();
            administrator.Show();
            this.Hide();
        }
    }
}
