﻿using System;
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
    public partial class fQuanlyHocSinh : Form
    {
        GiaoVien gv = new GiaoVien();
        public fQuanlyHocSinh()
        {
            InitializeComponent();
            
        }

        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();

        private void QuanlyHocSinh_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            dgvData.Rows.Clear();
            gv = this.Tag as GiaoVien;

            dtpNgaySinh.Text = DateTime.Now.ToString();
            dtpNgayVaoHoc.Text = DateTime.Now.ToString();
            txtMaHocSinh.Enabled = false;
            txtMatKhau.Enabled = false;
            
            if (db.HocSinh.Count() > 0)
            {
                txtMaHocSinh.Text = (db.HocSinh.Max(m => m.maHS + 1).ToString());
            }
            else
            {
                txtMaHocSinh.Text = "2020000";
            }
            txtMatKhau.Text = txtMaHocSinh.Text;
            rdbNam.Checked = true;
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtTenHocSinh.Text = "";

            if (gv.ten == "admin")
            {
                List<HocSinh> lisths = db.HocSinh.Where(m => m.trangThai == true).ToList();
                foreach (HocSinh i in lisths)
                {
                    String lop = db.Lop.Where(m => m.maLop == i.maLop).Select(m => m.tenLop).FirstOrDefault();
                    dgvData.Rows.Add(i.maHS, i.ten, i.ngaySinh, i.gioiTinh, i.diaChi, i.sdt, lop, i.tgBatDau);
                }

                cbbLop.DataSource = db.Lop.Where(m => m.trangThai == true).ToList();
                cbbLop.DisplayMember = "TenLop";
                cbbLop.ValueMember = "MaLop";
                cbbLop.SelectedIndex = -1;
                cbbLop.SelectedText = "Chọn lớp";

                cbbLopTimKiem.DataSource = db.Lop.Where(m => m.trangThai == true).ToList();
                cbbLopTimKiem.DisplayMember = "TenLop";
                cbbLopTimKiem.ValueMember = "MaLop";
                cbbLopTimKiem.SelectedIndex = -1;
                cbbLopTimKiem.SelectedText = "Chọn lớp";
            }
            else{
                Lop x = db.Lop.Where(m => m.trangThai == true && m.maGVCN == gv.maGV).FirstOrDefault();
                List<HocSinh> lisths = db.HocSinh.Where(m => m.trangThai == true && m.maLop == x.maLop).ToList();
                foreach (HocSinh i in lisths)
                {
                    dgvData.Rows.Add(i.maHS, i.ten, i.ngaySinh, i.gioiTinh, i.diaChi, i.sdt, x.tenLop, i.tgBatDau);
                }

                cbbLop.DataSource = db.Lop.Where(m => m.trangThai == true && m.maGVCN == gv.maGV).ToList();
                cbbLop.DisplayMember = "TenLop";
                cbbLop.ValueMember = "MaLop";
                cbbLop.SelectedIndex = 0;
                cbbLop.Enabled = false;

                cbbLopTimKiem.DataSource = db.Lop.Where(m => m.trangThai == true && m.maGVCN == gv.maGV).ToList();
                cbbLopTimKiem.DisplayMember = "TenLop";
                cbbLopTimKiem.ValueMember = "MaLop";
                cbbLopTimKiem.SelectedIndex = 0;
                cbbLopTimKiem.Enabled = false;
            }
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            
            
        }

        private bool KiemTra()
        {
            string tenhs = "", tendangnhap = "", matkhau = "",  sdt = "", diachi = "";
            if(cbbLop.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn lớp", " Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            tendangnhap = txtMaHocSinh.Text;
            matkhau = txtMatKhau.Text;
            sdt = txtSDT.Text;
            if(txtTenHocSinh.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên", " Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if(tendangnhap == "")
            {
                MessageBox.Show("Bạn chưa nhập tên đăng nhập", " Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            if(matkhau == "")
            {
                MessageBox.Show("Bạn chưa nhập mật khẩu", " Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            bool ck = true;
            for (int i = 0; i < matkhau.Length; i++)
            {
                if (matkhau[i] >= 'a' && matkhau[i] <= 'z' || matkhau[i] >= 'A' && matkhau[i] <= 'A' || matkhau[i] >= '0' && matkhau[i] <= '9')
                {
                    continue;
                }
                else
                {
                    ck = false;
                    break;
                }
            }
            if (!ck)
            {
                MessageBox.Show("Mật khẩu không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
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
                    break;
                }
            }
            if (!ck || sdt.Length!=10 || sdt[0]!='0')
            {
                MessageBox.Show("Số điện thoại không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
            return true;
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {
            if (!KiemTra())
            {
                return;
            }
            string tenhs = "", tendangnhap = "", matkhau = "", gioitinh = "", sdt = "", diachi = "";
            DateTime ngaysinh, ngayvaohoc;
            int malop, mahs;
            mahs = int.Parse(txtMaHocSinh.Text);
            HocSinh hocSinh = db.HocSinh.Where(m => m.maHS == mahs).FirstOrDefault();
            if(hocSinh != null)
            {
                return;
            }
            tenhs = txtTenHocSinh.Text;
            tendangnhap = txtMaHocSinh.Text;
            matkhau = txtMatKhau.Text;
            if (rdbNam.Checked)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nu";
            }
            malop = int.Parse(cbbLop.SelectedValue.ToString());
            sdt = txtSDT.Text;
            diachi = txtDiaChi.Text;
            ngaysinh = DateTime.Parse(dtpNgaySinh.Value.ToString());
            ngayvaohoc = DateTime.Parse(dtpNgayVaoHoc.Value.ToString());
            HocSinh hs = new HocSinh();
            hs.maHS = mahs;
            hs.taiKhoan = tendangnhap;
            hs.matKhau = matkhau;
            hs.ten = tenhs;
            hs.gioiTinh = gioitinh;
            hs.ngaySinh = ngaysinh;
            hs.sdt = sdt;
            hs.diaChi = diachi;
            hs.tgBatDau = ngayvaohoc;
            hs.trangThai = true;
            hs.maLop = malop;
            db.HocSinh.Add(hs);
            db.SaveChanges();
            MessageBox.Show("Thêm mới học sinh " + tenhs + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            QuanlyHocSinh_Load(sender, e);
            return;
        }

        private void btnLocLop_Click(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();
            string tenhs = "";
            int mahs;
            tenhs = txtTenHSTimKiem.Text;
            List<HocSinh> lisths = db.HocSinh.Where(m => m.trangThai == true).ToList();
            if(cbbLopTimKiem.SelectedIndex != -1)
            {
                int malop = int.Parse(cbbLopTimKiem.SelectedValue.ToString());
                lisths = lisths.Where(m => m.maLop == malop).ToList();
            }
            if (txtMaHSTimKiem.Text != "")
            {
                try
                {
                    mahs = int.Parse(txtMaHSTimKiem.Text);
                }
                catch (FormatException x)
                {
                    MessageBox.Show("Mã học sinh không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                lisths = lisths.Where(m => m.maHS.ToString().Contains(mahs.ToString())).ToList();
            }
            if (tenhs != "")
            {
                lisths = lisths.Where(m => m.ten.Contains(tenhs)).ToList();
            }
            foreach (HocSinh i in lisths)
            {
                String lop = db.Lop.Where(m => m.maLop == i.maLop).Select(m => m.tenLop).FirstOrDefault();
                dgvData.Rows.Add(i.maHS, i.ten, i.ngaySinh, i.gioiTinh, i.diaChi, i.sdt, lop, i.tgBatDau);
            }
        }

        private void btnLocHS_Click(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();
            string tenhs = "";
            int mahs;
            tenhs = txtTenHSTimKiem.Text;
            List<HocSinh> lisths = db.HocSinh.Where(m => m.trangThai == true).ToList();
            if(txtMaHSTimKiem.Text != "")
            {
                try
                {
                    mahs = int.Parse(txtMaHSTimKiem.Text);
                }
                catch(FormatException x)
                {
                    MessageBox.Show("Mã học sinh không hợp lệ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                lisths = lisths.Where(m => m.maHS.ToString().Contains(mahs.ToString())).ToList();
            }
            if(tenhs != "")
            {
                lisths = lisths.Where(m => m.ten.Contains(tenhs)).ToList();
            }
            if (cbbLopTimKiem.SelectedIndex != -1)
            {
                int malop = int.Parse(cbbLopTimKiem.SelectedValue.ToString());
                lisths = lisths.Where(m => m.maLop == malop).ToList();
            }
            foreach (HocSinh i in lisths)
            {
                String lop = db.Lop.Where(m => m.maLop == i.maLop).Select(m => m.tenLop).FirstOrDefault();
                dgvData.Rows.Add(i.maHS, i.ten, i.ngaySinh, i.gioiTinh, i.diaChi, i.sdt, lop, i.tgBatDau);
            }
            
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTra())
            {
                return;
            }
            string tenhs = "", gioitinh = "", sdt = "", diachi = "";
            DateTime ngaysinh, ngayvaohoc;
            int malop, mahs;
            mahs = int.Parse(txtMaHocSinh.Text);
            tenhs = txtTenHocSinh.Text;
            if (rdbNam.Checked)
            {
                gioitinh = "Nam";
            }
            else
            {
                gioitinh = "Nu";
            }
            malop = int.Parse(cbbLop.SelectedValue.ToString());
            sdt = txtSDT.Text;
            diachi = txtDiaChi.Text;
            ngaysinh = DateTime.Parse(dtpNgaySinh.Value.ToString());
            ngayvaohoc = DateTime.Parse(dtpNgayVaoHoc.Value.ToString());
            HocSinh hs = db.HocSinh.Where(m => m.maHS == mahs).FirstOrDefault();
            hs.ten = tenhs;
            hs.gioiTinh = gioitinh;
            hs.ngaySinh = ngaysinh;
            hs.sdt = sdt;
            hs.diaChi = diachi;
            hs.tgBatDau = ngayvaohoc;
            hs.trangThai = true;
            hs.maLop = malop;
            db.SaveChanges();
            MessageBox.Show("Cập nhật thông tin học sinh " + tenhs + " thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            QuanlyHocSinh_Load(sender, e);
            return;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int mahs = int.Parse(txtMaHocSinh.Text);
            HocSinh hs = db.HocSinh.Where(m => m.maHS == mahs).FirstOrDefault();
            if (hs != null)
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa học sinh " + hs.ten + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
                if(dialogResult == DialogResult.Yes)
                {
                    hs.trangThai = false;
                    db.SaveChanges();
                    MessageBox.Show("Xóa học sinh " + hs.ten + " thành công", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    QuanlyHocSinh_Load(sender, e);
                    return;
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            QuanlyHocSinh_Load(sender, e);
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvData.CurrentRow.Index;
            int mahs = int.Parse(dgvData.Rows[index].Cells[0].Value.ToString());

            HocSinh hs = db.HocSinh.Where(m => m.maHS == mahs && m.trangThai == true).FirstOrDefault();
            Lop l = db.Lop.Where(m => m.maLop == hs.maLop && m.trangThai == true).FirstOrDefault();
            txtMaHocSinh.Text = mahs.ToString();
            txtTenHocSinh.Text = hs.ten;

            if (hs.gioiTinh == "Nam")
            {
                rdbNam.Checked = true;
            }
            else
            {
                rdbNu.Checked = true;
            }
            string tenlop = dgvData.Rows[index].Cells[6].Value.ToString();
            List<Lop> list = db.Lop.Where(m => m.trangThai == true).ToList();
            cbbLop.DataSource = list;
            int vt = -1;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].tenLop == tenlop)
                {
                    vt = i;
                    break;
                }
            }
            cbbLop.SelectedIndex = vt;
            dtpNgaySinh.Text = hs.ngaySinh.ToString();
            txtSDT.Text = hs.sdt;
            txtDiaChi.Text = hs.diaChi;
            dtpNgayVaoHoc.Text = hs.tgBatDau.ToString();
        }
    }
}