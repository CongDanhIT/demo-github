using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Services.Them
{
    public class ServiceSanPham
    {
        static private string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
        static public bool ThemSanPham(string loai, string tenSanPham, string size, string mauSac, decimal gia, int idKho)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "INSERT INTO [Sản Phẩm] (Loai, TenSanPham, Size, MauSac, Gia, ID_Kho) " +
                                   "VALUES (@Loai, @TenSanPham, @Size, @MauSac, @Gia, @ID_Kho)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Loai", loai);
                        cmd.Parameters.AddWithValue("@TenSanPham", tenSanPham);
                        cmd.Parameters.AddWithValue("@Size", size);
                        cmd.Parameters.AddWithValue("@MauSac", mauSac);
                        cmd.Parameters.AddWithValue("@Gia", gia);
                        cmd.Parameters.AddWithValue("@ID_Kho", idKho);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi thêm sản phẩm: " + ex.Message);
                return false;
            }
        }
        public static bool UpdateSanPhamTonKho(int idSP, int idKhoNhap, string tenSanPham)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // 1️⃣ Lấy ID_Kho của sản phẩm từ bảng [Sản Phẩm]
                    string getKhoQuery = "SELECT ID_Kho FROM [Sản Phẩm] WHERE ID_SP = @ID_SP";
                    int idKhoHienTai = -1;

                    using (SqlCommand cmd = new SqlCommand(getKhoQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_SP", idSP);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            idKhoHienTai = Convert.ToInt32(result);
                        }
                        else
                        {
                            Console.WriteLine("Không tìm thấy sản phẩm!");
                            return false;
                        }
                    }

                    // 2️⃣ Kiểm tra ID_Kho của sản phẩm có trùng với ID_Kho nhập vào không
                    if (idKhoHienTai != idKhoNhap)
                    {
                        Console.WriteLine("ID_Kho không trùng khớp, không thể cập nhật!");
                        return false;
                    }

                    // 3️⃣ Kiểm tra xem dữ liệu đã tồn tại trong [Tồn Kho] chưa
                    string checkExistsQuery = "SELECT COUNT(*) FROM [Tồn Kho] WHERE ID_Kho = @ID_Kho AND TenSP = @TenSanPham";
                    int count = 0;

                    using (SqlCommand checkCmd = new SqlCommand(checkExistsQuery, conn))
                    {
                        checkCmd.Parameters.AddWithValue("@ID_Kho", idKhoNhap);
                        checkCmd.Parameters.AddWithValue("@TenSanPham", tenSanPham);

                        count = Convert.ToInt32(checkCmd.ExecuteScalar());
                    }

                    if (count > 0)
                    {
                        // 4️⃣ Nếu đã tồn tại, cập nhật lại thông tin
                        string updateQuery = "UPDATE [Tồn Kho] SET ID_SP = @ID_SP WHERE ID_Kho = @ID_Kho AND TenSP = @TenSanPham";

                        using (SqlCommand updateCmd = new SqlCommand(updateQuery, conn))
                        {
                            updateCmd.Parameters.AddWithValue("@ID_SP", idSP);
                            updateCmd.Parameters.AddWithValue("@ID_Kho", idKhoNhap);
                            updateCmd.Parameters.AddWithValue("@TenSanPham", tenSanPham);

                            int rowsUpdated = updateCmd.ExecuteNonQuery();
                            return rowsUpdated > 0;
                        }
                    }
                    else
                    {
                        // 5️⃣ Nếu chưa tồn tại, thêm mới vào [Tồn Kho]
                        string insertQuery = "INSERT INTO [Tồn Kho] (ID_SP, ID_Kho, TenSP) VALUES (@ID_SP, @ID_Kho, @TenSanPham)";

                        using (SqlCommand insertCmd = new SqlCommand(insertQuery, conn))
                        {
                            insertCmd.Parameters.AddWithValue("@ID_SP", idSP);
                            insertCmd.Parameters.AddWithValue("@ID_Kho", idKhoNhap);
                            insertCmd.Parameters.AddWithValue("@TenSanPham", tenSanPham);

                            int rowsInserted = insertCmd.ExecuteNonQuery();
                            return rowsInserted > 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi khi cập nhật Tồn Kho: " + ex.Message);
                return false;
            }
        }

    }
}
