using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKD_CHTT.Modal
{
    public class CurrentUser
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string IDRole { get; set; }
        // Các thuộc tính khác mà bạn cần lưu trữ

        // Constructor có thể để khởi tạo nếu cần
        public CurrentUser(string userId, string username, string email, string role)
        {
            UserId = userId;
            Username = username;
            Email = email;
            IDRole = role;
        }
    }

    public static class CurrentUserSession
    {
        public static CurrentUser CurrentUser { get; set; }

        // Bạn có thể tạo các phương thức để thiết lập, xóa hoặc lấy thông tin người dùng
        public static void SetCurrentUser(CurrentUser user)
        {
            CurrentUser = user;
        }

        public static void ClearCurrentUser()
        {
            CurrentUser = null;
        }
    }
}
