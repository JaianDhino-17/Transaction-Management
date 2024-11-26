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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DOAN_BMCSDL
{
    public partial class Administrator : Form
    {
        public Administrator()
        {
            InitializeComponent();
            CenterToScreen();
        }
        public static string username;
        

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            string host = textBox1.Text;
            string port = textBox2.Text;
            string sid = textBox3.Text;
            string user = txtUsername.Text;
            string password = txtPassword.Text;
            username = user;
            Database.Set_Database(host, port, sid, user, password);
            if (Database.Connect())
            {

                OracleConnection conn = Database.Get_connect();
                MessageBox.Show("Đăng nhập thành công!");
                Admin_Privilege admin_Privilege = new Admin_Privilege();
                admin_Privilege.ShowDialog();
                this.Hide();
            }
            else
                MessageBox.Show("Đăng nhập không thành công!\nVui lòng kiểm tra lại tài khoản");
        }
    }
}
