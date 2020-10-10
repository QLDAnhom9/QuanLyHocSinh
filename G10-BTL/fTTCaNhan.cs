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
    public partial class fTTCaNhan : Form
    {
        public fTTCaNhan()
        {
            InitializeComponent();
        }

        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();
        HocSinh hs = new HocSinh();
        GiaoVien gv = new GiaoVien();

        private void TTCaNhan_Load(object sender, EventArgs e)
        {
            dtpNgaySinh.Text = DateTime.Now.Day.ToString() + "/" + DateTime.Now.Month.ToString() + "/" + DateTime.Now.Year.ToString();
            hs = this.Tag as HocSinh;
            gv = this.Tag as GiaoVien;
            if(hs!=null)
            {
                lbTaiKhoan.Text = hs.taiKhoan.ToString();
                lbTenNguoiDung.Text = hs.ten;
                lbGioiTinh.Text = hs.gioiTinh;
                if(hs.gioiTinh == "Nam")
                {
                    rdbNam.Checked = true;
                }
                else
                {
                    rdbNu.Checked = true;
                }
                lbDiaChi.Text = hs.diaChi;
                lbSDT.Text = "0" + hs.sdt.ToString();
                lbBangCap.Visible = false;
                lbTieDeBangCap.Visible = false;
                txtBanCap.Visible = false;
            }
            else
            {
                lbTaiKhoan.Text = gv.taiKhoan;
                lbTenNguoiDung.Text = gv.ten;
                lbGioiTinh.Text = gv.gioiTinh;
                if (gv.gioiTinh == "Nam")
                {
                    rdbNam.Checked = true;
                }
                else
                {
                    rdbNu.Checked = true;
                }
                lbDiaChi.Text = gv.diaChi;
                lbSDT.Text = "0" + gv.sdt.ToString();
                lbBangCap.Visible = true;
                lbTieDeBangCap.Visible = true;
                txtBanCap.Visible = true;
                lbBangCap.Text = gv.bangCap;
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            String taikhoan = "", tennguoidung = "", gioitinh = "", ngaysinh = "", diachi = "", sdt = "", bangcap = "";
            taikhoan = txtTaiKhoan.Text;
            tennguoidung = txtTenNguoiDung.Text;
            if (rdbNam.Checked)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nu";
            }
            ngaysinh = dtpNgaySinh.Text;
            diachi = txtDiaChi.Text;
            sdt = txtSDT.Text;
            bool ck = true;
            for(int i = 0; i < taikhoan.Length; i++)
            {
                if (taikhoan[i] >= 'a' && taikhoan[i] <= 'z' || taikhoan[i] >= 'A' && taikhoan[i] <= 'A' || taikhoan[i] >= '0' && taikhoan[i] <= '9')
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
                MessageBox.Show("Tài khoản không hớp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            for (int i = 0; i < sdt.Length; i++)
            {
                if (sdt[i] >= '0' && sdt[i] <= '9')
                {
                    continue;
                }
                else
                {
                    ck = false;
                }
            }
            if (!ck || sdt.Length != 9 || (sdt[0]!='9' && sdt[0]!='3'))
            {
                MessageBox.Show("Số điện thoạt không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (hs!=null)
            {
                HocSinh hocsinh = db.HocSinh.Where(m => m.maHS == hs.maHS).SingleOrDefault();
                hocsinh.taiKhoan = taikhoan;
                hocsinh.diaChi = diachi;
                hocsinh.gioiTinh = gioitinh;
                hocsinh.ngaySinh = DateTime.Parse(ngaysinh);
                hocsinh.sdt = sdt;
                hocsinh.ten = tennguoidung;
                db.SaveChanges();
                MessageBox.Show("Sửa thông tin thành công", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Question);
                Close();
            }
            else
            {
                bangcap = txtBanCap.Text;
                GiaoVien giaovien = db.GiaoVien.Where(m => m.maGV == gv.maGV).SingleOrDefault();
                giaovien.diaChi = diachi;
                giaovien.gioiTinh = gioitinh;
                giaovien.ngaySinh = DateTime.Parse(ngaysinh);
                giaovien.sdt = sdt;
                giaovien.ten = tennguoidung;
                giaovien.bangCap = bangcap;
                giaovien.taiKhoan = taikhoan;
                db.SaveChanges();
                MessageBox.Show("Sửa thông tin thành công", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Question);
                Close();
            }
        }
    }
}
