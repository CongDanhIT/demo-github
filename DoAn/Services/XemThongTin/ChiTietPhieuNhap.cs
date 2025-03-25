using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace DoAn.Services.XemThongTin
{
    public class ChiTietPhieuNhap
    {
        private static string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";

        public static void XemCTPhieuNhap(string ID_PhieuNhap)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM [CTNK] WHERE ID_PhieuNhap = @ID_PhieuNhap";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_PhieuNhap", ID_PhieuNhap);
                        connection.Open();

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows) // Nếu không có dữ liệu
                            {
                                MessageBox.Show("Không tìm thấy chi tiết phiếu nhập!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                return;
                            }

                            string info = $"Chi tiết phiếu nhập: {ID_PhieuNhap}\n--------------------------------\n";
                            while (reader.Read()) // Lặp qua tất cả dữ liệu
                            {
                                info += $"ID_SP: {reader["ID_SP"]}, "
                                      + $"Tên SP: {reader["TenSP"]}, "
                                      + $"Số lượng: {reader["SoLuong"]}, "
                                      + $"Giá nhập: {reader["GiaNhap"]}\n";
                            }

                            MessageBox.Show(info, "Thông tin chi tiết phiếu nhập", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lấy thông tin chi tiết phiếu nhập: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
