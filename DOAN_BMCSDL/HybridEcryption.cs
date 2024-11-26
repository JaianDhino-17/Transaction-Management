using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Numerics;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DOAN_BMCSDL
{
    public partial class HybridEcryption : Form
    {
        public HybridEcryption()
        {
            InitializeComponent();
        }
        public string Encrypt_Hybrid (string plaintext, int key)
        {
            string Encrypted_Text = SymmetricEncryption.EncryptMultiplication(plaintext, key);
            string Encrypted_Key = AsymmestricEncryption.Encrypt_RSA(key.ToString());
            string Encrypted_Text_Key = Encrypted_Text + Encrypted_Key;
            return Encrypted_Text_Key;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string stk = textBox1.Text;
            string lgd = textBox2.Text;
            string amount = textBox3.Text;
            string content = textBox4.Text;
            int k = int.Parse(textBox8.Text);

            if (BigInteger.GreatestCommonDivisor(k, SymmetricEncryption.character.Length) != 1)
            {
                MessageBox.Show("Key này không thể tìm khóa nghịch đảo, vui lòng thử key khác!");
                return;
            }

            string query = "SELECT * FROM ACCOUNTS WHERE ACCOUNTID = '" + stk + "'";
            using (OracleCommand cmd = new OracleCommand(query, Database.conn))
            {
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DialogResult conFirm = MessageBox.Show("Bạn có chắc chắn muốn thực hiện giao dịch? ", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (conFirm == DialogResult.Yes)
                        {
                            string Encrypt_amount = Encrypt_Hybrid(amount, k);
                            string Encrypt_content = Encrypt_Hybrid(content, k);
                            string query2 = "INSERT INTO TRANSACTIONS (AccountID,TransactionType,Amount,TransactionDate,EncryptedDetails) VALUES ('" + stk + "','" + lgd + "','" + Encrypt_amount + "', TO_DATE('" + DateTime.Now.ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'), '" + Encrypt_content + "')";
                            using (OracleCommand cmd2 = new OracleCommand(query2, Database.conn))
                            {
                                try
                                {
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Giao dịch thành công!");
                                }
                                catch
                                {
                                    MessageBox.Show("Lỗi Sql!");
                                }
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("Không có Số Tài Khoản trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            TransactionManagement transactionManagement = new TransactionManagement();
            transactionManagement.ShowDialog();
            this.Close();
        }
    }
}
