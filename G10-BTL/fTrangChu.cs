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
    public partial class fTrangChu : Form
    {
        public fTrangChu()
        {
            InitializeComponent();
        }

        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();
        HocSinh hs = new HocSinh();
        GiaoVien gv = new GiaoVien();

        private void TrangChu_Load(object sender, EventArgs e)
        {
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            hs = this.Tag as HocSinh;
            gv = this.Tag as GiaoVien;
            if(gv!=null && gv.ten == "admin")
            {
                lbChucVu.Text = "Admin";
                lbTenNguoiDung.Text = "Admin";
            }
            else if(hs!=null)
            {
                HocSinh hocsinh = db.HocSinh.Where(m => m.maHS == hs.maHS).SingleOrDefault();
                lbChucVu.Text = "Học sinh";
                lbTenNguoiDung.Text = hocsinh.ten.ToString();
            }
            else
            {
                GiaoVien giaovine = db.GiaoVien.Where(m => m.maGV == gv.maGV).SingleOrDefault();
                lbChucVu.Text = "Giáo viên";
                lbTenNguoiDung.Text = giaovine.ten.ToString();
            }
        }

        private void btnDoiMatKhau_Click(object sender, EventArgs e)
        {
            fDoiMK doiMK = new fDoiMK();
            if (gv != null)
            {
                doiMK.Tag = gv;
            }
            else
            {
                doiMK.Tag = hs;
            }
            doiMK.ShowDialog();
            TrangChu_Load(sender, e);
        }

    }
}
