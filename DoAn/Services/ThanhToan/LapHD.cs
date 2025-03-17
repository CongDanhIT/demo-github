using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn.Services.ThanhToan
{
    public class LapHD
    {
        static private string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
        static public void CapNhatHD(int idTaiKhoan, decimal TongTien,string Id_HD , DateTime thoiGian)
        {
            string query = @"
            INSERT INTO [Hóa Đơn] (ID_HD, NgayGio, TongTien, ID_TK)
            VALUES (@IdHD, @ThoiGian, @TongTien, @IdTaiKhoan);";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@IdHD", Id_HD);
                cmd.Parameters.AddWithValue("@ThoiGian", thoiGian);
                cmd.Parameters.AddWithValue("@TongTien", TongTien);
                cmd.Parameters.AddWithValue("@IdTaiKhoan", idTaiKhoan);

                conn.Open();
                int affectedRows = cmd.ExecuteNonQuery();
                MessageBox.Show("thành công");
            }
        }
    }
}
