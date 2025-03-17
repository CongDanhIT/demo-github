using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Services.DangNhap
{
    public  class XacThucDangNhap
    {
      static private string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";

        /// <summary>
        /// Kiểm tra thông tin đăng nhập.
        /// </summary>
       static public bool DangNhap(string tenDangNhap, string matKhau)
        {
            bool isValidUser = false;

            string query = "SELECT COUNT(*) FROM [Tài Khoản] WHERE TenDN = @TenDangNhap AND Password = @MatKhau";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@TenDangNhap", tenDangNhap);
                cmd.Parameters.AddWithValue("@MatKhau", matKhau); // Cần hash mật khẩu nếu dùng bảo mật
                conn.Open();
                int count = (int)cmd.ExecuteScalar();
                isValidUser = count > 0;
            }

            return isValidUser;
        }
    }
}
