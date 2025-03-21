using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Modal
{
    public class NhanVien
    {
        public string Id_NV { get; set; }
        public string HoTen { get; set; }
        public string SDT { get; set; }
        public NhanVien(string id_NV, string hoTen, string sDT)
        {
            Id_NV = id_NV;
            HoTen = hoTen;
            SDT = sDT;
        }
        public NhanVien() { }

    }
    public static class NhanVienStatic
    {
        public static string Id_NV { get; set; }
        public static string HoTen { get; set; }
        public static string SDT { get; set; }

        // Phương thức để cập nhật dữ liệu nhân viên vào class static
        public static void SetNhanVien(string id, string name, string sdt)
        {
            Id_NV = id;
            HoTen = name;
            SDT = sdt;
        }

        // Phương thức nhận một đối tượng `NhanVien` để gán dữ liệu
        public static void SetNhanVien(NhanVien nv)
        {
            if (nv != null)
            {
                Id_NV = nv.Id_NV;
                HoTen = nv.HoTen;
                SDT = nv.SDT;
            }
        }
    }
}
