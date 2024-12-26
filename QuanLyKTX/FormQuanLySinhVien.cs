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

namespace QuanLyKTX
{
    public partial class FormQuanLySinhVien : Form
    {
        SqlConnection con = new SqlConnection();
        //Dùng trong trường hợp sửa
        string maSV = "";
        private bool isComboBoxChanged = false;

        public FormQuanLySinhVien()
        {
            InitializeComponent();
            dgvSinhVien.AutoGenerateColumns = false;
            cboTenPhong.GotFocus += new EventHandler(cboTenPhong_GotFocus);
        }

        private void cboTenPhong_GotFocus(object sender, EventArgs e)
        {
            // Hiển thị các phòng trống khi ComboBox nhận được focus
            cboTenPhong.DataSource = LoadPhongTrong();
            cboTenPhong.DisplayMember = "TenPhong";
            cboTenPhong.ValueMember = "MaPhong";
        }

        private DataTable LoadPhongTrong()
        {
            string tenPhongSql = @"SELECT DISTINCT p.TenPhong, p.MaPhong 
                                   FROM Phong p LEFT JOIN SinhVien sv ON p.MaPhong = sv.MaPhong 
                                   GROUP BY p.MaPhong, p.TenPhong, p.SoNguoiToiDa
                                   HAVING COUNT(sv.MaPhong) < p.SoNguoiToiDa";
            SqlDataAdapter tenPhongAdapter = new SqlDataAdapter(tenPhongSql, con);
            DataTable tableTenPhong = new DataTable();
            tenPhongAdapter.Fill(tableTenPhong);
            return tableTenPhong;
        }

        private DataTable LoadAllPhong()
        {
            //Hiển thị dữ liệu lên cboTenPhong
            string tenPhongSql = @"SELECT DISTINCT * FROM Phong";
            SqlDataAdapter tenPhongAdapter = new SqlDataAdapter(tenPhongSql, con);
            DataTable tableTenPhong = new DataTable();
            tenPhongAdapter.Fill(tableTenPhong);
            return tableTenPhong;
        }

        private void FormQuanLySinhVien_Load(object sender, EventArgs e)
        {
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            int soLuongSinhVien = 0;

            //Kết nối tới CSDL
            if(con.State == ConnectionState.Closed)
            {
                con.ConnectionString = @"Server=ADMIN-PC\MSSQLSERVER1;Database=QLKTX;Integrated Security=True";
                con.Open();
            }

            //Hiển thị dữ liệu lên dgvSinhVien
            string sinhVienSql = @"SELECT sv.*, p.TenPhong, p.MaPhong from SinhVien sv, Phong p where sv.MaPhong = p.MaPhong";
            SqlDataAdapter sinhVienAdapter = new SqlDataAdapter(sinhVienSql, con);
            DataTable tableSinhVien = new DataTable();
            sinhVienAdapter.Fill(tableSinhVien);
            dgvSinhVien.DataSource = tableSinhVien;

            //Hiển thị dữ liệu lên cboTenPhong
            string tenPhongSql = @"SELECT DISTINCT * FROM Phong";                                  
            SqlDataAdapter tenPhongAdapter = new SqlDataAdapter (tenPhongSql, con);
            DataTable tableTenPhong = new DataTable();
            tenPhongAdapter.Fill(tableTenPhong);
            cboTenPhong.DataSource = tableTenPhong;
            cboTenPhong.DisplayMember = "TenPhong";
            cboTenPhong.ValueMember = "MaPhong";

            // Khi click vào DataGridView thì hiển thị dữ liệu của dòng được chọn lên control tương ứng
            txtMSSV.DataBindings.Clear();
            txtHoTen.DataBindings.Clear();
            txtLop.DataBindings.Clear();
            txtCCCD.DataBindings.Clear();
            txtDiaChi.DataBindings.Clear();
            txtSDT.DataBindings.Clear();
            rdNam.DataBindings.Clear();
            rdNu.DataBindings.Clear();
            cboTenPhong.DataBindings.Clear();
            dtpNgaySinh.DataBindings.Clear();
            txtEmail.DataBindings.Clear();

            if (dgvSinhVien.Rows.Count == 0)
            {
                txtMSSV.Clear();
                txtHoTen.Clear();
                txtLop.Clear();
                txtCCCD.Clear();
                txtDiaChi.Clear();
                txtSDT.Clear();
                rdNam.Checked = false;
                rdNu.Checked = false;
                cboTenPhong.Text = "";
                dtpNgaySinh.Value = DateTime.Now;
                txtEmail.Clear();
            }

            txtMSSV.DataBindings.Add("Text",dgvSinhVien.DataSource,"Mssv",false,DataSourceUpdateMode.Never);
            txtHoTen.DataBindings.Add("Text",dgvSinhVien.DataSource,"HoTen",false, DataSourceUpdateMode.Never);
            txtLop.DataBindings.Add("Text", dgvSinhVien.DataSource, "Lop", false, DataSourceUpdateMode.Never);
            txtCCCD.DataBindings.Add("Text", dgvSinhVien.DataSource, "CCCD", false, DataSourceUpdateMode.Never);
            txtDiaChi.DataBindings.Add("Text", dgvSinhVien.DataSource, "DiaChi", false, DataSourceUpdateMode.Never);
            txtSDT.DataBindings.Add("Text",dgvSinhVien.DataSource,"SDT",false,DataSourceUpdateMode.Never);
            cboTenPhong.DataBindings.Add("SelectedValue",dgvSinhVien.DataSource,"MaPhong",false,DataSourceUpdateMode.Never);
            dtpNgaySinh.DataBindings.Add("Value", dgvSinhVien.DataSource, "NgaySinh", false, DataSourceUpdateMode.Never);
            txtEmail.DataBindings.Add("Text", dgvSinhVien.DataSource, "Email", false, DataSourceUpdateMode.Never);

            // Định dạng giới tính
            Binding nam = new Binding("Checked", dgvSinhVien.DataSource, "GioiTinh",false,DataSourceUpdateMode.Never);
            nam.Format += (s, evt) =>
            {
                evt.Value = Convert.ToInt32(evt.Value) == 0;
            };
            rdNam.DataBindings.Add(nam);
            Binding nu = new Binding("Checked", dgvSinhVien.DataSource, "GioiTinh",false,DataSourceUpdateMode.Never);
            nu.Format += (s, evt) =>
            {
                evt.Value = Convert.ToInt32(evt.Value) == 1;
            };
            rdNu.DataBindings.Add(nu);

            //làm cho các nút mờ đi
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;
            txtCCCD.Enabled = false;
            txtDiaChi.Enabled = false;
            txtHoTen.Enabled = false;
            txtLop.Enabled = false;
            txtMSSV.Enabled = false;
            txtSDT.Enabled = false;
            cboTenPhong.Enabled = false;
            rdNam.Enabled = false;
            rdNu.Enabled = false;
            dtpNgaySinh.Enabled = false;
            txtEmail.Enabled = false;

            //Làm sáng các nút thêm sửa xóa
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }
        //Định dạng giới tính
        private void dgvSinhVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSinhVien.Columns[e.ColumnIndex].Name == "GioiTinh")
            {
                e.Value = (byte)e.Value == 0 ? "Nam" : "Nữ";
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //Đánh dấu là thêm mới
            maSV = "";
            //xóa trắng các trường
            txtCCCD.Text = "";
            txtDiaChi.Text = "";
            txtHoTen.Text = "";
            txtLop.Text = "";
            txtMSSV.Text = "";
            txtSDT.Text = "";
            rdNam.Checked = false;
            rdNu.Checked = false;
            dtpNgaySinh.Value = DateTime.Now;
            cboTenPhong.Text = "";
            txtEmail.Text = "";
            txtMSSV.Focus();

            //Làm mờ nút thêm, sửa, xóa
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            //Làm sáng nút lưu, hủy
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;

            //Làm sáng các control thêm thông tin
            txtCCCD.Enabled = true;
            txtDiaChi.Enabled = true;
            txtHoTen.Enabled = true;
            txtLop.Enabled = true;
            txtMSSV.Enabled = true;
            txtSDT.Enabled = true;
            rdNam.Enabled = true;
            rdNu.Enabled = true;
            cboTenPhong.Enabled = true;
            dtpNgaySinh.Enabled = true;
            txtEmail.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiểm tra dữ liệu được đưa vào
            if (txtMSSV.Text.Trim() == "")
                MessageBox.Show("Mã số sinh viên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtHoTen.Text.Trim() == "")
                MessageBox.Show("Họ tên không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtLop.Text.Trim() == "")
                MessageBox.Show("Lớp không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtCCCD.Text.Trim() == "")
                MessageBox.Show("Căn cước công dân không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtDiaChi.Text.Trim() == "")
                MessageBox.Show("Địa chỉ không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtSDT.Text.Trim() == "")
                MessageBox.Show("Số điện thoại không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if(cboTenPhong.Text.Trim() == "")
                MessageBox.Show("Chưa chọn phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (!rdNam.Checked && !rdNu.Checked)
                MessageBox.Show("Vui lòng chọn giới tính!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtEmail.Text.Trim() == "")
                MessageBox.Show("Email không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    //Thêm mới
                    if (maSV == "")
                    {
                        string sql = @"INSERT INTO SinhVien VALUES(@Mssv,@HoTen,@Lop,@NgaySinh,@GioiTinh,@CCCD,@SDT,@DiaChi,@MaPhong,@Email)";
                        SqlCommand cmd = new SqlCommand(sql,con);
                        cmd.Parameters.Add(@"Mssv",SqlDbType.VarChar,9).Value = txtMSSV.Text;
                        cmd.Parameters.Add(@"HoTen",SqlDbType.NVarChar,40).Value = txtHoTen.Text;
                        cmd.Parameters.Add(@"Lop",SqlDbType.VarChar,7).Value = txtLop.Text;
                        cmd.Parameters.Add(@"NgaySinh",SqlDbType.Date).Value = dtpNgaySinh.Value.ToString();
                        cmd.Parameters.Add(@"GioiTinh",SqlDbType.TinyInt).Value = rdNam.Checked ? 0 : 1;
                        cmd.Parameters.Add(@"CCCD",SqlDbType.VarChar,12).Value = txtCCCD.Text;
                        cmd.Parameters.Add(@"SDT",SqlDbType.VarChar,10).Value = txtSDT.Text;
                        cmd.Parameters.Add(@"DiaChi",SqlDbType.NVarChar,30).Value = txtDiaChi.Text;
                        cmd.Parameters.Add(@"MaPhong",SqlDbType.VarChar,6).Value = cboTenPhong.SelectedValue.ToString();
                        cmd.Parameters.Add(@"Email", SqlDbType.NVarChar, 100).Value = txtEmail.Text;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm sinh viên mới thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string sql = @"UPDATE SinhVien
                                       SET  Mssv = @Mssv,
                                            HoTen = @HoTen,
                                            Lop = @Lop,
                                            NgaySinh = @NgaySinh,
                                            GioiTinh = @GioiTinh,
                                            CCCD = @CCCD,
                                            SDT = @SDT,
                                            DiaChi = @DiaChi,
                                            MaPhong = @MaPhong,
                                            Email = @Email
                                       WHERE Mssv = @MssvCu";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.Add(@"Mssv", SqlDbType.VarChar, 9).Value = txtMSSV.Text;
                        cmd.Parameters.Add(@"MssvCu", SqlDbType.VarChar, 9).Value = maSV;
                        cmd.Parameters.Add(@"HoTen", SqlDbType.NVarChar, 40).Value = txtHoTen.Text;
                        cmd.Parameters.Add(@"Lop", SqlDbType.VarChar, 7).Value = txtLop.Text;
                        cmd.Parameters.Add(@"NgaySinh", SqlDbType.Date).Value = dtpNgaySinh.Value.ToString();
                        cmd.Parameters.Add(@"GioiTinh", SqlDbType.TinyInt).Value = rdNam.Checked ? 0 : 1;
                        cmd.Parameters.Add(@"CCCD", SqlDbType.VarChar, 12).Value = txtCCCD.Text;
                        cmd.Parameters.Add(@"SDT", SqlDbType.VarChar, 10).Value = txtSDT.Text;
                        cmd.Parameters.Add(@"DiaChi", SqlDbType.NVarChar, 30).Value = txtDiaChi.Text;
                        cmd.Parameters.Add(@"MaPhong", SqlDbType.VarChar, 6).Value = cboTenPhong.SelectedValue.ToString();
                        cmd.Parameters.Add(@"Email", SqlDbType.NVarChar, 100).Value = txtEmail.Text;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Chỉnh sửa sinh viên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    FormQuanLySinhVien_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi trùng mã số sinh viên", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa sinh viên " + txtHoTen.Text + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dgvSinhVien.SelectedRows.Count > 0)
            {
                if (kq == DialogResult.Yes)
                {
                    try
                    {
                        string sql = @"DELETE FROM SinhVien WHERE Mssv = @Mssv";
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.Add("@Mssv", SqlDbType.VarChar, 9).Value = txtMSSV.Text;
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Xóa sinh viên thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        // Tải lại form
                        FormQuanLySinhVien_Load(sender, e);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
                MessageBox.Show("Vui lòng chọn sinh viên để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //Đánh dấu là cập nhật
            maSV = txtMSSV.Text;

            //Làm mờ nút Thêm mới, Sửa và Xóa
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // Làm sáng nút Lưu và Hủy
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;

            //Làm sáng các control
            txtCCCD.Enabled = true;
            txtDiaChi.Enabled = true;
            txtHoTen.Enabled = true;
            txtLop.Enabled = true;
            txtMSSV.Enabled = true;
            txtSDT.Enabled = true;
            rdNam.Enabled = true;
            rdNu.Enabled = true;
            cboTenPhong.Enabled = true;
            dtpNgaySinh.Enabled = true;
            txtEmail.Enabled = true;
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            FormQuanLySinhVien_Load(sender, e);
        }

        private void cboTenPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            isComboBoxChanged = true;
        }

        private void txtMSSV_TextChanged(object sender, EventArgs e)
        {
            // Lấy vị trí con trỏ hiện tại
            int cursorPosition = txtMSSV.SelectionStart;

            // Chuyển nội dung trong TextBox thành chữ hoa
            txtMSSV.Text = txtMSSV.Text.ToUpper();

            // Đặt lại con trỏ ở đúng vị trí
            txtMSSV.SelectionStart = cursorPosition;
        }

        private void txtLop_TextChanged(object sender, EventArgs e)
        {
            // Lấy vị trí con trỏ hiện tại
            int cursorPosition = txtLop.SelectionStart;

            // Chuyển nội dung trong TextBox thành chữ hoa
            txtLop.Text = txtLop.Text.ToUpper();

            // Đặt lại con trỏ ở đúng vị trí
            txtLop.SelectionStart = cursorPosition;
        }
    }
}
