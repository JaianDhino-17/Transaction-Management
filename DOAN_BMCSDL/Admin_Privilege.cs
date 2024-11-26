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
        TableSpace tableSpace;
        Profile profile;
        public Admin_Privilege()
        {
            InitializeComponent();
            CenterToScreen();
            conn=Database.Get_connect();
            tableSpace= new TableSpace(conn);
            profile = new Profile(conn);
            Load_Combobox();
            
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
            return false;
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
            return false;

        }

        private void cbTable_space_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cbTable.SelectedIndex;
            string tablespace=cbTable_space.SelectedItem.ToString();
        }

        void Load_Combobox()
        {
            //Load Table Space
            DataTable read_tableSpace = tableSpace.GetName_Tablespace();
            foreach (DataRow r in read_tableSpace.Rows)
            {
                cbTable_space.Items.Add(r[0]);
            }
            cbTable_space.SelectedIndex = 0;


            //Load Profile
            DataTable read_profile = profile.GetName_Profile();
            foreach (DataRow r in read_profile.Rows)
            {
                cbProfile.Items.Add(r[0]);
            }
            cbProfile.SelectedIndex = 0;
        }
       

        private void CbProfile_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i = cbProfile.SelectedIndex;
            string profile = cbProfile.SelectedItem.ToString();
        }

        private void btnGrant_Click(object sender, EventArgs e)
        {
            //Thủ tục cấp Quota
            if (string.IsNullOrWhiteSpace(txtUser.Text) ||
        string.IsNullOrWhiteSpace(txtQuota.Text) ||
        cbTable_space.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }

            string username = txtUser.Text;
            string tablespace = cbTable_space.SelectedItem.ToString();
            string quota = txtQuota.Text;

            
            bool isSuccess = SetQuota(username, tablespace, quota);

            if (isSuccess)
            {
                MessageBox.Show($"Quota cho người dùng {username} đã được cập nhật thành công trong tablespace {tablespace}.");
            }
            else
            {
                MessageBox.Show("Đã xảy ra lỗi khi cấp quota.");
            }
        }

        private bool SetQuota(string username, string tablespace, string quota)
        {
            if (Database.Connect())
            {
                try
                {
                    using (OracleConnection conn = Database.Get_connect())
                    {
                        using (OracleCommand cmd = new OracleCommand("SET_QUOTA", conn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;

                           
                            cmd.Parameters.Add("v_username", OracleDbType.Varchar2).Value = username;
                            cmd.Parameters.Add("v_tablespace", OracleDbType.Varchar2).Value = tablespace;
                            cmd.Parameters.Add("v_quota", OracleDbType.Varchar2).Value = quota;  
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
            return false;
        }



    }
    
}
