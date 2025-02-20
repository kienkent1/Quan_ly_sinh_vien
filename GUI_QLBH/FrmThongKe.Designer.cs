
namespace GUI_QLBH
{
    partial class FrmThongKe
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
            this.tcThongKe = new System.Windows.Forms.TabControl();
            this.tpsanpham = new System.Windows.Forms.TabPage();
            this.dgvsp = new System.Windows.Forms.DataGridView();
            this.tptonkho = new System.Windows.Forms.TabPage();
            this.dgvtonkho = new System.Windows.Forms.DataGridView();
            this.lichsu = new System.Windows.Forms.TabPage();
            this.button1 = new System.Windows.Forms.Button();
            this.dgvlichsu = new System.Windows.Forms.DataGridView();
            this.tcThongKe.SuspendLayout();
            this.tpsanpham.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvsp)).BeginInit();
            this.tptonkho.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvtonkho)).BeginInit();
            this.lichsu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvlichsu)).BeginInit();
            this.SuspendLayout();
            // 
            // tcThongKe
            // 
            this.tcThongKe.Controls.Add(this.tpsanpham);
            this.tcThongKe.Controls.Add(this.tptonkho);
            this.tcThongKe.Controls.Add(this.lichsu);
            this.tcThongKe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcThongKe.Location = new System.Drawing.Point(0, 0);
            this.tcThongKe.Margin = new System.Windows.Forms.Padding(4);
            this.tcThongKe.Name = "tcThongKe";
            this.tcThongKe.SelectedIndex = 0;
            this.tcThongKe.Size = new System.Drawing.Size(741, 393);
            this.tcThongKe.TabIndex = 1;
            this.tcThongKe.Selected += new System.Windows.Forms.TabControlEventHandler(this.tcThongKe_Selected);
            // 
            // tpsanpham
            // 
            this.tpsanpham.CausesValidation = false;
            this.tpsanpham.Controls.Add(this.dgvsp);
            this.tpsanpham.Location = new System.Drawing.Point(4, 25);
            this.tpsanpham.Margin = new System.Windows.Forms.Padding(4);
            this.tpsanpham.Name = "tpsanpham";
            this.tpsanpham.Padding = new System.Windows.Forms.Padding(4);
            this.tpsanpham.Size = new System.Drawing.Size(733, 364);
            this.tpsanpham.TabIndex = 0;
            this.tpsanpham.Text = "Sản Phẩm Nhập Kho";
            this.tpsanpham.UseVisualStyleBackColor = true;
            // 
            // dgvsp
            // 
            this.dgvsp.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvsp.Location = new System.Drawing.Point(0, 0);
            this.dgvsp.Margin = new System.Windows.Forms.Padding(4);
            this.dgvsp.Name = "dgvsp";
            this.dgvsp.RowHeadersWidth = 51;
            this.dgvsp.Size = new System.Drawing.Size(732, 364);
            this.dgvsp.TabIndex = 0;
            // 
            // tptonkho
            // 
            this.tptonkho.CausesValidation = false;
            this.tptonkho.Controls.Add(this.dgvtonkho);
            this.tptonkho.Location = new System.Drawing.Point(4, 25);
            this.tptonkho.Margin = new System.Windows.Forms.Padding(4);
            this.tptonkho.Name = "tptonkho";
            this.tptonkho.Padding = new System.Windows.Forms.Padding(4);
            this.tptonkho.Size = new System.Drawing.Size(733, 364);
            this.tptonkho.TabIndex = 1;
            this.tptonkho.Text = "Tồn Kho";
            this.tptonkho.UseVisualStyleBackColor = true;
            // 
            // dgvtonkho
            // 
            this.dgvtonkho.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvtonkho.Location = new System.Drawing.Point(-4, 0);
            this.dgvtonkho.Margin = new System.Windows.Forms.Padding(4);
            this.dgvtonkho.Name = "dgvtonkho";
            this.dgvtonkho.RowHeadersWidth = 51;
            this.dgvtonkho.Size = new System.Drawing.Size(737, 364);
            this.dgvtonkho.TabIndex = 0;
            // 
            // lichsu
            // 
            this.lichsu.Controls.Add(this.button1);
            this.lichsu.Controls.Add(this.dgvlichsu);
            this.lichsu.Location = new System.Drawing.Point(4, 25);
            this.lichsu.Name = "lichsu";
            this.lichsu.Size = new System.Drawing.Size(733, 364);
            this.lichsu.TabIndex = 2;
            this.lichsu.Text = "Lịch sử";
            this.lichsu.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::GUI_QLBH.Properties.Resources.contract__2_;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(603, 315);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 41);
            this.button1.TabIndex = 2;
            this.button1.Text = "&Hiển thị";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dgvlichsu
            // 
            this.dgvlichsu.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvlichsu.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvlichsu.Location = new System.Drawing.Point(-4, 0);
            this.dgvlichsu.Name = "dgvlichsu";
            this.dgvlichsu.RowHeadersWidth = 51;
            this.dgvlichsu.RowTemplate.Height = 24;
            this.dgvlichsu.Size = new System.Drawing.Size(737, 309);
            this.dgvlichsu.TabIndex = 0;
            this.dgvlichsu.Click += new System.EventHandler(this.dgvlichsu_Click);
            // 
            // FrmThongKe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 393);
            this.Controls.Add(this.tcThongKe);
            this.Name = "FrmThongKe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FrmThongKe";
            this.tcThongKe.ResumeLayout(false);
            this.tpsanpham.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvsp)).EndInit();
            this.tptonkho.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvtonkho)).EndInit();
            this.lichsu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvlichsu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tcThongKe;
        private System.Windows.Forms.TabPage tpsanpham;
        private System.Windows.Forms.DataGridView dgvsp;
        private System.Windows.Forms.TabPage tptonkho;
        private System.Windows.Forms.DataGridView dgvtonkho;
        private System.Windows.Forms.TabPage lichsu;
        private System.Windows.Forms.DataGridView dgvlichsu;
        private System.Windows.Forms.Button button1;
    }
}