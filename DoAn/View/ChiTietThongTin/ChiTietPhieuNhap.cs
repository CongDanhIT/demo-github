using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn.View.ChiTietThongTin
{
    public partial class ChiTietPhieuNhap : Form
    {
        private string maPhieuNhap; // Lưu mã phiếu nhập để dùng trong form
        public ChiTietPhieuNhap(string idMaPhieu)
        {
            InitializeComponent();
            this.maPhieuNhap = idMaPhieu;
            txt_MaPhieu.Text = maPhieuNhap;
        }

        private void ChiTietPhieuNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
