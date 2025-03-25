using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DoAn.Modal;
using System.Windows.Forms;
using static DoAn.View.ChuQuan._2_GiaoDienCQ;

namespace DoAn.Services.Them
{
    public class NhapSP
    {
        static readonly string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";

        // Cập nhật số lượng sản phẩm đã có trong kho
        public static void NhapSanPhamDaCo(List<SanPhamHienThi> danhSachSanPhamCu, string ID_PhieuNhap)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Câu lệnh cập nhật tồn kho
                    string queryUpdateTonKho = "UPDATE [Tồn Kho] SET SoLuongTon = SoLuongTon + @SoLuong WHERE ID_SP = @ID_SP";

                    // Câu lệnh chèn vào bảng CTNK (thêm cả ID_SP)
                    string queryInsertCTNK = "INSERT INTO [CTNK] (ID_SP, TenSP, ID_PhieuNhap, SoLuong, GiaNhap) " +
                                             "VALUES (@ID_SP, @TenSP, @ID_PhieuNhap, @SoLuong, @GiaNhap)";

                    using (SqlCommand cmdUpdate = new SqlCommand(queryUpdateTonKho, conn))
                    using (SqlCommand cmdInsert = new SqlCommand(queryInsertCTNK, conn))
                    {
                        foreach (var sp in danhSachSanPhamCu)
                        {
                            // Xóa tham số cũ
                            cmdUpdate.Parameters.Clear();
                            cmdInsert.Parameters.Clear();

                            // **Cập nhật bảng [Tồn Kho]**
                            cmdUpdate.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                            cmdUpdate.Parameters.AddWithValue("@ID_SP", sp.ID);

                            int rowsAffected = cmdUpdate.ExecuteNonQuery();

                            if (rowsAffected == 0)
                            {
                                Console.WriteLine($"Không tìm thấy sản phẩm ID {sp.ID} để cập nhật!");
                                continue;
                            }

                            // **Chèn vào bảng [CTNK]** (Bổ sung ID_SP)
                            cmdInsert.Parameters.AddWithValue("@ID_SP", sp.ID);
                            cmdInsert.Parameters.AddWithValue("@TenSP", sp.TenSP);
                            cmdInsert.Parameters.AddWithValue("@ID_PhieuNhap", ID_PhieuNhap);
                            cmdInsert.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                            cmdInsert.Parameters.AddWithValue("@GiaNhap", sp.Gia);

                            cmdInsert.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi cập nhật sản phẩm đã có: {ex.Message}");
            }
        }


        // Chèn sản phẩm mới vào bảng [Tồn Kho]
        public static void NhapSanPhamMoi(List<SanPhamHienThi> danhSachSanPhamMoi, string ID_PhieuNhap)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string queryTonKho = "INSERT INTO [Tồn Kho] (TenSP, ViTriHang, SoLuongTon, ID_Kho) VALUES (@TenSP, @ViTriHang, @SoLuongTon, @ID_Kho)";
                    string queryCTNK = "INSERT INTO [CTNK] (TenSP, ID_PhieuNhap, SoLuong, GiaNhap, ID_SanPham) VALUES (@TenSP, @ID_PhieuNhap, @SoLuong, @GiaNhap, NULL)";

                    using (SqlCommand cmdTonKho = new SqlCommand(queryTonKho, conn))
                    using (SqlCommand cmdCTNK = new SqlCommand(queryCTNK, conn))
                    {
                        foreach (var sp in danhSachSanPhamMoi)
                        {
                            // Chèn vào bảng Tồn Kho
                            cmdTonKho.Parameters.Clear();
                            cmdTonKho.Parameters.AddWithValue("@TenSP", sp.TenSP);
                            cmdTonKho.Parameters.AddWithValue("@ViTriHang", sp.ViTri);
                            cmdTonKho.Parameters.AddWithValue("@SoLuongTon", sp.SoLuong);
                            cmdTonKho.Parameters.AddWithValue("@ID_Kho", sp.Kho);
                            cmdTonKho.ExecuteNonQuery();

                            // Chèn vào bảng CTNK
                            cmdCTNK.Parameters.Clear();
                            cmdCTNK.Parameters.AddWithValue("@TenSP", sp.TenSP);
                            cmdCTNK.Parameters.AddWithValue("@ID_PhieuNhap", ID_PhieuNhap);
                            cmdCTNK.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                            cmdCTNK.Parameters.AddWithValue("@GiaNhap", sp.Gia);
                            cmdCTNK.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi khi thêm sản phẩm mới: {ex.Message}");
            }
        }

        // Cập nhật phiếu nhập
        public static void CapNhatPhieuNhap(string ID_PhieuNhap, int ID_Kho, DateTime NgayNhap, decimal TongGia)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                INSERT INTO [Phiếu Nhập] (ID_PhieuNhap, ID_Kho, NgayNhap, TongGia)
                VALUES (@ID_PhieuNhap, @ID_Kho, @NgayNhap, @TongGia)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_PhieuNhap", ID_PhieuNhap);
                        cmd.Parameters.AddWithValue("@ID_Kho", ID_Kho);
                        cmd.Parameters.AddWithValue("@NgayNhap", NgayNhap);
                        cmd.Parameters.AddWithValue("@TongGia", TongGia);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý lỗi tại đây (nếu cần)
                Console.WriteLine($"Lỗi khi thêm phiếu nhập: {ex.Message}");
            }
        }
        public static void CapNhatChiTietPhieuNhap(string ID_PhieuNhap, List<SanPhamHienThi> danhSachSanPham)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                INSERT INTO CTNK (ID_PhieuNhap, ID_SP, TenSP, SoLuong, GiaNhap)
                VALUES (@ID_PhieuNhap, @ID_SP, @TenSP, @SoLuong, @GiaNhap)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        foreach (var sp in danhSachSanPham)
                        {
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ID_PhieuNhap", ID_PhieuNhap);
                            cmd.Parameters.AddWithValue("@ID_SP", sp.ID > 0 ? (object)sp.ID : DBNull.Value); // ID_SP là NULL nếu sản phẩm mới
                            cmd.Parameters.AddWithValue("@TenSP", sp.TenSP);
                            cmd.Parameters.AddWithValue("@SoLuong", sp.SoLuong);
                            cmd.Parameters.AddWithValue("@GiaNhap", sp.Gia);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý lỗi tại đây (nếu cần)
                Console.WriteLine($"Lỗi khi cập nhật chi tiết phiếu nhập: {ex.Message}");
            }
        }
    }
}
