using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn.Modal;
using DoAn.Services.CapNhat;
using DoAn.Services.XemThongTin;

namespace DoAn.View.CTTK
{
    public partial class ThongTinTK : Form
    {
        public ThongTinTK()
        {
            InitializeComponent();
        }

        private void btn_XacNhan_Click(object sender, EventArgs e)
        {   string Id = text_ID.Text; 
            string name = txt_Name.Text;
            string SDT = txt_SDT.Text;
            CapNhatTTNV.capNhatTT(Id, name, SDT);

        }

        private void ThongTinTK_Load(object sender, EventArgs e)
        {
            ThongTinNV.GetNhanVienByIDTK(CurrentUserSession.CurrentUser.ID_TK);
            txt_Name.Text = NhanVienStatic.HoTen;
            txt_SDT.Text = NhanVienStatic.SDT;
            text_ID.Text = NhanVienStatic.Id_NV;
        }

        private void btn_DangXuat_Click(object sender, EventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Close();  // Đóng form cha nếu có
            }
            else
            {
                MessageBox.Show("không thấy owner");
            }

            DangNhap form = new DangNhap();
            form.Show();
            CurrentUserSession.ClearCurrentUser();
        }
    }
}
