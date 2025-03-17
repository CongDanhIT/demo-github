namespace QLKD_CHTT.View.Nhân_Viên
{
    partial class _2_GiaoDienNV
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            split_vachNgan = new SplitContainer();
            panel2 = new Panel();
            label2 = new Label();
            label1 = new Label();
            txt_TongTienHDs = new TextBox();
            txt_SLHoaDon = new TextBox();
            panel1 = new Panel();
            pic_TK = new PictureBox();
            tab_NV = new TabControl();
            tabPage1 = new TabPage();
            tabPage2 = new TabPage();
            ((System.ComponentModel.ISupportInitialize)split_vachNgan).BeginInit();
            split_vachNgan.Panel1.SuspendLayout();
            split_vachNgan.Panel2.SuspendLayout();
            split_vachNgan.SuspendLayout();
            panel2.SuspendLayout();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pic_TK).BeginInit();
            tab_NV.SuspendLayout();
            SuspendLayout();
            // 
            // split_vachNgan
            // 
            split_vachNgan.Dock = DockStyle.Fill;
            split_vachNgan.Location = new Point(0, 0);
            split_vachNgan.Name = "split_vachNgan";
            // 
            // split_vachNgan.Panel1
            // 
            split_vachNgan.Panel1.BackColor = Color.FromArgb(181, 187, 212);
            split_vachNgan.Panel1.Controls.Add(panel2);
            split_vachNgan.Panel1.Controls.Add(panel1);
            // 
            // split_vachNgan.Panel2
            // 
            split_vachNgan.Panel2.Controls.Add(tab_NV);
            split_vachNgan.Size = new Size(1422, 977);
            split_vachNgan.SplitterDistance = 384;
            split_vachNgan.TabIndex = 0;
            // 
            // panel2
            // 
            panel2.BorderStyle = BorderStyle.Fixed3D;
            panel2.Controls.Add(label2);
            panel2.Controls.Add(label1);
            panel2.Controls.Add(txt_TongTienHDs);
            panel2.Controls.Add(txt_SLHoaDon);
            panel2.Dock = DockStyle.Fill;
            panel2.Location = new Point(0, 125);
            panel2.Name = "panel2";
            panel2.Size = new Size(384, 852);
            panel2.TabIndex = 3;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label2.Location = new Point(138, 188);
            label2.Name = "label2";
            label2.Size = new Size(99, 25);
            label2.TabIndex = 5;
            label2.Text = "Tổng tiền ";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.Location = new Point(86, 40);
            label1.Name = "label1";
            label1.Size = new Size(207, 25);
            label1.TabIndex = 4;
            label1.Text = "Số hóa đơn trong ngày";
            // 
            // txt_TongTienHDs
            // 
            txt_TongTienHDs.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_TongTienHDs.Location = new Point(45, 228);
            txt_TongTienHDs.Name = "txt_TongTienHDs";
            txt_TongTienHDs.ReadOnly = true;
            txt_TongTienHDs.Size = new Size(284, 34);
            txt_TongTienHDs.TabIndex = 3;
            // 
            // txt_SLHoaDon
            // 
            txt_SLHoaDon.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txt_SLHoaDon.Location = new Point(45, 81);
            txt_SLHoaDon.Name = "txt_SLHoaDon";
            txt_SLHoaDon.ReadOnly = true;
            txt_SLHoaDon.Size = new Size(284, 34);
            txt_SLHoaDon.TabIndex = 2;
            txt_SLHoaDon.TextChanged += textBox2_TextChanged;
            // 
            // panel1
            // 
            panel1.BorderStyle = BorderStyle.FixedSingle;
            panel1.Controls.Add(pic_TK);
            panel1.Dock = DockStyle.Top;
            panel1.Location = new Point(0, 0);
            panel1.Name = "panel1";
            panel1.Size = new Size(384, 125);
            panel1.TabIndex = 0;
            // 
            // pic_TK
            // 
            pic_TK.Image = Properties.Resources.user;
            pic_TK.Location = new Point(140, 23);
            pic_TK.Name = "pic_TK";
            pic_TK.Size = new Size(104, 81);
            pic_TK.SizeMode = PictureBoxSizeMode.StretchImage;
            pic_TK.TabIndex = 0;
            pic_TK.TabStop = false;
            // 
            // tab_NV
            // 
            tab_NV.Controls.Add(tabPage1);
            tab_NV.Controls.Add(tabPage2);
            tab_NV.Dock = DockStyle.Fill;
            tab_NV.Location = new Point(0, 0);
            tab_NV.Name = "tab_NV";
            tab_NV.SelectedIndex = 0;
            tab_NV.Size = new Size(1034, 977);
            tab_NV.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Location = new Point(4, 29);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(1026, 944);
            tabPage1.TabIndex = 0;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            tabPage2.Location = new Point(4, 29);
            tabPage2.Name = "tabPage2";
            tabPage2.Padding = new Padding(3);
            tabPage2.Size = new Size(242, 92);
            tabPage2.TabIndex = 1;
            tabPage2.Text = "tabPage2";
            tabPage2.UseVisualStyleBackColor = true;
            // 
            // _2_GiaoDienNV
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1422, 977);
            Controls.Add(split_vachNgan);
            Name = "_2_GiaoDienNV";
            Text = "_2_GiaoDienNV";
            split_vachNgan.Panel1.ResumeLayout(false);
            split_vachNgan.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)split_vachNgan).EndInit();
            split_vachNgan.ResumeLayout(false);
            panel2.ResumeLayout(false);
            panel2.PerformLayout();
            panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)pic_TK).EndInit();
            tab_NV.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer split_vachNgan;
        private Panel panel1;
        private PictureBox pic_TK;
        private Panel panel2;
        private TextBox txt_TongTienHDs;
        private TextBox txt_SLHoaDon;
        private Label label1;
        private Label label2;
        private TabControl tab_NV;
        private TabPage tabPage1;
        private TabPage tabPage2;
    }
}