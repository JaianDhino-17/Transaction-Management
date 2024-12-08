using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace DOAN_BMCSDL
{
    public partial class Unlock_ChangePass : Form
    {
        OracleConnection conn;
        TableSpace tableSpace;

        public Unlock_ChangePass()
        {
            InitializeComponent();
            CenterToScreen();
            conn = Database.Get_connect();
            tableSpace = new TableSpace(conn);
            Load_combo();
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

        private void btnNewPass_Click_1(object sender, EventArgs e)
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

        private void btn_unLock_Click(object sender, EventArgs e)
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

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int i=comboBox1.SelectedIndex;
            string tablespace=comboBox1.SelectedItem.ToString();
        }
        void Load_combo()
        {
            DataTable read_tbSpace = tableSpace.GetName_Tablespace();
            foreach (DataRow r in read_tbSpace.Rows)
            {
                comboBox1.Items.Add(r[0]);
            }
            comboBox1.SelectedIndex = 0;
        }

        private void btn_Quota_update_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) ||
                string.IsNullOrWhiteSpace(txtQuota.Text) ||
                comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin.");
                return;
            }
            string username = txtUser.Text;
            string tablespace = comboBox1.SelectedItem.ToString();
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
    }
}
          
    