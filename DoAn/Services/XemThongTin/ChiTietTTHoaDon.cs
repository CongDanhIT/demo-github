using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn.Services.XemThongTin
{
    public class ChiTietTTHoaDon
    {
        private static string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
        
        public static void XemTTHoaDon(string ID_HD)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [CTHD] WHERE ID_HD = @ID_HD";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_HD", ID_HD);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows) // Nếu không có dữ liệu
                            {
                                MessageBox.Show("Không tìm thấy chi tiết hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            string info = $"Chi tiết hóa đơn: {ID_HD}\n--------------------------------\n";
                            while (reader.Read()) // Lặp qua tất cả dữ liệu
                            {
                                info += $"ID_CTHD: {reader["ID_HD"]}, "
                                      + $"ID_SP: {reader["ID_SP"]}, "
                                      + $"Số lượng: {reader["SoLuong"]}, "
                                      + $"Đơn giá: {reader["GiaHienTai"]}, ";
                                      
                            }

                            MessageBox.Show(info, "Thông tin chi tiết hóa đơn", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin chi tiết hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
