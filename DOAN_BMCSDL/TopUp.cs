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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace DOAN_BMCSDL
{
    public partial class TopUp : Form
    {
        //private string connectionString = @"Data Source=localhost:1521/orcl;Persist Security Info=True;User ID=DoAn_Nhom4;Password=thedung4";

        static int key;
        public TopUp()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
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
                        DialogResult conFirm = MessageBox.Show("Bạn có chắc chắn muốn nạp "+ amount +" vào tài khoản "+ accID +"?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (conFirm == DialogResult.Yes)
                        {
                            //SymmetricEncryption symmetricEncryption = new SymmetricEncryption();
                            string EnAmount = SymmetricEncryption.EncryptCeasar_Func(amount) ;
                            string EnContext = SymmetricEncryption.EncryptCeasar_Func(context);
                            string query2 = "INSERT INTO TRANSACTIONS (AccountID,TransactionType,Amount,TransactionDate,EncryptedDetails) VALUES ('" + accID + "','Nạp tiền','" + EnAmount + "', TO_DATE('" + DateTime.Now.ToString("yyyy-MM-dd") + "', 'YYYY-MM-DD'), '"+ EnContext +"')";
                            using (OracleCommand cmd2 = new OracleCommand(query2, Database.conn))
                            {
                                try
                                {
                                    cmd2.ExecuteNonQuery();
                                    MessageBox.Show("Nạp tiền thành công!");
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
