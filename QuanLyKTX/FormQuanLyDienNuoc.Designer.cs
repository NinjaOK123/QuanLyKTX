namespace QuanLyKTX
{
    partial class FormQuanLyDienNuoc
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
            this.btnTim = new System.Windows.Forms.Button();
            this.txtMaPhongTim = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rdThanhToan = new System.Windows.Forms.RadioButton();
            this.rdChuaThanhToan = new System.Windows.Forms.RadioButton();
            this.cboTenPhong = new System.Windows.Forms.ComboBox();
            this.btnTinhTongTien = new System.Windows.Forms.Button();
            this.txtTongTien = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.txtChiSoNuocMoi = new System.Windows.Forms.TextBox();
            this.txtChiSoDienCu = new System.Windows.Forms.TextBox();
            this.dtpThang = new System.Windows.Forms.DateTimePicker();
            this.txtChiSoDienMoi = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.dgvHoaDon = new System.Windows.Forms.DataGridView();
            this.MaHD = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TenPhong = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Thang = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChiSoDienCu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChiSoDienMoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChiSoNuocCu = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ChiSoNuocMoi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TongTien = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TrangThai = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label3 = new System.Windows.Forms.Label();
            this.btnThoat = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.txtChiSoNuocCu = new System.Windows.Forms.TextBox();
            this.btnHuy = new System.Windows.Forms.Button();
            this.btnLuu = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.txtMaHD = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).BeginInit();
            this.SuspendLayout();
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(788, 224);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(133, 40);
            this.btnTim.TabIndex = 89;
            this.btnTim.Text = "Tìm";
            this.btnTim.UseVisualStyleBackColor = true;
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // txtMaPhongTim
            // 
            this.txtMaPhongTim.Location = new System.Drawing.Point(581, 238);
            this.txtMaPhongTim.Name = "txtMaPhongTim";
            this.txtMaPhongTim.Size = new System.Drawing.Size(157, 22);
            this.txtMaPhongTim.TabIndex = 88;
            this.txtMaPhongTim.TextChanged += new System.EventHandler(this.txtMaHDT_TextChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label11.Location = new System.Drawing.Point(370, 238);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(179, 18);
            this.label11.TabIndex = 87;
            this.label11.Text = "Nhập Mã Phòng Cần Tìm:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rdThanhToan);
            this.groupBox1.Controls.Add(this.rdChuaThanhToan);
            this.groupBox1.Location = new System.Drawing.Point(530, 92);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(208, 75);
            this.groupBox1.TabIndex = 86;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Trạng Thái";
            // 
            // rdThanhToan
            // 
            this.rdThanhToan.AutoSize = true;
            this.rdThanhToan.Location = new System.Drawing.Point(6, 21);
            this.rdThanhToan.Name = "rdThanhToan";
            this.rdThanhToan.Size = new System.Drawing.Size(121, 20);
            this.rdThanhToan.TabIndex = 54;
            this.rdThanhToan.TabStop = true;
            this.rdThanhToan.Text = "Đã Thanh Toán\r\n";
            this.rdThanhToan.UseVisualStyleBackColor = true;
            // 
            // rdChuaThanhToan
            // 
            this.rdChuaThanhToan.AutoSize = true;
            this.rdChuaThanhToan.Location = new System.Drawing.Point(6, 47);
            this.rdChuaThanhToan.Name = "rdChuaThanhToan";
            this.rdChuaThanhToan.Size = new System.Drawing.Size(135, 20);
            this.rdChuaThanhToan.TabIndex = 55;
            this.rdChuaThanhToan.TabStop = true;
            this.rdChuaThanhToan.Text = "Chưa Thanh Toán";
            this.rdChuaThanhToan.UseVisualStyleBackColor = true;
            // 
            // cboTenPhong
            // 
            this.cboTenPhong.FormattingEnabled = true;
            this.cboTenPhong.Location = new System.Drawing.Point(373, 47);
            this.cboTenPhong.Name = "cboTenPhong";
            this.cboTenPhong.Size = new System.Drawing.Size(121, 24);
            this.cboTenPhong.TabIndex = 85;
            // 
            // btnTinhTongTien
            // 
            this.btnTinhTongTien.Location = new System.Drawing.Point(937, 225);
            this.btnTinhTongTien.Name = "btnTinhTongTien";
            this.btnTinhTongTien.Size = new System.Drawing.Size(133, 40);
            this.btnTinhTongTien.TabIndex = 84;
            this.btnTinhTongTien.Text = "Tính Tiền";
            this.btnTinhTongTien.UseVisualStyleBackColor = true;
            this.btnTinhTongTien.Click += new System.EventHandler(this.btnTinhTongTien_Click);
            // 
            // txtTongTien
            // 
            this.txtTongTien.Location = new System.Drawing.Point(606, 186);
            this.txtTongTien.Name = "txtTongTien";
            this.txtTongTien.Size = new System.Drawing.Size(157, 22);
            this.txtTongTien.TabIndex = 83;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label10.Location = new System.Drawing.Point(527, 186);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(73, 18);
            this.label10.TabIndex = 82;
            this.label10.Text = "Tổng tiền:";
            // 
            // txtChiSoNuocMoi
            // 
            this.txtChiSoNuocMoi.Location = new System.Drawing.Point(174, 186);
            this.txtChiSoNuocMoi.Name = "txtChiSoNuocMoi";
            this.txtChiSoNuocMoi.Size = new System.Drawing.Size(73, 22);
            this.txtChiSoNuocMoi.TabIndex = 81;
            // 
            // txtChiSoDienCu
            // 
            this.txtChiSoDienCu.Location = new System.Drawing.Point(398, 140);
            this.txtChiSoDienCu.Name = "txtChiSoDienCu";
            this.txtChiSoDienCu.Size = new System.Drawing.Size(73, 22);
            this.txtChiSoDienCu.TabIndex = 75;
            // 
            // dtpThang
            // 
            this.dtpThang.CustomFormat = "DD/MM/YYYY";
            this.dtpThang.Location = new System.Drawing.Point(103, 92);
            this.dtpThang.Name = "dtpThang";
            this.dtpThang.Size = new System.Drawing.Size(263, 22);
            this.dtpThang.TabIndex = 73;
            // 
            // txtChiSoDienMoi
            // 
            this.txtChiSoDienMoi.Location = new System.Drawing.Point(398, 186);
            this.txtChiSoDienMoi.Name = "txtChiSoDienMoi";
            this.txtChiSoDienMoi.Size = new System.Drawing.Size(73, 22);
            this.txtChiSoDienMoi.TabIndex = 77;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label8.Location = new System.Drawing.Point(44, 93);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 18);
            this.label8.TabIndex = 72;
            this.label8.Text = "Tháng:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label7.Location = new System.Drawing.Point(45, 141);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(112, 18);
            this.label7.TabIndex = 78;
            this.label7.Text = "Chỉ số nước cũ:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label6.Location = new System.Drawing.Point(12, 256);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(141, 18);
            this.label6.TabIndex = 71;
            this.label6.Text = "Danh sách hóa đơn:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label5.Location = new System.Drawing.Point(265, 188);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(115, 18);
            this.label5.TabIndex = 76;
            this.label5.Text = "Chỉ số điện mới:";
            // 
            // dgvHoaDon
            // 
            this.dgvHoaDon.AllowUserToAddRows = false;
            this.dgvHoaDon.AllowUserToDeleteRows = false;
            this.dgvHoaDon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvHoaDon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvHoaDon.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.MaHD,
            this.TenPhong,
            this.Thang,
            this.ChiSoDienCu,
            this.ChiSoDienMoi,
            this.ChiSoNuocCu,
            this.ChiSoNuocMoi,
            this.TongTien,
            this.TrangThai});
            this.dgvHoaDon.Location = new System.Drawing.Point(15, 286);
            this.dgvHoaDon.MultiSelect = false;
            this.dgvHoaDon.Name = "dgvHoaDon";
            this.dgvHoaDon.RowHeadersWidth = 51;
            this.dgvHoaDon.RowTemplate.Height = 24;
            this.dgvHoaDon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvHoaDon.Size = new System.Drawing.Size(1078, 260);
            this.dgvHoaDon.TabIndex = 70;
            this.dgvHoaDon.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvHoaDon_CellFormatting);
            // 
            // MaHD
            // 
            this.MaHD.DataPropertyName = "MaHD";
            this.MaHD.HeaderText = "Mã Hóa Đơn";
            this.MaHD.MinimumWidth = 6;
            this.MaHD.Name = "MaHD";
            // 
            // TenPhong
            // 
            this.TenPhong.DataPropertyName = "TenPhong";
            this.TenPhong.HeaderText = "Tên Phòng";
            this.TenPhong.MinimumWidth = 6;
            this.TenPhong.Name = "TenPhong";
            // 
            // Thang
            // 
            this.Thang.DataPropertyName = "Thang";
            this.Thang.HeaderText = "Tháng";
            this.Thang.MinimumWidth = 6;
            this.Thang.Name = "Thang";
            // 
            // ChiSoDienCu
            // 
            this.ChiSoDienCu.DataPropertyName = "ChiSoDienCu";
            this.ChiSoDienCu.HeaderText = "Chỉ số điện cũ";
            this.ChiSoDienCu.MinimumWidth = 6;
            this.ChiSoDienCu.Name = "ChiSoDienCu";
            // 
            // ChiSoDienMoi
            // 
            this.ChiSoDienMoi.DataPropertyName = "ChiSoDienMoi";
            this.ChiSoDienMoi.HeaderText = "Chỉ số điện mới";
            this.ChiSoDienMoi.MinimumWidth = 6;
            this.ChiSoDienMoi.Name = "ChiSoDienMoi";
            // 
            // ChiSoNuocCu
            // 
            this.ChiSoNuocCu.DataPropertyName = "ChiSoNuocCu";
            this.ChiSoNuocCu.HeaderText = "Chỉ số nước cũ";
            this.ChiSoNuocCu.MinimumWidth = 6;
            this.ChiSoNuocCu.Name = "ChiSoNuocCu";
            // 
            // ChiSoNuocMoi
            // 
            this.ChiSoNuocMoi.DataPropertyName = "ChiSoNuocMoi";
            this.ChiSoNuocMoi.HeaderText = "Chỉ số nước mới";
            this.ChiSoNuocMoi.MinimumWidth = 6;
            this.ChiSoNuocMoi.Name = "ChiSoNuocMoi";
            // 
            // TongTien
            // 
            this.TongTien.DataPropertyName = "TongTien";
            this.TongTien.HeaderText = "Tổng tiền";
            this.TongTien.MinimumWidth = 6;
            this.TongTien.Name = "TongTien";
            // 
            // TrangThai
            // 
            this.TrangThai.DataPropertyName = "TrangThai";
            this.TrangThai.FillWeight = 140F;
            this.TrangThai.HeaderText = "Trạng thái";
            this.TrangThai.MinimumWidth = 6;
            this.TrangThai.Name = "TrangThai";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label3.Location = new System.Drawing.Point(274, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 18);
            this.label3.TabIndex = 74;
            this.label3.Text = "Chỉ số điện cũ:";
            // 
            // btnThoat
            // 
            this.btnThoat.Location = new System.Drawing.Point(937, 170);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(133, 40);
            this.btnThoat.TabIndex = 69;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label9.Location = new System.Drawing.Point(36, 187);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(121, 18);
            this.label9.TabIndex = 80;
            this.label9.Text = "Chỉ số nước mới:";
            // 
            // txtChiSoNuocCu
            // 
            this.txtChiSoNuocCu.Location = new System.Drawing.Point(174, 140);
            this.txtChiSoNuocCu.Name = "txtChiSoNuocCu";
            this.txtChiSoNuocCu.Size = new System.Drawing.Size(73, 22);
            this.txtChiSoNuocCu.TabIndex = 79;
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(937, 115);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(133, 40);
            this.btnHuy.TabIndex = 68;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.UseVisualStyleBackColor = true;
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.Location = new System.Drawing.Point(937, 61);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(133, 40);
            this.btnLuu.TabIndex = 67;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.UseVisualStyleBackColor = true;
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(788, 115);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(133, 40);
            this.btnSua.TabIndex = 66;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = true;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(788, 170);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(133, 40);
            this.btnXoa.TabIndex = 65;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = true;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(788, 61);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(133, 40);
            this.btnThem.TabIndex = 64;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = true;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // txtMaHD
            // 
            this.txtMaHD.Location = new System.Drawing.Point(150, 50);
            this.txtMaHD.Name = "txtMaHD";
            this.txtMaHD.Size = new System.Drawing.Size(100, 22);
            this.txtMaHD.TabIndex = 63;
            this.txtMaHD.TextChanged += new System.EventHandler(this.txtMaHD_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label4.Location = new System.Drawing.Point(311, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 18);
            this.label4.TabIndex = 62;
            this.label4.Text = "Phòng:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label2.Location = new System.Drawing.Point(47, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(97, 18);
            this.label2.TabIndex = 61;
            this.label2.Text = "Mã Hóa Đơn:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(163)));
            this.label1.ForeColor = System.Drawing.Color.MediumBlue;
            this.label1.Location = new System.Drawing.Point(438, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(254, 29);
            this.label1.TabIndex = 60;
            this.label1.Text = "Hóa Đơn Điện Nước";
            // 
            // FormQuanLyDienNuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1104, 556);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.txtMaPhongTim);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.cboTenPhong);
            this.Controls.Add(this.btnTinhTongTien);
            this.Controls.Add(this.txtTongTien);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.txtChiSoNuocMoi);
            this.Controls.Add(this.txtChiSoDienCu);
            this.Controls.Add(this.dtpThang);
            this.Controls.Add(this.txtChiSoDienMoi);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dgvHoaDon);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.txtChiSoNuocCu);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnSua);
            this.Controls.Add(this.btnXoa);
            this.Controls.Add(this.btnThem);
            this.Controls.Add(this.txtMaHD);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "FormQuanLyDienNuoc";
            this.Text = "FormQuanLyDienNuoc";
            this.Load += new System.EventHandler(this.FormQuanLyDienNuoc_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHoaDon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnTim;
        private System.Windows.Forms.TextBox txtMaPhongTim;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton rdThanhToan;
        private System.Windows.Forms.RadioButton rdChuaThanhToan;
        private System.Windows.Forms.ComboBox cboTenPhong;
        private System.Windows.Forms.Button btnTinhTongTien;
        private System.Windows.Forms.TextBox txtTongTien;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtChiSoNuocMoi;
        private System.Windows.Forms.TextBox txtChiSoDienCu;
        private System.Windows.Forms.DateTimePicker dtpThang;
        private System.Windows.Forms.TextBox txtChiSoDienMoi;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DataGridView dgvHoaDon;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaHD;
        private System.Windows.Forms.DataGridViewTextBoxColumn TenPhong;
        private System.Windows.Forms.DataGridViewTextBoxColumn Thang;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiSoDienCu;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiSoDienMoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiSoNuocCu;
        private System.Windows.Forms.DataGridViewTextBoxColumn ChiSoNuocMoi;
        private System.Windows.Forms.DataGridViewTextBoxColumn TongTien;
        private System.Windows.Forms.DataGridViewTextBoxColumn TrangThai;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtChiSoNuocCu;
        private System.Windows.Forms.Button btnHuy;
        private System.Windows.Forms.Button btnLuu;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.TextBox txtMaHD;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}