
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
namespace QLKD_CHTT.Modal.db
{
    public class XacThucLogin
    {
        static bool AuthenticateUser(string username, string password)
        {
            string connectionString = "Data Source=DESKTOP-J7AQH5B;Initial Catalog=QLKD_CHTT;User ID=sa;Password=123456";
            string query = "SELECT COUNT(*) FROM Users WHERE Username = @username AND Password = @password";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password); // Nếu dùng hash, cần so sánh đúng cách

                    int count = (int)cmd.ExecuteScalar();
                    return count > 0; // Nếu có tài khoản trùng khớp, trả về true
                }
            }
        }
    }
}
