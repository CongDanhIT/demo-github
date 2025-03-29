using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn.Services.Them;
using DoAn.View.ChuQuan;

namespace DoAn.View.ChiTietThongTin
{
    public partial class FormDuaSPLenCH : Form
    {
        public FormDuaSPLenCH(int kho, string tenSP, _2_GiaoDienCQ form)
        {
            InitializeComponent();
            this.kho = kho;
            this.tenSP = tenSP;
            this.form = form;
        }
        private int kho;
        public string tenSP;
        private _2_GiaoDienCQ form;
        private string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
        private void FormDuaSPLenCH_Load(object sender, EventArgs e)
        {
            LoadSanPhamChuaCoTonKho();
            txt_Kho.Text = kho.ToString();
            txt_SP.Text = tenSP; 
        }
        public void LoadSanPhamChuaCoTonKho()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = @"
                    SELECT SP.*
                    FROM [Sản Phẩm] SP
                    LEFT JOIN [Tồn Kho] TK ON SP.ID_SP = TK.ID_SP
                    WHERE TK.ID_SP IS NULL;";

                    using (SqlDataAdapter adapter = new SqlDataAdapter(query, conn))
                    {
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);
                        BangSPCho.DataSource = dt;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                int idSP = Convert.ToInt32(txt_IDSP.Text);   // ID_SP từ txt_SP.Text
                int idKhoNhap = Convert.ToInt32(txt_Kho.Text);  // ID_Kho nhập vào từ txt_Kho.Text
                string tenSP = txt_SP.Text;  // Tên sản phẩm lấy từ ô nhập liệu

                bool result = ServiceSanPham.UpdateSanPhamTonKho(idSP, idKhoNhap, tenSP);

                if (result)
                {
                    MessageBox.Show("Cập nhật tồn kho thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    form.LoadBangSPChuaBan();
                }
                else
                {
                    MessageBox.Show("Cập nhật thất bại! Kiểm tra lại ID_Kho hoặc ID_SP.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi nhập dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
    }
}
