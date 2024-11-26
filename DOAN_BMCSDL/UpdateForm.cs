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

namespace DOAN_BMCSDL
{
    public partial class UpdateForm : Form
    {
        private string connectionString = @"Data Source=localhost:1521/orcl;Persist Security Info=True;User ID=DoAn_Nhom4;Password=thedung4";
        
        public UpdateForm()
        {
            InitializeComponent();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string accountID = txtAccountID.Text.Trim();
            string userID = txtUserID.Text.Trim();           
            string accountNumber = txtAccountNumber.Text.Trim();
            string accountType = txtAccountType.Text.Trim();
            decimal balance = decimal.Parse(txtBalance.Text.Trim());

            string query = "UPDATE Accounts " +
                           "SET AccountID=:accountID, UserID = :userID, AccountNumber = :accountNumber, AccountType = :accountType, Balance = :balance " +
                           "WHERE AccountID = :accountID";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                try
                {
                    conn.Open();
                    using (OracleCommand cmd = new OracleCommand(query, conn))
                    {
                        cmd.Parameters.Add(new OracleParameter("userID", userID));
                        cmd.Parameters.Add(new OracleParameter("accountID",accountID));
                        cmd.Parameters.Add(new OracleParameter("accountNumber", accountNumber));
                        cmd.Parameters.Add(new OracleParameter("accountType", accountType));
                        cmd.Parameters.Add(new OracleParameter("balance", balance));
                        cmd.Parameters.Add(new OracleParameter("accountID", accountID));

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Tài khoản đã được cập nhật thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi khi cập nhật tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btn_Esc_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void UpdateForm_Load(object sender, EventArgs e)
        {

        }
    }
}
