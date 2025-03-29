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
    public partial class FormSuaHoaDon : Form
    {
        public FormSuaHoaDon(string idHD,_2_GiaoDienCQ form)
        {
            InitializeComponent();
            this.idHD = idHD;
            this.form = form;
        }
        private string idHD;
        private _2_GiaoDienCQ form;
        private void FormSuaHoaDon_Load(object sender, EventArgs e)
        {
            txt_IDHD.Text = idHD;
            LoadCTHD();
        }
        private string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
        private void LoadCTHD()
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT CTHD.*, SP.TenSanPham  
                FROM [CTHD] CTHD  
                INNER JOIN [Sản Phẩm] SP ON CTHD.ID_SP = SP.ID_SP  
                WHERE CTHD.ID_HD = @ID_HD";

                    using (SqlCommand cmd = new SqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ID_HD", idHD);

                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        DataTable dt = new DataTable();
                        adapter.Fill(dt);

                        BangChiTietHD.DataSource = dt; // Gán dữ liệu vào DataGridView

                        // Ẩn cột ID_SP và ID_HD
                        BangChiTietHD.Columns["ID_SP"].Visible = false;
                        BangChiTietHD.Columns["ID_HD"].Visible = false;

                        // Đưa cột TenSanPham lên vị trí thứ 2
                        BangChiTietHD.Columns["TenSanPham"].DisplayIndex = 1;

                        // Kiểm tra xem đã có cột chọn chưa (tránh thêm nhiều lần)
                        if (BangChiTietHD.Columns["ChonButton"] == null)
                        {
                            DataGridViewButtonColumn btnColumn = new DataGridViewButtonColumn();
                            btnColumn.Name = "ChonButton";
                            btnColumn.HeaderText = "Chọn";
                            btnColumn.Text = "Chọn";
                            btnColumn.UseColumnTextForButtonValue = true;
                            BangChiTietHD.Columns.Insert(0, btnColumn); // Thêm cột vào đầu bảng
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải chi tiết hóa đơn: " + ex.Message);
            }
        }

        private void BangChiTietHD_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && BangChiTietHD.Columns[e.ColumnIndex].Name == "ChonButton")
            {
                // Lấy giá trị từ hàng được chọn
                string idSP = BangChiTietHD.Rows[e.RowIndex].Cells["ID_SP"].Value.ToString();
                string soLuong = BangChiTietHD.Rows[e.RowIndex].Cells["SoLuong"].Value.ToString();
                string gia = BangChiTietHD.Rows[e.RowIndex].Cells["SoTien"].Value.ToString();

                // Gán vào các TextBox
                txt_IDSP.Text = idSP;
                txt_SL.Text = soLuong;
                txt_Gia.Text = gia;
            }
        }

        private void btn_XacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                decimal gia = decimal.Parse(txt_Gia.Text);
                int soLuong = int.Parse(txt_SL.Text);
                string idSP = txt_IDSP.Text;
                string idHD = txt_IDHD.Text;

                // Cập nhật chi tiết hóa đơn
                bool success = CapNhatCTHD.CapNhatChiTietHD(idHD, idSP, gia, soLuong);

                // Tính lại tổng tiền hóa đơn
                if (success)
                {
                    bool updated = CapNhatCTHD.TinhLaiTongTien(idHD);
                    if (updated)
                    {
                        MessageBox.Show("Cập nhật CTHD và tổng tiền hóa đơn thành công!");
                        form.LoadBangHD();
                    }
                    else
                        MessageBox.Show("Cập nhật CTHD thành công nhưng không tính lại tổng tiền!");
                }
                else
                {
                    MessageBox.Show("Không có bản ghi nào được cập nhật!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi: " + ex.Message);
            }
        }
    }
}
