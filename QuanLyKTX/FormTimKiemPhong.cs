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
    public partial class FormTimKiemPhong : Form
    {
        SqlConnection con = new SqlConnection();
        public FormTimKiemPhong()
        {
            InitializeComponent();
            dgvSinhVien.AutoGenerateColumns = false;
        }

        private void FormTimKiemPhong_Load(object sender, EventArgs e)
        {
            //Kết nối tới CSDL
            if (con.State == ConnectionState.Closed)
            {
                con.ConnectionString = @"Server=ADMIN-PC\MSSQLSERVER1;Database=QLKTX;Integrated Security=True";
                con.Open();
            }

            // Đặt DataGridView về trạng thái trống
            dgvSinhVien.DataSource = null;

            //focus
            txtMaPhongT.Focus();

            //Xóa trắng các trường
            txtMaPhongT.Text = "";
            txtDonGia.Text = "";
            txtMaPhong.Text = "";
            txtSoNguoiToiDa.Text = "";
            txtTenPhong.Text = "";
            cboDayPhong.Text = "";
            cboLoaiPhong.Text = "";

            //Làm mờ các control
            txtMaPhong.Enabled = false;
            txtTenPhong.Enabled = false;
            txtDonGia.Enabled = false;
            txtSoNguoiToiDa.Enabled = false;
            cboDayPhong.Enabled = false;
            cboLoaiPhong.Enabled = false;
        }

        private void btnTimPhong_Click(object sender, EventArgs e)
        {
            string maPhongTim = txtMaPhongT.Text.Trim();

            if (string.IsNullOrEmpty(maPhongTim))
            {
                MessageBox.Show("Vui lòng nhập mã phòng để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                try
                {
                    // Truy vấn thông tin phòng theo MaPhong
                    string sqlTimPhong = @"SELECT * 
                               FROM Phong
                               WHERE MaPhong = @MaPhong";
                    SqlCommand cmd = new SqlCommand(sqlTimPhong, con);
                    cmd.Parameters.AddWithValue("@MaPhong", maPhongTim);
                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable tablePhongTim = new DataTable();
                    adapter.Fill(tablePhongTim);

                    if (tablePhongTim.Rows.Count > 0)
                    {
                        //Hiển thị dữ liệu của bảng phòng lên comboBox cboDayPhong
                        string dayPhongSql = @"SELECT * from DayKTX";
                        SqlDataAdapter dayPhongAdapter = new SqlDataAdapter(dayPhongSql, con);
                        DataTable tableDayPhong = new DataTable();
                        dayPhongAdapter.Fill(tableDayPhong);
                        cboDayPhong.DataSource = tableDayPhong;
                        cboDayPhong.DisplayMember = "TenDay";
                        cboDayPhong.ValueMember = "MaDay";

                        //Hiển thị dữ liệu của bảng phòng lên comboBox cboLoaiPhong
                        string loaiPhongSql = @"SELECT * from LoaiPhong";
                        SqlDataAdapter loaiPhongAdapter = new SqlDataAdapter(loaiPhongSql, con);
                        DataTable tableLoaiPhong = new DataTable();
                        loaiPhongAdapter.Fill(tableLoaiPhong);
                        cboLoaiPhong.DataSource = tableLoaiPhong;
                        cboLoaiPhong.DisplayMember = "TenLoai";
                        cboLoaiPhong.ValueMember = "MaLoai";

                        // Hiển thị thông tin lên các control
                        DataRow row = tablePhongTim.Rows[0];
                        txtMaPhong.Text = row["MaPhong"].ToString();
                        txtTenPhong.Text = row["TenPhong"].ToString();
                        txtSoNguoiToiDa.Text = row["SoNguoiToiDa"].ToString();
                        cboDayPhong.SelectedValue = row["MaDay"].ToString();
                        cboLoaiPhong.SelectedValue = row["MaLoai"].ToString();

                        // Lấy giá phòng từ bảng LoaiPhong
                        string sqlGiaPhong = @"SELECT GiaPhong
                                   FROM LoaiPhong
                                   WHERE MaLoai = @MaLoai";
                        SqlCommand cmdGiaPhong = new SqlCommand(sqlGiaPhong, con);
                        cmdGiaPhong.Parameters.AddWithValue("@MaLoai", row["MaLoai"].ToString());
                        object giaPhong = cmdGiaPhong.ExecuteScalar();
                        txtDonGia.Text = giaPhong != null ? giaPhong.ToString() : "0";

                        // Truy vấn danh sách sinh viên trong phòng
                        string sqlSinhVien = @"SELECT Mssv, HoTen, Lop, NgaySinh, GioiTinh, CCCD, SDT, DiaChi
                                   FROM SinhVien
                                   WHERE MaPhong = @MaPhong";
                        SqlCommand cmdSinhVien = new SqlCommand(sqlSinhVien, con);
                        cmdSinhVien.Parameters.AddWithValue("@MaPhong", maPhongTim);
                        SqlDataAdapter adapterSinhVien = new SqlDataAdapter(cmdSinhVien);
                        DataTable tableSinhVien = new DataTable();
                        adapterSinhVien.Fill(tableSinhVien);

                        // Hiển thị danh sách sinh viên trong DataGridView
                        dgvSinhVien.DataSource = tableSinhVien;
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy thông tin phòng với mã này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Có lỗi xảy ra: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnResetPhong_Click(object sender, EventArgs e)
        {
            FormTimKiemPhong_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtMaPhongT_TextChanged(object sender, EventArgs e)
        {
            // Lấy vị trí con trỏ hiện tại
            int cursorPosition = txtMaPhongT.SelectionStart;

            // Chuyển nội dung trong TextBox thành chữ hoa
            txtMaPhongT.Text = txtMaPhongT.Text.ToUpper();

            // Đặt lại con trỏ ở đúng vị trí
            txtMaPhongT.SelectionStart = cursorPosition;
        }

        private void dgvSinhVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSinhVien.Columns[e.ColumnIndex].Name == "GioiTinh" && e.Value != null && e.Value != DBNull.Value)
            {
                int gioiTinh = Convert.ToInt32(e.Value); // Chuyển đổi an toàn sang int
                e.Value = gioiTinh == 0 ? "Nam" : "Nữ";
            }
        }
    }
}
