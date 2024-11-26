using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;

namespace DOAN_BMCSDL
{
    public class Database
    {
        public static OracleConnection conn;

        public static string Host;
        public static string Port;
        public static string Sid;
        public static string User;
        public static string Password;

        public static void Set_Database(string host, string port, string sid, string user, string password)
        {
            Database.Host = host;
            Database.Port = port;
            Database.Sid = sid;
            Database.User = user;
            Database.Password = password;
        }

        public static bool Connect()
        {
            string consys = "";
            try
            {
                if (User.ToUpper().Equals("SYS"))
                {
                    consys = ";DBA Privilege = SYSDBA;";
                }

                string connString = "Data Source=(DESCRIPTION =(ADDRESS = ( PROTOCOL = TCP) (HOST = " + Host + ")(PORT = " + Port + "))(CONNECT_DATA = (SERVER = DEDICATED)(SERVICE_NAME = "
                + Sid + "))); User ID = " + User + "; Password = " + Password + consys;

                conn = new OracleConnection();
                conn.ConnectionString = connString;
                conn.Open();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static OracleConnection Get_connect()
        {
            if (conn == null)
                Connect();
            return conn;
        }

    }
}
