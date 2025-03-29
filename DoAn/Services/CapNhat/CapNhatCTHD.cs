using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Services.CapNhat
{
    public class CapNhatCTHD
    {
       
            // Chuỗi kết nối CSDL (khuyến nghị lấy từ cấu hình thay vì hardcode)
            private static readonly string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";

            public static bool CapNhatChiTietHD(string idHD, string idSP, decimal gia, int soLuong)
            {
                try
                {
                    // Tính toán giá trị cần cập nhật
                    decimal soTien = gia;
                    decimal giaHienTai = soLuong * soTien;

                    string query = @"UPDATE CTHD 
                                SET SoTien = @SoTien, GiaHienTai = @GiaHienTai, SoLuong = @SoLuong 
                                WHERE ID_HD = @ID_HD AND ID_SP = @ID_SP";

                    using (SqlConnection conn = new SqlConnection(connectionString))
                    {
                        conn.Open();
                        using (SqlCommand cmd = new SqlCommand(query, conn))
                        {
                            cmd.Parameters.Add("@SoTien", System.Data.SqlDbType.Decimal).Value = soTien;
                            cmd.Parameters.Add("@GiaHienTai", System.Data.SqlDbType.Decimal).Value = giaHienTai;
                            cmd.Parameters.Add("@SoLuong", System.Data.SqlDbType.Int).Value = soLuong;
                            cmd.Parameters.Add("@ID_HD", System.Data.SqlDbType.NVarChar).Value = idHD;
                            cmd.Parameters.Add("@ID_SP", System.Data.SqlDbType.NVarChar).Value = idSP;

                            int rowsAffected = cmd.ExecuteNonQuery(); // Thực thi lệnh cập nhật

                            return rowsAffected > 0; // Trả về true nếu có dòng được cập nhật
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Lỗi cập nhật CTHD: " + ex.Message);
                    return false;
                }
            }
        // Phương thức tính lại tổng tiền hóa đơn
        public static bool TinhLaiTongTien(string idHD)
        {
            try
            {
                decimal tongTien = 0;

                // Câu truy vấn tính tổng tiền từ cột GiaHienTai
                string query = "SELECT SUM(GiaHienTai) FROM CTHD WHERE ID_HD = @ID_HD";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add("@ID_HD", System.Data.SqlDbType.NVarChar).Value = idHD;

                        object result = cmd.ExecuteScalar();
                        if (result != DBNull.Value)
                        {
                            tongTien = Convert.ToDecimal(result);
                        }
                    }
                }

                // Cập nhật lại tổng tiền vào bảng HoaDon
                string updateQuery = "UPDATE [Hóa Đơn] SET TongTien = @TongTien WHERE ID_HD = @ID_HD";

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(updateQuery, conn))
                    {
                        cmd.Parameters.Add("@TongTien", System.Data.SqlDbType.Decimal).Value = tongTien;
                        cmd.Parameters.Add("@ID_HD", System.Data.SqlDbType.NVarChar).Value = idHD;

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Lỗi tính lại tổng tiền: " + ex.Message);
                return false;
            }
        }
    }
}
