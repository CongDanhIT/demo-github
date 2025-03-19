using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoAn.Modal;
using System.Windows.Forms;

namespace DoAn.Services.ThanhToan
{
    public class KhoHangService
    {
        static private string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
        public static void CapNhatTonKho(List<SanPham> danhSachSanPham)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction()) // Bắt đầu giao dịch
                {
                    try
                    {
                        foreach (SanPham sanPham in danhSachSanPham)
                        {
                            string query = @"
                        UPDATE [Tồn Kho] 
                        SET SoLuongTon = SoLuongTon - @SoLuongMua
                        WHERE ID_SP = @ID_SP AND SoLuongTon >= @SoLuongMua";

                            using (SqlCommand command = new SqlCommand(query, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@ID_SP", sanPham.ID_SanPham);
                                command.Parameters.AddWithValue("@SoLuongMua", sanPham.SoLuong);

                                int rowsAffected = command.ExecuteNonQuery();
                                if (rowsAffected == 0)
                                {
                                    throw new Exception($"Không đủ hàng trong kho cho sản phẩm {sanPham.Ten} (ID: {sanPham.ID_SanPham})");
                                }
                            }
                        }

                        transaction.Commit(); // Lưu thay đổi nếu không có lỗi
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Hoàn tác nếu có lỗi
                        MessageBox.Show("Lỗi khi cập nhật tồn kho:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
