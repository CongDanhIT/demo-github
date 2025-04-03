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
                    string query = @"SELECT CTHD.ID_HD, CTHD.ID_SP, SP.TenSanPham, CTHD.SoLuong, CTHD.GiaHienTai 
                              FROM [CTHD] 
                              JOIN [Sản Phẩm] SP ON CTHD.ID_SP = SP.ID_SP 
                              WHERE CTHD.ID_HD = @ID_HD";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_HD", ID_HD);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                            {
                                MessageBox.Show("Không tìm thấy chi tiết hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            string info = $"Chi tiết hóa đơn: {ID_HD}\n--------------------------------\n";
                            while (reader.Read())
                            {
                                info +=  $"ID_SP: {reader["ID_SP"]}, "
                                      + $"Tên sản phẩm: {reader["TenSanPham"]}, "
                                      + $"Số lượng: {reader["SoLuong"]}, "
                                      + $"Đơn giá: {reader["GiaHienTai"]}\n";
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
