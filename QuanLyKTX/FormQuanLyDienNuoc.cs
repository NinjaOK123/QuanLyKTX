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
    public partial class FormQuanLyDienNuoc : Form
    {
        private SqlConnection con;
        public string userRole;
        //Mã phòng (dùng trong trường hợp sửa)
        string maHoaDon = "";
        public FormQuanLyDienNuoc()
        {
            InitializeComponent();
            con = new SqlConnection();
            dgvHoaDon.AutoGenerateColumns = false;
        }
        private void ConnectToDatabase()
        {
            try
            {
                // Kiểm tra trạng thái kết nối
                if (con.State == ConnectionState.Closed)
                {
                    // Thiết lập chuỗi kết nối
                    con.ConnectionString = @"Server=ADMIN-PC\MSSQLSERVER1;Database=QLKTX;Integrated Security=True";

                    // Mở kết nối
                    con.Open();
                    Console.WriteLine("Kết nối thành công.");
                }
            }
            catch (Exception ex)
            {
                // Xử lý ngoại lệ khi không thể kết nối
                MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu. Lỗi: " + ex.Message, "Lỗi kết nối", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void FormQuanLyDienNuoc_Load(object sender, EventArgs e)
        {
            // Kết nối tới cơ sở dữ liệu
            if (con.State == ConnectionState.Closed)
            {
                con.ConnectionString = @"Server=ADMIN-PC\MSSQLSERVER1;Database=QLKTX;Integrated Security=True";
                con.Open();
            }

            // Hiển thị dữ liệu của bảng điện nước lên dgvHoaDon
            string HDSql = @"SELECT DN.*, P.TenPhong from Phong P, DienNuoc DN where P.MaPhong = DN.MaPhong";
            SqlDataAdapter HDAdapter = new SqlDataAdapter(HDSql, con);
            DataTable tableHD = new DataTable();
            HDAdapter.Fill(tableHD);
            dgvHoaDon.DataSource = tableHD;

            // Tắt các nút Lưu, Hủy, Tính tiền
            btnTinhTongTien.Enabled = false;
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;

            // Bật các nút Thêm, Sửa, Xóa, Tìm
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnTim.Enabled = true;
            txtMaPhongTim.Enabled = true;
            txtMaPhongTim.Text = "";

            // Tắt các điều khiển (control) để không cho phép thay đổi thông tin khi chưa chọn hóa đơn
            txtChiSoDienCu.Enabled = false;
            txtChiSoDienMoi.Enabled = false;
            txtChiSoNuocCu.Enabled = false;
            txtChiSoNuocMoi.Enabled = false;
            txtMaHD.Enabled = false;
            cboTenPhong.Enabled = false;
            txtTongTien.Enabled = false;
            dtpThang.Enabled = false;
            rdChuaThanhToan.Enabled = false;
            rdThanhToan.Enabled = false;

            // Hiển thị dữ liệu của bảng Phong lên ComboBox cboPhong
            string TenPhongSql = @"SELECT * FROM Phong";
            SqlDataAdapter TenPhongAdapter = new SqlDataAdapter(TenPhongSql, con);
            DataTable tableTenPhong = new DataTable();
            TenPhongAdapter.Fill(tableTenPhong);
            cboTenPhong.DataSource = tableTenPhong;
            cboTenPhong.DisplayMember = "TenPhong";
            cboTenPhong.ValueMember = "MaPhong";

            // Khi click vào DataGridView thì hiển thị dữ liệu của dòng được chọn lên các control tương ứng
            txtMaHD.DataBindings.Clear();
            cboTenPhong.DataBindings.Clear();
            dtpThang.DataBindings.Clear();
            txtChiSoDienCu.DataBindings.Clear();
            txtChiSoDienMoi.DataBindings.Clear();
            txtChiSoNuocCu.DataBindings.Clear();
            txtChiSoNuocMoi.DataBindings.Clear();
            txtTongTien.DataBindings.Clear();
            rdChuaThanhToan.DataBindings.Clear();
            rdThanhToan.DataBindings.Clear();

            // Khi trong data không còn gì thì trên các control sẽ rỗng
            if (dgvHoaDon.Rows.Count == 0)
            {
                txtMaHD.Text = string.Empty;
                txtTongTien.Text = string.Empty;
                txtChiSoDienCu.Text = string.Empty;
                txtChiSoDienMoi.Text = string.Empty;
                txtChiSoNuocCu.Text = string.Empty;
                txtChiSoNuocMoi.Text = string.Empty;
                rdChuaThanhToan.Checked = false;
                rdThanhToan.Checked = false;
                cboTenPhong.Text = string.Empty;
                dtpThang.Value = DateTime.Now;
            }

            // Binding dữ liệu từ DataGridView vào các control
            cboTenPhong.DataBindings.Add("SelectedValue", dgvHoaDon.DataSource, "MaPhong", false, DataSourceUpdateMode.Never);
            txtMaHD.DataBindings.Add("Text", dgvHoaDon.DataSource, "MaHD", false, DataSourceUpdateMode.Never);
            txtChiSoDienCu.DataBindings.Add("Text", dgvHoaDon.DataSource, "ChiSoDienCu", false, DataSourceUpdateMode.Never);
            txtChiSoDienMoi.DataBindings.Add("Text", dgvHoaDon.DataSource, "ChiSoDienMoi", false, DataSourceUpdateMode.Never);
            txtChiSoNuocCu.DataBindings.Add("Text", dgvHoaDon.DataSource, "ChiSoNuocCu", false, DataSourceUpdateMode.Never);
            txtChiSoNuocMoi.DataBindings.Add("Text", dgvHoaDon.DataSource, "ChiSoNuocMoi", false, DataSourceUpdateMode.Never);
            txtTongTien.DataBindings.Add("Text", dgvHoaDon.DataSource, "TongTien", false, DataSourceUpdateMode.Never);
            dtpThang.DataBindings.Add("Value", dgvHoaDon.DataSource, "Thang", false, DataSourceUpdateMode.Never);

            // Định dạng trạng thái của radio button
            // Binding cho RadioButton "Đã Thanh Toán"
            Binding bindingThanhToan = new Binding("Checked", dgvHoaDon.DataSource, "TrangThai", false, DataSourceUpdateMode.Never);
            bindingThanhToan.Format += (s, evt) =>
            {
                // Gán giá trị 1 cho trạng thái "Đã Thanh Toán"
                evt.Value = Convert.ToInt32(evt.Value) == 1;
            };
            rdThanhToan.DataBindings.Add(bindingThanhToan);

            // Binding cho RadioButton "Chưa Thanh Toán"
            Binding bindingChuaThanhToan = new Binding("Checked", dgvHoaDon.DataSource, "TrangThai", false, DataSourceUpdateMode.Never);
            bindingChuaThanhToan.Format += (s, evt) =>
            {
                // Gán giá trị 0 cho trạng thái "Chưa Thanh Toán"
                evt.Value = Convert.ToInt32(evt.Value) == 0;
            };
            rdChuaThanhToan.DataBindings.Add(bindingChuaThanhToan);
            if (string.IsNullOrEmpty(userRole))
            {
                MessageBox.Show("Vai trò chưa được gán giá trị!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            MessageBox.Show($"Vai trò hiện tại: {userRole}", "Thông báo", MessageBoxButtons.OK);

            if (userRole.Equals("admin", StringComparison.OrdinalIgnoreCase))
            {
                btnThem.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnTim.Enabled = true;
                txtMaPhongTim.Enabled = true;
            }
            else if (userRole.Equals("user", StringComparison.OrdinalIgnoreCase))
            {
                btnThem.Enabled = false;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnTim.Enabled = true;
                txtMaPhongTim.Enabled = true;
            }
            else
            {
                MessageBox.Show("Vai trò không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvHoaDon_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvHoaDon.Columns[e.ColumnIndex].Name == "TrangThai")
            {
                e.Value = (byte)e.Value == 0 ? "Chưa thanh toán" : "Đã thanh toán";
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //Đánh dấu thêm mới
            maHoaDon = "";

            //clear dữ liệu trên các control
            txtMaHD.Text = "";
            txtChiSoDienCu.Text = "";
            txtChiSoDienMoi.Text = "";
            txtChiSoNuocCu.Text = "";
            txtChiSoNuocMoi.Text = "";
            txtTongTien.Text = "";
            cboTenPhong.Text = "";
            rdThanhToan.Checked = false;
            rdChuaThanhToan.Checked = false;
            dtpThang.Value = DateTime.Now;
            txtMaHD.Focus();

            // Làm mờ nút Thêm mới, Sửa và Xóa, Tìm
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnTim.Enabled = false;

            //Làm sáng các nút lưu, hủy
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnTinhTongTien.Enabled = true;

            //Làm sáng các control
            txtMaHD.Enabled = true;
            txtChiSoDienCu.Enabled = true;
            txtChiSoDienMoi.Enabled = true;
            txtChiSoNuocCu.Enabled = true;
            txtChiSoNuocMoi.Enabled = true;
            txtTongTien.Enabled = true;
            cboTenPhong.Enabled = true;
            dtpThang.Enabled = true;
            rdChuaThanhToan.Enabled = true;
            rdThanhToan.Enabled = true;

            //tắt tìm kiếm
            txtMaPhongTim.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //Đánh dấu là cập nhật
            maHoaDon = txtMaHD.Text;

            // Làm mờ nút Thêm mới, Sửa và Xóa 
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnTim.Enabled = false;
            txtMaPhongTim.Enabled = false;

            // Làm sáng nút Lưu và hủy, tính tiền
            btnLuu.Enabled = true;
            btnHuy.Enabled = true;
            btnTinhTongTien.Enabled = true;

            //Làm sáng các control
            txtMaHD.Enabled = true;
            txtChiSoDienCu.Enabled = true;
            txtChiSoDienMoi.Enabled = true;
            txtChiSoNuocCu.Enabled = true;
            txtChiSoNuocMoi.Enabled = true;
            txtTongTien.Enabled = true;
            cboTenPhong.Enabled = false;
            dtpThang.Enabled = true;
            rdChuaThanhToan.Enabled = true;
            rdThanhToan.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có dòng nào trong DataGridView được chọn không
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                // Lấy mã hóa đơn (MaHD) từ dòng đã chọn
                string maHD = dgvHoaDon.SelectedRows[0].Cells["MaHD"].Value.ToString(); // Thay đổi tên cột nếu cần

                // Hiển thị hộp thoại xác nhận xóa
                DialogResult kq = MessageBox.Show("Bạn có chắc chắn muốn xóa hóa đơn " + maHD + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (kq == DialogResult.Yes)
                {
                    try
                    {
                        // Câu lệnh SQL để xóa hóa đơn
                        string sql = @"DELETE FROM DienNuoc WHERE MaHD = @MaHD";
                        SqlCommand cmd = new SqlCommand(sql, con);

                        // Thêm tham số cho câu lệnh SQL
                        cmd.Parameters.AddWithValue("@MaHD", maHD);

                        // Thực thi câu lệnh xóa
                        int rowsAffected = cmd.ExecuteNonQuery();

                        // Kiểm tra số dòng bị ảnh hưởng
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Xóa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // Xóa dòng trong DataGridView
                            dgvHoaDon.Rows.RemoveAt(dgvHoaDon.SelectedRows[0].Index);

                            // Cập nhật lại giao diện nếu cần, ví dụ gọi lại FormQuanLyDienNuoc_Load để làm mới dữ liệu
                            FormQuanLyDienNuoc_Load(sender, e); // Hoặc cập nhật lại dữ liệu của DataGridView
                        }
                        else
                        {
                            MessageBox.Show("Không tìm thấy hóa đơn để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                // Thông báo nếu không có dòng nào được chọn
                MessageBox.Show("Vui lòng chọn hóa đơn cần xóa.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void txtMaHDT_TextChanged(object sender, EventArgs e)
        {
            // Lấy vị trí con trỏ hiện tại
            int cursorPosition = txtMaPhongTim.SelectionStart;

            // Chuyển nội dung trong TextBox thành chữ hoa
            txtMaPhongTim.Text = txtMaPhongTim.Text.ToUpper();

            // Đặt lại con trỏ ở đúng vị trí
            txtMaPhongTim.SelectionStart = cursorPosition;
        }

        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                string maPhongTimKiem = txtMaPhongTim.Text.Trim(); // Lấy mã hóa đơn cần tìm từ TextBox

                if (string.IsNullOrWhiteSpace(maPhongTimKiem))
                {
                    MessageBox.Show("Vui lòng nhập mã phòng cần tìm!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                //bool timThay = false;

                //// Duyệt qua từng dòng trong DataGridView
                //foreach (DataGridViewRow row in dgvHoaDon.Rows)
                //{
                //    if (row.Cells["MaHD"].Value != null && row.Cells["MaHD"].Value.ToString() == maHDTimKiem)
                //    {
                //        // Nếu tìm thấy, thiết lập dòng được chọn
                //        row.Selected = true;

                //        // Cuộn tới dòng tìm được
                //        dgvHoaDon.FirstDisplayedScrollingRowIndex = row.Index;

                //        // Thiết lập con trỏ tới dòng tìm được
                //        dgvHoaDon.CurrentCell = row.Cells[0];

                //        timThay = true;
                //        break;
                //    }
                //}

                // Truy vấn danh sách hóa đơn trong phòng
                    string sqlHoaDon = @"SELECT d.*, p.TenPhong
                                       FROM Phong p, DienNuoc d
                                       WHERE p.MaPhong = d.MaPhong and d.MaPhong = @MaPhong";
                    SqlCommand cmdHD = new SqlCommand(sqlHoaDon, con);
                    cmdHD.Parameters.AddWithValue("@MaPhong", maPhongTimKiem);
                    SqlDataAdapter adapterHD = new SqlDataAdapter(cmdHD);
                    DataTable tableHD = new DataTable();
                    adapterHD.Fill(tableHD);
                    if (tableHD.Rows.Count > 0)
                    {
                        MessageBox.Show("Đã tìm thấy phòng","Thông báo",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        dgvHoaDon.DataSource = tableHD;

                        // Khi click vào DataGridView thì hiển thị dữ liệu của dòng được chọn lên control tương ứng
                        txtMaHD.DataBindings.Clear();
                        cboTenPhong.DataBindings.Clear();
                        dtpThang.DataBindings.Clear();
                        txtChiSoDienCu.DataBindings.Clear();
                        txtChiSoDienMoi.DataBindings.Clear();
                        txtChiSoNuocCu.DataBindings.Clear();
                        txtChiSoNuocMoi.DataBindings.Clear();
                        txtTongTien.DataBindings.Clear();
                        rdChuaThanhToan.DataBindings.Clear();
                        rdThanhToan.DataBindings.Clear();

                        //Khi trong data không còn gì thì trên các control sẽ rỗng
                        if (dgvHoaDon.Rows.Count == 0)
                        {
                            txtMaHD.Text = string.Empty;
                            txtTongTien.Text = string.Empty;
                            txtChiSoDienCu.Text = string.Empty;
                            txtChiSoDienMoi.Text = string.Empty;
                            txtChiSoNuocCu.Text = string.Empty;
                            txtChiSoNuocMoi.Text = string.Empty;
                            rdChuaThanhToan.Checked = false;
                            rdThanhToan.Checked = false;
                            cboTenPhong.Text = string.Empty;
                            dtpThang.Value = DateTime.Now;
                        }

                        cboTenPhong.DataBindings.Add("SelectedValue", dgvHoaDon.DataSource, "MaPhong", false, DataSourceUpdateMode.Never);
                        txtMaHD.DataBindings.Add("Text", dgvHoaDon.DataSource, "MaHD", false, DataSourceUpdateMode.Never);
                        txtChiSoDienCu.DataBindings.Add("Text", dgvHoaDon.DataSource, "ChiSoDienCu", false, DataSourceUpdateMode.Never);
                        txtChiSoDienMoi.DataBindings.Add("Text", dgvHoaDon.DataSource, "ChiSoDienMoi", false, DataSourceUpdateMode.Never);
                        txtChiSoNuocCu.DataBindings.Add("Text", dgvHoaDon.DataSource, "ChiSoNuocCu", false, DataSourceUpdateMode.Never);
                        txtChiSoNuocMoi.DataBindings.Add("Text", dgvHoaDon.DataSource, "ChiSoNuocMoi", false, DataSourceUpdateMode.Never);
                        txtTongTien.DataBindings.Add("Text", dgvHoaDon.DataSource, "TongTien", false, DataSourceUpdateMode.Never);
                        dtpThang.DataBindings.Add("value", dgvHoaDon.DataSource, "Thang", false, DataSourceUpdateMode.Never);

                        // Định dạng trạng Thái
                        // Binding cho RadioButton "Đã Thanh Toán"
                        Binding bindingThanhToan = new Binding("Checked", dgvHoaDon.DataSource, "TrangThai", false, DataSourceUpdateMode.Never);
                        bindingThanhToan.Format += (s, evt) =>
                        {
                            // Gán giá trị 1 cho trạng thái "Đã Thanh Toán"
                            evt.Value = Convert.ToInt32(evt.Value) == 1;
                        };
                        rdThanhToan.DataBindings.Add(bindingThanhToan);

                        // Binding cho RadioButton "Chưa Thanh Toán"
                        Binding bindingChuaThanhToan = new Binding("Checked", dgvHoaDon.DataSource, "TrangThai", false, DataSourceUpdateMode.Never);
                        bindingChuaThanhToan.Format += (s, evt) =>
                        {
                            // Gán giá trị 0 cho trạng thái "Chưa Thanh Toán"
                            evt.Value = Convert.ToInt32(evt.Value) == 0;
                        };
                        rdChuaThanhToan.DataBindings.Add(bindingChuaThanhToan);
                    }
                    else
                        MessageBox.Show("Không tìm thấy phòng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            btnHuy.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            // Kiểm tra dữ liệu nhập vào
            if (string.IsNullOrWhiteSpace(txtMaHD.Text))
            {
                MessageBox.Show("Mã hóa đơn không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtMaHD.Focus();
                return;
            }
            else if (cboTenPhong.SelectedValue == null)
            {
                MessageBox.Show("Chưa chọn phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                cboTenPhong.Focus();
                return;
            }
            else if (!rdThanhToan.Checked && !rdChuaThanhToan.Checked)
            {
                MessageBox.Show("Chưa chọn trạng thái thanh toán!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                rdChuaThanhToan.Focus();
                return;
            }
            else if(txtTongTien.Text.Trim() == "")
            {
                MessageBox.Show("Chưa tính tiền hóa đơn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtTongTien.Focus();
                return;
            }    
            else if (!decimal.TryParse(txtChiSoDienMoi.Text, out decimal chiSoDienMoi) || chiSoDienMoi < 0 ||
                !decimal.TryParse(txtChiSoDienCu.Text, out decimal chiSoDienCu) || chiSoDienCu < 0 ||
                chiSoDienMoi < chiSoDienCu)
            {
                MessageBox.Show("Chỉ số điện không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtChiSoDienMoi.Focus();
                return;
            }
            else if (!decimal.TryParse(txtChiSoNuocMoi.Text, out decimal chiSoNuocMoi) || chiSoNuocMoi < 0 ||
                !decimal.TryParse(txtChiSoNuocCu.Text, out decimal chiSoNuocCu) || chiSoNuocCu < 0 ||
                chiSoNuocMoi < chiSoNuocCu)
            {
                MessageBox.Show("Chỉ số nước không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtChiSoNuocMoi.Focus();
                return;
            }
            else
            {
                try
                {
                    // Tính tổng tiền (ví dụ đơn giá điện là 3000, nước là 5000)
                    //decimal donGiaDien = 3000;
                    //ecimal donGiaNuoc = 5000;
                    //decimal tongTien = (chiSoDienMoi - chiSoDienCu) * donGiaDien + (chiSoNuocMoi - chiSoNuocCu) * donGiaNuoc;
                    decimal tongTien = (chiSoDienMoi - chiSoDienCu) * 3000 + (chiSoNuocMoi - chiSoNuocCu) * 5000;
                    // Nếu là thêm mới
                    if (string.IsNullOrEmpty(maHoaDon))
                    {
                        string sqlInsert = @"INSERT INTO DienNuoc (MaHD, MaPhong, Thang, ChiSoDienCu, ChiSoDienMoi, ChiSoNuocCu, ChiSoNuocMoi, TongTien, TrangThai)
                         VALUES (@MaHD, @MaPhong, @Thang, @ChiSoDienCu, @ChiSoDienMoi, @ChiSoNuocCu, @ChiSoNuocMoi, @TongTien, @TrangThai)";
                        SqlCommand cmdInsert = new SqlCommand(sqlInsert, con);
                        cmdInsert.Parameters.AddWithValue("@MaHD", txtMaHD.Text);
                        cmdInsert.Parameters.AddWithValue("@MaPhong", cboTenPhong.SelectedValue.ToString());
                        cmdInsert.Parameters.AddWithValue("@Thang", dtpThang.Value);
                        cmdInsert.Parameters.AddWithValue("@ChiSoDienCu", chiSoDienCu);
                        cmdInsert.Parameters.AddWithValue("@ChiSoDienMoi", chiSoDienMoi);
                        cmdInsert.Parameters.AddWithValue("@ChiSoNuocCu", chiSoNuocCu);
                        cmdInsert.Parameters.AddWithValue("@ChiSoNuocMoi", chiSoNuocMoi);
                        cmdInsert.Parameters.AddWithValue("@TongTien", tongTien);
                        cmdInsert.Parameters.AddWithValue("@TrangThai", rdThanhToan.Checked ? 1 : 0); // Mặc định là chưa thanh toán
                        cmdInsert.ExecuteNonQuery();
                        // Hiển thị thông báo thành công
                        MessageBox.Show("Thêm hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else // Nếu là chỉnh sửa
                    {
                        string sqlUpdate = @"UPDATE DienNuoc
                                 SET MaHD = @MaHDMoi   ,MaPhong = @MaPhong, Thang = @Thang, ChiSoDienCu = @ChiSoDienCu,
                                     ChiSoDienMoi = @ChiSoDienMoi, ChiSoNuocCu = @ChiSoNuocCu,
                                     ChiSoNuocMoi = @ChiSoNuocMoi, TongTien = @TongTien, TrangThai = @TrangThai
                                 WHERE MaHD = @MaHDCu";
                        SqlCommand cmdUpdate = new SqlCommand(sqlUpdate, con);
                        cmdUpdate.Parameters.AddWithValue(@"MaHDCu", maHoaDon);
                        cmdUpdate.Parameters.AddWithValue("@MaHDMoi", txtMaHD.Text);
                        cmdUpdate.Parameters.AddWithValue("@MaPhong", cboTenPhong.SelectedValue.ToString());
                        cmdUpdate.Parameters.AddWithValue("@Thang", dtpThang.Value);
                        cmdUpdate.Parameters.AddWithValue("@ChiSoDienCu", chiSoDienCu);
                        cmdUpdate.Parameters.AddWithValue("@ChiSoDienMoi", chiSoDienMoi);
                        cmdUpdate.Parameters.AddWithValue("@ChiSoNuocCu", chiSoNuocCu);
                        cmdUpdate.Parameters.AddWithValue("@ChiSoNuocMoi", chiSoNuocMoi);
                        cmdUpdate.Parameters.AddWithValue("@TongTien", tongTien);
                        cmdUpdate.Parameters.AddWithValue("@TrangThai", rdThanhToan.Checked ? 1 : 0); // Mặc định là chưa thanh toán
                        cmdUpdate.ExecuteNonQuery();
                        // Hiển thị thông báo thành công
                        MessageBox.Show("Chỉnh sửa hóa đơn thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    // Tải lại dữ liệu
                    FormQuanLyDienNuoc_Load(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi trùng mã hóa đơn", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }           
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            FormQuanLyDienNuoc_Load(sender, e);
        }
        private decimal TinhTienDien(decimal dienTieuThu, bool laTheTraTruoc = false, int soNguoiO = 0)
        {
            decimal[] giaBacThang = { 1806, 1866, 2167, 2729, 3050, 3151 };

            if (laTheTraTruoc)
            {
                return dienTieuThu * 2649;
            }

            if (soNguoiO > 0)
            {
                int soHo = (int)Math.Ceiling(soNguoiO / 4.0m);
                decimal tongTien = 0;

                for (int i = 0; i < soHo; i++)
                {
                    decimal dienChoHo = Math.Min(dienTieuThu, 200);
                    tongTien += TinhTienDienTheoBac(dienChoHo, giaBacThang);
                    dienTieuThu -= dienChoHo;
                }

                if (dienTieuThu > 0)
                {
                    tongTien += TinhTienDienTheoBac(dienTieuThu, giaBacThang);
                }

                return tongTien;
            }

            if (soNguoiO == 0)
            {
                return dienTieuThu * 2167;
            }

            return TinhTienDienTheoBac(dienTieuThu, giaBacThang);
        }

        private decimal TinhTienDienTheoBac(decimal dienTieuThu, decimal[] giaBacThang)
        {
            decimal tongTien = 0;

            if (dienTieuThu > 400)
            {
                tongTien += (dienTieuThu - 400) * giaBacThang[5];
                dienTieuThu = 400;
            }
            if (dienTieuThu > 300)
            {
                tongTien += (dienTieuThu - 300) * giaBacThang[4];
                dienTieuThu = 300;
            }
            if (dienTieuThu > 200)
            {
                tongTien += (dienTieuThu - 200) * giaBacThang[3];
                dienTieuThu = 200;
            }
            if (dienTieuThu > 100)
            {
                tongTien += (dienTieuThu - 100) * giaBacThang[2];
                dienTieuThu = 100;
            }
            if (dienTieuThu > 50)
            {
                tongTien += (dienTieuThu - 50) * giaBacThang[1];
                dienTieuThu = 50;
            }
            if (dienTieuThu > 0)
            {
                tongTien += dienTieuThu * giaBacThang[0];
            }

            return tongTien;
        }

        private decimal TinhTienNuoc(decimal nuocTieuThu)
        {
            decimal[] giaNuoc = { 5973, 7052, 8669, 15929 };
            decimal tongTien = 0;

            if (nuocTieuThu > 30)
            {
                tongTien += (nuocTieuThu - 30) * giaNuoc[3];
                nuocTieuThu = 30;
            }
            if (nuocTieuThu > 20)
            {
                tongTien += (nuocTieuThu - 20) * giaNuoc[2];
                nuocTieuThu = 20;
            }
            if (nuocTieuThu > 10)
            {
                tongTien += (nuocTieuThu - 10) * giaNuoc[1];
                nuocTieuThu = 10;
            }
            if (nuocTieuThu > 0)
            {
                tongTien += nuocTieuThu * giaNuoc[0];
            }

            // Cộng thêm thuế GTGT (5%) và phí môi trường (10%)
            tongTien += tongTien * 0.15m;

            return tongTien;
        }
        private void btnTinhTongTien_Click(object sender, EventArgs e)
        {
            try
            {
                if (!decimal.TryParse(txtChiSoDienMoi.Text, out decimal chiSoDienMoi) || chiSoDienMoi < 0 ||
                    !decimal.TryParse(txtChiSoDienCu.Text, out decimal chiSoDienCu) || chiSoDienCu < 0 ||
                    !decimal.TryParse(txtChiSoNuocMoi.Text, out decimal chiSoNuocMoi) || chiSoNuocMoi < 0 ||
                    !decimal.TryParse(txtChiSoNuocCu.Text, out decimal chiSoNuocCu) || chiSoNuocCu < 0)
                {
                    MessageBox.Show("Chỉ số điện hoặc nước không hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                decimal dienTieuThu = chiSoDienMoi - chiSoDienCu;
                decimal nuocTieuThu = chiSoNuocMoi - chiSoNuocCu;

                if (dienTieuThu < 0 || nuocTieuThu < 0)
                {
                    MessageBox.Show("Chỉ số mới phải lớn hơn hoặc bằng chỉ số cũ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Tính tiền điện và tiền nước
                decimal tienDien = TinhTienDien(dienTieuThu);
                decimal tienNuoc = TinhTienNuoc(nuocTieuThu);

                // Cộng thuế GTGT (8%) cho tiền điện
                tienDien += tienDien * 0.08m;

                // Tổng tiền
                decimal tongTien = tienDien + tienNuoc;

                txtTongTien.Text = tongTien.ToString("N0");

                MessageBox.Show("Tính tổng tiền thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txtMaHD_TextChanged(object sender, EventArgs e)
        {
            // Lấy vị trí con trỏ hiện tại
            int cursorPosition = txtMaHD.SelectionStart;

            // Chuyển nội dung trong TextBox thành chữ hoa
            txtMaHD.Text = txtMaHD.Text.ToUpper();

            // Đặt lại con trỏ ở đúng vị trí
            txtMaHD.SelectionStart = cursorPosition;
        }
    }
}
