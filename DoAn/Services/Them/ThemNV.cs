using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Services.Them
{
   public class ThemNV
    {
        private static string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";

        // Thêm tài khoản vào bảng Tài Khoản
         static public bool AddTK(string idNV, string TenDN, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO [Tài Khoản] (ID_NV, TenDN, password, ID_VaiTro) VALUES (@ID_NV, @TenDN, @password, 0)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_NV", idNV);
                    cmd.Parameters.AddWithValue("@TenDN", TenDN);
                    cmd.Parameters.AddWithValue("@password", password);

                    return cmd.ExecuteNonQuery() > 0; // Trả về true nếu chèn thành công
                }
            }
        }

        // Lấy ID_TK từ bảng Tài Khoản dựa vào TenDN và password
      static  private int GetIDTK(string TenDN, string password)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT ID_TK FROM [Tài Khoản] WHERE TenDN = @TenDN AND password = @password";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@TenDN", TenDN);
                    cmd.Parameters.AddWithValue("@password", password);
                    var result = cmd.ExecuteScalar();
                    return result != null ? Convert.ToInt32(result) : -1;
                }
            }
        }

        // Thêm nhân viên vào bảng Nhân Viên
       static public bool AddNV(string idNV, string hoTen, string SDT, string TenDN, string password)
        {
            int id_TK = GetIDTK(TenDN, password);
            if (id_TK == -1) return false; // Không tìm thấy ID_TK, không thể thêm nhân viên

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "INSERT INTO [Nhân Viên] (ID_NV, HoTen, SDT, ID_TK) VALUES (@ID_NV, @HoTen, @SDT, @ID_TK)";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@ID_NV", idNV);
                    cmd.Parameters.AddWithValue("@HoTen", hoTen);
                    cmd.Parameters.AddWithValue("@SDT", SDT);
                    cmd.Parameters.AddWithValue("@ID_TK", id_TK);

                    return cmd.ExecuteNonQuery() > 0;
                }
            }
        }

    }
}
