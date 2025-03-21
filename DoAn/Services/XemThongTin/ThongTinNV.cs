using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoAn.Modal;

namespace DoAn.Services.XemThongTin
{

    public static class ThongTinNV
    {
        private static string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
        public static void GetNhanVienByIDTK(string idTK )
        {
            string query = "SELECT Id_NV, HoTen, SDT FROM [Nhân Viên] WHERE ID_TK = @ID_TK";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@ID_TK", idTK);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read()) // Nếu có dữ liệu
                        {
                            NhanVienStatic.SetNhanVien(new NhanVien(
                                reader["Id_NV"].ToString(),
                                reader["HoTen"].ToString(),
                                reader["SDT"].ToString()
                            ));
                        }
                        else
                        {
                            // Nếu không có dữ liệu, đặt giá trị rỗng
                            NhanVienStatic.SetNhanVien(new NhanVien("", "", ""));
                        }
                    }
                }
            }
        }

    }
}
