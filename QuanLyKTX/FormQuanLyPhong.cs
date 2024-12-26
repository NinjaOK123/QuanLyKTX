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
    public partial class FormQuanLyPhong : Form
    {
        SqlConnection con = new SqlConnection();
        //Mã phòng (dùng trong trường hợp sửa)
        string maPhong = "";
        string maLoai = "";

        public FormQuanLyPhong()
        {
            InitializeComponent();
            dgvPhong.AutoGenerateColumns = false;
        }

        private void FormQuanLyPhong_Load(object sender, EventArgs e)
        {
            //Kết nối tới CSDL
            if (con.State == ConnectionState.Closed)
            {
                con.ConnectionString = @"Server=ADMIN-PC\MSSQLSERVER1;Database=QLKTX;Integrated Security=True";
                con.Open();
            }

            //Hiển thị dữ liệu các dãy phòng lên comboBox cboDayPhong
            string dayPhongSql = @"SELECT DISTINCT * from DayKTX";                            
            SqlDataAdapter dayPhongAdapter = new SqlDataAdapter(dayPhongSql,con);
            DataTable tableDayPhong = new DataTable();
            dayPhongAdapter.Fill(tableDayPhong);
            cboDayPhong.DataSource = tableDayPhong;
            cboDayPhong.DisplayMember = "TenDay";
            cboDayPhong.ValueMember = "MaDay";

            //Hiển thị dữ liệu của bảng phòng lên comboBox cboLoaiPhong
            string loaiPhongSql = @"SELECT DISTINCT * from LoaiPhong";
            SqlDataAdapter loaiPhongAdapter = new SqlDataAdapter(loaiPhongSql, con);
            DataTable tableLoaiPhong = new DataTable();
            loaiPhongAdapter.Fill(tableLoaiPhong);
            cboLoaiPhong.DataSource = tableLoaiPhong;
            cboLoaiPhong.DisplayMember = "TenLoai";
            cboLoaiPhong.ValueMember = "MaLoai";

            //Hiển thị dữ liệu lên dgvPhong
            string phongSql = @"SELECT P.*, Lp.TenLoai, Lp.GiaPhong, D.TenDay 
                                from Phong P, LoaiPhong Lp, DayKTX D
                                where P.MaLoai = Lp.MaLoai and P.MaDay = D.MaDay";
            SqlDataAdapter phongAdapter = new SqlDataAdapter(phongSql,con);
            DataTable tablePhong = new DataTable();
            phongAdapter.Fill(tablePhong);
            dgvPhong.DataSource = tablePhong;        

            // Khi click vào DataGridView thì hiển thị dữ liệu của dòng được chọn lên control tương ứng
            txtGiaPhong.DataBindings.Clear();  
            txtMaPhong.DataBindings.Clear();
            txtTenPhong.DataBindings.Clear();
            txtSoNguoiToiDa.DataBindings.Clear();
            cboLoaiPhong.DataBindings.Clear();
            cboDayPhong.DataBindings.Clear();

            if (dgvPhong.Rows.Count == 0)
            {
                txtGiaPhong.Text = "";
                txtMaPhong.Text = "";
                txtSoNguoiToiDa.Text = "";
                txtTenPhong.Text = "";
                cboDayPhong.Text = "";
                cboLoaiPhong.Text = "";
            }

            txtMaPhong.DataBindings.Add("Text",dgvPhong.DataSource,"MaPhong",false,DataSourceUpdateMode.Never);
            txtTenPhong.DataBindings.Add("Text",dgvPhong.DataSource,"TenPhong",false,DataSourceUpdateMode.Never);
            txtSoNguoiToiDa.DataBindings.Add("Text", dgvPhong.DataSource, "SoNguoiToiDa", false, DataSourceUpdateMode.Never);
            cboDayPhong.DataBindings.Add("SelectedValue",dgvPhong.DataSource,"MaDay",false, DataSourceUpdateMode.Never);
            cboLoaiPhong.DataBindings.Add("SelectedValue", dgvPhong.DataSource, "MaLoai", false, DataSourceUpdateMode.Never);
            txtGiaPhong.DataBindings.Add("Text", dgvPhong.DataSource, "GiaPhong", false, DataSourceUpdateMode.Never);   

            //Làm mờ nút lưu, hủy
            btnHuy.Enabled = false;
            btnLuu.Enabled = false;

            //Làm mờ các control
            txtMaPhong.Enabled = false;
            txtTenPhong.Enabled = false;
            txtGiaPhong.Enabled = false;
            txtSoNguoiToiDa.Enabled = false;
            cboDayPhong.Enabled = false;
            cboLoaiPhong.Enabled = false;

            //Làm sáng nút Thêm, sửa, xóa
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            //Đánh dấu thêm mới
            maPhong = "";
            maLoai = "";

            //Xóa trắng các trường
            txtGiaPhong.Text = "";
            txtMaPhong.Text = "";
            txtSoNguoiToiDa.Text = "";
            txtTenPhong.Text = "";
            cboDayPhong.Text = "";
            cboLoaiPhong.Text = "";
            lblSoLuong.Text = 0 + "";

            //Làm mờ nút Thêm mới, Sửa và Xóa
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // Làm sáng nút Lưu và Hủy
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;

            //Làm sáng các control
            cboDayPhong.Enabled = true;
            cboLoaiPhong.Enabled = true;
            txtMaPhong.Enabled = true;
            txtTenPhong.Enabled = true;
            txtSoNguoiToiDa.Enabled = true;
            txtGiaPhong.Enabled = true;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            //Kiểm tra dữ liệu
            int soNguoiToiDa = 10; // Số người tối đa
            int soNguoiThucTe; // Kiểm tra nếu giá trị nhập là số hợp lệ

            //Kiểm tra nếu số người thực tế vượt quá số lượng tối đa
            if (int.TryParse(txtSoNguoiToiDa.Text, out soNguoiThucTe))
            {
                if (soNguoiThucTe > soNguoiToiDa)
                    MessageBox.Show("Số người tối đa phải nhỏ hơn hoặc bằng 8!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (soNguoiThucTe < 0)
                    MessageBox.Show("Vui lòng nhập một số hợp lệ!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            if (cboDayPhong.Text.Trim() == "")                                                                          //Kiểm tra chọn dãy phòng
                MessageBox.Show("Chưa chọn dãy phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (cboLoaiPhong.Text.Trim() == "")                                                                    //Kiểm tra chọn loại phòng
                MessageBox.Show("Chưa chọn loại phòng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtMaPhong.Text.Trim() == "")                                                                      //Kiểm tra mã phòng có để trống không
                MessageBox.Show("Mã phòng không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //else if(isExist)                                                                                            //check mã phòng đã tồn tại hay chưa
            //    MessageBox.Show("Mã phòng đã tồn tại.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error); 
            else if (txtTenPhong.Text.Trim() == "")                                                                     //Kiểm tra tên phòng có để trống không
                MessageBox.Show("Tên phòng không được để trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (txtSoNguoiToiDa.Text.Trim() == "")                                                                  //Kiểm tra số người tối đa có để trống không
                MessageBox.Show("Số người tối đa không được bỏ trống!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else
            {
                try
                {
                    //Thêm mới
                    if (maPhong == "" && maLoai == "")
                    {
                        string sql = @"INSERT INTO Phong VALUES(@MaPhong, @MaDay, @MaLoai, @TenPhong, @SoNguoiToiDa)";
                        SqlCommand cmd = new SqlCommand(sql,con);
                        cmd.Parameters.Add(@"MaDay",SqlDbType.VarChar,6).Value = cboDayPhong.SelectedValue.ToString();
                        cmd.Parameters.Add(@"MaLoai",SqlDbType.VarChar,10).Value = cboLoaiPhong.SelectedValue.ToString();
                        cmd.Parameters.Add(@"MaPhong", SqlDbType.VarChar, 6).Value = txtMaPhong.Text;
                        cmd.Parameters.Add(@"TenPhong", SqlDbType.NVarChar, 30).Value = txtTenPhong.Text;
                        cmd.Parameters.Add(@"SoNguoiToiDa", SqlDbType.Int).Value = soNguoiThucTe;                       
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Thêm phòng mới thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else //Sửa
                    {
                        string sql = @"UPDATE Phong
                                       SET  MaPhong = @MaPhong,
                                            MaDay = @MaDay,
                                            MaLoai = @MaLoai,
                                            TenPhong = @TenPhong,
                                            SoNguoiToiDa = @SoNguoiToiDa
                                       WHERE MaPhong = @MaPhongCu";  

                        string sqlGP = @"UPDATE LoaiPhong
                                       SET  GiaPhong = @GiaPhongMoi
                                       WHERE MaLoai = @MaLoaiPhongCu";

                        SqlCommand cmdGp = new SqlCommand(sqlGP,con);
                        SqlCommand cmd = new SqlCommand(sql, con);
                        cmd.Parameters.Add(@"MaPhong", SqlDbType.VarChar, 6).Value = txtMaPhong.Text;
                        cmd.Parameters.Add(@"MaPhongCu", SqlDbType.VarChar, 6).Value = maPhong;
                        cmd.Parameters.Add(@"MaDay", SqlDbType.VarChar, 6).Value = cboDayPhong.SelectedValue.ToString();
                        cmd.Parameters.Add(@"MaLoai", SqlDbType.VarChar, 10).Value = cboLoaiPhong.SelectedValue.ToString();
                        cmd.Parameters.Add(@"TenPhong", SqlDbType.VarChar, 30).Value = txtTenPhong.Text;
                        cmd.Parameters.Add(@"SoNguoiToiDa", SqlDbType.Int).Value = soNguoiThucTe;
                        cmdGp.Parameters.Add(@"GiaPhongMoi",SqlDbType.Float).Value = float.Parse(txtGiaPhong.Text);
                        cmdGp.Parameters.Add(@"MaLoai", SqlDbType.VarChar, 10).Value = cboLoaiPhong.SelectedValue.ToString();
                        cmdGp.Parameters.Add(@"MaLoaiPhongCu", SqlDbType.VarChar, 10).Value = maLoai;
                        cmdGp.ExecuteNonQuery();
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Chỉnh sửa phòng thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    //Tải lại form
                    FormQuanLyPhong_Load(sender, e);
                }
                catch(Exception ex)
                {
                    MessageBox.Show("Lỗi trùng mã phòng","Lỗi",MessageBoxButtons.OK,MessageBoxIcon.Error);
                }
            }          
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            //Đánh dấu là cập nhật
            maPhong = txtMaPhong.Text;
            maLoai = cboLoaiPhong.SelectedValue.ToString();

            //Làm mờ nút Thêm mới, Sửa và Xóa
            btnThem.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;

            // Làm sáng nút Lưu và Hủy
            btnHuy.Enabled = true;
            btnLuu.Enabled = true;

            //Làm sáng các control
            cboDayPhong.Enabled = true;
            cboLoaiPhong.Enabled = true;
            txtMaPhong.Enabled = true;
            txtTenPhong.Enabled = true;
            txtSoNguoiToiDa.Enabled = true;
            txtGiaPhong.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult kq;
            kq = MessageBox.Show("Bạn có muốn xóa phòng " + txtMaPhong.Text + " không?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(dgvPhong.SelectedRows.Count > 0)
            {
                if (kq == DialogResult.Yes)
                {
                    // Kiểm tra xem có sinh viên nào đang ở trong phòng này không
                    string checkQuery = "SELECT COUNT(*) FROM SinhVien WHERE MaPhong = @MaPhong";
                    SqlCommand checkCmd = new SqlCommand(checkQuery, con);
                    checkCmd.Parameters.Add("@MaPhong", SqlDbType.VarChar, 6).Value = txtMaPhong.Text;
                    int studentCount = (int)checkCmd.ExecuteScalar();   //Thực thi truy vấn và trả về 1 giá trị duy nhất

                    // Kiểm tra xem còn hóa đơn nào không
                    string checkQueryHD = "SELECT COUNT(*) FROM DienNuoc WHERE MaPhong = @MaPhong";
                    SqlCommand checkCmdHD = new SqlCommand(checkQueryHD, con);
                    checkCmdHD.Parameters.Add("@MaPhong", SqlDbType.VarChar, 6).Value = txtMaPhong.Text;
                    int hoaDonCount = (int)checkCmdHD.ExecuteScalar();   //Thực thi truy vấn và trả về 1 giá trị duy nhất

                    //Thực hiện kiểm tra xem có sinh viên trong phòng không nếu có thì báo lỗi không cho xóa không thì thực hiện xóa
                    if (studentCount > 0)
                        MessageBox.Show("Không thể xóa phòng vì còn sinh viên đang ở.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else if (hoaDonCount > 0)   //Kiểm tra xem còn hóa đơn hay không
                        MessageBox.Show("Không thể xóa phòng vì vẫn còn các hóa đơn.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                    {
                        try
                        {
                            string sql = @"DELETE FROM Phong WHERE MaPhong = @MaPhong";
                            SqlCommand cmd = new SqlCommand(sql, con);
                            cmd.Parameters.Add("@MaPhong", SqlDbType.VarChar, 6).Value = txtMaPhong.Text;
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Xóa phòng thành công.", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            // Tải lại form
                            FormQuanLyPhong_Load(sender, e);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show($"Lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
                MessageBox.Show("Vui lòng chọn phòng để xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            FormQuanLyPhong_Load(sender, e);
        }

        private void cboLoaiPhong_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboLoaiPhong.SelectedValue != null)
            {
                string selectedMaLoai = cboLoaiPhong.SelectedValue.ToString(); 
                UpdateGiaPhong(selectedMaLoai);
            }
        }

        private void UpdateGiaPhong(string maLoai)
        {
            string query = "SELECT GiaPhong FROM LoaiPhong WHERE MaLoai = @MaLoai"; 
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@MaLoai", maLoai); 
            object result = cmd.ExecuteScalar(); 
            if (result != null)
                txtGiaPhong.Text = result.ToString();  
        }

        private void CountSinhVien(string maPhong)
        {
            string query = @"select Count(sv.Mssv) 
                              from Phong p left join SinhVien sv on p.MaPhong = sv.MaPhong 
                              where p.MaPhong = @MaPhong
                              group by p.MaPhong";                        
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue(@"MaPhong",maPhong);
            object result = cmd.ExecuteScalar();
            if (result != null)
                lblSoLuong.Text = result.ToString();
        }

        private void txtMaPhong_TextChanged(object sender, EventArgs e)
        {
            if(txtMaPhong.Text.Trim() != "")
            {
                CountSinhVien(txtMaPhong.Text);
            }
            // Lấy vị trí con trỏ hiện tại
            int cursorPosition = txtMaPhong.SelectionStart;

            // Chuyển nội dung trong TextBox thành chữ hoa
            txtMaPhong.Text = txtMaPhong.Text.ToUpper();

            // Đặt lại con trỏ ở đúng vị trí
            txtMaPhong.SelectionStart = cursorPosition;
        }
    }
}
