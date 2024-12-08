using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;
using System.Data;
using System.Windows.Forms;

namespace DOAN_BMCSDL
{
    public class PhanQuyen
    {
        OracleConnection conn;

        public PhanQuyen(OracleConnection conn)
        {
            this.conn = conn;
        }
        
        public OracleDataReader Get_User()
        {
            try
            {
                string Procedure = "sys.PKG_PHANQUYEN.PRO_SELECT_USER";
                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter resultPara = new OracleParameter();
                resultPara.ParameterName = "@Result";
                resultPara.OracleDbType = OracleDbType.RefCursor;
                resultPara.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultPara);

                cmd.ExecuteNonQuery();

                if (resultPara.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)resultPara.Value).GetDataReader();
                    return ret;
                }
            }
            catch
            {
                MessageBox.Show("Lỗi gọi chạy thủ tục : PRO_SELECT_USER");
                return null;
            }
            return null;
        }

        public OracleDataReader Get_Roles()
        {
            try
            {
                string Procedure = "sys.PKG_PHANQUYEN.PRO_SELECT_ROLES";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType= CommandType.StoredProcedure;

                OracleParameter resultPara = new OracleParameter();
                resultPara.ParameterName = "@Result";
                resultPara.OracleDbType = OracleDbType.RefCursor;
                resultPara.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultPara);

                cmd.ExecuteNonQuery();

                if (resultPara.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)resultPara.Value).GetDataReader();
                    return ret;
                }

            }
            catch
            {
                MessageBox.Show("Lỗi gọi chạy thủ tục : PRO_SELECT_ROLES");
                return null;
            }
            return null;
        }


        public OracleDataReader Get_Proc_User(string userowner,string type)
        {
            try
            {
                string Procedure = "sys.PKG_PHANQUYEN.PRO_SELECT_PROCEDURE_USER";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter userOwner = new OracleParameter();
                userOwner.ParameterName = "@userowner";
                userOwner.OracleDbType = OracleDbType.Varchar2;
                userOwner.Value = userowner.ToUpper();
                userOwner.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(userOwner);

                OracleParameter pro_type = new OracleParameter();
                pro_type.ParameterName = "@pro_type";
                pro_type.OracleDbType = OracleDbType.Varchar2;
                pro_type.Value = type.ToUpper();
                pro_type.Direction= ParameterDirection.Input;
                cmd.Parameters.Add (pro_type);

                OracleParameter resultPara = new OracleParameter();
                resultPara.ParameterName = "@Result";
                resultPara.OracleDbType = OracleDbType.RefCursor;
                resultPara.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultPara);

                cmd.ExecuteNonQuery();

                if (userOwner.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)resultPara.Value).GetDataReader();
                    return ret;
                }

            }
            catch
            {
                MessageBox.Show("Lỗi gọi chạy thủ tục : PRO_SELECT_PROCEDURE_USER");
                return null;
            }
            return null;
        }

        public OracleDataReader Get_Table_User(string userowner)
        {
            try
            {
                string Procedure = "sys.PKG_PHANQUYEN.PRO_SELECT_TABLE";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter userOwner = new OracleParameter();
                userOwner.ParameterName = "@userowner";
                userOwner.OracleDbType = OracleDbType.Varchar2;
                userOwner.Value = userowner.ToUpper();
                userOwner.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(userOwner);

                OracleParameter resultPara = new OracleParameter();
                resultPara.ParameterName = "@Result";
                resultPara.OracleDbType = OracleDbType.RefCursor;
                resultPara.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultPara);

                cmd.ExecuteNonQuery();

                if (userOwner.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)resultPara.Value).GetDataReader();
                    return ret;
                }

            }
            catch
            {
                MessageBox.Show("Lỗi gọi chạy thủ tục : PRO_SELECT_TABLE");
                return null;
            }
            return null;
        }
        // LOI
        public DataTable Get_Roles_User(string username)
        {
            try
            {
                string Procedure = "sys.PKG_PHANQUYEN.PRO_USER_ROLES";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter UserName = new OracleParameter();
                UserName.ParameterName = "@username";
                UserName.OracleDbType = OracleDbType.Varchar2;
                UserName.Value = username.ToUpper();
                UserName.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(UserName);

                OracleParameter resultPara = new OracleParameter();
                resultPara.ParameterName = "@Result";
                resultPara.OracleDbType = OracleDbType.RefCursor;
                resultPara.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultPara);

                cmd.ExecuteNonQuery();

                if (resultPara.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)resultPara.Value).GetDataReader();
                    DataTable data= new DataTable();
                    data.Load(ret);
                    return data;
                }

            }
            catch
            {
                MessageBox.Show("Lỗi gọi chạy thủ tục : PRO_USER_ROLES");
                return null;
            }
            return null;
        }

        public int Get_Roles_User_Check(string username, string roles)
        {
            try
            {
                string Procedure = "sys.PKG_PHANQUYEN.PRO_USER_ROLES_CHECK";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter userName = new OracleParameter();
                userName.ParameterName = "@username";
                userName.OracleDbType = OracleDbType.Varchar2;
                userName.Value = username.ToUpper();
                userName.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(userName);

                OracleParameter Roles = new OracleParameter();
                Roles.ParameterName = "@roles";
                Roles.OracleDbType = OracleDbType.Varchar2;
                Roles.Value = roles.ToUpper();
                Roles.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(Roles);

                OracleParameter resultPara = new OracleParameter();
                resultPara.ParameterName = "@Result";
                resultPara.OracleDbType = OracleDbType.Int16;
                resultPara.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultPara);

                cmd.ExecuteNonQuery();

                if (resultPara.Value != DBNull.Value)
                {
                    return int.Parse(resultPara.Value.ToString());
                }

            }
            catch
            {
                MessageBox.Show("Lỗi gọi chạy thủ tục : PRO_USER_ROLES_CHECK");
                return -1;
            }
            return -1;
        }

        public DataTable Get_Grant_User(string username)
        {
            try
            {
                string Procedure = "sys.PKG_PHANQUYEN.PRO_SELECT_GRANT_USER";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter UserName = new OracleParameter();
                UserName.ParameterName = "@username";
                UserName.OracleDbType = OracleDbType.Varchar2;
                UserName.Value = username.ToUpper();
                UserName.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(UserName);

                OracleParameter resultPara = new OracleParameter();
                resultPara.ParameterName = "@Result";
                resultPara.OracleDbType = OracleDbType.RefCursor;
                resultPara.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultPara);

                cmd.ExecuteNonQuery();

                if (resultPara.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)resultPara.Value).GetDataReader();
                    DataTable data = new DataTable();
                    data.Load(ret);
                    return data;
                }

            }
            catch
            {
                MessageBox.Show("Lỗi gọi chạy thủ tục : PRO_SELECT_GRANT_USER");
                return null;
            }
            return null;
        }

        public OracleDataReader Get_Grant(string username, string userschema, string tab)
        {
            try
            {
                string Procedure = "sys.PKG_PHANQUYEN.PRO_SELECT_GRANT";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter UserName = new OracleParameter();
                UserName.ParameterName = "@username";
                UserName.OracleDbType = OracleDbType.Varchar2;
                UserName.Value = username.ToUpper();
                UserName.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(UserName);

                OracleParameter UserSchema = new OracleParameter();
                UserSchema.ParameterName = "@userschema";
                UserSchema.OracleDbType = OracleDbType.Varchar2;
                UserSchema.Value = userschema.ToUpper();
                UserSchema.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(UserSchema);

                OracleParameter TableName = new OracleParameter();
                TableName.ParameterName = "@tablename";
                TableName.OracleDbType = OracleDbType.Varchar2;
                TableName.Value = tab.ToUpper();
                TableName.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(TableName);

                OracleParameter resultPara = new OracleParameter();
                resultPara.ParameterName = "@Result";
                resultPara.OracleDbType = OracleDbType.RefCursor;
                resultPara.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(resultPara);

                cmd.ExecuteNonQuery();

                if (UserName.Value != DBNull.Value)
                {
                    OracleDataReader ret = ((OracleRefCursor)resultPara.Value).GetDataReader();
                    
                    return ret;
                }

            }
            catch
            {
                MessageBox.Show("Lỗi gọi chạy thủ tục : PRO_SELECT_GRANT");
                return null;
            }
            return null;
        }

        public bool Grant_Revoke_Pro(string username, string userschema, string pro_tab,string type_pro,int dk)
        {
            try
            {
                string Procedure = "sys.PKG_PHANQUYEN.PRO_GRANT_REVOKE";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter UserName = new OracleParameter();
                UserName.ParameterName = "@username";
                UserName.OracleDbType = OracleDbType.Varchar2;
                UserName.Value = username.ToUpper();
                UserName.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(UserName);

                OracleParameter UserSchema = new OracleParameter();
                UserSchema.ParameterName = "@userschema";
                UserSchema.OracleDbType = OracleDbType.Varchar2;
                UserSchema.Value = userschema.ToUpper();
                UserSchema.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(UserSchema);

                OracleParameter ProTab = new OracleParameter();
                ProTab.ParameterName = "@tablename";
                ProTab.OracleDbType = OracleDbType.Varchar2;
                ProTab.Value = pro_tab.ToUpper();
                ProTab.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(ProTab);

                OracleParameter TypePro = new OracleParameter();
                TypePro.ParameterName = "@typepro";
                TypePro.OracleDbType = OracleDbType.Varchar2;
                TypePro.Value = type_pro.ToUpper();
                TypePro.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(TypePro);

                OracleParameter DK = new OracleParameter();
                DK.ParameterName = "@dk";
                DK.OracleDbType = OracleDbType.Int16;
                DK.Value = dk;
                DK.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(DK);

               

                cmd.ExecuteNonQuery();

                return true;

            }
            catch
            {
                MessageBox.Show("Lỗi gọi chạy thủ tục : PRO_GRANT_REVOKE");
                return false;
            }
            
        }

        public bool Grant_Revoke_Roles(string username, string role, int dk)
        {
            try
            {
                string Procedure = "sys.PKG_PHANQUYEN.PRO_GRANT_REVOKE_ROLES";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter UserName = new OracleParameter();
                UserName.ParameterName = "@username";
                UserName.OracleDbType = OracleDbType.Varchar2;
                UserName.Value = username.ToUpper();
                UserName.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(UserName);

                OracleParameter Role = new OracleParameter();
                Role.ParameterName = "@role";
                Role.OracleDbType = OracleDbType.Varchar2;
                Role.Value = role.ToUpper();
                Role.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(Role);

                

                OracleParameter DK = new OracleParameter();
                DK.ParameterName = "@dk";
                DK.OracleDbType = OracleDbType.Int16;
                DK.Value = dk;
                DK.Direction = ParameterDirection.Input;
                cmd.Parameters.Add(DK);



                cmd.ExecuteNonQuery();

                return true;

            }
            catch
            {
                MessageBox.Show("Lỗi gọi chạy thủ tục : PRO_GRANT_REVOKE_ROLES");
                return false;
            }

        }

    }
}
