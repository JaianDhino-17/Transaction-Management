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
using Oracle.ManagedDataAccess.Types;

namespace DOAN_BMCSDL
{
    public partial class Admin_Privilege : Form

    {
        OracleConnection conn;
        public Admin_Privilege()
        {
            InitializeComponent();
            CenterToScreen();
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Vui lòng nhập tên người dùng và mật khẩu.");
                return;
            }


            string username = txtUser.Text;
            string newPassword = txtPass.Text;


            bool isSuccess = Unlock_Account(username, newPassword);

            if (isSuccess)
            {
                MessageBox.Show("Tài khoản đã được mở khóa.");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi mở khóa tài khoản.");
            }
        }



        private bool Unlock_Account(string username, string newPassword)
        {

            Database.Set_Database("localhost", "1521", "orcl", "sys", "sys");

            if (Database.Connect())
            {
                try
                {

                    using (OracleConnection conn = Database.Get_connect())
                    {

                        using (OracleCommand cmd = new OracleCommand("P_UNLOCK_ACCOUNT", conn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add("P_USERNAME", OracleDbType.Varchar2).Value = username;
                            cmd.Parameters.Add("P_PASS", OracleDbType.Varchar2).Value = newPassword;
                            cmd.ExecuteNonQuery();

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Kết nối không thành công.");
                return false;
            }
        }


        private void btnNewPass_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text))
            {
                MessageBox.Show("Vui lòng nhập tên người dùng và mật khẩu mới.");
                return;
            }

          
            string username = txtUser.Text;
            string newPassword = txtPass.Text;

            
            bool isSuccess = ChangePassword(username, newPassword);

            if (isSuccess)
            {
                MessageBox.Show("Mật khẩu đã được thay đổi thành công.");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi thay đổi mật khẩu.");
            }
        }

        private bool ChangePassword(string username, string newPassword)
        {
            
            Database.Set_Database("localhost", "1521", "orcl", "sys", "sys");

            if (Database.Connect())
            {
                try
                {
                   
                    using (OracleConnection conn = Database.Get_connect())
                    {
                        
                        using (OracleCommand cmd = new OracleCommand("P_CHANGE_PASSWORD", conn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;
                            cmd.Parameters.Add("P_USERNAME", OracleDbType.Varchar2).Value = username;
                            cmd.Parameters.Add("P_PASS", OracleDbType.Varchar2).Value = newPassword;
                            cmd.ExecuteNonQuery();

                            return true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi: " + ex.Message);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Kết nối không thành công.");
                return false;
            }
        }

        private void cbTable_space_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

    }
    
}
