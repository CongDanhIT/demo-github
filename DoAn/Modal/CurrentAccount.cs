using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Modal
{
    public class CurrentAccount
    {
        public string ID_TK { get; set; }
        public string ID_NV { get; set; }
        public string IDRole { get; set; }
        // Các thuộc tính khác mà bạn cần lưu trữ

        // Constructor có thể để khởi tạo nếu cần
        public CurrentAccount(string idTaiKhoan, string idNV,  string role)
        {
            ID_TK = idTaiKhoan;
            ID_NV = idNV;
            IDRole = role;
        }
    }
    public static class CurrentUserSession
    {
        public static CurrentAccount CurrentUser { get; private set; }

        // Thiết lập thông tin người dùng
        public static void SetCurrentUser(CurrentAccount user)
        {
            CurrentUser = user;
        }

        // Xóa thông tin người dùng (đăng xuất)
        public static void ClearCurrentUser()
        {
            CurrentUser = null;
        }

        // Kiểm tra xem có người dùng hiện tại hay không
        public static bool IsUserLoggedIn()
        {
            return CurrentUser != null;
        }
    }
    public class UserService
    {
        public CurrentAccount Login(string username, string password)
        {

            // Kết nối đến cơ sở dữ liệu và kiểm tra thông tin đăng nhập
            string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
            string query = "SELECT ID_TK, ID_NV, ID_VaiTro FROM [Tài Khoản]s WHERE TenDN = @Username ";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@Password", password);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                if (reader.HasRows)
                {
                    reader.Read(); // Lấy thông tin người dùng
                    string IdTK = reader.IsDBNull(0) ? "NULL" : reader.GetInt32(0).ToString();
                    string IdNV = reader.IsDBNull(1) ? "NULL" : reader.GetString(1);
                    string IdVaiTro = reader.IsDBNull(2) ? "NULL" : reader.GetInt32(2).ToString();

                    // Tạo đối tượng CurrentUser và trả về
                    return new CurrentAccount(IdTK, IdNV, IdVaiTro);
                }
                else
                {
                    return null; // Nếu không tìm thấy người dùng
                }
            }
        }

    }
}
