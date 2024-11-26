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
    public class TableSpace
    {
        OracleConnection conn;

        public TableSpace(OracleConnection conn)
        {
            this.conn = conn;
        }
        /*public DataTable GetName_Tablespace()
        {
            try
            {
                string Procedure = "P_GET_TABLESPACES";

                OracleCommand cmd = new OracleCommand();
                cmd.Connection = conn;
                cmd.CommandText = Procedure;
                cmd.CommandType = CommandType.StoredProcedure;

                OracleParameter resultPara = new OracleParameter();
            }
        }*/
    }
}
