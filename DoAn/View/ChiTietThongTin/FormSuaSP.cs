using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn.Services.CapNhat;
using DoAn.View.ChuQuan;

namespace DoAn.View.ChiTietThongTin
{
    public partial class FormSuaSP : Form
    {
        private string maSP;
        private string loai;
        private string tenSanPham;
        private string size;
        private string mauSac;
        private decimal gia;
        private int? idKho;
        private int idSP;
        private _2_GiaoDienCQ form;

        // Constructor nhận dữ liệu
        public FormSuaSP(int idSP, string maSP, string loai, string tenSanPham, string size, string mauSac, decimal gia, int? idKho, _2_GiaoDienCQ form)
        {
            InitializeComponent();

            // Gán giá trị vào biến
            this.idSP = idSP;
            this.maSP = maSP;
            this.loai = loai;
            this.tenSanPham = tenSanPham;
            this.size = size;
            this.mauSac = mauSac;
            this.gia = gia;
            this.idKho = idKho;
            this.form = form;
        }

        private void FormSuaSP_Load(object sender, EventArgs e)
        {
            // Hiển thị dữ liệu lên các ô nhập (giả sử bạn có các TextBox)
            txt_IDSP.Text = idSP.ToString();
            txt_Loai.Text = loai;
            txt_SP.Text = tenSanPham;
            txt_Size.Text = size;
            txt_Mau.Text = mauSac;
            txt_Gia.Text = gia.ToString();
            txt_Kho.Text = idKho?.ToString();
        }

        private void btn_CapNhat_Click(object sender, EventArgs e)
        {
            int idSP = Convert.ToInt32(txt_IDSP.Text);
            string maSP = txt_IDSP.Text;
            string loai = txt_Loai.Text;
            string tenSanPham = txt_SP.Text;
            string size = txt_Size.Text;
            string mauSac = txt_Mau.Text;
            decimal gia = Convert.ToDecimal(txt_Gia.Text);
            int? idKho = string.IsNullOrEmpty(txt_Kho.Text) ? (int?)null : Convert.ToInt32(txt_Kho.Text);

            bool ketQua = CapNhatSP.CapNhatSanPham(idSP, maSP, loai, tenSanPham, size, mauSac, gia, idKho);

            if (ketQua)
            {
                MessageBox.Show("Cập nhật sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                form.LoadBangSP_GDSP();
            }
            else
            {
                MessageBox.Show("Cập nhật không thành công!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}