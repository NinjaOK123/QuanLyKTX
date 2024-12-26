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
    public partial class FormTimKiemSinhVien : Form
    {
        SqlConnection con = new SqlConnection();
        public FormTimKiemSinhVien()
        {
            InitializeComponent();
            dgvSinhVien.AutoGenerateColumns = false;
        }

        private void ClearFormControls()
        {
            txtMSSV.Text = string.Empty;
            txtMSSVT.Text = string.Empty;
            txtHoTen.Text = string.Empty;
            txtLop.Text = string.Empty;
            txtCCCD.Text = string.Empty;
            txtDiaChi.Text = string.Empty;
            txtSDT.Text = string.Empty;
            txtMaPhong.Text = string.Empty;
            txtEmail .Text = string.Empty;
            // Đặt trạng thái RadioButton
            rdNam.Checked = false;
            rdNu.Checked = false;

            // Đặt DateTimePicker về ngày hiện tại
            dtpNgaySinh.Value = DateTime.Now;
        }

        // Hàm để bật hoặc tắt các điều khiển trên form
        private void EnableFormControls(bool enabled)
        {
            txtCCCD.Enabled = enabled;
            txtDiaChi.Enabled = enabled;
            txtHoTen.Enabled = enabled;
            txtLop.Enabled = enabled;
            txtMSSVT.Enabled = enabled;
            txtSDT.Enabled = enabled;
            txtMaPhong.Enabled = enabled;
            rdNam.Enabled = enabled;
            rdNu.Enabled = enabled;
            dtpNgaySinh.Enabled = enabled;
            txtEmail.Enabled = enabled;
        }

        private void FormTimKiemSinhVien_Load(object sender, EventArgs e)
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
            txtMSSV.Focus();

            // Xóa liên kết dữ liệu và đặt các TextBox, ComboBox, RadioButton về trạng thái mặc định
            ClearFormControls();

            // Tắt các điều khiển nhập liệu
            EnableFormControls(false);
        }

        private void btnResetSV_Click(object sender, EventArgs e)
        {
            FormTimKiemSinhVien_Load(sender, e);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnTimSV_Click(object sender, EventArgs e)
        {
            // Kiểm tra kết nối cơ sở dữ liệu
            if (con.State == ConnectionState.Closed)
            {
                MessageBox.Show("Không thể kết nối tới cơ sở dữ liệu.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Lấy MSSV từ textbox
            string mssv = txtMSSV.Text.Trim();
            if (string.IsNullOrEmpty(mssv))
            {
                MessageBox.Show("Vui lòng nhập MSSV để tìm kiếm.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Truy vấn cơ sở dữ liệu với JOIN để lấy dữ liệu từ SinhVien và Phong
            string query = @"SELECT SV.Mssv, SV.HoTen, SV.Lop, SV.CCCD, SV.DiaChi, SV.SDT, SV.GioiTinh, SV.NgaySinh, SV.MaPhong, SV.Email, P.TenPhong
                            FROM SinhVien SV JOIN Phong P ON SV.MaPhong = P.MaPhong                           
                            WHERE SV.Mssv = @Mssv";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@Mssv", mssv);

            try
            {
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable table = new DataTable();
                adapter.Fill(table);

                if (table.Rows.Count > 0) // Nếu tìm thấy sinh viên
                {
                    MessageBox.Show("Đã tìm thấy sinh viên!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // Hiển thị dữ liệu trong DataGridView
                    dgvSinhVien.DataSource = table;

                    // Đổ dữ liệu từ kết quả tìm kiếm vào các control
                    DataRow row = table.Rows[0];
                    txtMSSVT.Text = row["Mssv"].ToString();
                    txtHoTen.Text = row["HoTen"].ToString();
                    txtLop.Text = row["Lop"].ToString();
                    txtCCCD.Text = row["CCCD"].ToString();
                    txtDiaChi.Text = row["DiaChi"].ToString();
                    txtSDT.Text = row["SDT"].ToString();
                    txtMaPhong.Text = row["TenPhong"].ToString();
                    txtEmail.Text = row["Email"].ToString();

                    // Chuyển đổi ngày tháng
                    if (DateTime.TryParse(row["NgaySinh"].ToString(), out DateTime ngaySinh))
                    {
                        dtpNgaySinh.Value = ngaySinh;
                    }

                    // Xử lý giới tính
                    int gioiTinh = Convert.ToInt32(row["GioiTinh"]);
                    rdNam.Checked = gioiTinh == 0;
                    rdNu.Checked = gioiTinh == 1;

                    // Bật các điều khiển sau khi tìm kiếm
                    EnableFormControls(true);
                }
                else
                {
                    MessageBox.Show("Không tìm thấy sinh viên với MSSV này.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    dgvSinhVien.DataSource = null; // Xóa dữ liệu cũ
                    ClearFormControls();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            //làm cho các nút mờ đi
            txtCCCD.Enabled = false;
            txtDiaChi.Enabled = false;
            txtHoTen.Enabled = false;
            txtLop.Enabled = false;
            txtMSSVT.Enabled = false;
            txtSDT.Enabled = false;
            txtMaPhong.Enabled = false;
            txtEmail.Enabled = false;
            rdNam.Enabled = false;
            rdNu.Enabled = false;
            dtpNgaySinh.Enabled = false;
        }

        private void dgvSinhVien_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvSinhVien.Columns[e.ColumnIndex].Name == "GioiTinh" && e.Value != null && e.Value != DBNull.Value)
            {
                int gioiTinh = Convert.ToInt32(e.Value); // Chuyển đổi an toàn sang int
                e.Value = gioiTinh == 0 ? "Nam" : "Nữ";
            }
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
    }
}
