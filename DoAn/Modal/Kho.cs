using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoAn.Modal
{
    public class Kho
    {
        public int Id_Kho { get; set; }
        public string TenKho { get; set; }
       
        public Kho (int id_kho, string Tenkho )
        {
            Id_Kho = id_kho;
            TenKho = Tenkho;
        }
        public Kho() { }
    }
}
