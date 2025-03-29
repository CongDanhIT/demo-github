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
using DoAn.Services.CapNhat;
using DoAn.View.ChuQuan;

namespace DoAn.View.ChiTietThongTin
{
    public partial class FormChiTietPhieuNhap : Form
    {
        private string maPhieuNhap; // Lưu mã phiếu nhập để dùng trong form
        private _2_GiaoDienCQ Form;
        public FormChiTietPhieuNhap(string idMaPhieu,_2_GiaoDienCQ form)
        {
            InitializeComponent();
            this.maPhieuNhap = idMaPhieu;
            txt_MaPhieu.Text = maPhieuNhap;
            this.Form = form;
        }
        private  string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";

        private void ChiTietPhieuNhap_Load(object sender, EventArgs e)
        {
            LoadChiTietPhieuNhap(maPhieuNhap,Form);
        }
        private void LoadChiTietPhieuNhap(string idPhieuNhap,_2_GiaoDienCQ form)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM CTNK WHERE ID_PhieuNhap = @ID_PhieuNhap";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    adapter.SelectCommand.Parameters.AddWithValue("@ID_PhieuNhap", idPhieuNhap);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    BangChiTietPN.DataSource = dt;
                }

                // Kiểm tra nếu chưa có cột nút "Chọn" thì mới thêm vào
                if (BangChiTietPN.Columns["btnChon"] == null)
                {
                    DataGridViewButtonColumn btnChon = new DataGridViewButtonColumn
                    {
                        Name = "btnChon",
                        HeaderText = "Chọn",
                        Text = "Chọn",
                        UseColumnTextForButtonValue = true
                    };
                    BangChiTietPN.Columns.Insert(0, btnChon); // Chèn vào vị trí đầu tiên
                }

                // Xóa sự kiện cũ trước khi đăng ký lại để tránh bị gọi nhiều lần
                BangChiTietPN.CellClick -= BangChiTietPN_CellContentClick;
                BangChiTietPN.CellClick += BangChiTietPN_CellContentClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu chi tiết phiếu nhập: " + ex.Message);
            }
        }

        private void BangChiTietPN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && BangChiTietPN.Columns[e.ColumnIndex] is DataGridViewButtonColumn)
            {
                // Lấy dữ liệu từ hàng được chọn
                string tenSP = BangChiTietPN.Rows[e.RowIndex].Cells["TenSP"].Value.ToString();
                string soLuong = BangChiTietPN.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString();
                string giaNhap = BangChiTietPN.Rows[e.RowIndex].Cells["GiaNhap"].Value.ToString();

                // Gán dữ liệu vào các TextBox
                txt_TenSP.Text = tenSP;
                txt_SoLuong.Text = soLuong;
                txt_GiaNhap.Text = giaNhap;
            }
        }

        private void btn_XacNhan_Click(object sender, EventArgs e)
        {

            string tenSP = txt_TenSP.Text;
            int.TryParse(txt_SoLuong.Text, out int soLuong);
            decimal.TryParse(txt_GiaNhap.Text, out decimal giaNhap);

            bool capNhatThanhCong = CapNhatCTNK.CapNhat(maPhieuNhap, tenSP, soLuong, giaNhap);
            bool capNhatTongTienThanhCong = CapNhatCTNK.CapNhatTongTienPN(maPhieuNhap);

            if (capNhatThanhCong && capNhatTongTienThanhCong)
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadChiTietPhieuNhap(maPhieuNhap,Form);
                Form.LoadPhieuNhap();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
    
}
