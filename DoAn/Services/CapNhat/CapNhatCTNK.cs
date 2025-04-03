using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn.Services.CapNhat
{
    public class CapNhatCTNK
    {
        static private string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";

        public static bool CapNhat(string idPhieuNhap, string tenSP, int soLuong, decimal giaNhap, string idSP, int SLBanDau)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlTransaction transaction = conn.BeginTransaction(); // Bắt đầu transaction để đảm bảo tính nhất quán dữ liệu

                    try
                    {
                        // Cập nhật SoLuong và GiaNhap trong bảng CTNK
                        string queryUpdateCTNK = @"
                    UPDATE CTNK 
                    SET SoLuong = @SoLuong, GiaNhap = @GiaNhap 
                    WHERE ID_PhieuNhap = @ID_PhieuNhap AND TenSP = @TenSP";

                        using (SqlCommand cmd = new SqlCommand(queryUpdateCTNK, conn, transaction))
                        {
                            cmd.Parameters.AddWithValue("@ID_PhieuNhap", idPhieuNhap);
                            cmd.Parameters.AddWithValue("@TenSP", tenSP);
                            cmd.Parameters.AddWithValue("@SoLuong", soLuong);
                            cmd.Parameters.AddWithValue("@GiaNhap", giaNhap);
                            cmd.ExecuteNonQuery();
                        }

                        // Nếu có ID_SP thì cần cập nhật SoLuongTon trong bảng [Tồn Kho]
                        if (!string.IsNullOrEmpty(idSP))
                        {
                            int soLuongTonThayDoi = SLBanDau - soLuong;
                            string queryUpdateTonKho = @"
                        UPDATE [Tồn Kho] 
                        SET SoLuongTon = SoLuongTon + @ThayDoiSoLuong 
                        WHERE ID_SP = @ID_SP";

                            using (SqlCommand cmd = new SqlCommand(queryUpdateTonKho, conn, transaction))
                            {
                                cmd.Parameters.AddWithValue("@ID_SP", idSP);
                                cmd.Parameters.AddWithValue("@ThayDoiSoLuong", -soLuongTonThayDoi); // Đảo dấu vì khi SLBanDau lớn hơn thì giảm, ngược lại thì tăng
                                cmd.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit(); // Nếu mọi thứ đều thành công, commit transaction
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // Nếu có lỗi, rollback để không làm mất dữ liệu
                        MessageBox.Show("Lỗi khi cập nhật CTNK: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi kết nối cơ sở dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public static bool CapNhatTongTienPN(string idPhieuNhap)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
            UPDATE [Phiếu Nhập]
            SET TongGia = (
                SELECT SUM(SoLuong * GiaNhap) 
                FROM CTNK 
                WHERE ID_PhieuNhap = @ID_PhieuNhap
            )
            WHERE ID_PhieuNhap = @ID_PhieuNhap";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_PhieuNhap", idPhieuNhap);
                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0; // Trả về true nếu cập nhật thành công
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật tổng giá: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
    }
}
