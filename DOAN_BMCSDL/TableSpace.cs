using System;
using System.Data;
using System.Windows.Forms;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace DOAN_BMCSDL
{
    public class TableSpace
    {
        private OracleConnection conn;

        public TableSpace(OracleConnection connection)
        {
            conn = connection;
        }

        public DataTable GetName_Tablespace()
        {
            DataTable data = null;

            try
            {
                // Tên thủ tục lưu trữ
                string procedureName = "P_GET_TABLESPACES";

                using (OracleCommand cmd = new OracleCommand(procedureName, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Tạo tham số đầu ra kiểu RefCursor
                    OracleParameter resultPara = new OracleParameter
                    {
                        ParameterName = "@Result",
                        OracleDbType = OracleDbType.RefCursor,
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(resultPara);

                    // Thực thi thủ tục
                    cmd.ExecuteNonQuery();

                    // Kiểm tra nếu giá trị trả về không phải DBNull
                    if (resultPara.Value != DBNull.Value)
                    {
                        // Lấy dữ liệu từ RefCursor
                        OracleDataReader reader = ((OracleRefCursor)resultPara.Value).GetDataReader();

                        // Đọc dữ liệu vào DataTable
                        data = new DataTable();
                        data.Load(reader);
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý lỗi chi tiết
                MessageBox.Show("Lỗi khi gọi thủ tục: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return data;
        }
    }
}
