using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using DoAn.Modal;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using DoAn.Services.DangNhap;
using DoAn.View.ChuQuan;
using DoAn.View.NhanVien;
namespace DoAn
{
    public partial class DangNhap : Form
    {
        public DangNhap()
        {
            InitializeComponent();
        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            string UserName = txt_TenDN.Text;
            string password = txt_MatKhau.Text;
            UserService userService = new UserService();
            CurrentAccount acount = userService.Login(UserName, password);
            if (XacThucDangNhap.DangNhap(UserName,password) == true)
            {
                // Lưu thông tin người dùng vào session
                CurrentUserSession.SetCurrentUser(acount);
                MessageBox.Show("Đăng nhập thành công");
                this.Hide();
                if(CurrentUserSession.CurrentUser.IDRole == "1")
                {
                    _2_GiaoDienCQ FormCQ = new _2_GiaoDienCQ();
                    FormCQ.Show();
                }
                else
                {
                    _2_GiaoDienNV FormNV = new _2_GiaoDienNV();
                    FormNV.Show();
                }
            }
            else
            {
                MessageBox.Show("Đăng nhập thất bại.");
            }
            
        }
    }
}
