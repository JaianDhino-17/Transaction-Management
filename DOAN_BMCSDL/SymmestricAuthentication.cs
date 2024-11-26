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
    public partial class SymmestricAuthentication : Form
    {
        public SymmestricAuthentication()
        {
            InitializeComponent();
            textBox1.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nonce = textBox1.Text;
            int key = 5;

            //Mã hóa số NONCE từ A gửi đến
            textBox2.Text = SymmetricEncryption.EncryptMultiplication(nonce, key);
            
            //Số NONCE bất kỳ từ B
            Random rand = new Random();
            int randomNumberInRange = rand.Next(0, 100);
            textBox3.Text = randomNumberInRange.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(SymmetricEncryption.EncryptMultiplication(textBox1.Text, 5) == textBox2.Text)
            {
                MessageBox.Show("Nội dung mã hóa đúng!.");
            }
            else
            {
                MessageBox.Show("Nội dung mã hóa sai!. \n");
                this.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox4.Text = SymmetricEncryption.EncryptMultiplication(textBox3.Text,5);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Giả sử đã gửi cho CSDL để xác thực
            if (SymmetricEncryption.EncryptMultiplication(textBox3.Text, 5) == textBox4.Text)
            {
                MessageBox.Show("Xác thực thành công 3 trên 4 bước xác thực!.");
                //Challenge_ResponeAuthentication challenge_ResponeAuthentication = new Challenge_ResponeAuthentication(password);
                //challenge_ResponeAuthentication.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Xác thực không thành thành công!. \nVui lòng đăng nhập lại!");
                this.Close();
            }
        }
    }
}
