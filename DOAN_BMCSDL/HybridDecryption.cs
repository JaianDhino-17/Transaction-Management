using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DOAN_BMCSDL
{
    public partial class HybridDecryption : Form
    {
        public HybridDecryption()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TransactionManagement transactionManagement = new TransactionManagement();
            transactionManagement.ShowDialog();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string Tran_ID = textBox1.Text;

            string query = "SELECT AMOUNT FROM TRANSACTIONS WHERE TransactionID = '" + Tran_ID + "'";
            OracleCommand command = new OracleCommand(query, Database.conn);
            OracleDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                string Encrypt_Amount = reader.GetString(0);
                string Encrypt_Key = Encrypt_Amount.Substring(Encrypt_Amount.Length - 2);

                BigInteger Pr_K = BigInteger.Parse(File.ReadAllText("privateKey.pem"));
                BigInteger n = BigInteger.Parse(File.ReadAllText("nValue.pem"));
                string Decrypt_Key = AsymmestricEncryption.Decrypt_RSA_withPrivate_key(Encrypt_Key, Pr_K, n);

                MessageBox.Show($"Khóa đối xứng là: {Decrypt_Key}");
            }
            else 
            {
                MessageBox.Show("Không có thông tin giao dịch trên!");
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string Tran_ID = textBox1.Text;
            int Key = int.Parse(textBox3.Text);

            string query = "SELECT * FROM TRANSACTIONS WHERE TransactionID = '" + Tran_ID + "'";
            OracleCommand command = new OracleCommand(query, Database.conn);
            OracleDataReader reader = command.ExecuteReader();
            if (reader.Read())
            {
                label16.Text = "Mã giao dịch";
                label15.Text = "Số tài khoản";
                label6.Text = "Kiểu giao dịch";
                label7.Text = "Tiền giao dịch";
                label8.Text = "Ngày giao dịch";
                label19.Text = "Nội dung giao dịch";

                string Encrypt_Amount_Key = reader.GetString(3);
                string Encrypt_Amount = Encrypt_Amount_Key.Substring(0, Encrypt_Amount_Key.Length - 2);
                string Decrypt_Amount = SymmetricEncryption.DecryptMultiplication(Encrypt_Amount, Key);

                string Encrypt_Content_Key = reader.GetString(5);
                string Encrypt_Content = Encrypt_Content_Key.Substring(0, Encrypt_Content_Key.Length - 2);
                string Decrypt_Content = SymmetricEncryption.DecryptMultiplication(Encrypt_Content, Key);


                label18.Text = Decrypt_Content;
                label9.Text = reader.GetString(4);
                label10.Text = Decrypt_Amount;
                label11.Text = reader.GetString(2);
                label12.Text = reader.GetString(1);
                label13.Text = reader.GetString(0);
            }
            else
            {
                MessageBox.Show("Không có thông tin giao dịch trên!");
            }
        }

        private void HybridDecryption_Load(object sender, EventArgs e)
        {

        }
    }
}
