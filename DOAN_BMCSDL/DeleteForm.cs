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
    public partial class DeleteForm : Form
    {
        private string connectionString = @"Data Source=localhost:1521/orcl;Persist Security Info=True;User ID=DoAnNhom4;Password=thedung4";

        public DeleteForm()
        {
            InitializeComponent();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            string userID = txtUserID.Text;
            string accountID = txtAccountID.Text;

            // SQL queries to delete records
            string accountQuery = "DELETE FROM Accounts WHERE AccountID = :accountID";
            string userQuery = "DELETE FROM Users WHERE UserID = :userID";

            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                OracleTransaction transaction = null;
                try
                {
                    conn.Open();
                    transaction = conn.BeginTransaction();

                    // Delete from Accounts table first
                    using (OracleCommand cmd = new OracleCommand(accountQuery, conn))
                    {
                        cmd.Transaction = transaction;
                        cmd.Parameters.Add(new OracleParameter("accountID", accountID));
                        int rowsAffected = cmd.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            MessageBox.Show("No account was deleted. Please check the AccountID.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                            return;
                        }
                    }

                    // Delete from Users table
                    using (OracleCommand cmdUser = new OracleCommand(userQuery, conn))
                    {
                        cmdUser.Transaction = transaction;
                        cmdUser.Parameters.Add(new OracleParameter("userID", userID));
                        int rowsAffected = cmdUser.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            MessageBox.Show("No user was deleted. Please check the UserID.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            transaction.Rollback();
                            return;
                        }
                    }

                    transaction.Commit();
                    MessageBox.Show("Tài khoản và người dùng đã được xóa thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    if (transaction != null) transaction.Rollback();
                    MessageBox.Show("Đã xảy ra lỗi khi xóa tài khoản: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }



        private void btnEsc_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}