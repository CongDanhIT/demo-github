namespace QLKD_CHTT
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            panel1 = new Panel();
            split_VachNgan = new SplitContainer();
            img_DangNhap = new PictureBox();
            lbl_MatKhau = new Label();
            lbl_TenDN = new Label();
            btn_DangNhap = new Button();
            txt_MatKhau = new TextBox();
            txt_TenDN = new TextBox();
            lbl_DangNhap = new Label();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)split_VachNgan).BeginInit();
            split_VachNgan.Panel1.SuspendLayout();
            split_VachNgan.Panel2.SuspendLayout();
            split_VachNgan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)img_DangNhap).BeginInit();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(split_VachNgan);
            panel1.Location = new Point(271, 132);
            panel1.Name = "panel1";
            panel1.Size = new Size(948, 512);
            panel1.TabIndex = 0;
            // 
            // split_VachNgan
            // 
            split_VachNgan.Dock = DockStyle.Fill;
            split_VachNgan.Location = new Point(0, 0);
            split_VachNgan.Name = "split_VachNgan";
            // 
            // split_VachNgan.Panel1
            // 
            split_VachNgan.Panel1.Controls.Add(img_DangNhap);
            // 
            // split_VachNgan.Panel2
            // 
            split_VachNgan.Panel2.BackColor = Color.FromArgb(217, 217, 217);
            split_VachNgan.Panel2.Controls.Add(lbl_MatKhau);
            split_VachNgan.Panel2.Controls.Add(lbl_TenDN);
            split_VachNgan.Panel2.Controls.Add(btn_DangNhap);
            split_VachNgan.Panel2.Controls.Add(txt_MatKhau);
            split_VachNgan.Panel2.Controls.Add(txt_TenDN);
            split_VachNgan.Panel2.Controls.Add(lbl_DangNhap);
            split_VachNgan.Size = new Size(946, 510);
            split_VachNgan.SplitterDistance = 478;
            split_VachNgan.TabIndex = 0;
            // 
            // img_DangNhap
            // 
            img_DangNhap.Dock = DockStyle.Fill;
            img_DangNhap.Image = (Image)resources.GetObject("img_DangNhap.Image");
            img_DangNhap.Location = new Point(0, 0);
            img_DangNhap.Name = "img_DangNhap";
            img_DangNhap.Size = new Size(478, 510);
            img_DangNhap.SizeMode = PictureBoxSizeMode.StretchImage;
            img_DangNhap.TabIndex = 0;
            img_DangNhap.TabStop = false;
            // 
            // lbl_MatKhau
            // 
            lbl_MatKhau.AutoSize = true;
            lbl_MatKhau.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_MatKhau.Location = new Point(75, 249);
            lbl_MatKhau.Name = "lbl_MatKhau";
            lbl_MatKhau.Size = new Size(94, 28);
            lbl_MatKhau.TabIndex = 5;
            lbl_MatKhau.Text = "Mật khẩu";
            // 
            // lbl_TenDN
            // 
            lbl_TenDN.AutoSize = true;
            lbl_TenDN.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lbl_TenDN.Location = new Point(75, 118);
            lbl_TenDN.Name = "lbl_TenDN";
            lbl_TenDN.Size = new Size(140, 28);
            lbl_TenDN.TabIndex = 4;
            lbl_TenDN.Text = "Tên đăng nhập";
            // 
            // btn_DangNhap
            // 
            btn_DangNhap.BackColor = Color.Black;
            btn_DangNhap.ForeColor = Color.White;
            btn_DangNhap.Location = new Point(276, 347);
            btn_DangNhap.Name = "btn_DangNhap";
            btn_DangNhap.Size = new Size(113, 44);
            btn_DangNhap.TabIndex = 3;
            btn_DangNhap.Text = "Đăng nhập";
            btn_DangNhap.UseVisualStyleBackColor = false;
            btn_DangNhap.Click += btn_DangNhap_Click;
            // 
            // txt_MatKhau
            // 
            txt_MatKhau.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_MatKhau.Location = new Point(75, 282);
            txt_MatKhau.Name = "txt_MatKhau";
            txt_MatKhau.PasswordChar = '*';
            txt_MatKhau.Size = new Size(314, 34);
            txt_MatKhau.TabIndex = 2;
            // 
            // txt_TenDN
            // 
            txt_TenDN.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_TenDN.Location = new Point(75, 158);
            txt_TenDN.Name = "txt_TenDN";
            txt_TenDN.Size = new Size(314, 34);
            txt_TenDN.TabIndex = 1;
            // 
            // lbl_DangNhap
            // 
            lbl_DangNhap.AutoSize = true;
            lbl_DangNhap.Font = new Font("Segoe UI Semibold", 22.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lbl_DangNhap.Location = new Point(131, 31);
            lbl_DangNhap.Name = "lbl_DangNhap";
            lbl_DangNhap.Size = new Size(213, 50);
            lbl_DangNhap.TabIndex = 0;
            lbl_DangNhap.Text = "Đăng Nhập";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Window;
            ClientSize = new Size(1422, 977);
            Controls.Add(panel1);
            Name = "Form1";
            Text = "Form1";
            panel1.ResumeLayout(false);
            split_VachNgan.Panel1.ResumeLayout(false);
            split_VachNgan.Panel2.ResumeLayout(false);
            split_VachNgan.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)split_VachNgan).EndInit();
            split_VachNgan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)img_DangNhap).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Panel panel1;
        private SplitContainer split_VachNgan;
        private PictureBox img_DangNhap;
        private Label lbl_DangNhap;
        private Label lbl_MatKhau;
        private Label lbl_TenDN;
        private Button btn_DangNhap;
        private TextBox txt_MatKhau;
        private TextBox txt_TenDN;
    }
}
