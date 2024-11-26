using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_BMCSDL
{
    public partial class Challenge_ResponeAuthentication : Form
    {
        string password;
        string pass_nonce;
        public Challenge_ResponeAuthentication(string Password)
        {
            InitializeComponent();
            this.password = Password;
        }

        private void Challenge_ResponeAuthentication_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Tạo đối tượng Random
            Random rand = new Random();

            // Tạo một số nguyên ngẫu nhiên trong một khoảng nhất định (0 đến 99)
            int randomNumberInRange = rand.Next(0, 100);
            textBox1.Text = randomNumberInRange.ToString();

            pass_nonce = password + textBox1.Text;
            textBox2.Text = SimpleAuthentication.HashPassword(pass_nonce);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Giả sử đã gửi cho CSDL để xác thực
            if (SimpleAuthentication.HashPassword(pass_nonce) == textBox2.Text)
            {
                MessageBox.Show("Xác thực thành công 2 trên 4 bước xác thực!.");
                SymmestricAuthentication symmestricAuthentication = new SymmestricAuthentication();
                symmestricAuthentication.ShowDialog();
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
