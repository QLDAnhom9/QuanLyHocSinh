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
    public partial class fQuanlyGiaoVien : Form
    {
        List<GiaoVien> listgv;

        public fQuanlyGiaoVien()
        {
            InitializeComponent();
        }

        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();

        private void QuanlyGiaoVien_Load(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Maximized;
            dgvData.Rows.Clear();
            
            dtpNgaySinh.Text = DateTime.Now.ToString();
            dtpNgayVaoLam.Text = DateTime.Now.ToString();
            txtMaGV.Enabled = false;
            txtMaGV.Text = (db.GiaoVien.Max(m => m.maGV) + 1).ToString();
            rdbNam.Checked = true;
            txtBangCap.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtTenGV.Text = "";

            listgv = db.GiaoVien.Where(m => m.trangThai == true).ToList();
            foreach (GiaoVien i in listgv)
            {
                dgvData.Rows.Add(i.maGV, i.ten, i.ngaySinh, i.gioiTinh, i.diaChi, i.sdt, i.bangCap, i.tgBatDau);
            }

            dgvData.ClearSelection();   
        }

        private bool KiemTra()
        {
            string tengv, sdt;
            tengv = txtTenGV.Text;

            sdt = txtSDT.Text;
            if (tengv == "")
            {
                MessageBox.Show("Bạn chưa nhập tên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            if ( sdt != "" && sdt.Length != 10 )
            {
                MessageBox.Show("Số điện thoạt không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

            int ma = int.Parse(txtMaGV.Text.ToString());
            GiaoVien gvv = db.GiaoVien.Where(m => m.maGV == ma).SingleOrDefault();
            if(gvv != null)
            {
                return;
            }
            string tengv = "", taikhoan = "", matkhau = "", ngaysinh = "", gioitinh = "", sdt = "", diachi = "", bangcap = "", ngayvaolam = "";
            tengv = txtTenGV.Text;
            ngaysinh = dtpNgaySinh.Value.ToString();
            if (rdbNam.Checked)
            {
                gioitinh = Settings.NAM;
            }
            else
            {
                gioitinh = Settings.NU;
            }
            sdt = txtSDT.Text;
            diachi = txtDiaChi.Text;
            bangcap = txtBangCap.Text;
            ngayvaolam = dtpNgayVaoLam.Value.ToString();
            
            GiaoVien gv = new GiaoVien();
            gv.maGV = ma;
            gv.taiKhoan = taikhoan;
            gv.matKhau = matkhau;
            gv.bangCap = bangcap;
            gv.diaChi = diachi;
            gv.gioiTinh = gioitinh;
            gv.ngaySinh = DateTime.Parse(ngaysinh);
            gv.sdt = sdt;
            gv.ten = tengv;
            gv.tgBatDau = DateTime.Parse(ngayvaolam);
            gv.trangThai = true;
            db.GiaoVien.Add(gv);
            db.SaveChanges();
            MessageBox.Show("Thêm mới giáo viên " + tengv + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            QuanlyGiaoVien_Load(sender, e);
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {

        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int magv = int.Parse(txtMaGV.Text.ToString());
            GiaoVien gv = db.GiaoVien.Where(m => m.maGV == magv).FirstOrDefault();
            if(gv == null)
            {
                MessageBox.Show("Bạn chưa chọn giáo viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            DialogResult result = MessageBox.Show("Bạn có muốn xóa giáo viên " + gv.ten, "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (result == DialogResult.Yes)
            {
                GiaoVien giaovien = db.GiaoVien.Where(m => m.maGV == gv.maGV).FirstOrDefault();
                giaovien.trangThai = false;
                db.SaveChanges();
                MessageBox.Show("Xóa thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                QuanlyGiaoVien_Load(sender, e);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            QuanlyGiaoVien_Load(sender, e);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            int ma = int.Parse(txtMaGV.Text.ToString());
            GiaoVien gvv = db.GiaoVien.Where(m => m.maGV == ma).SingleOrDefault();
            if (gvv == null)
            {
                return;
            }
            string tengv = "", ngaysinh = "", gioitinh = "", sdt = "", diachi = "", bangcap = "", ngayvaolam = "";
            tengv = txtTenGV.Text;
            ngaysinh = dtpNgaySinh.Value.ToString();
            if (rdbNam.Checked)
            {
                gioitinh = Settings.NAM;
            }
            else
            {
                gioitinh = Settings.NU;
            }
            sdt = txtSDT.Text;
            diachi = txtDiaChi.Text;
            bangcap = txtBangCap.Text;
            ngayvaolam = dtpNgayVaoLam.Value.ToString();
            GiaoVien gv = db.GiaoVien.Where(m => m.maGV == ma).FirstOrDefault();
            gv.bangCap = bangcap;
            gv.diaChi = diachi;
            gv.gioiTinh = gioitinh;
            gv.ngaySinh = DateTime.Parse(ngaysinh);
            gv.sdt = sdt;
            gv.ten = tengv;
            gv.tgBatDau = DateTime.Parse(ngayvaolam);
            gv.trangThai = true;
            db.SaveChanges();
            MessageBox.Show("Cập nhật thông tin giáo viên " + tengv + " thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            QuanlyGiaoVien_Load(sender, e);
        }

        private void txtMaGV_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnLoc_Click(object sender, EventArgs e)
        {
            if (txtMaGVTimKiem.Text != "")
            {
                try
                {
                    int ma = int.Parse(txtMaGVTimKiem.Text);
                    listgv = listgv.Where(m => m.maGV == ma).ToList();
                }
                catch (Exception)
                {
                    MessageBox.Show("Nhập sai mã giáo viên","Cảnh báo",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    return;
                }                
            }
            if(txtTenGVTimKiem.Text != "")
            {
                listgv = listgv.Where(m => m.ten.ToUpper().Contains(txtTenGVTimKiem.Text.ToUpper())).ToList();
            }
            dgvData.Rows.Clear();
            foreach (GiaoVien i in listgv)
            {
                dgvData.Rows.Add(i.maGV, i.ten, i.ngaySinh, i.gioiTinh, i.diaChi, i.sdt, i.bangCap, i.tgBatDau);
            }
            dgvData.ClearSelection();
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvData.CurrentRow.Index;
            int magv = int.Parse(dgvData.Rows[index].Cells[0].Value.ToString());
            GiaoVien gv = db.GiaoVien.Where(m => m.maGV == magv).FirstOrDefault();

            txtMaGV.Text = gv.maGV.ToString();
            txtTenGV.Text = gv.ten;
            txtBangCap.Text = gv.bangCap;
            txtDiaChi.Text = gv.diaChi;
            txtSDT.Text = gv.sdt;
            

            if (gv.gioiTinh == Settings.NAM)
            {
                rdbNam.Checked = true;
            }
            else
            {
                rdbNu.Checked = true;
            }
            dtpNgaySinh.CustomFormat = Settings.DINH_DANG_NGAY;
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgayVaoLam.CustomFormat = Settings.DINH_DANG_NGAY;
            dtpNgayVaoLam.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.Value = DateTime.Parse(gv.ngaySinh.ToString());
            try
            {
                dtpNgayVaoLam.Value = DateTime.Parse(gv.tgBatDau.ToString());
            }
            catch (Exception)
            {
                dtpNgayVaoLam.Value = DateTime.Today;
            }
        }
    }
}
