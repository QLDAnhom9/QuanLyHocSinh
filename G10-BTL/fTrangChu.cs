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
            if(gv!=null && gv.ten == Settings.ADMIN)
            {
                lbChucVu.Text = Settings.ADMIN.ToUpper();
                lbTenNguoiDung.Text = Settings.ADMIN.ToUpper();
            }
            else if(hs!=null)
            {
                HocSinh hocsinh = db.HocSinh.Where(m => m.maHS == hs.maHS).SingleOrDefault();
                lbChucVu.Text = Settings.HOC_SINH;
                lbTenNguoiDung.Text = hocsinh.ten.ToString();
            }
            else
            {
                GiaoVien giaovien = db.GiaoVien.Where(m => m.maGV == gv.maGV).SingleOrDefault();
                lbChucVu.Text = Settings.GIAO_VIEN;
                lbTenNguoiDung.Text = giaovien.ten.ToString();
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
