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
        PhanQuyen p;

        public Admin_Privilege()
        {
            InitializeComponent();
            CenterToScreen();
            conn = Database.Get_connect();
            p = new PhanQuyen(conn);

            Load_User();
            Load_Roles();
        }

        void set_Color_checkBox_user()
        {
            if (cb_select_us.Checked)
                cb_select_us.ForeColor = Color.Green;
            else
                cb_select_us.ForeColor = Color.Red;

            if (cb_ins_us.Checked)
                cb_ins_us.ForeColor = Color.Green;
            else
                cb_ins_us.ForeColor = Color.Red;

            if (cb_upd_us.Checked)
                cb_upd_us.ForeColor = Color.Green;
            else
                cb_upd_us.ForeColor = Color.Red;

            if (cb_del_us.Checked)
                cb_del_us.ForeColor = Color.Green;
            else
                cb_del_us.ForeColor = Color.Red;
        }


        void set_Color_checkBox_Roles()
        {
            if (cb_select_ro.Checked)
                cb_select_ro.ForeColor = Color.Green;
            else
                cb_select_ro.ForeColor = Color.Red;

            if (cb_insert_ro.Checked)
                cb_insert_ro.ForeColor = Color.Green;
            else
                cb_insert_ro.ForeColor = Color.Red;

            if (cb_update_ro.Checked)
                cb_update_ro.ForeColor = Color.Green;
            else
                cb_update_ro.ForeColor = Color.Red;

            if (cb_delete_ro.Checked)
                cb_delete_ro.ForeColor = Color.Green;
            else
                cb_delete_ro.ForeColor = Color.Red;
        }

        void set_Color_Grant_user()
        {
            if (cb_user_proc.Checked)
                cb_user_proc.ForeColor = Color.Green;
            else
                cb_user_proc.ForeColor = Color.Red;

            if (cb_user_fun.Checked)
                cb_user_fun.ForeColor = Color.Green;
            else
                cb_user_fun.ForeColor = Color.Red;

            if (cb_user_pk.Checked)
                cb_user_pk.ForeColor = Color.Green;
            else
                cb_user_pk.ForeColor = Color.Red;

        }

        void set_Color_Grant_Roles()
        {
            if (cb_roles_proc.Checked)
                cb_roles_proc.ForeColor = Color.Green;
            else
                cb_roles_proc.ForeColor = Color.Red;

            if (cb_roles_fun.Checked)
                cb_roles_fun.ForeColor = Color.Green;
            else
                cb_roles_fun.ForeColor = Color.Red;

            if (cb_roles_pk.Checked)
                cb_roles_pk.ForeColor = Color.Green;
            else
                cb_roles_pk.ForeColor = Color.Red;

        }


        void set_lable_Table()
        {
            string t = "Table : ";
            if (cmb_table.SelectedItem != null)
            {
                t += cmb_table.SelectedItem.ToString();
            }
            lb_table_roles.Text = t;
            lb_table_user.Text = t;
        }

        void set_Text_Button(string user, string role)
        {
            int kq = p.Get_Roles_User_Check(user, role);
            if (kq == 1)
            {
                btn_Grant_Revoke_Role.Text = "Revoke";
            }
            else if (kq == 0)
            {
                btn_Grant_Revoke_Role.Text = "Grant";
            }
        }

        void Load_User()
        {
            OracleDataReader read = p.Get_User();
            while (read.Read())
            {
                cmb_user.Items.Add(read[0].ToString());
                cmb_username.Items.Add(read[0].ToString());
            }
            read.Close();
            cmb_user.SelectedIndex = 0;
            cmb_username.SelectedIndex = 0;
        }

        void Load_Roles()
        {
            OracleDataReader read = p.Get_Roles();
            while (read.Read())
            {
                cmb_roles.Items.Add(read[0].ToString());
            }
            read.Close();
            cmb_roles.SelectedIndex = 0;
        }

        void Clear_Combobox()
        {
            cmb_procedure.Items.Clear();
            cmb_function.Items.Clear();
            cmb_package.Items.Clear();
            cmb_table.Items.Clear();
        }

        void Select_Combobox()
        {
            if (cmb_procedure.Items.Count == 0)
                cmb_procedure.Items.Add("");
            if (cmb_function.Items.Count == 0)
                cmb_function.Items.Add("");
            if (cmb_package.Items.Count == 0)
                cmb_package.Items.Add("");
            if (cmb_table.Items.Count == 0)
                cmb_table.Items.Add("");

            cmb_procedure.SelectedIndex = 0;
            cmb_function.SelectedIndex = 0;
            cmb_package.SelectedIndex = 0;
            cmb_table.SelectedIndex = 0;
        }


        void Load_pro_user(string userowner)
        {
            Clear_Combobox();
            OracleDataReader read_proc = p.Get_Proc_User(userowner, "PROCEDURE");
            while (read_proc.Read())
            {
                cmb_procedure.Items.Add(read_proc[0].ToString());
            }
            read_proc.Close();

            OracleDataReader read_fun = p.Get_Proc_User(userowner, "FUNCTION");
            while (read_fun.Read())
            {
                cmb_function.Items.Add(read_fun[0].ToString());
            }
            read_fun.Close();

            OracleDataReader read_pack = p.Get_Proc_User(userowner, "PACKAGE");
            while (read_pack.Read())
            {
                cmb_package.Items.Add(read_pack[0].ToString());
            }
            read_pack.Close();

            OracleDataReader read_tab = p.Get_Table_User(userowner);
            while (read_tab.Read())
            {
                cmb_table.Items.Add(read_tab[0].ToString());
            }
            read_tab.Close();
            Select_Combobox();

        }

        void Load_Roles_User(string user)
        {
            dtg_roles.DataSource = p.Get_Roles_User(user);
        }

        void Load_Grant_User(string user)
        {
            dtg_grant.DataSource = p.Get_Grant_User(user);
        }

        void Load_Grant_Roles(string roles)
        {
            dtg_grant_roles.DataSource = p.Get_Grant_User(roles);
        }

        void Load_Grant_Table_User(string user, string userschema, string table)
        {
            OracleDataReader read = p.Get_Grant(user, userschema, table);
            bool select, insert, update, delete;
            select = insert = update = delete = false;
            while (read.Read())
            {
                string r = read[0].ToString();
                if (r.Equals("SELECT"))
                    select = true;
                if (r.Equals("INSERT"))
                    insert = true;
                if (r.Equals("UPDATE"))
                    update = true;
                if (r.Equals("DELETE"))
                    delete = true;

            }
            cb_select_us.Checked = select;
            cb_ins_us.Checked = insert;
            cb_upd_us.Checked = update;
            cb_del_us.Checked = delete;
        }

        void Load_Grant_Table_Roles(string roles, string userschema, string table)
        {
            OracleDataReader read = p.Get_Grant(roles, userschema, table);
            bool select, insert, update, delete;
            select = insert = update = delete = false;
            while (read.Read())
            {
                string r = read[0].ToString();
                if (r.Equals("SELECT"))
                    select = true;
                if (r.Equals("INSERT"))
                    insert = true;
                if (r.Equals("UPDATE"))
                    update = true;
                if (r.Equals("DELETE"))
                    delete = true;

            }
            cb_select_ro.Checked = select;
            cb_insert_ro.Checked = insert;
            cb_update_ro.Checked = update;
            cb_delete_ro.Checked = delete;
        }

        bool Load_Execute(string user_role, string userschema, string pro)
        {
            OracleDataReader read = p.Get_Grant(user_role, userschema, pro);
            bool execute = false;
            while (read.Read())
            {
                if (read[0].ToString().Equals("EXECUTE"))
                    execute = true;
            }
            return execute;
        }

        private void cmb_user_SelectedIndexChange(object sender, EventArgs e)
        { 
            string userowner=cmb_user.SelectedItem.ToString();
            Load_pro_user(userowner);
        }

        private void btn_accMng_Click(object sender, EventArgs e)
        {
            this.Close();
            Unlock_ChangePass unlock_ChangePass = new Unlock_ChangePass();
            unlock_ChangePass.ShowDialog();
        }

        private void cmb_username_SelectedIndexChanged(object sender, EventArgs e)
        {
            string user=cmb_username.SelectedItem.ToString();
            string userschema=cmb_user.SelectedItem.ToString().ToLower();
            Load_Roles_User(user);
            Load_Grant_User(user);
            lb_user.Text = "User : " + user;

            if(cmb_table.SelectedItem!=null)
            {
                string table=cmb_table.SelectedItem.ToString();
                Load_Grant_Table_User(user, userschema,table);
                set_Color_checkBox_user();
            }

            if (cmb_procedure.SelectedItem != null)
            { 
                string procedure=cmb_procedure.SelectedItem.ToString();
                cb_user_proc.Checked=Load_Execute(user,userschema,procedure);
            }

            if (cmb_function.SelectedItem != null) 
            {
                string function=cmb_function.SelectedItem.ToString();
                cb_user_fun.Checked=Load_Execute(user,userschema,function);
            }

            if (cmb_package.SelectedItem != null) 
            {
                string package=cmb_package.SelectedItem.ToString();
                cb_user_pk.Checked = Load_Execute(user, userschema, package);
            }

            if (cmb_roles.SelectedItem != null) 
            {
                string roles=cmb_roles.SelectedItem.ToString();
                set_Text_Button(user,roles);
            }

            set_Color_Grant_user();
        }

        private void cmb_table_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userschema = cmb_user.SelectedItem.ToString();
            set_lable_Table();
            string table = cmb_table.SelectedItem.ToString();
            if (cmb_username.SelectedItem != null) 
            {
                string user=cmb_username.SelectedItem.ToString();
                Load_Grant_Table_User(user,userschema,table);
                set_Color_checkBox_user();
            }

            if (cmb_roles.SelectedItem != null) 
            {
                string roles = cmb_roles.SelectedItem.ToString();
                Load_Grant_Table_Roles(roles,userschema,table);
                set_Color_checkBox_Roles();
            }
        }


        private void cmb_roles_SelectedIndexChanged(object sender, EventArgs e)
        {
            string userschema = cmb_user.SelectedItem.ToString();
            string role = cmb_roles.SelectedItem.ToString();
            Load_Grant_Roles(role);

            if(cmb_table.SelectedItem!= null)
            {
                string table = cmb_table.SelectedItem.ToString();
                Load_Grant_Table_Roles(role, userschema, table);
                set_Color_checkBox_Roles();
            }

            if(cmb_procedure.SelectedItem!=null)
            {
                string procedure = cmb_procedure.SelectedItem.ToString();
                cb_roles_proc.Checked = Load_Execute(role, userschema, procedure);
            }

            if(cmb_function.SelectedItem!=null)
            {
                string function=cmb_function.SelectedItem.ToString();
                cb_roles_fun.Checked = Load_Execute(role, userschema, function);
            }

            if(cmb_package.SelectedItem!=null)
            {
                string package = cmb_package.SelectedItem.ToString();
                cb_roles_pk.Checked = Load_Execute(role,userschema,package);
            }

            if(cmb_username.SelectedItem!=null)
            {
                string user = cmb_username.SelectedItem.ToString();
                set_Text_Button(user, role);
            }

            set_Color_Grant_Roles();

        }

        private void cmb_procedure_SelectedIndexChanged(object sender, EventArgs e)
        {
            string procedure=cmb_procedure.SelectedItem.ToString();
            string userschema=cmb_user.SelectedItem.ToString();

            if (cmb_username.SelectedItem != null)
            {
                string user = cmb_username.SelectedItem.ToString();
                cb_user_proc.Checked=Load_Execute(user, userschema,procedure);
            }

            if(cmb_roles.SelectedItem!=null)
            {
                string role=cmb_roles.SelectedItem.ToString();
                cb_roles_proc.Checked=Load_Execute(role,userschema,procedure);
            }
            set_Color_Grant_Roles();
            set_Color_Grant_user();
        }

        private void cmb_function_SelectedIndexChanged(object sender, EventArgs e)
        {
            string function=cmb_function.SelectedItem.ToString();
            string userschema= cmb_user.SelectedItem.ToString();
            if (cmb_username.SelectedItem != null)
            { 
                string user=cmb_username.SelectedItem.ToString();
                cb_user_fun.Checked=Load_Execute(user, userschema,function);
            }

            if (cmb_roles.SelectedItem!=null)
            {
                string role =cmb_roles.SelectedItem.ToString();
                cb_roles_fun.Checked=Load_Execute(role,userschema,function);
            }

            set_Color_Grant_Roles();
            set_Color_Grant_user();
        }

        private void cmb_package_SelectedIndexChanged(object sender, EventArgs e)
        {
            string package=cmb_package.SelectedItem.ToString();
            string userschema=cmb_user.SelectedItem.ToString();

            if (cmb_username.SelectedItem != null)
            { 
                string user=cmb_username.SelectedItem.ToString();
                cb_user_pk.Checked=Load_Execute(user, userschema,package);
            }

            if(cmb_roles.SelectedItem!=null)
            {
                string role=cmb_roles.SelectedItem.ToString();
                cb_roles_pk.Checked=Load_Execute(role,userschema,package);
            }
            set_Color_Grant_Roles();
            set_Color_Grant_user();
        }

        bool Grant_Revoke_pro(string user_roles, string schema, string pro_tab, string type_user,
            string type_pro_tab, string type, bool grant_revoke)
        {
            if (pro_tab.Equals(""))
            {
                MessageBox.Show("Mục" + type_pro_tab + "Trống!");
                return false;
            }

            if (user_roles.Equals(""))
            {
                MessageBox.Show("Mục" + type_user + "Trống!");
                return false;
            }
            int dk;
            DialogResult res;
            if (grant_revoke)
            {
                dk = 1;
                res = MessageBox.Show("Bạn muốn cấp quyền " + type + "  " + type_pro_tab + ": " + pro_tab + " cho " + type_user +
                    ":" + user_roles + " không? ", "Thông báo", MessageBoxButtons.YesNo);
            }
            else
            {
                dk = 0;
                res = MessageBox.Show("Bạn muốn hủy quyền " + type + "  " + type_pro_tab + ": " + pro_tab + " cho " + type_user +
                   ":" + user_roles + " không? ", "Thông báo", MessageBoxButtons.YesNo);

            }

            if (res == DialogResult.Yes)
            {
                if (p.Grant_Revoke_Pro(user_roles, schema, pro_tab, type, dk))
                {
                    if (grant_revoke)
                    {
                        MessageBox.Show("Gán quyền thành công!");
                    }
                    else
                    {
                        MessageBox.Show("Thu hồi quyền thành công");
                    }

                }
                else

                    return false;
                return true;

            }
            else
                return false;
        }

        bool Grant_Revoke_Role(string user, string role, int dk)
        {
            if(user.Equals(""))
            {
                MessageBox.Show("Mục User trống!");
                return false;
            }

            if (role.Equals(""))
            {
                MessageBox.Show("Mục Role trống!");
                return false;
            }

            DialogResult res;
            if (dk == 1)
            { 
                res=MessageBox.Show("Bạn muốn gán User : " +user+ " vào Role: " + role+" không? ",
                    "Thông báo",MessageBoxButtons.YesNo);
            }
            else
            {
                res = MessageBox.Show("Bạn muốn gỡ User : " + user + " ở Role: " + role + " không? ",
                    "Thông báo", MessageBoxButtons.YesNo);
            }

            if (res == DialogResult.Yes)
            {
                if (p.Grant_Revoke_Roles(user, role, dk))
                {
                    if (dk == 1)
                        MessageBox.Show("Gán nhóm quyền thành công!");
                    else
                        MessageBox.Show("Thu hồi nhóm quyền thành công!");
                }
                else
                    return false;
                return true;
            }
            else
                return false ;

        }

        void Grant_Revoke_Pro_User(ComboBox cmb, CheckBox c, string pro)
        {
            bool check = c.Checked;
            string procedure = cmb.SelectedItem.ToString();
            string user = cmb_username.SelectedItem.ToString();
            string schema = cmb_user.SelectedItem.ToString();
            if (Grant_Revoke_pro(user, schema, procedure, "User", pro, "Execute", check))
            {
                set_Color_Grant_user();
                Load_Grant_User(user);
            }
            else
                c.Checked = !check;
        }

        void Grant_Revoke_Pro_Role(ComboBox cmb, CheckBox c, string pro)
        {
            bool check = c.Checked;
            string procedure = cmb.SelectedItem.ToString();
            string role = cmb_roles.SelectedItem.ToString();
            string schema = cmb_user.SelectedItem.ToString();
            if (Grant_Revoke_pro(role, schema, procedure, "Role", pro, "Execute", check))
            {
                set_Color_Grant_Roles();
                Load_Grant_Roles(role);
            }
            else
                c.Checked = !check;
        }

        void Grant_Revoke_Table_User(CheckBox c, string type)
        {
            bool check = c.Checked;
            string table = cmb_table.SelectedItem.ToString();
            string user = cmb_username.SelectedItem.ToString();
            string schema = cmb_user.SelectedItem.ToString();
            if (Grant_Revoke_pro(user, schema, table, "User", "Table", type, check))
            {
                set_Color_checkBox_user();
                
            }
            else
                c.Checked = !check;
        }
        void Grant_Revoke_Table_Role(CheckBox c, string type)
        {
            bool check = c.Checked;
            string table = cmb_table.SelectedItem.ToString();
            string role = cmb_roles.SelectedItem.ToString();
            string schema = cmb_user.SelectedItem.ToString();
            if (Grant_Revoke_pro(role, schema, table, "Role", "Table", type, check))
            
                set_Color_checkBox_user();

            
            else
                c.Checked = !check;
        }

        private void cb_user_proc_Click(Object sender, EventArgs e)
        {
            Grant_Revoke_Pro_User(cmb_procedure, cb_user_proc, "Procedure");
        }
        private void cb_roles_proc_Click(Object sender, EventArgs e)
        {
            Grant_Revoke_Pro_Role(cmb_procedure, cb_roles_proc, "Procedure");
        }

        private void cb_roles_proc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cb_user_proc_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cb_user_fun_Click(Object sender, EventArgs e)
        {
            Grant_Revoke_Pro_User(cmb_function, cb_user_fun, "Function");
        }

        private void cb_roles_fun_Click(Object sender, EventArgs e)
        {
            Grant_Revoke_Pro_Role(cmb_function, cb_roles_fun, "Function");
        }

        private void cb_user_pk_Click(Object sender, EventArgs e)
        {
            Grant_Revoke_Pro_User(cmb_package, cb_user_pk, "Package");
        }
        private void cb_roles_pk_Click(Object sender, EventArgs e)
        {
            Grant_Revoke_Pro_Role(cmb_package, cb_roles_pk, "Package");
        }

        private void cb_select_us_Click(Object sender, EventArgs e)
        {
            Grant_Revoke_Table_User(cb_select_us, "Select");

        }
        private void cb_insert_us_Click(Object sender, EventArgs e)
        {
            Grant_Revoke_Table_User(cb_ins_us, "Insert");

        }
        private void cb_upd_us_Click(Object sender, EventArgs e)
        {
            Grant_Revoke_Table_User(cb_upd_us, "Update");

        }

        private void cb_del_us_Click(Object sender, EventArgs e)
        {
            Grant_Revoke_Table_User(cb_del_us, "Delete");

        }

        private void cb_select_ro_Click(object sender, EventArgs e)
        {
            Grant_Revoke_Table_Role(cb_select_ro, "Select");
        }

        private void cb_insert_ro_Click(object sender, EventArgs e)
        {
            Grant_Revoke_Table_Role(cb_insert_ro, "Insert");
        }

        private void cb_update_ro_Click(object sender, EventArgs e)
        {
            Grant_Revoke_Table_Role(cb_update_ro, "Update");
        }

        private void cb_delete_ro_Click(object sender, EventArgs e)
        {
            Grant_Revoke_Table_Role(cb_delete_ro, "Delete");
        }

        private void btn_Grant_Revoke_Role_Click(object sender, EventArgs e)
        {
            string user=cmb_username.SelectedItem.ToString();
            string role=cmb_roles.SelectedItem.ToString();
            int dk;
            if(btn_Grant_Revoke_Role.Text.Equals("Grant"))
            {
                dk = 1;
            }
            else
            {
                dk = 0;
            }
            if (Grant_Revoke_Role(user,role,dk))
            {
                set_Text_Button(user,role);
                Load_Roles_User(user);
            }
        }

        private void cb_insert_ro_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cb_user_proc_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}