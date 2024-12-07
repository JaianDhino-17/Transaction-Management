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
                cmb_user.Items.Add(read[0].ToString());
                cmb_username.Items.Add(read[0].ToString());
            }
            read.Close();
            cmb_user.SelectedIndex = 0;
            cmb_username.SelectedIndex = 0;
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
                cmb_function.Items.Add(read_pack[0].ToString());
            }
            read_pack.Close();

            OracleDataReader read_tab = p.Get_Table_User(userowner);
            while (read_tab.Read())
            {
                cmb_function.Items.Add(read_tab[0].ToString());
            }
            read_tab.Close();
            Select_Combobox();

        }

    }
}