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
    public class Profile
    {
        OracleConnection conn;

        public Profile(OracleConnection conn)
        {
            this.conn = conn;
        }
        public DataTable GetName_Profile()
        {
            try
            {
                string Procedure = "P_GET_PROFILES";

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
                    DataTable data = new DataTable();
                    data.Load(ret);
                    return data;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.GetBaseException().ToString());

            }
            return null;
        }


    }
}
