namespace DoAn.View.ChuQuan
{
    partial class ThemKho
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
            this.label1 = new System.Windows.Forms.Label();
            this.text_TenKho = new System.Windows.Forms.TextBox();
            this.bnt_XacNhan = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 19.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(173, 62);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(430, 45);
            this.label1.TabIndex = 0;
            this.label1.Text = "VUI LÒNG NHẬP TÊN KHO";
            // 
            // text_TenKho
            // 
            this.text_TenKho.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_TenKho.Location = new System.Drawing.Point(141, 160);
            this.text_TenKho.Name = "text_TenKho";
            this.text_TenKho.Size = new System.Drawing.Size(522, 34);
            this.text_TenKho.TabIndex = 1;
            // 
            // bnt_XacNhan
            // 
            this.bnt_XacNhan.Location = new System.Drawing.Point(331, 253);
            this.bnt_XacNhan.Name = "bnt_XacNhan";
            this.bnt_XacNhan.Size = new System.Drawing.Size(133, 51);
            this.bnt_XacNhan.TabIndex = 2;
            this.bnt_XacNhan.Text = "Xác Nhận";
            this.bnt_XacNhan.UseVisualStyleBackColor = true;
            this.bnt_XacNhan.Click += new System.EventHandler(this.bnt_XacNhan_Click);
            // 
            // ThemKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 369);
            this.Controls.Add(this.bnt_XacNhan);
            this.Controls.Add(this.text_TenKho);
            this.Controls.Add(this.label1);
            this.Name = "ThemKho";
            this.Text = "ThemKho";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox text_TenKho;
        private System.Windows.Forms.Button bnt_XacNhan;
    }
}