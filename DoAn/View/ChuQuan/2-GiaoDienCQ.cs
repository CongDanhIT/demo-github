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
using DoAn.Modal;
using DoAn.Services.Them;
using DoAn.Services.XemThongTin;
using static RandomHoaDon;

namespace DoAn.View.ChuQuan
{
    public partial class _2_GiaoDienCQ : Form
    {
        public _2_GiaoDienCQ()
        {
            InitializeComponent();
        }
       // public List<SanPham> danhSachSanPham = new List<SanPham>();
        public List<Kho> Kho = new List<Kho>();
        private string connectionString = "Server=DESKTOP-J7AQH5B;Database=QLKD_CHTT;User Id=sa;Password=123456;";
        // Danh sách sản phẩm mới nhập (cần thêm vào kho)
        private List<SanPhamMoiNhap> danhSachSanPhamMoi = new List<SanPhamMoiNhap>();

        // Danh sách sản phẩm đã có trong kho (cần cập nhật số lượng)
        private List<SanPham> danhSachSanPhamCu = new List<SanPham>();
        private void _2_GiaoDienCQ_Load(object sender, EventArgs e)
        {
            LoadToanBoKho();
            LoadTonKho();
            LoadPhieuNhap();
            List<Kho> danhSachKho = DSKho.DsKho(); // Lấy danh sách kho từ database

            if (danhSachKho.Count > 0)
            {
                comb_IDKho.DataSource = danhSachKho;
                comb_IDKho.DisplayMember = "TenKho"; // Hiển thị TenKho trong combobox
                comb_IDKho.ValueMember = "ID_Kho"; // Lấy ID_Kho làm giá trị
                comb_KhoPN.DataSource = danhSachKho;
                comb_KhoPN.DisplayMember = "TenKho"; // Hiển thị TenKho trong combobox
                comb_KhoPN.ValueMember = "ID_Kho";
            }
        }
        private void LoadPhieuNhap()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM [Phiếu Nhập]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    BangPN.DataSource = dt;
                }

                // Kiểm tra nếu chưa có cột button "Xem" thì mới thêm vào
                if (BangPN.Columns["btnXem"] == null)
                {
                    DataGridViewButtonColumn btnXem = new DataGridViewButtonColumn
                    {
                        Name = "btnXem",
                        HeaderText = "Xem",
                        Text = "Xem",
                        UseColumnTextForButtonValue = true
                    };
                    BangPN.Columns.Insert(0, btnXem);
                }

                // **Xóa sự kiện cũ trước khi đăng ký lại**
                BangPN.CellClick -= BangPN_CellContentClick;
                BangPN.CellClick += BangPN_CellContentClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }
        public void LoadTonKho()
        {
             // Thay bằng chuỗi kết nối của bạn
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM [Tồn Kho]";
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                BangTonKhoAll.DataSource = dt;
                BangTonKhoAll.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }
        public void LoadToanBoKho()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT * FROM Kho ORDER BY ID_Kho ASC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    DataTable dt = new DataTable();
                    adapter.Fill(dt);

                    BangKho.DataSource = dt; // Gán dữ liệu vào DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu Kho: " + ex.Message);
            }
        }
        private void LoadBangSP(int idKho)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
            SELECT SP.*, TK.SoLuongTon 
            FROM [Sản Phẩm] SP
            INNER JOIN [Tồn Kho] TK ON SP.ID_SP = TK.ID_SP
            WHERE TK.ID_Kho = @ID_Kho
            ORDER BY SP.ID_SP ASC";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    adapter.SelectCommand.Parameters.AddWithValue("@ID_Kho", idKho);

                    DataTable dt = new DataTable();
                    adapter.Fill(dt);
                    BangTonKho.DataSource = dt;

                    // Sắp xếp thứ tự cột
                    BangTonKho.Columns["TenSanPham"].DisplayIndex = 1; // Tên sản phẩm ở vị trí [1]
                    BangTonKho.Columns["Gia"].DisplayIndex = 4; // Giá ở vị trí [4]

                    // Ẩn các cột không cần hiển thị
                    BangTonKho.Columns["ID_SP"].Visible = false;
                    BangTonKho.Columns["MaSanPham"].Visible = false;
                    BangTonKho.Columns["Size"].Visible = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void comb_IDKho_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comb_IDKho.SelectedValue != null)
            {
                int idKho;
                if (int.TryParse(comb_IDKho.SelectedValue.ToString(), out idKho))
                {
                    LoadBangSP(idKho); // Gọi hàm load sản phẩm
                }
            }
        }

        private void btn_ThemKho_Click(object sender, EventArgs e)
        {
            ThemKho form = new ThemKho();
            form.ShowDialog();
            
        }

        private void btn_Reset_Click(object sender, EventArgs e)
        {
            LoadToanBoKho();
        }

        // Khai báo danh sách trung gian để quản lý dữ liệu
        private BindingList<SanPhamHienThi> danhSachHienThi = new BindingList<SanPhamHienThi>();

        private void btn_ThemVao_Click(object sender, EventArgs e)
        {
            // Lấy dữ liệu từ các TextBox và ComboBox
            string tenSP = txt_TenSP.Text.Trim();
            string soLuongText = txt_SoLuong.Text.Trim();
            string giaText = txt_Gia.Text.Trim();
            string viTri = txt_ViTri.Text.Trim();
            bool daTonTai = check_TonTai.Checked; // Kiểm tra checkbox

            // Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrWhiteSpace(tenSP) ||
                string.IsNullOrWhiteSpace(soLuongText) ||
                string.IsNullOrWhiteSpace(giaText) ||
                comb_KhoPN.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin sản phẩm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Chuyển đổi số lượng, giá và kho
            if (!int.TryParse(soLuongText, out int soLuong) ||
                !decimal.TryParse(giaText, out decimal gia) ||
                !int.TryParse(comb_KhoPN.SelectedValue.ToString(), out int kho))
            {
                MessageBox.Show("Số lượng, giá và kho phải là số hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (daTonTai) // Sản phẩm đã có: cần ID
            {
                // Kiểm tra và chuyển đổi ID sang kiểu int
                if (!int.TryParse(txt_idSP.Text.Trim(), out int id))
                {
                    MessageBox.Show("Vui lòng nhập ID hợp lệ cho sản phẩm đã có!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Tìm sản phẩm trong danh sách hiển thị
                var sanPhamTonTai = danhSachHienThi.FirstOrDefault(sp => sp.ID == id);

                if (sanPhamTonTai != null)
                {
                    // Nếu đã tồn tại, cập nhật số lượng
                    sanPhamTonTai.SoLuong += soLuong;
                }
                else
                {
                    // Nếu chưa có, thêm mới vào danh sách hiển thị
                    danhSachHienThi.Add(new SanPhamHienThi
                    {
                        ID = id,
                        TenSP = tenSP,
                        SoLuong = soLuong,
                        Gia = gia,
                        Kho = kho,
                        ViTri = viTri
                    });
                }
            }
            else // Sản phẩm mới không có ID
            {
                // Tìm sản phẩm trong danh sách hiển thị
                var sanPhamMoi = danhSachHienThi.FirstOrDefault(sp => sp.TenSP == tenSP && sp.Kho == kho && sp.ViTri == viTri);

                if (sanPhamMoi != null)
                {
                    // Nếu đã tồn tại, cập nhật số lượng
                    sanPhamMoi.SoLuong += soLuong;
                }
                else
                {
                    // Nếu chưa có, thêm mới vào danh sách hiển thị
                    danhSachHienThi.Add(new SanPhamHienThi
                    {
                        ID = 0, // Sản phẩm mới không có ID
                        TenSP = tenSP,
                        SoLuong = soLuong,
                        Gia = gia,
                        Kho = kho,
                        ViTri = viTri
                    });
                }
            }

            // Cập nhật ListBox từ danh sách trung gian
            CapNhatListBox();

            // Cập nhật tổng tiền
            CapNhatTongTien();
        }

        // Phương thức cập nhật ListBox từ danh sách trung gian
        private void CapNhatListBox()
        {
            listB_NhapKho.Items.Clear(); // Xóa toàn bộ dữ liệu cũ trong ListBox

            foreach (var sp in danhSachHienThi)
            {
                if (sp.ID > 0) // Sản phẩm có ID
                {
                    listB_NhapKho.Items.Add($"ID: {sp.ID} - {sp.TenSP} | SL: {sp.SoLuong} | Giá: {sp.Gia}");
                }
                else // Sản phẩm mới
                {
                    listB_NhapKho.Items.Add($"{sp.TenSP} | SL: {sp.SoLuong} | Giá: {sp.Gia} | Kho: {sp.Kho} | Vị trí: {sp.ViTri}");
                }
            }
        }

        // Phương thức cập nhật tổng tiền
        private void CapNhatTongTien()
        {
            decimal tongTien = danhSachHienThi.Sum(sp => sp.SoLuong * sp.Gia);
            txt_TongTien.Text = tongTien.ToString("N0"); // Định dạng số có dấu phẩy ngăn cách
        }

        // Lớp đại diện cho sản phẩm hiển thị
        public class SanPhamHienThi
        {
            public int ID { get; set; } // ID sản phẩm (0 nếu là sản phẩm mới)
            public string TenSP { get; set; } // Tên sản phẩm
            public int SoLuong { get; set; } // Số lượng
            public decimal Gia { get; set; } // Giá
            public int Kho { get; set; } // Kho
            public string ViTri { get; set; } // Vị trí
        }




        private void btn_XacNhan_Click(object sender, EventArgs e)
        {
            try
            {
                // Kiểm tra xem có sản phẩm nào trong danh sách hiển thị không
                if (danhSachHienThi.Count == 0)
                {
                    MessageBox.Show("Không có sản phẩm nào để nhập kho!");
                    return; // Dừng hàm nếu không có sản phẩm
                }

                DateTime time = DateTime.Now;
                int idKho = Convert.ToInt32(comb_KhoPN.SelectedValue);
                string ID_Phieu = RandomPhieuNhap.GeneratePhieuNhap();
                decimal TongGia = decimal.Parse(txt_TongTien.Text);

                // Phân loại sản phẩm từ danh sách hiển thị
                var danhSachSanPhamCu = danhSachHienThi.Where(sp => sp.ID > 0).ToList();
                var danhSachSanPhamMoi = danhSachHienThi.Where(sp => sp.ID == 0).ToList();

                // Kiểm tra danh sách sản phẩm trước khi nhập
                if (danhSachSanPhamMoi.Count > 0 && danhSachSanPhamCu.Count > 0)
                {
                    NhapSP.NhapSanPhamMoi(danhSachSanPhamMoi, ID_Phieu);
                    NhapSP.NhapSanPhamDaCo(danhSachSanPhamCu, ID_Phieu);
                }
                else if (danhSachSanPhamMoi.Count > 0)
                {
                    NhapSP.NhapSanPhamMoi(danhSachSanPhamMoi, ID_Phieu);
                }
                else if (danhSachSanPhamCu.Count > 0)
                {
                    NhapSP.NhapSanPhamDaCo(danhSachSanPhamCu, ID_Phieu);
                }

                // Cập nhật phiếu nhập chỉ khi có sản phẩm
                NhapSP.CapNhatPhieuNhap(ID_Phieu, idKho, time, TongGia);

                // Chuyển đổi BindingList<SanPhamHienThi> thành List<SanPhamHienThi>
                List<SanPhamHienThi> danhSachHienThiList = danhSachHienThi.ToList();

                // Cập nhật chi tiết phiếu nhập
                NhapSP.CapNhatChiTietPhieuNhap(ID_Phieu, danhSachHienThiList);

                // Xóa danh sách hiển thị sau khi nhập thành công
                danhSachHienThi.Clear();
                CapNhatListBox(); // Cập nhật lại ListBox
                MessageBox.Show("Nhập hàng thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadTonKho();
                LoadPhieuNhap();
            }
            catch (Exception ex)
            {
                // Ghi log lỗi hoặc xử lý lỗi tại đây (nếu cần)
                Console.WriteLine($"Lỗi khi xác nhận phiếu nhập: {ex.Message}");
            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào được chọn trong ListBox không
            if (listB_NhapKho.SelectedIndex == -1)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Lấy thông tin sản phẩm từ ListBox
            string selectedItem = listB_NhapKho.SelectedItem.ToString();

            // Tìm sản phẩm trong danh sách BindingList để xóa
            var sanPhamXoa = danhSachHienThi.FirstOrDefault(sp =>
                selectedItem.Contains(sp.TenSP) &&
                selectedItem.Contains($"SL: {sp.SoLuong}") &&
                selectedItem.Contains($"Giá: {sp.Gia}"));

            if (sanPhamXoa != null)
            {
                danhSachHienThi.Remove(sanPhamXoa); // Xóa sản phẩm khỏi danh sách
                CapNhatListBox(); // Cập nhật lại ListBox
                CapNhatTongTien(); // Cập nhật lại tổng tiền
            }   
        }

        private void BangPN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && e.ColumnIndex == 0) // Kiểm tra nếu nhấn vào cột "Xem"
            {
                string maPhieuNhap = BangPN.Rows[e.RowIndex].Cells["ID_PhieuNhap"].Value.ToString(); // Giả sử có cột MaPhieuNhap
                ChiTietPhieuNhap.XemCTPhieuNhap(maPhieuNhap);
            }
        }
    }
}
