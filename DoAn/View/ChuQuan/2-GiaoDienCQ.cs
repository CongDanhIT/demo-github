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
using System.Windows.Forms.DataVisualization.Charting;
using DoAn.Helper;
using DoAn.Modal;
using DoAn.Services.Them;
using DoAn.Services.XemThongTin;
using DoAn.View.ChiTietThongTin;
using DoAn.View.CTTK;
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
        private DataTable dtPN; // Lưu dữ liệu phiếu nhập gốc
        private DataTable dtHD; // Lưu dữ liệu hóa đơn gốc
        private void _2_GiaoDienCQ_Load(object sender, EventArgs e)
        {
            LoadToanBoKho();
            LoadTonKho();
            LoadPhieuNhap();
            LoadBangSPChuaBan();
            LoadBangSP_GDSP();
            LoadNhanVien();
            LoadBangHD();
            LoadNhanViencomb();
            List<Kho> danhSachKho = DSKho.DsKho(); // Lấy danh sách kho từ database
            tab_QLK.Visible = true;
            tab_QLKD.Visible = false;
            if (danhSachKho.Count > 0)
            {
                comb_IDKho.DataSource = danhSachKho;
                comb_IDKho.DisplayMember = "TenKho"; // Hiển thị TenKho trong combobox
                comb_IDKho.ValueMember = "ID_Kho"; // Lấy ID_Kho làm giá trị
                comb_KhoPN.DataSource = danhSachKho;
                comb_KhoPN.DisplayMember = "TenKho"; // Hiển thị TenKho trong combobox
                comb_KhoPN.ValueMember = "ID_Kho";
                comb_Kho_SP.DataSource = danhSachKho;
                comb_Kho_SP.DisplayMember = "TenKho"; // Hiển thị TenKho trong combobox
                comb_Kho_SP.ValueMember = "ID_Kho";
            }
        }
        private void LoadDanhSachHoaDon()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy khoảng thời gian từ DateTimePicker
                    DateTime tuNgay = date_Start2.Value.Date;
                    DateTime denNgay = date_end2.Value.Date.AddDays(1).AddTicks(-1);

                    // Truy vấn SQL lấy danh sách hóa đơn kèm theo tên nhân viên
                    string query = @"
                SELECT hd.ID_HD, hd.NgayGio, nv.HoTen AS NhanVien, hd.TongTien 
                FROM [Hóa Đơn] hd
                INNER JOIN [Tài Khoản] tk ON hd.ID_TK = tk.ID_TK
                INNER JOIN [Nhân Viên] nv ON tk.ID_NV = nv.ID_NV
                WHERE hd.NgayGio BETWEEN @TuNgay AND @DenNgay
                ORDER BY hd.NgayGio DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@TuNgay", tuNgay);
                        cmd.Parameters.AddWithValue("@DenNgay", denNgay);

                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        dgv_HoaDon.DataSource = dt;

                        // Căn chỉnh tự động kích thước cột
                        dgv_HoaDon.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                    }
                  //  MessageBox.Show($"Query: {query}\nTuNgay: {tuNgay}\nDenNgay: {denNgay}");

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải danh sách hóa đơn: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LoadDoanhThu()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy dữ liệu từ UI
                    int thang = date_Start.Value.Month;
                    int nam = date_Start.Value.Year;

                    // Truy vấn SQL tính tổng doanh thu của tháng đó
                    string query = @"
                SELECT SUM(TongTien) 
                FROM [Hóa Đơn] 
                WHERE MONTH(NgayGio) = @Thang AND YEAR(NgayGio) = @Nam";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Thang", thang);
                        cmd.Parameters.AddWithValue("@Nam", nam);

                        object result = cmd.ExecuteScalar();
                        decimal doanhThu = (result != DBNull.Value) ? Convert.ToDecimal(result) : 0;

                        // Hiển thị tổng doanh thu lên Label
                        lbl_TongDoanhThu.Text = $"Tổng doanh thu: {doanhThu:N0} VNĐ";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tính doanh thu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadLocSP()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Lấy dữ liệu từ UI
                    string danhMuc = comb_DanhMuc.SelectedItem?.ToString();
                    string kieuThongKe = comb_KieuTK.SelectedItem?.ToString() ?? "Doanh thu";
                    DateTime tuNgay = date_Start.Value.Date;
                    DateTime denNgay = date_end.Value.Date.AddDays(1).AddTicks(-1);
                    string groupBy = radio_Thang.Checked ? "MONTH" : radio_Nam.Checked ? "YEAR" : "DAY";

                    string conditionDanhMuc = (string.IsNullOrEmpty(danhMuc) || danhMuc == "Tất Cả") ? "" : "AND sp.Loai = @DanhMuc";

                    string query = kieuThongKe == "Doanh Thu"
                        ? @"
           SELECT sp.TenSanPham, 
                  DATEPART(" + groupBy + @", hd.NgayGio) AS ThoiGian,
                  SUM(cthd.SoTien) AS DoanhThu
           FROM CTHD cthd
           INNER JOIN [Hóa Đơn] hd ON cthd.ID_HD = hd.ID_HD
           INNER JOIN [Sản Phẩm] sp ON cthd.ID_SP = sp.ID_SP
           WHERE hd.NgayGio BETWEEN @TuNgay AND @DenNgay " + conditionDanhMuc + @"
           GROUP BY sp.TenSanPham, DATEPART(" + groupBy + @", hd.NgayGio)
           ORDER BY DoanhThu DESC"
                        : @"
           SELECT sp.TenSanPham, 
                  DATEPART(" + groupBy + @", hd.NgayGio) AS ThoiGian,
                  SUM(cthd.SoLuong) AS SLBan
           FROM CTHD cthd
           INNER JOIN [Hóa Đơn] hd ON cthd.ID_HD = hd.ID_HD
           INNER JOIN [Sản Phẩm] sp ON cthd.ID_SP = sp.ID_SP
           WHERE hd.NgayGio BETWEEN @TuNgay AND @DenNgay " + conditionDanhMuc + @"
           GROUP BY sp.TenSanPham, DATEPART(" + groupBy + @", hd.NgayGio)
           ORDER BY SLBan DESC";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.Add(new SqlParameter("@TuNgay", SqlDbType.DateTime) { Value = tuNgay });
                        cmd.Parameters.Add(new SqlParameter("@DenNgay", SqlDbType.DateTime) { Value = denNgay });

                        if (!string.IsNullOrEmpty(danhMuc) && danhMuc != "Tất Cả")
                        {
                            cmd.Parameters.Add(new SqlParameter("@DanhMuc", danhMuc));
                        }

                        DataTable dt = new DataTable();
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        BangLocSP.DataSource = dt;
                        BangLocSP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                        // Gọi hàm load dữ liệu lên Chart
                        LoadChart(dt, kieuThongKe);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void LoadChart(DataTable dt, string kieuThongKe)
        {
            // Xóa dữ liệu cũ trên biểu đồ
            chart_SanPham.Series.Clear();
            chart_SanPham.Titles.Clear();

            // Thêm tiêu đề cho biểu đồ
            chart_SanPham.Titles.Add(kieuThongKe == "Doanh Thu" ? "Biểu đồ Doanh Thu Sản Phẩm" : "Biểu đồ Số Lượng Bán");

            // Tạo Series mới
            Series series = new Series(kieuThongKe == "Doanh Thu" ? "Doanh Thu" : "Số Lượng Bán");
            series.ChartType = SeriesChartType.Column; // Có thể đổi sang SeriesChartType.Line nếu muốn hiển thị dạng đường

            // Duyệt dữ liệu và thêm vào series
            foreach (DataRow row in dt.Rows)
            {
                string tenSP = row["TenSanPham"].ToString();
                int thoiGian = Convert.ToInt32(row["ThoiGian"]);
                decimal giaTri = kieuThongKe == "Doanh Thu" ? Convert.ToDecimal(row["DoanhThu"]) : Convert.ToInt32(row["SLBan"]);

                // Thêm dữ liệu vào biểu đồ
                series.Points.AddXY(tenSP + " (" + thoiGian + ")", giaTri);
            }

            // Thêm Series vào Chart
            chart_SanPham.Series.Add(series);
        }




        public void LoadBangHD()
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = @"
                SELECT HD.*, NV.HoTen  
                FROM [Hóa Đơn] HD  
                INNER JOIN [Tài khoản] TK ON HD.ID_TK = TK.ID_TK  
                INNER JOIN [Nhân Viên] NV ON TK.ID_NV = NV.ID_NV";

                    SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                    dtHD = new DataTable(); // Lưu dữ liệu vào biến toàn cục
                    adapter.Fill(dtHD);
                    BangHoaDon.DataSource = dtHD; // Gán dữ liệu vào DataGridView

                    // Tăng chiều rộng cột HoTen
                    BangHoaDon.Columns["HoTen"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    BangHoaDon.Columns["HoTen"].Width = 200;

                    // Kiểm tra và thêm cột "Xem"
                    if (BangHoaDon.Columns["XemButton"] == null)
                    {
                        DataGridViewButtonColumn btnXem = new DataGridViewButtonColumn();
                        btnXem.Name = "XemButton";
                        btnXem.HeaderText = "";
                        btnXem.Text = "Xem";
                        btnXem.UseColumnTextForButtonValue = true;
                        BangHoaDon.Columns.Insert(0, btnXem); // Thêm vào vị trí đầu tiên
                    }

                    // Kiểm tra và thêm cột "Sửa"
                    if (BangHoaDon.Columns["SuaButton"] == null)
                    {
                        DataGridViewButtonColumn btnSua = new DataGridViewButtonColumn();
                        btnSua.Name = "SuaButton";
                        btnSua.HeaderText = "";
                        btnSua.Text = "Sửa";
                        btnSua.UseColumnTextForButtonValue = true;
                        BangHoaDon.Columns.Insert(1, btnSua); // Thêm vào vị trí thứ hai
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void LoadNhanVien()
        {
            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open();
                string query = @"
                        SELECT NV.*, TK.*, VT.*
                        FROM [Nhân Viên] NV
                        JOIN [Tài Khoản] TK ON NV.ID_TK = TK.ID_TK
                        JOIN [Vai Trò] VT ON TK.ID_VaiTro = VT.ID_VaiTro;";

                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);

                BangThongTinNV.DataSource = dt;

                // Ẩn các cột không cần hiển thị
                BangThongTinNV.Columns["ID_NV"].Visible = false;
                BangThongTinNV.Columns["ID_TK"].Visible = false;
                BangThongTinNV.Columns["ID_VaiTro"].Visible = false;
                BangThongTinNV.Columns["ID_NV1"].Visible = false;
                BangThongTinNV.Columns["ID_TK1"].Visible = false;
                BangThongTinNV.Columns["ID_VaiTro1"].Visible = false;
                BangThongTinNV.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

        }
        public void LoadBangSP_GDSP()
        {
            string query = "SELECT * FROM [Sản Phẩm]"; // Truy vấn lấy tất cả dữ liệu từ bảng Sản Phẩm

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                BangSP_GDSP.DataSource = dataTable; // Gán dữ liệu vào DataGridView

                // Tùy chỉnh DataGridView
                BangSP_GDSP.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                BangSP_GDSP.AllowUserToAddRows = false; // Không cho phép thêm dòng trống

                // Ẩn cột MaSanPham
                if (BangSP_GDSP.Columns["MaSanPham"] != null)
                {
                    BangSP_GDSP.Columns["MaSanPham"].Visible = false;
                }

                // Thêm nút "Sửa" vào đầu danh sách cột nếu chưa có
                if (BangSP_GDSP.Columns["btnSua"] == null)
                {
                    DataGridViewButtonColumn btnSua = new DataGridViewButtonColumn();
                    btnSua.Name = "btnSua";
                    btnSua.HeaderText = "";
                    btnSua.Text = "Sửa";
                    btnSua.UseColumnTextForButtonValue = true;
                    btnSua.Width = 100; // Điều chỉnh chiều rộng

                    BangSP_GDSP.Columns.Insert(0, btnSua); // Chèn vào vị trí đầu tiên
                }

                // Đảm bảo không đăng ký sự kiện trùng lặp
                BangSP_GDSP.CellContentClick -= BangSP_GDSP_CellContentClick;
                BangSP_GDSP.CellContentClick += BangSP_GDSP_CellContentClick; // Đăng ký sự kiện ngay trong hàm
            }
        }
        public void LoadBangSPChuaBan()
        {
            string query = "SELECT * FROM [Tồn Kho] WHERE ID_SP IS NULL";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                BangSPChuaBan.DataSource = dataTable; // Gán dữ liệu vào DataGridView

                // Kiểm tra nếu chưa có cột Button thì thêm vào
                if (BangSPChuaBan.Columns["btnDuaLenCuaHang"] == null)
                {
                    DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
                    btn.Name = "btnDuaLenCuaHang";
                    btn.HeaderText = "";
                    btn.Text = "Đưa lên cửa hàng";
                    btn.UseColumnTextForButtonValue = true;
                    btn.Width = 150; // Tăng chiều rộng
                    BangSPChuaBan.Columns.Insert(0, btn);
                }
                BangSPChuaBan.CellContentClick -= BangSPChuaBan_CellContentClick; // Xóa event cũ nếu có
                BangSPChuaBan.CellContentClick += BangSPChuaBan_CellContentClick; // Đăng ký event
            }
        }
        public void LoadPhieuNhap()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "SELECT * FROM [Phiếu Nhập]";
                    SqlDataAdapter adapter = new SqlDataAdapter(query, conn);
                    dtPN = new DataTable(); // Lưu dữ liệu vào biến toàn cục
                    adapter.Fill(dtPN);
                    BangPN.DataSource = dtPN; // Gán dữ liệu vào DataGridView
                }

                // Kiểm tra nếu chưa có cột button "Xem" thì mới thêm vào
                if (BangPN.Columns["btnXem"] == null)
                {
                    DataGridViewButtonColumn btnXem = new DataGridViewButtonColumn
                    {
                        Name = "btnXem",
                        HeaderText = "",
                        Text = "Xem",
                        UseColumnTextForButtonValue = true
                    };
                    BangPN.Columns.Insert(0, btnXem);
                }

                // Kiểm tra nếu chưa có cột button "Sửa" thì mới thêm vào
                if (BangPN.Columns["btnSua"] == null)
                {
                    DataGridViewButtonColumn btnSua = new DataGridViewButtonColumn
                    {
                        Name = "btnSua",
                        HeaderText = "",
                        Text = "Sửa",
                        UseColumnTextForButtonValue = true
                    };
                    BangPN.Columns.Insert(1, btnSua); // Chèn ngay sau cột "Xem"
                }

                BangPN.CellContentClick -= BangPN_CellContentClick;
                BangPN.CellContentClick += BangPN_CellContentClick;
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
                    BangTonKho.Columns["TenSanPham"].Width = 100; // Điều chỉnh tùy theo nhu cầu
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
                LoadBangSPChuaBan();
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
        // Khai báo formChiTietPhieuNhap ở cấp lớp để kiểm soát form đã mở
        private FormChiTietPhieuNhap formChiTietPhieuNhap = null;
        private void BangPN_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maPhieuNhap = BangPN.Rows[e.RowIndex].Cells["ID_PhieuNhap"].Value.ToString(); // Lấy mã phiếu nhập

                if (e.ColumnIndex == BangPN.Columns["btnXem"].Index) // Nếu nhấn vào nút "Xem"
                {
                    ChiTietPhieuNhap.XemCTPhieuNhap(maPhieuNhap);
                }
                else if (e.ColumnIndex == BangPN.Columns["btnSua"].Index) // Nếu nhấn vào nút "Sửa"
                {
                    // Kiểm tra nếu form đã mở thì chỉ cần focus lại
                    if (formChiTietPhieuNhap != null && !formChiTietPhieuNhap.IsDisposed)
                    {
                        formChiTietPhieuNhap.Focus();
                        return; // Không mở lại form mới
                    }

                    // Nếu form chưa mở, tạo form mới
                    formChiTietPhieuNhap = new FormChiTietPhieuNhap(maPhieuNhap, this);

                    // Đăng ký sự kiện khi form đóng để giải phóng biến
                    formChiTietPhieuNhap.FormClosed += (s, args) => formChiTietPhieuNhap = null;

                    formChiTietPhieuNhap.ShowDialog();

                    // Load lại dữ liệu sau khi sửa
                    LoadPhieuNhap();
                }
            }
        }

        private void BangTonKho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

       

        

        private void BangSPChuaBan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // Kiểm tra hàng hợp lệ
            {
                DataGridViewRow row = BangSPChuaBan.Rows[e.RowIndex];

                // Lấy giá trị từ cột ID_Kho và TenSanPham (đổi tên cột nếu cần)
                int kho = Convert.ToInt32(row.Cells["ID_Kho"].Value);
                string TenSP = row.Cells["TenSP"].Value.ToString();



                // Truyền vào FormDuaSPLenCH
                FormDuaSPLenCH form = new FormDuaSPLenCH(kho, TenSP, this);
                form.ShowDialog();
            }
        }


        private void BangSP_GDSP_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0 && BangSP_GDSP.Columns[e.ColumnIndex].Name == "btnSua")
            {
                int idSP = Convert.ToInt32(BangSP_GDSP.Rows[e.RowIndex].Cells["ID_SP"].Value);
                string maSP = BangSP_GDSP.Rows[e.RowIndex].Cells["MaSanPham"].Value?.ToString();
                string loai = BangSP_GDSP.Rows[e.RowIndex].Cells["Loai"].Value?.ToString();
                string tenSanPham = BangSP_GDSP.Rows[e.RowIndex].Cells["TenSanPham"].Value?.ToString();
                string size = BangSP_GDSP.Rows[e.RowIndex].Cells["Size"].Value?.ToString();
                string mauSac = BangSP_GDSP.Rows[e.RowIndex].Cells["MauSac"].Value?.ToString();
                decimal gia = Convert.ToDecimal(BangSP_GDSP.Rows[e.RowIndex].Cells["Gia"].Value);
                int? idKho = BangSP_GDSP.Rows[e.RowIndex].Cells["ID_Kho"].Value as int?;

                // Mở form sửa sản phẩm và truyền dữ liệu
                FormSuaSP formSua = new FormSuaSP(idSP,maSP, loai, tenSanPham, size, mauSac, gia, idKho,this);
                formSua.ShowDialog(); // Hiển thị form
            }
        }

        private void btn_ThemSP_Click(object sender, EventArgs e)
        {
            try
            {
                string loai = text_Loai.Text;
                string size = text_Size.Text;
                string tenSP = text_SP.Text;
                string mauSac = text_MauSac.Text;

                // Kiểm tra và chuyển đổi giá trị 'gia'
                if (!decimal.TryParse(text_GiaSanPham.Text, out decimal gia))
                {
                    MessageBox.Show("Giá sản phẩm không hợp lệ! Vui lòng nhập số.", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Kiểm tra và chuyển đổi ID kho
                if (!int.TryParse(comb_Kho_SP.SelectedValue?.ToString(), out int kho))
                {
                    MessageBox.Show("Kho sản phẩm không hợp lệ!", "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }


                bool thanhCong = ServiceSanPham.ThemSanPham(loai, tenSP, size, mauSac, gia, kho);

                if (thanhCong)
                {
                    MessageBox.Show("Thêm sản phẩm thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadBangSP_GDSP();
                }
                else
                {
                    MessageBox.Show("Thêm sản phẩm thất bại. Vui lòng thử lại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_ThemNV_Click(object sender, EventArgs e)
        {
            string Ten= txt_Ten.Text;
            string TenDN = txt_TenDN.Text;
            string password = txt_MK.Text;
            string SDT = txt_SDT.Text;
            string idNV = RandomMaNhanVien.GenerateMaNhanVien();
            if (ThemNV.AddTK(idNV, TenDN, password))
            {
                // Bước 2: Nếu thêm tài khoản thành công, thêm nhân viên
                if (ThemNV.AddNV(idNV, Ten, SDT, TenDN, password))
                {
                    MessageBox.Show("Thêm nhân viên thành công!");
                    LoadNhanVien();
                }
                else
                {
                    MessageBox.Show("Lỗi khi thêm nhân viên.");
                }
            }
            else
            {
                MessageBox.Show("Lỗi khi thêm tài khoản.");
            }

        }

        private void pic_TK_Click(object sender, EventArgs e)
        {
            ThongTinTK form = new ThongTinTK();
            form.Owner = this;
            form.ShowDialog();
        }

       
        private void BangHoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string columnName = BangHoaDon.Columns[e.ColumnIndex].Name;
                string idHoaDon = BangHoaDon.Rows[e.RowIndex].Cells["ID_HD"].Value.ToString();

                if (columnName == "XemButton")
                {
                    // Xử lý khi bấm vào nút "Xem"
                    ChiTietTTHoaDon.XemTTHoaDon(idHoaDon);
                }
                else if (columnName == "SuaButton")
                {
                    // Xử lý khi bấm vào nút "Sửa"
                    FormSuaHoaDon suaForm = new FormSuaHoaDon(idHoaDon,this);
                    suaForm.ShowDialog();
                }
            }
        }

   

        private void pnl_QLKD_Click(object sender, EventArgs e)
        {
            tab_QLK.Visible = false;
            tab_TK.Visible = false;
            tab_QLKD.Visible = true;
        }
        private void pnl_QLK_Click(object sender, EventArgs e)
        {
            tab_QLK.Visible = true;
            tab_QLKD.Visible = false;
            tab_TK.Visible = false;
        }
        private void pnl_ThongKe_Click(object sender, EventArgs e)
        {
            tab_QLK.Visible = false;
            tab_QLKD.Visible = false;
            tab_TK.Visible = true;
            
        }

        private void btn_LocSP_Click(object sender, EventArgs e)
        {
            LoadLocSP();
            

         

        }
        private void LoadNhanViencomb()
        {
            string query = "SELECT HoTen FROM [Nhân Viên]";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // Thêm dòng "Tất Cả" vào DataTable
                DataRow allRow = dt.NewRow();
                allRow["HoTen"] = "Tất Cả";
                dt.Rows.InsertAt(allRow, 0); // Chèn vào đầu danh sách

                comb_NV.DataSource = dt;
                comb_NV.DisplayMember = "HoTen";
                comb_NV.SelectedIndex = 0; // Mặc định chọn "Tất Cả"
            }
        }
        private void LoadBangTKNV()
        {
            string query = @"
        SELECT NV.HoTen, SUM(HD.TongTien) AS DoanhThu
        FROM [Hóa Đơn] HD
        JOIN [Tài Khoản] TK ON HD.ID_TK = TK.ID_TK
        JOIN [Nhân Viên] NV ON TK.ID_NV = NV.ID_NV
        WHERE HD.NgayGio BETWEEN @StartDate AND @EndDate
        AND (@HoTen = N'Tất Cả' OR NV.HoTen = @HoTen)
        GROUP BY NV.HoTen
        ORDER BY DoanhThu DESC;
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                conn.Open(); // Mở kết nối trước khi thực hiện truy vấn
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // Đảm bảo ngày luôn ở định dạng hợp lệ
                    DateTime startDate = date_Start1.Value.Date;
                    DateTime endDate = date_end1.Value.Date.AddDays(1).AddTicks(-1); // Lấy hết ngày cuối cùng

                    cmd.Parameters.AddWithValue("@StartDate", startDate);
                    cmd.Parameters.AddWithValue("@EndDate", endDate);

                    // Lấy giá trị từ ComboBox, kiểm tra null
                    string selectedNV = "Tất Cả"; // Giá trị mặc định

                    if (comb_NV.SelectedItem is DataRowView row)
                    {
                        selectedNV = row["HoTen"].ToString(); // Lấy đúng dữ liệu từ DataTable
                    }
                    else if (comb_NV.SelectedItem != null)
                    {
                        selectedNV = comb_NV.SelectedItem.ToString();
                    }
                    cmd.Parameters.AddWithValue("@HoTen", selectedNV);

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    BangTKNV.DataSource = dt;
                  //  MessageBox.Show($"Query: {query}\nStartDate: {startDate}, EndDate: {endDate}, HoTen: {selectedNV}");

                }
            }
        }

        private void HienThiNVDT()
        {
          
            string query = @"
        SELECT TOP 1 NV.HoTen, SUM(HD.TongTien) AS DoanhThu
        FROM [Hóa Đơn] HD
        JOIN [Tài Khoản] TK ON HD.ID_TK = TK.ID_TK
        JOIN [Nhân Viên] NV ON TK.ID_NV = NV.ID_NV
        WHERE HD.NgayGio BETWEEN @StartDate AND @EndDate
        GROUP BY NV.HoTen
        ORDER BY DoanhThu DESC;
    ";

            using (SqlConnection conn = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@StartDate", date_Start1.Value);
                cmd.Parameters.AddWithValue("@EndDate", date_end1.Value);

                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    string tenNV = reader["HoTen"].ToString();
                    string doanhThu = Convert.ToDecimal(reader["DoanhThu"]).ToString("#,##0 VNĐ");

                    lbl_NVDT.Text = $"🎉 Nhân viên xuất sắc: {tenNV}\n📊 Doanh thu: {doanhThu}";
                }
                else
                {
                    lbl_NVDT.Text = "Không có dữ liệu";
                }
                reader.Close();
            }
        }


        private void btn_LocNV_Click(object sender, EventArgs e)
        {
            LoadBangTKNV();
            HienThiNVDT();
        }

        private void btn_Loc3_Click(object sender, EventArgs e)
        {
            LoadDoanhThu();
            LoadDanhSachHoaDon();
        }

        private void txt_SearchPN_TextChanged(object sender, EventArgs e)
        {
            if (dtPN != null) // Kiểm tra dữ liệu đã load chưa
            {
                string filterText = txt_SearchPN.Text.Trim().Replace("'", "''"); // Tránh lỗi SQL Injection
                dtPN.DefaultView.RowFilter = $"ID_PhieuNhap LIKE '%{filterText}%' OR CONVERT(NgayNhap, 'System.String') LIKE '%{filterText}%'";
            }
        }

        private void txt_SearchHD1_TextChanged(object sender, EventArgs e)
        {

            if (dtHD != null) // Kiểm tra dữ liệu đã load chưa
            {
                string filterText = txt_SearchHD1.Text.Trim().Replace("'", "''"); // Tránh lỗi SQL Injection
                dtHD.DefaultView.RowFilter = $"ID_HD LIKE '%{filterText}%' OR HoTen LIKE '%{filterText}%'";
            }
        }

        private void BangKho_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
       
}
