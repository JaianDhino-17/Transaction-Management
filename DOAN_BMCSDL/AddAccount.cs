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

namespace DOAN_BMCSDL
{
    public partial class Add : Form
    {
        private string connectionString = @"Data Source=localhost:1521/orcl;Persist Security Info=True;User ID=DoAn_Nhom4;Password=thedung4";

        public Add()
        {
            InitializeComponent();
        }

        private void btn_Save_Click(object sender, EventArgs e)
        {
            string accountID = txtAccountID.Text;
            string userID = txtUserID.Text;
            string accountNumber = txtAccountNumber.Text;
            string accountType = txtAccountType.Text;
            string balance = txtBalance.Text;
            string email = txtEmail.Text;
            string fullname = txtFullName.Text;
            string password = txtPassword.Text;
            int key = 5;
            string balanceEncrypt = SymmetricEncryption.EncryptAddition(balance, key);
            string emailEncrypt = SymmetricEncryption.EncryptAddition(email, key);
            string passwordEncrypt = SymmetricEncryption.EncryptAddition(password, key);

            // Câu lệnh thêm dữ liệu vào USERS và ACCOUNTS
            string addUsers = "INSERT INTO USERS (USERID, FULLNAME, EMAIL, PASSWORDHASH) VALUES (:userID, :fullName, :email, :password)";
            string addAccounts = "INSERT INTO ACCOUNTS (ACCOUNTID, USERID, ACCOUNTNUMBER, ACCOUNTTYPE, BALANCE) VALUES (:accountID, :userID, :accountNumber, :accountType, :balance)";

            // Kết nối và bắt đầu transaction
            using (OracleConnection conn = new OracleConnection(connectionString))
            {
                conn.Open();
                OracleTransaction transaction = conn.BeginTransaction(); 
                try
                {
                    // Thêm dữ liệu vào bảng USERS
                    using (OracleCommand cmdUsers = new OracleCommand(addUsers, conn))
                    {
                        cmdUsers.Transaction = transaction;
                        cmdUsers.Parameters.Add(new OracleParameter("userID", userID));
                        cmdUsers.Parameters.Add(new OracleParameter("fullName", fullname));
                        cmdUsers.Parameters.Add(new OracleParameter("email", emailEncrypt));
                        cmdUsers.Parameters.Add(new OracleParameter("password", passwordEncrypt));

                        cmdUsers.ExecuteNonQuery();
                    }

                    // Thêm dữ liệu vào bảng ACCOUNTS
                    using (OracleCommand cmdAccounts = new OracleCommand(addAccounts, conn))
                    {
                        cmdAccounts.Transaction = transaction;
                        cmdAccounts.Parameters.Add(new OracleParameter("accountID", accountID));
                        cmdAccounts.Parameters.Add(new OracleParameter("userID", userID));
                        cmdAccounts.Parameters.Add(new OracleParameter("accountNumber", accountNumber));
                        cmdAccounts.Parameters.Add(new OracleParameter("accountType", accountType));
                        cmdAccounts.Parameters.Add(new OracleParameter("balance", balanceEncrypt));

                        cmdAccounts.ExecuteNonQuery();
                    }

                    // Commit transaction nếu không có lỗi
                    transaction.Commit();
                    MessageBox.Show("Người dùng và tài khoản đã được thêm thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    // Rollback transaction nếu có lỗi
                    transaction.Rollback();
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Esc_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
