using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Modal
{
    public class SanPham
    {
        public string Ten { get; set; }
        public decimal Gia { get; set; } // Dùng decimal cho số tiền chính xác hơn
        public int SoLuong { get; set; } = 1; // Mặc định số lượng là 1
        // Constructor
        public int ID_SanPham { get; set; } // ID của sản phẩm
        public SanPham(int idSanPham, string ten, decimal gia)
        {
            ID_SanPham = idSanPham;
            Ten = ten;
            Gia = gia;
            SoLuong = 1; // Mặc định số lượng là 1
        }

    }
}
