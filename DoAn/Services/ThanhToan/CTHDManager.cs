using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn.Modal;

namespace DoAn.Services.ThanhToan
{
    public class CTHDManager
    {
        static private string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
        /// <summary>
        /// Cập nhật CTHD từ danh sách sản phẩm đã chọn
        /// </summary>
        /// <param name="idHoaDon">ID của hóa đơn</param>
        /// <param name="danhSachSanPham">Danh sách sản phẩm đã chọn</param>
        public static void CapNhatCTHD(string idHoaDon, List<SanPham> danhSachSanPham)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (SqlTransaction transaction = connection.BeginTransaction()) // Giao dịch để đảm bảo an toàn dữ liệu
                {
                    try
                    {
                        foreach (SanPham sanPham in danhSachSanPham)
                        {
                            string query = @"
                            INSERT INTO CTHD (ID_HD, ID_SP, SoTien, SoLuong, GiaHienTai)
                            VALUES (@ID_HD, @ID_SP, @SoTien, @SoLuong, @GiaHienTai)";

                            using (SqlCommand command = new SqlCommand(query, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@ID_HD", idHoaDon);
                                command.Parameters.AddWithValue("@ID_SP", sanPham.ID_SanPham);
                                command.Parameters.AddWithValue("@SoTien", sanPham.Gia * sanPham.SoLuong);
                                command.Parameters.AddWithValue("@SoLuong", sanPham.SoLuong);
                                command.Parameters.AddWithValue("@GiaHienTai", sanPham.Gia);

                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit(); // Lưu thay đổi nếu không có lỗi
                        //Console.WriteLine("Cập nhật CTHD thành công!");
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Hoàn tác nếu có lỗi
                        MessageBox.Show("Lỗi khi cập nhật CTHD:\n" + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
