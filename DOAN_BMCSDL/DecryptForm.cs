using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Numerics;

namespace DOAN_BMCSDL
{
    public partial class DecryptForm : Form
    {
        public DecryptForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string TransID = textBox1.Text;
            string key = textBox2.Text;
            string p = textBox3.Text;
            string q = textBox4.Text;
            BigInteger modulo = new BigInteger(SymmetricEncryption.character.Length);

            string query = "SELECT * FROM TRANSACTIONS WHERE TRANSACTIONID = '"+ TransID +"'";
            using (OracleCommand cmd = new OracleCommand(query,Database.conn))
            {
                try
                {
                    OracleDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        string DecryptAmount;
                        string DecryptContext;
                        label3.Text = "Mã giao dịch";
                        label4.Text = "Số tài khoản";
                        label5.Text = "Kiểu giao dịch";
                        label6.Text = "Tiền giao dịch";
                        label7.Text = "Ngày giao dịch";
                        label8.Text = "Nội dung giao dịch";

                        if (reader.GetString(2) == "Chuyển tiền")
                        {
                            if (BigInteger.GreatestCommonDivisor(BigInteger.Parse(key), modulo) != 1)
                            {
                                MessageBox.Show("Key này không thể tìm khóa nghịch đảo, vui lòng thử key khác!");
                                return;
                            }
                            DecryptAmount = SymmetricEncryption.DecryptMultiplication(reader.GetString(3), int.Parse(textBox2.Text));
                            DecryptContext = SymmetricEncryption.DecryptMultiplication(reader.GetString(5), int.Parse(textBox2.Text));
                         }
                        else if(reader.GetString(2) == "Rút tiền")
                        {
                            if (((int)AsymmestricEncryption.publicKey == 0 && (int)AsymmestricEncryption.privateKey == 0) || (p != "" && q != ""))
                                AsymmestricEncryption.pub_k_And_pri_k(int.Parse(p), int.Parse(q));
                            DecryptAmount = AsymmestricEncryption.Decrypt_RSA(reader.GetString(3));
                            DecryptContext = AsymmestricEncryption.Decrypt_RSA(reader.GetString(5));
                        }    
                        else
                        {
                            DecryptAmount = SymmetricEncryption.DecryptAddition(reader.GetString(3), int.Parse(textBox2.Text));
                            DecryptContext = SymmetricEncryption.DecryptAddition(reader.GetString(5), int.Parse(textBox2.Text));
                        }
                        label9.Text = DecryptContext;
                        label10.Text = reader.GetString(4);
                        label11.Text = DecryptAmount;
                        label12.Text = reader.GetString(2);
                        label13.Text = reader.GetString(1);
                        label14.Text = reader.GetString(0);
                    }
                    else
                    {
                        MessageBox.Show("Không tồn tại mã giao dịch " + TransID + " trong hệ thống!");
                    }
                }
                catch (Exception ex) {
                    MessageBox.Show("Lỗi Sql!");
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void DecryptForm_Load(object sender, EventArgs e)
        {

        }
    }
}
