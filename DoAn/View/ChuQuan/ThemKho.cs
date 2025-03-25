using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn.Services.Them;

namespace DoAn.View.ChuQuan
{
    public partial class ThemKho : Form
    {
        public ThemKho()
        {
            InitializeComponent();
        }

        private void bnt_XacNhan_Click(object sender, EventArgs e)
        {
          string tenKho =  text_TenKho.Text;
            AddKho.Add(tenKho);

        }
    }
}
