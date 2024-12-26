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
using QuanLyKTX;

namespace QuanLyKTX
{
    public partial class QuênPassForm : Form
    {
        public QuênPassForm()
        {
            InitializeComponent();
            txtEmail.PasswordChar = '\0';
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLaylaimatkhau_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text;

            // Kiểm tra nếu email không rỗng
            if (string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng nhập email!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kết nối đến cơ sở dữ liệu
            string connectionString = @"Server=ADMIN-PC\MSSQLSERVER1;Database=QLKTX;Integrated Security=True"; // Thay đổi theo kết nối của bạn
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Kiểm tra xem email có tồn tại trong cơ sở dữ liệu không
                string checkEmailQuery = "SELECT COUNT(*) FROM [User] WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(checkEmailQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@Email", email);
                    int emailExists = (int)cmd.ExecuteScalar();

                    if (emailExists == 0)
                    {
                        MessageBox.Show("Email không tồn tại trong hệ thống!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Tạo mật khẩu ngẫu nhiên mới
                string newPassword = GenerateRandomPassword();

                // Cập nhật mật khẩu mới vào cơ sở dữ liệu
                string updatePasswordQuery = "UPDATE [User] SET MatKhau = @MatKhau WHERE Email = @Email";
                using (SqlCommand cmd = new SqlCommand(updatePasswordQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@MatKhau", newPassword);
                    cmd.Parameters.AddWithValue("@Email", email);
                    cmd.ExecuteNonQuery();
                }

                // Hiển thị mật khẩu mới
                MessageBox.Show("Mật khẩu mới của bạn là: " + newPassword, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                frmDangNhap loginForm = new frmDangNhap();
                loginForm.Show();

                // Đóng form đăng ký
                this.Close();
            }
        }
        private string GenerateRandomPassword()
        {
            var random = new Random();
            const string validChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*";
            StringBuilder password = new StringBuilder();
            for (int i = 0; i < 8; i++) // Mật khẩu có độ dài 8 ký tự
            {
                password.Append(validChars[random.Next(validChars.Length)]);
            }
            return password.ToString();
        }
    }
}
