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
    public partial class fDoiMK : Form
    {
        public fDoiMK()
        {
            InitializeComponent();
        }

        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();
        private GiaoVien gv = new GiaoVien();
        private HocSinh hs = new HocSinh();

        private void Nhapdiem_Load(object sender, EventArgs e)
        {
            gv = this.Tag as GiaoVien;
            hs = this.Tag as HocSinh;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXaxNhan_Click(object sender, EventArgs e)
        {
            String matkhau="", matkhaumoi="", nlmatkhaumoi="";
            matkhau = txtMatKhauCu.Text;
            matkhaumoi = txtMatKhauMoi.Text;
            nlmatkhaumoi = txtNhapLaiMKMoi.Text;

            if(matkhau == "" || matkhaumoi == "" || nlmatkhaumoi == "")
            {
                MessageBox.Show("Vui lòng điền đầy đủ thông tin", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(gv !=null && matkhau != gv.matKhau)
            {
                MessageBox.Show("Mật khẩu cũ không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (hs != null && matkhau != hs.matKhau)
            {
                MessageBox.Show("Mật khẩu cũ không đúng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if(matkhaumoi != nlmatkhaumoi)
            {
                MessageBox.Show("Nhập lại mật khẩu mới không đúng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            bool ck = true;
            for(int i = 0; i < matkhaumoi.Length; i++)
            {
                if(matkhaumoi[i]>='a' && matkhaumoi[i]<='z' || matkhaumoi[i] >= 'A' && matkhaumoi[i] <= 'A' || matkhaumoi[i] >= '0' && matkhaumoi[i] <= '9')
                {
                    continue;
                }
                else
                {
                    ck = false;
                }
            }
            if (!ck)
            {
                MessageBox.Show("Mật khẩu mới không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (gv != null)
            {
                GiaoVien giaovien = db.GiaoVien.Where(m => m.maGV == gv.maGV).FirstOrDefault(); 
                giaovien.matKhau = matkhaumoi;
                MessageBox.Show(matkhaumoi, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                db.SaveChanges();
                MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            if (hs != null)
            {
                HocSinh hocsinh = db.HocSinh.Where(m => m.maHS == hs.maHS).FirstOrDefault();
                hocsinh.matKhau = matkhaumoi;
                db.SaveChanges();
                MessageBox.Show("Đổi mật khẩu thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            this.Close();
        }
    }
}
