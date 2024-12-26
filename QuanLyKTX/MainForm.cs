using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuanLyKTX
{
    public partial class MainForm : Form
    {
        public string userRole { get; set; }
        //Hiện form con trong form cha
        private Form currentFormChild;
        public void OpenChildForm(Form childForm)
        {
            if(currentFormChild != null) 
                currentFormChild.Close();
            currentFormChild = childForm;
            childForm.TopLevel = false;
            childForm.FormBorderStyle = FormBorderStyle.None;
            childForm.Dock = DockStyle.Fill;
            panel_Body.Controls.Add(childForm);
            panel_Body.Tag = childForm;
            childForm.BringToFront();
            childForm.Show();
        }
        public MainForm()
        {
            InitializeComponent();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDangNhap lg = new frmDangNhap();
            lg.Show();
            this.Close();
        }

        private void quảnLýPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQuanLyPhong());
        }

        private void quảnLýSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormQuanLySinhVien());
        }

        private void hóaĐơnĐiệnNướcToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormQuanLyDienNuoc formQuanLyDienNuoc = new FormQuanLyDienNuoc();
            formQuanLyDienNuoc.userRole = this.userRole;  // Truyền giá trị userRole vào form QuanLyDienNuoc
            OpenChildForm(formQuanLyDienNuoc);
        }

        private void tìmSinhViênToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTimKiemSinhVien());
        }

        private void tìmPhòngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenChildForm(new FormTimKiemPhong());
        }
    }
}
