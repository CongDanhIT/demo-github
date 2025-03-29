using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Services.CapNhat
{
    public class CapNhatSP
    {
        static private string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
        public static bool CapNhatSanPham(int idSP, string maSP, string loai, string tenSanPham, string size, string mauSac, decimal gia, int? idKho)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                        UPDATE [Sản Phẩm] 
                        SET MaSanPham = @MaSP, Loai = @Loai, TenSanPham = @TenSanPham, 
                            Size = @Size, MauSac = @MauSac, Gia = @Gia, ID_Kho = @IDKho 
                        WHERE ID_SP = @IDSP";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@IDSP", idSP);
                        cmd.Parameters.AddWithValue("@MaSP", maSP);
                        cmd.Parameters.AddWithValue("@Loai", loai);
                        cmd.Parameters.AddWithValue("@TenSanPham", tenSanPham);
                        cmd.Parameters.AddWithValue("@Size", size);
                        cmd.Parameters.AddWithValue("@MauSac", mauSac);
                        cmd.Parameters.AddWithValue("@Gia", gia);
                        cmd.Parameters.AddWithValue("@IDKho", (object)idKho ?? DBNull.Value);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // Trả về `true` nếu cập nhật thành công, ngược lại `false`
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Lỗi cập nhật sản phẩm: {ex.Message}");
                return false;
            }
        }

    }
}
