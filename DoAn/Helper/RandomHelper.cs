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
}
