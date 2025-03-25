namespace DoAn.View.ChiTietThongTin
{
    partial class ChiTietPhieuNhap
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
            this.BangChiTietPN = new System.Windows.Forms.DataGridView();
            this.txt_MaPhieu = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.BangChiTietPN)).BeginInit();
            this.SuspendLayout();
            // 
            // BangChiTietPN
            // 
            this.BangChiTietPN.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BangChiTietPN.Location = new System.Drawing.Point(66, 134);
            this.BangChiTietPN.Name = "BangChiTietPN";
            this.BangChiTietPN.RowHeadersWidth = 51;
            this.BangChiTietPN.RowTemplate.Height = 24;
            this.BangChiTietPN.Size = new System.Drawing.Size(624, 359);
            this.BangChiTietPN.TabIndex = 0;
            // 
            // txt_MaPhieu
            // 
            this.txt_MaPhieu.Font = new System.Drawing.Font("Segoe UI", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_MaPhieu.Location = new System.Drawing.Point(179, 65);
            this.txt_MaPhieu.Name = "txt_MaPhieu";
            this.txt_MaPhieu.Size = new System.Drawing.Size(383, 38);
            this.txt_MaPhieu.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.label1.Location = new System.Drawing.Point(316, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(109, 28);
            this.label1.TabIndex = 2;
            this.label1.Text = "MÃ PHIẾU";
            // 
            // ChiTietPhieuNhap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(741, 581);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_MaPhieu);
            this.Controls.Add(this.BangChiTietPN);
            this.Name = "ChiTietPhieuNhap";
            this.Text = "ChiTietPhieuNhap";
            this.Load += new System.EventHandler(this.ChiTietPhieuNhap_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BangChiTietPN)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView BangChiTietPN;
        private System.Windows.Forms.TextBox txt_MaPhieu;
        private System.Windows.Forms.Label label1;
    }
}