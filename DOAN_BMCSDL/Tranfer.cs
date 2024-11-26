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
    public partial class Tranfer : Form
    {
        //private string connectionString = @"Data Source=localhost:1521/orcl;Persist Security Info=True;User ID=DoAn_Nhom4;Password=thedung4";
        public Tranfer()
        {
            InitializeComponent();
        }

        private void conFirmBtn_Click(object sender, EventArgs e)
        {
            string accID1 = textBox1.Text;
            string accID2 = textBox2.Text;
            string amount = textBox3.Text;
            string context = richTextBox1.Text;

            string query = "SELECT * FROM ACCOUNTS WHERE ACCOUNTID = '" + accID1 + "'";
            using (OracleCommand cmd = new OracleCommand(query, Database.conn))
            {
                using (OracleDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        string query3 = "SELECT * FROM ACCOUNTS WHERE ACCOUNTID = '" + accID2 + "'";
                        using (OracleCommand cmd3 = new OracleCommand(query3, Database.conn))
                        {
                            using (OracleDataReader reader2 = cmd3.ExecuteReader())
                            {
                                if (!reader2.Read())
                                {
                                    MessageBox.Show("Không có Số Tài Khoản người nhận trong hệ thống!");
                                    return;
                                }
                            }
                        }
                        DialogResult conFirm = MessageBox.Show("Bạn có chắc chắn muốn chuyển " + amount + " vào tài khoản " + accID2 + "?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);                            
                        if (conFirm == DialogResult.Yes)
                        {
                            int balance = reader.GetInt32(4);
                            if (int.Parse(amount) > balance)
                            {
                                MessageBox.Show("Số dư tài khoản không đủ để chuyển tiền!");
                                return;
                            }
                            BigInteger key = new BigInteger(int.Parse(textBox4.Text));
                            BigInteger modulo = new BigInteger(SymmetricEncryption.character.Length);
                            if (BigInteger.GreatestCommonDivisor(key, modulo) != 1)
                            {
                                MessageBox.Show("Key này không thể tìm khóa nghịch đảo, vui lòng thử key khác!");
                                return;
                            }

                            string EnAmount = SymmetricEncryption.EncryptMultiplication(amount, (int)key);
                            string EnContext = SymmetricEncryption.EncryptMultiplication(context, (int)key);
                            string query2 = "INSERT INTO TRANSACTIONS (AccountID,TransactionType,Amount,TransactionDate,EncryptedDetails) VALUES ('" + accID1 + "','Chuyển tiền','" + EnAmount + "', TO_DATE('" + DateTime.Now.ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'), '"+ EnContext +"')";
                            using (OracleCommand cmd2 = new OracleCommand(query2, Database.conn))
                            {
                                try
                                {
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Chuyển tiền thành công!");
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
                        MessageBox.Show("Không có Số Tài Khoản người gửi trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
