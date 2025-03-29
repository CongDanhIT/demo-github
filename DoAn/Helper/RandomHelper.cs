using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Helper
{
    public class RandomHelper
    {
        private static Random random = new Random();
        private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        public static string GenerateRandomString()
        {
            StringBuilder result = new StringBuilder(6);
            for (int i = 0; i < 6; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }
            return result.ToString();
        }
    }
}
public class RandomHoaDon
{
    private static char currentChar = 'A'; // Ký tự đầu tiên
    private static int currentNumber = 0;  // Bắt đầu từ 00000
    private static bool isMaxReached = false; // Kiểm tra đã đạt giới hạn chưa
    private static string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
    public static string GenerateHoaDon()
    {
        string lastMaHoaDon = GetLastHoaDonFromDatabase();
        string newMaHoaDon = NextHoaDon(lastMaHoaDon);        
        return newMaHoaDon;
    }
    private static string GetLastHoaDonFromDatabase()
    {
        using (SqlConnection conn = new SqlConnection(connectionString))
        {
            conn.Open();
            string query = "SELECT TOP 1 ID_HD FROM [Hóa Đơn] ORDER BY ID_HD DESC";
            using (SqlCommand cmd = new SqlCommand(query, conn))
            {
                var result = cmd.ExecuteScalar();
                return result?.ToString() ?? "A00000"; // Nếu chưa có, bắt đầu từ A00000
            }
        }
    }

    private static string NextHoaDon(string lastHoaDon)
    {
        char currentChar = lastHoaDon[0];
        int currentNumber = int.Parse(lastHoaDon.Substring(1));

        currentNumber++;

        if (currentNumber > 99999)
        {
            currentNumber = 0;
            currentChar++;

            if (currentChar > 'Z')
            {
                throw new InvalidOperationException("Đã đạt giới hạn mã hóa đơn (Z99999).");
            }
        }

        return $"{currentChar}{currentNumber:D5}";
    }
    public class RandomPhieuNhap
    {
        private static string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";

        public static string GeneratePhieuNhap()
        {
            string lastMaPhieuNhap = GetLastPhieuNhapFromDatabase();
            return NextPhieuNhap(lastMaPhieuNhap);
        }

        private static string GetLastPhieuNhapFromDatabase()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT TOP 1 ID_PhieuNhap FROM [Phiếu Nhập] ORDER BY ID_PhieuNhap DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    var result = cmd.ExecuteScalar();
                    return result?.ToString() ?? "A00000"; // Nếu chưa có, bắt đầu từ A00000
                }
            }
        }

        private static string NextPhieuNhap(string lastPhieuNhap)
        {
            char currentChar = lastPhieuNhap[0];
            int currentNumber = int.Parse(lastPhieuNhap.Substring(1));

            currentNumber++;

            if (currentNumber > 99999)
            {
                currentNumber = 0;
                currentChar++;

                if (currentChar > 'Z')
                {
                    throw new InvalidOperationException("Đã đạt giới hạn mã phiếu nhập (Z99999).");
                }
            }

            return $"{currentChar}{currentNumber:D5}";
        }
    }
    public class RandomMaNhanVien
    {
        private static string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";

        public static string GenerateMaNhanVien()
        {
            string lastMaNV = GetLastMaNhanVienFromDatabase();
            return NextMaNhanVien(lastMaNV);
        }

        private static string GetLastMaNhanVienFromDatabase()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = "SELECT TOP 1 ID_NV FROM [Nhân Viên] WHERE ID_NV LIKE 'NV%' ORDER BY ID_NV DESC";
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    var result = cmd.ExecuteScalar();
                    return result?.ToString() ?? "NV0000"; // Nếu chưa có NV, bắt đầu từ NV0000
                }
            }
        }

        private static string NextMaNhanVien(string lastMaNV)
        {
            int currentNumber = int.Parse(lastMaNV.Substring(2)); // Lấy số phía sau "NV"
            currentNumber++;

            return $"NV{currentNumber:D4}"; // Định dạng thành NV0001, NV0002,... (đúng 6 ký tự)
        }
    }

}
