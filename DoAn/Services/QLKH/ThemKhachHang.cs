using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn.Helper;

namespace DoAn.Services.QLKH
{
    public class ThemKhachHang
    {
        static private string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
        static public void ThemKH(string TenKhach, string SDT,string MaKH)
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO [Khách hàng] (MA_KH, TenKhach, SDT) VALUES (@MaKH, @TenKhach, @SDT)";

                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("@MaKH", MaKH); // Chèn MA_KH
                    cmd.Parameters.AddWithValue("@TenKhach", TenKhach);
                    cmd.Parameters.AddWithValue("@SDT", SDT);

                    try
                    {
                        conn.Open();
                        int rowsAffected = cmd.ExecuteNonQuery();
                        MessageBox.Show(rowsAffected > 0 ? "Thêm khách hàng thành công!" : "Không có dữ liệu nào được thêm.",
                                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}
