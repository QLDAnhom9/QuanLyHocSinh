using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G10_BTL.GUI
{
    public partial class fDangnhap : Form
    {
        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();
        public fDangnhap()
        {
            InitializeComponent();
        }

        private void Dangnhap_Load(object sender, EventArgs e)
        {
            label1.BackColor = Color.Transparent;
            label2.BackColor = Color.Transparent;
            label3.BackColor = Color.Transparent;
            txtname.Text = "admin";
            txtpasswold.Text = "admin123";
        }

        private void btnDangnhap_Click(object sender, EventArgs e)
        {
            GiaoVien gv = db.GiaoVien.Where(m => m.taiKhoan == txtname.Text && m.matKhau == txtpasswold.Text).SingleOrDefault();
            HocSinh hs = db.HocSinh.Where(m => m.taiKhoan == txtname.Text && m.matKhau == txtpasswold.Text).SingleOrDefault();
            if (gv == null && hs == null)
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu không chính xác!", "Đăng nhập không thành công", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            else
            {
                
                if(gv != null)
                {
                    QuanLyAdmin ql = new QuanLyAdmin();
                    this.Hide();
                    ql.Tag = gv;
                    ql.ShowDialog();
                    this.Close();
                }
                else
                {
                    QuanLyAdmin ql = new QuanLyAdmin();
                    this.Hide();
                    ql.Tag = hs;
                    ql.ShowDialog();
                    this.Close();
                }
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn thoát ứng dụng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if(result == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
