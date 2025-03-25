using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn.Modal;

namespace DoAn.Services.XemThongTin
{
    public class DSKho
    {
        private static string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
        public static List <Kho>  DsKho()
        {
            List<Kho> danhSachKho = new List<Kho>();

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Kho";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        connection.Open();
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())  // Đọc tất cả dữ liệu
                            {
                                Kho kho = new Kho
                                {
                                    Id_Kho = reader.GetInt32(0),   // Giả sử cột đầu tiên là ID kiểu int
                                    TenKho = reader.GetString(1)  // Giả sử cột thứ hai là tên kho kiểu string
                                };
                                danhSachKho.Add(kho);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi: " + ex.Message);
            }

            return danhSachKho;
        }
    }
}
