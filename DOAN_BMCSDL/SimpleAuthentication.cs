using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace DOAN_BMCSDL
{
    public partial class SimpleAuthentication : Form
    {
        string password;
        public SimpleAuthentication(string Password)
        {
            this.password = Password;
            InitializeComponent();
            textBox1.Text = HashPassword(password);
        }
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            //Giả sử đã gửi cho CSDL để xác thực
            string passwordHash = HashPassword(password);
            if (passwordHash == textBox1.Text)
            {
                MessageBox.Show("Xác thực thành công 1 trên 4 bước xác thực!.");
                Challenge_ResponeAuthentication challenge_ResponeAuthentication = new Challenge_ResponeAuthentication(password);
                challenge_ResponeAuthentication.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Xác thực không thành thành công!. \nVui lòng đăng nhập lại");
                this.Close();
            }
        }
    }
}
