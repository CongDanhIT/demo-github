using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace DoAn.DataAccess
{
    public class DatabaseHelper 
    {
        private readonly string _connectionString;
        private SqlConnection _connection;

        public DatabaseHelper(string connectionString)
        {
            _connectionString = connectionString;
            _connection = new SqlConnection(_connectionString);
        }

        // Mở kết nối
        private void OpenConnection()
        {
            if (_connection.State == ConnectionState.Closed)
            {
                _connection.Open();
            }
        }

        // Đóng kết nối
        private void CloseConnection()
        {
            if (_connection.State == ConnectionState.Open)
            {
                _connection.Close();
            }
        }

        // Thực thi câu lệnh INSERT, UPDATE, DELETE (không trả về kết quả)
        public int ExecuteNonQuery(string query, params SqlParameter[] parameters)
        {
            OpenConnection();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected;
            }
        }

        // Thực thi câu lệnh SELECT (trả về một giá trị đơn lẻ, ví dụ: COUNT(*))
        public object ExecuteScalar(string query, params SqlParameter[] parameters)
        {
            OpenConnection();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                return command.ExecuteScalar();
            }
        }

        // Thực thi SELECT và trả về DataTable
        public DataTable ExecuteQuery(string query, params SqlParameter[] parameters)
        {
            OpenConnection();
            using (SqlCommand command = new SqlCommand(query, _connection))
            {
                if (parameters != null)
                {
                    command.Parameters.AddRange(parameters);
                }
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    return dataTable;
                }
            }
        }

        // Giải phóng tài nguyên khi không sử dụng nữa
        public void Dispose()
        {
            CloseConnection();
            _connection.Dispose();
        }
    }
}
