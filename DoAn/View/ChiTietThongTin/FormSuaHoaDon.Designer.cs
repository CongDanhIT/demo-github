namespace DoAn.View.ChiTietThongTin
{
    partial class FormSuaHoaDon
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
            this.btn_XacNhan = new System.Windows.Forms.Button();
            this.txt_IDSP = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_IDHD = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_SL = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_Gia = new System.Windows.Forms.TextBox();
            this.BangChiTietHD = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.BangChiTietHD)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_XacNhan
            // 
            this.btn_XacNhan.Location = new System.Drawing.Point(198, 402);
            this.btn_XacNhan.Name = "btn_XacNhan";
            this.btn_XacNhan.Size = new System.Drawing.Size(94, 24);
            this.btn_XacNhan.TabIndex = 0;
            this.btn_XacNhan.Text = "Xác Nhận";
            this.btn_XacNhan.UseVisualStyleBackColor = true;
            this.btn_XacNhan.Click += new System.EventHandler(this.btn_XacNhan_Click);
            // 
            // txt_IDSP
            // 
            this.txt_IDSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txt_IDSP.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IDSP.Location = new System.Drawing.Point(82, 158);
            this.txt_IDSP.Name = "txt_IDSP";
            this.txt_IDSP.ReadOnly = true;
            this.txt_IDSP.Size = new System.Drawing.Size(210, 30);
            this.txt_IDSP.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label1.Location = new System.Drawing.Point(78, 116);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID SẢN PHẨM";
            // 
            // txt_IDHD
            // 
            this.txt_IDHD.BackColor = System.Drawing.Color.Gray;
            this.txt_IDHD.Font = new System.Drawing.Font("Segoe UI Semibold", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_IDHD.Location = new System.Drawing.Point(490, 78);
            this.txt_IDHD.Name = "txt_IDHD";
            this.txt_IDHD.ReadOnly = true;
            this.txt_IDHD.Size = new System.Drawing.Size(258, 30);
            this.txt_IDHD.TabIndex = 3;
            this.txt_IDHD.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label2.Location = new System.Drawing.Point(535, 29);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(168, 31);
            this.label2.TabIndex = 4;
            this.label2.Text = "MÃ HÓA ĐƠN";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label3.Location = new System.Drawing.Point(78, 219);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "SỐ LƯỢNG";
            // 
            // txt_SL
            // 
            this.txt_SL.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txt_SL.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_SL.Location = new System.Drawing.Point(82, 261);
            this.txt_SL.Name = "txt_SL";
            this.txt_SL.Size = new System.Drawing.Size(210, 30);
            this.txt_SL.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label4.Location = new System.Drawing.Point(78, 308);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(39, 23);
            this.label4.TabIndex = 8;
            this.label4.Text = "GIÁ";
            // 
            // txt_Gia
            // 
            this.txt_Gia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txt_Gia.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Gia.Location = new System.Drawing.Point(82, 350);
            this.txt_Gia.Name = "txt_Gia";
            this.txt_Gia.Size = new System.Drawing.Size(210, 30);
            this.txt_Gia.TabIndex = 7;
            // 
            // BangChiTietHD
            // 
            this.BangChiTietHD.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BangChiTietHD.Location = new System.Drawing.Point(374, 140);
            this.BangChiTietHD.Name = "BangChiTietHD";
            this.BangChiTietHD.RowHeadersWidth = 51;
            this.BangChiTietHD.RowTemplate.Height = 24;
            this.BangChiTietHD.Size = new System.Drawing.Size(484, 262);
            this.BangChiTietHD.TabIndex = 9;
            this.BangChiTietHD.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.BangChiTietHD_CellContentClick);
            // 
            // FormSuaHoaDon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(924, 501);
            this.Controls.Add(this.BangChiTietHD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_Gia);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_SL);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_IDHD);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_IDSP);
            this.Controls.Add(this.btn_XacNhan);
            this.Name = "FormSuaHoaDon";
            this.Text = "FormSuaHoaDon";
            this.Load += new System.EventHandler(this.FormSuaHoaDon_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BangChiTietHD)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_XacNhan;
        private System.Windows.Forms.TextBox txt_IDSP;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txt_IDHD;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_SL;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_Gia;
        private System.Windows.Forms.DataGridView BangChiTietHD;
    }
}