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
    public partial class RegisForm : Form
    {
        public RegisForm()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPass.Text;
            string password1 = txtPass1.Text;
            string email = txtEmail.Text;

            // Kiểm tra nếu tên tài khoản, mật khẩu và email không rỗng
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(password1) || string.IsNullOrEmpty(email))
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra nếu mật khẩu và mật khẩu nhập lại có giống nhau không
            if (password != password1)
            {
                MessageBox.Show("Mật khẩu nhập lại không khớp!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kiểm tra email có hợp lệ không
            if (!IsValidEmail(email))
            {
                MessageBox.Show("Địa chỉ email không hợp lệ! Địa chỉ email hợp lệ là tên+mssv@student.agu.edu.vn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Kết nối đến cơ sở dữ liệu
            string connectionString = @"Server=ADMIN-PC\MSSQLSERVER1;Database=QLKTX;Integrated Security=True"; // Thay đổi đường dẫn kết nối của bạn
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                // Kiểm tra xem tên tài khoản đã tồn tại chưa
                string checkUserQuery = "SELECT COUNT(*) FROM [User] WHERE TenTaiKhoan = @username";
                using (SqlCommand cmd = new SqlCommand(checkUserQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);

                    int userExists = (int)cmd.ExecuteScalar();
                    if (userExists > 0)
                    {
                        MessageBox.Show("Tên tài khoản đã tồn tại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Kiểm tra xem email đã tồn tại chưa
                string checkEmailQuery = "SELECT COUNT(*) FROM [User] WHERE Email = @email";
                using (SqlCommand cmd = new SqlCommand(checkEmailQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@email", email);

                    int emailExists = (int)cmd.ExecuteScalar();
                    if (emailExists > 0)
                    {
                        MessageBox.Show("Email đã được sử dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                }

                // Nếu tên tài khoản và email đều chưa tồn tại, thêm mới vào bảng User
                string insertQuery = "INSERT INTO [User] (TenTaiKhoan, MatKhau, Email, Role) VALUES (@username, @password, @email, 'user')";
                using (SqlCommand cmd = new SqlCommand(insertQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@password", password);
                    cmd.Parameters.AddWithValue("@email", email);

                    cmd.ExecuteNonQuery();
                }

                MessageBox.Show("Đăng ký thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close(); // Đóng form đăng ký sau khi đăng ký thành công
                frmDangNhap loginForm = new frmDangNhap();
                loginForm.Show();

                // Đóng form đăng ký
                this.Close();

            }
        }
        // Kiểm tra tính hợp lệ của email
        private bool IsValidEmail(string email)
        {
            try
            {
                var mail = new System.Net.Mail.MailAddress(email);
                return mail.Address == email;
            }
            catch
            {
                return false;
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
