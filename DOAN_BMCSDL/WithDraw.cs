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

namespace DOAN_BMCSDL
{
    public partial class WithDraw : Form
    {
        //private string connectionString = @"Data Source=localhost:1521/orcl;Persist Security Info=True;User ID=DoAn_Nhom4;Password=thedung4";

        public WithDraw()
        {
            InitializeComponent();
        }

        private void conFirmBtn_Click(object sender, EventArgs e)
        {
            string accID = textBox1.Text;
            string amount = textBox2.Text;
            string context = richTextBox1.Text;

            string query = "SELECT * FROM ACCOUNTS WHERE ACCOUNTID = '" + accID + "'";
            using (OracleCommand cmd = new OracleCommand(query, Database.conn))
            {
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        DialogResult conFirm = MessageBox.Show("Bạn có chắc chắn muốn rút " + amount + " từ tài khoản " + accID + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (conFirm == DialogResult.Yes)
                        {
                            int balance = reader.GetInt32(4);
                            if (int.Parse(amount) > balance)
                            {
                                MessageBox.Show("Số dư tài khoản không đủ để rút tiền!");
                                return;
                            }

                            int p = int.Parse(textBox3.Text);
                            int q = int.Parse(textBox4.Text);
                            AsymmestricEncryption.pub_k_And_pri_k(p,q);
                            string EnMount = AsymmestricEncryption.Encrypt_RSA(amount);
                            string EnContext = AsymmestricEncryption.Encrypt_RSA(context);

                            string query2 = "INSERT INTO TRANSACTIONS (AccountID,TransactionType,Amount,TransactionDate,EncryptedDetails) VALUES ('" + accID + "','Rút tiền','" + EnMount + "', TO_DATE('" + DateTime.Now.ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'), '" + EnContext + "')";
                            using (OracleCommand cmd2 = new OracleCommand(query2, Database.conn))
                            {
                                try
                                {
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Rút tiền thành công!");
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

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void WithDraw_Load(object sender, EventArgs e)
        {

        }
    }
}
