using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Modal
{
    public class SanPhamMoiNhap
    {
        public string TenSP { get; set; }
        public string ViTriHang { get; set; }

        public int SoLuongTon { get; set; }
        
        public decimal Gia {  get; set; }
        public int Kho { get; set; }
        public SanPhamMoiNhap() { }
        SanPhamMoiNhap(string ten, int soluong, decimal gia, string vitri,string kho)
        {
            TenSP = ten;
            ViTriHang = vitri;
            SoLuongTon = soluong;
        } 
    }
}
