namespace DoAn.View.ChiTietThongTin
{
    partial class FormDuaSPLenCH
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
            this.BangSPCho = new System.Windows.Forms.DataGridView();
            this.txt_Kho = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_SP = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_IDSP = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.BangSPCho)).BeginInit();
            this.SuspendLayout();
            // 
            // BangSPCho
            // 
            this.BangSPCho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.BangSPCho.Location = new System.Drawing.Point(384, 67);
            this.BangSPCho.Name = "BangSPCho";
            this.BangSPCho.RowHeadersWidth = 51;
            this.BangSPCho.RowTemplate.Height = 24;
            this.BangSPCho.Size = new System.Drawing.Size(525, 296);
            this.BangSPCho.TabIndex = 0;
            // 
            // txt_Kho
            // 
            this.txt_Kho.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txt_Kho.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txt_Kho.Location = new System.Drawing.Point(85, 95);
            this.txt_Kho.Name = "txt_Kho";
            this.txt_Kho.ReadOnly = true;
            this.txt_Kho.Size = new System.Drawing.Size(188, 22);
            this.txt_Kho.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(82, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 23);
            this.label1.TabIndex = 2;
            this.label1.Text = "ID_Kho";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(83, 138);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 23);
            this.label2.TabIndex = 4;
            this.label2.Text = "Tên Sản Phẩm";
            // 
            // txt_SP
            // 
            this.txt_SP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txt_SP.Location = new System.Drawing.Point(86, 173);
            this.txt_SP.Name = "txt_SP";
            this.txt_SP.ReadOnly = true;
            this.txt_SP.Size = new System.Drawing.Size(188, 22);
            this.txt_SP.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(12, 299);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(348, 23);
            this.label3.TabIndex = 6;
            this.label3.Text = "Vui Lòng Nhập ID Sản Phẩm Muốn kết nối";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // txt_IDSP
            // 
            this.txt_IDSP.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.txt_IDSP.Location = new System.Drawing.Point(85, 341);
            this.txt_IDSP.Name = "txt_IDSP";
            this.txt_IDSP.Size = new System.Drawing.Size(188, 22);
            this.txt_IDSP.TabIndex = 5;
            this.txt_IDSP.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Segoe UI", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(114, 387);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 29);
            this.button1.TabIndex = 7;
            this.button1.Text = "Kết nối";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // FormDuaSPLenCH
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.ClientSize = new System.Drawing.Size(958, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txt_IDSP);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txt_SP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_Kho);
            this.Controls.Add(this.BangSPCho);
            this.Name = "FormDuaSPLenCH";
            this.Text = "FormDuaSPLenCH";
            this.Load += new System.EventHandler(this.FormDuaSPLenCH_Load);
            ((System.ComponentModel.ISupportInitialize)(this.BangSPCho)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView BangSPCho;
        private System.Windows.Forms.TextBox txt_Kho;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_SP;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_IDSP;
        private System.Windows.Forms.Button button1;
    }
}