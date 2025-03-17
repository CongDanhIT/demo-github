using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DoAn.Modal;
using DoAn.Services.ThanhToan;

namespace DoAn.View.NhanVien
{
    public partial class _2_GiaoDienNV : Form
    {
        public _2_GiaoDienNV()
        {
            InitializeComponent();
        }
        public List<SanPham> danhSachSanPham = new List<SanPham>();

        private void _2_GiaoDienNV_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'qLKD_CHTTDataSet.Sản_Phẩm' table. You can move, or remove it, as needed.
            this.sản_PhẩmTableAdapter.Fill(this.qLKD_CHTTDataSet.Sản_Phẩm);
            /*Duyệt tất cả cột trong DataGridView.
               Kiểm tra tên cột có phải "EditColumn" không.
                Nếu đúng, ép kiểu về DataGridViewButtonColumn.
                Nếu là ButtonColumn, đặt text = "Chọn" và cho phép hiển thị.
                Thoát vòng lặp vì đã tìm thấy cột cần sửa.*/
            foreach (DataGridViewColumn col in BangSP.Columns)
            {
                if (col.Name == "EditColumn") // Tìm đúng cột cần sửa
                {
                    DataGridViewButtonColumn btnColumn = col as DataGridViewButtonColumn;
                    if (btnColumn != null)
                    {
                        btnColumn.Text = "Chọn";
                        btnColumn.UseColumnTextForButtonValue = true; // Hiển thị text trên nút
                    }
                    break;
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex >= 0 && BangSP.Columns[e.ColumnIndex].Name == "EditColumn")
            {
                int rowIndex = e.RowIndex;
                if (rowIndex >= 0)
                {
                    string tenSanPham = BangSP.Rows[rowIndex].Cells[1].Value?.ToString() ?? "N/A";
                    string gia = BangSP.Rows[rowIndex].Cells[4].Value?.ToString() ?? "N/A";
                    DataRowView rowView = (DataRowView)BangSP.Rows[e.RowIndex].DataBoundItem;
                    int idSanPham = Convert.ToInt32(rowView["ID_SP"]);

                    //TESTTTTTT
                    //if (e.RowIndex >= 0) // Đảm bảo người dùng click vào hàng hợp lệ
                    //{
                    //    DataRowView rowView = (DataRowView)BangSP.Rows[e.RowIndex].DataBoundItem;
                    //    int idSanPham = Convert.ToInt32(rowView["ID_SP"]);
                    //    MessageBox.Show($"ID Sản phẩm: {idSanPham}");
                    //}

                    if (decimal.TryParse(gia, out decimal giaSanPham))
                    {
                        // Kiểm tra xem sản phẩm đã tồn tại trong danh sách chưa
                        SanPham sanPham = danhSachSanPham.FirstOrDefault(sp => sp.ID_SanPham == idSanPham);

                        if (sanPham != null)
                        {
                            // Nếu sản phẩm đã tồn tại, tăng số lượng
                            sanPham.SoLuong++;
                        }
                        else
                        {
                            // Nếu chưa có, thêm mới vào danh sách
                            sanPham = new SanPham(idSanPham, tenSanPham, giaSanPham);
                            danhSachSanPham.Add(sanPham);
                        }

                        // Cập nhật ListBox
                        CapNhatHienThiListBox();

                        // Cập nhật tổng tiền
                        CapNhatTongTien();
                    }
                    else
                    {
                        MessageBox.Show("Giá sản phẩm không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        // Hàm cập nhật danh sách sản phẩm trong ListBox
        private void CapNhatHienThiListBox()
        {
            list_HienThi.Items.Clear();
            foreach (var sp in danhSachSanPham)
            {
                string item = $"{sp.Ten} - {sp.SoLuong} x {sp.Gia:N0} VND";
                list_HienThi.Items.Add(item);
            }
        }
        private void CapNhatTongTien()
        {
            decimal tongTien = danhSachSanPham.Sum(sp => sp.Gia * sp.SoLuong);
            txt_TongTien.Text = tongTien.ToString("N0") ; // Hiển thị có dấu phẩy và đơn vị tiền tệ
        }

        private void fillByToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.sản_PhẩmTableAdapter.FillBy(this.qLKD_CHTTDataSet.Sản_Phẩm);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void fillBy1ToolStripButton_Click(object sender, EventArgs e)
        {
            try
            {
                this.sản_PhẩmTableAdapter.FillBy1(this.qLKD_CHTTDataSet.Sản_Phẩm);
            }
            catch (System.Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void txt_TongTien_TextChanged(object sender, EventArgs e)
        {

        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            // Lưu danh sách index của các mục được chọn (sắp xếp giảm dần)
            // Việc sắp xếp selectedIndexes theo thứ tự giảm dần là cần thiết.
            // Nếu bạn xóa từ chỉ mục nhỏ đến lớn, các phần tử phía sau sẽ bị lệch index và có thể gây lỗi.
            List<int> selectedIndexes = list_HienThi.SelectedIndices.Cast<int>().OrderByDescending(i => i).ToList();

            // Xóa trong danhSachSanPham theo index
            foreach (int index in selectedIndexes)
            {
                danhSachSanPham.RemoveAt(index);
            }

            // Xóa trong ListBox theo index
            foreach (int index in selectedIndexes)
            {
                list_HienThi.Items.RemoveAt(index);
            }

            // Cập nhật tổng tiền sau khi xóa
            CapNhatTongTien();
        }

        private void btn_ThanhToan_Click(object sender, EventArgs e)
        {
            DateTime now = DateTime.Now;
            decimal TongTien = decimal.Parse(txt_TongTien.Text);
            int id_tk = int.Parse(CurrentUserSession.CurrentUser.ID_TK);
            string Id_HD = RandomHoaDon.GenerateHoaDon();
            LapHD.CapNhatHD(id_tk, TongTien, Id_HD, now);
            CTHDManager.CapNhatCTHD(Id_HD, danhSachSanPham);

            // cần cập nhật Bảng hóa Đơn
            // cần Cập nhật chi tiết hóa đơn

        }
    }
}
