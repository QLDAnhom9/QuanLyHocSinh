namespace G10_BTL.GUI
{
    partial class fQuanlyDiem
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
            this.dgvData = new System.Windows.Forms.DataGridView();
            this.btnClear = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cbbHocSinh = new System.Windows.Forms.ComboBox();
            this.cbbHocKy = new System.Windows.Forms.ComboBox();
            this.cbbLop = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.cbbMonHoc = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.btnXoa = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.txtDiemCuoiKy = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtDiemGiuaKy = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.btnCapNhat = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.MaHocSinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenHocSinh = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.LopHoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaMonHoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenMonHoc = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DIemGiuaKy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.DiemCuoiKy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.HocKy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvData
            // 
            this.dgvData.AllowUserToAddRows = false;
            this.dgvData.AllowUserToDeleteRows = false;
            this.dgvData.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvData.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaHocSinh,
            this.TenHocSinh,
            this.LopHoc,
            this.MaMonHoc,
            this.TenMonHoc,
            this.DIemGiuaKy,
            this.DiemCuoiKy,
            this.HocKy});
            this.dgvData.Location = new System.Drawing.Point(9, 252);
            this.dgvData.Name = "dgvData";
            this.dgvData.ReadOnly = true;
            this.dgvData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvData.Size = new System.Drawing.Size(883, 285);
            this.dgvData.TabIndex = 14;
            this.dgvData.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvData_CellClick);
            this.dgvData.SelectionChanged += new System.EventHandler(this.dgvData_SelectionChanged);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnClear.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClear.Image = global::G10_BTL.Properties.Resources.back;
            this.btnClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnClear.Location = new System.Drawing.Point(713, 203);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(63, 38);
            this.btnClear.TabIndex = 53;
            this.btnClear.Text = "Clear";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(337, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(221, 24);
            this.label5.TabIndex = 52;
            this.label5.Text = "Quản lý điểm học sinh";
            this.label5.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cbbHocSinh);
            this.groupBox1.Controls.Add(this.cbbHocKy);
            this.groupBox1.Controls.Add(this.cbbLop);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.cbbMonHoc);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(123, 45);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(653, 95);
            this.groupBox1.TabIndex = 47;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Thông tin cần thiết";
            // 
            // cbbHocSinh
            // 
            this.cbbHocSinh.FormattingEnabled = true;
            this.cbbHocSinh.Location = new System.Drawing.Point(107, 62);
            this.cbbHocSinh.Name = "cbbHocSinh";
            this.cbbHocSinh.Size = new System.Drawing.Size(186, 24);
            this.cbbHocSinh.TabIndex = 13;
            this.cbbHocSinh.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // cbbHocKy
            // 
            this.cbbHocKy.FormattingEnabled = true;
            this.cbbHocKy.Items.AddRange(new object[] {
            "Toán",
            "Tiếng Việt",
            "Tiếng Anh",
            "Thể Dục",
            "Khoa Học Tự Nhiên",
            "Đạo Đức",
            "Mỹ Thuật",
            ""});
            this.cbbHocKy.Location = new System.Drawing.Point(425, 29);
            this.cbbHocKy.Name = "cbbHocKy";
            this.cbbHocKy.Size = new System.Drawing.Size(187, 24);
            this.cbbHocKy.TabIndex = 12;
            this.cbbHocKy.SelectedIndexChanged += new System.EventHandler(this.cbbHocKy_SelectedIndexChanged);
            // 
            // cbbLop
            // 
            this.cbbLop.FormattingEnabled = true;
            this.cbbLop.Items.AddRange(new object[] {
            "Toán",
            "Tiếng Việt",
            "Tiếng Anh",
            "Thể Dục",
            "Khoa Học Tự Nhiên",
            "Đạo Đức",
            "Mỹ Thuật",
            ""});
            this.cbbLop.Location = new System.Drawing.Point(107, 28);
            this.cbbLop.Name = "cbbLop";
            this.cbbLop.Size = new System.Drawing.Size(187, 24);
            this.cbbLop.TabIndex = 11;
            this.cbbLop.SelectedIndexChanged += new System.EventHandler(this.cbbLop_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 26);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(34, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Lớp:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(357, 31);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Học kỳ:";
            // 
            // cbbMonHoc
            // 
            this.cbbMonHoc.FormattingEnabled = true;
            this.cbbMonHoc.Items.AddRange(new object[] {
            "Toán",
            "Tiếng Việt",
            "Tiếng Anh",
            "Thể Dục",
            "Khoa Học Tự Nhiên",
            "Đạo Đức",
            "Mỹ Thuật",
            ""});
            this.cbbMonHoc.Location = new System.Drawing.Point(425, 59);
            this.cbbMonHoc.Name = "cbbMonHoc";
            this.cbbMonHoc.Size = new System.Drawing.Size(186, 24);
            this.cbbMonHoc.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(357, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(62, 16);
            this.label4.TabIndex = 3;
            this.label4.Text = "Môn học:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(87, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Tên học sinh:";
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnXoa.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.Image = global::G10_BTL.Properties.Resources.login;
            this.btnXoa.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnXoa.Location = new System.Drawing.Point(503, 205);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(120, 35);
            this.btnXoa.TabIndex = 51;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.txtDiemCuoiKy);
            this.groupBox2.Controls.Add(this.label10);
            this.groupBox2.Controls.Add(this.txtDiemGiuaKy);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(123, 146);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(653, 53);
            this.groupBox2.TabIndex = 49;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Điểm ";
            // 
            // txtDiemCuoiKy
            // 
            this.txtDiemCuoiKy.Location = new System.Drawing.Point(442, 19);
            this.txtDiemCuoiKy.Name = "txtDiemCuoiKy";
            this.txtDiemCuoiKy.Size = new System.Drawing.Size(151, 22);
            this.txtDiemCuoiKy.TabIndex = 10;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(348, 22);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(87, 16);
            this.label10.TabIndex = 9;
            this.label10.Text = "Điểm cuối kỳ:";
            // 
            // txtDiemGiuaKy
            // 
            this.txtDiemGiuaKy.Location = new System.Drawing.Point(117, 20);
            this.txtDiemGiuaKy.Name = "txtDiemGiuaKy";
            this.txtDiemGiuaKy.Size = new System.Drawing.Size(154, 22);
            this.txtDiemGiuaKy.TabIndex = 8;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(10, 26);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(88, 16);
            this.label9.TabIndex = 1;
            this.label9.Text = "Điểm giữa kỳ:";
            // 
            // btnCapNhat
            // 
            this.btnCapNhat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnCapNhat.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCapNhat.Image = global::G10_BTL.Properties.Resources.student2;
            this.btnCapNhat.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCapNhat.Location = new System.Drawing.Point(311, 205);
            this.btnCapNhat.Name = "btnCapNhat";
            this.btnCapNhat.Size = new System.Drawing.Size(120, 35);
            this.btnCapNhat.TabIndex = 50;
            this.btnCapNhat.Text = "Cập nhật";
            this.btnCapNhat.UseVisualStyleBackColor = false;
            this.btnCapNhat.Click += new System.EventHandler(this.btnCapNhat_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnThem.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.Image = global::G10_BTL.Properties.Resources.confirm;
            this.btnThem.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThem.Location = new System.Drawing.Point(125, 205);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(120, 35);
            this.btnThem.TabIndex = 48;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // MaHocSinh
            // 
            this.MaHocSinh.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MaHocSinh.FillWeight = 65F;
            this.MaHocSinh.HeaderText = "Mã học sinh";
            this.MaHocSinh.Name = "MaHocSinh";
            this.MaHocSinh.ReadOnly = true;
            this.MaHocSinh.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // TenHocSinh
            // 
            this.TenHocSinh.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TenHocSinh.HeaderText = "Tên học sinh";
            this.TenHocSinh.Name = "TenHocSinh";
            this.TenHocSinh.ReadOnly = true;
            this.TenHocSinh.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // LopHoc
            // 
            this.LopHoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.LopHoc.FillWeight = 60F;
            this.LopHoc.HeaderText = "Lớp";
            this.LopHoc.Name = "LopHoc";
            this.LopHoc.ReadOnly = true;
            this.LopHoc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // MaMonHoc
            // 
            this.MaMonHoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.MaMonHoc.FillWeight = 80F;
            this.MaMonHoc.HeaderText = "Mã môn học";
            this.MaMonHoc.Name = "MaMonHoc";
            this.MaMonHoc.ReadOnly = true;
            this.MaMonHoc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            this.MaMonHoc.Visible = false;
            // 
            // TenMonHoc
            // 
            this.TenMonHoc.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.TenMonHoc.HeaderText = "Tên môn học";
            this.TenMonHoc.Name = "TenMonHoc";
            this.TenMonHoc.ReadOnly = true;
            this.TenMonHoc.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DIemGiuaKy
            // 
            this.DIemGiuaKy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DIemGiuaKy.FillWeight = 65F;
            this.DIemGiuaKy.HeaderText = "Điểm giữa kỳ";
            this.DIemGiuaKy.Name = "DIemGiuaKy";
            this.DIemGiuaKy.ReadOnly = true;
            this.DIemGiuaKy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // DiemCuoiKy
            // 
            this.DiemCuoiKy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.DiemCuoiKy.FillWeight = 65F;
            this.DiemCuoiKy.HeaderText = "Điểm cuối kỳ";
            this.DiemCuoiKy.Name = "DiemCuoiKy";
            this.DiemCuoiKy.ReadOnly = true;
            this.DiemCuoiKy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // HocKy
            // 
            this.HocKy.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.HocKy.FillWeight = 60F;
            this.HocKy.HeaderText = "Học kỳ";
            this.HocKy.Name = "HocKy";
            this.HocKy.ReadOnly = true;
            this.HocKy.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
            // 
            // fQuanlyDiem
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(898, 545);
            this.ControlBox = false;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnCapNhat);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.dgvData);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fQuanlyDiem";
            this.Text = "QuanlyDiem";
            this.Load += new System.EventHandler(this.QuanlyDiem_Load);
            this.DoubleClick += new System.EventHandler(this.fQuanlyDiem_DoubleClick);
            ((System.ComponentModel.ISupportInitialize)(this.dgvData)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvData;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cbbHocSinh;
        private System.Windows.Forms.ComboBox cbbHocKy;
        private System.Windows.Forms.ComboBox cbbLop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cbbMonHoc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtDiemCuoiKy;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtDiemGiuaKy;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btnCapNhat;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaHocSinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenHocSinh;
        private System.Windows.Forms.DataGridViewTextBoxColumn LopHoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaMonHoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenMonHoc;
        private System.Windows.Forms.DataGridViewTextBoxColumn DIemGiuaKy;
        private System.Windows.Forms.DataGridViewTextBoxColumn DiemCuoiKy;
        private System.Windows.Forms.DataGridViewTextBoxColumn HocKy;
    }
}