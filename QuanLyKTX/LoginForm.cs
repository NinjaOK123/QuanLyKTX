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
    public partial class frmDangNhap : Form
    {
        private SqlConnection con;
        public frmDangNhap()
        {
            InitializeComponent();
            con = new SqlConnection();
        }
        // Phương thức kết nối đến cơ sở dữ liệu
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

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text.Trim();
            string password = txtPass.Text.Trim();

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tài khoản và mật khẩu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ConnectToDatabase();

            if (con.State == ConnectionState.Open)
            {
                try
                {
                    string query = "SELECT Role FROM [User] WHERE TenTaiKhoan = @username AND MatKhau = @password";
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@username", username);
                        cmd.Parameters.AddWithValue("@password", password);

                        SqlDataReader reader = cmd.ExecuteReader();

                        if (reader.Read())
                        {
                            string role = reader["Role"].ToString().ToLower();
                            reader.Close(); // Đóng reader trước khi mở form

                            if (role == "admin")
                            {
                                MainForm adminForm = new MainForm();
                                adminForm.userRole = role;  // Truyền giá trị role vào MainForm
                                adminForm.Show();           // Mở form MainForm cho admin
                                FormQuanLyDienNuoc userForm = new FormQuanLyDienNuoc();
                                userForm.userRole = role;   // Truyền giá trị role vào form QuanLyDienNuoc cho user
                            }
                            else if (role == "user")
                            {
                                FormQuanLyDienNuoc userForm = new FormQuanLyDienNuoc();
                                userForm.userRole = role;   // Truyền giá trị role vào form QuanLyDienNuoc cho user
                                userForm.Show();
                            }
                            else
                            {
                                MessageBox.Show("Vai trò không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Tên đăng nhập hoặc mật khẩu không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }

                        reader.Close();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi truy vấn dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    con.Close(); // Đảm bảo đóng kết nối
                }
            }
        }

        private void txtPass_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                btnDangNhap_Click(sender, e); // Gọi lại hàm đăng nhập khi nhấn Enter
            }
        }

        private void lblquenpass_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            QuênPassForm forgetPasswordForm = new QuênPassForm();
            forgetPasswordForm.Show();
            this.Hide();
        }

        private void lblregis_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RegisForm regisForm = new RegisForm();
            regisForm.Show();
            this.Hide();
        }
    }
}
