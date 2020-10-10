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
    public partial class fMonHoc : Form
    {
        public fMonHoc()
        {
            InitializeComponent();
        }

        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();
        Mon mon = new Mon();

        private void GiaoVien_Load(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();

            cbbGVDay.DataSource = db.GiaoVien.Where(m => m.trangThai == true).ToList();
            cbbGVDay.ValueMember = "maGV";
            cbbGVDay.DisplayMember = "ten";

            cbbHocKy.DataSource = db.HocKy.ToList();
            cbbHocKy.DisplayMember = "tenHK";
            cbbHocKy.ValueMember = "maHK";

            cbbLop.DataSource = db.Lop.Where(m=>m.trangThai == true).ToList();
            cbbLop.DisplayMember = "tenLop";
            cbbLop.ValueMember = "maLop";

            cbbLop.SelectedIndex = -1;
            cbbHocKy.SelectedIndex = -1;
            cbbGVDay.SelectedIndex = -1;

            cbbGVDay.SelectedText = "Chọn giáo viên dạy";
            cbbHocKy.SelectedText = "Chọn học kỳ";
            cbbLop.SelectedText = "Chọn lớp";

            if (db.Mon.Count() > 0)
            {
                txtMaMonHoc.Text = (db.Mon.Select(m => m.maMon).Max() + 1).ToString();
            }
            else
            {
                txtMaMonHoc.Text = "1";
            }
            txtMaMonHoc.Enabled = false;
            txtTenMonHoc.Text = "";

            List<Mon> list = db.Mon.Where(m => m.trangThai == true).ToList();
            foreach(Mon i in list)
            {
                HocKy hk = db.HocKy.Where(m => m.maHK == i.maHK).FirstOrDefault();
                GiaoVien gv = db.GiaoVien.Where(m => m.maGV == i.gvDay).FirstOrDefault();
                Lop l = db.Lop.Where(m => m.maLop == i.maLop && m.trangThai == true).FirstOrDefault();
                if (l != null)
                {
                    dgvData.Rows.Add(i.maMon, i.tenMon, gv.ten, hk.tenHK, l.tenLop, l.nam);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            GiaoVien_Load(sender, e);
        }

        private bool KiemTra()
        {
            if (cbbLop.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn lớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbbHocKy.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn học kỳ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbbGVDay.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn giáo viên dạy", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            int malop = int.Parse(cbbLop.SelectedValue.ToString());
            int mahocky = int.Parse(cbbHocKy.SelectedValue.ToString());
            int magv = int.Parse(cbbGVDay.SelectedValue.ToString());
            int mamon = int.Parse(txtMaMonHoc.Text);
            string tenmon = txtTenMonHoc.Text;
            Mon x = db.Mon.Where(m => m.maMon == mamon).FirstOrDefault();
            if (x != null || x == mon)
            {
                return;
            }

            if (tenmon == "")
            {
                MessageBox.Show("Bạn chưa nhập tên môn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thêm mới môn " + txtTenMonHoc.Text + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes) {
                Mon m = new Mon();
                m.tenMon = tenmon;
                m.gvDay = magv;
                m.maHK = mahocky;
                m.maLop = malop;
                m.trangThai = true;
                db.Mon.Add(m);
                db.SaveChanges();
                MessageBox.Show("Thêm mới môn " + txtTenMonHoc.Text + " thành công", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GiaoVien_Load(sender, e);
            }
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            if (!KiemTra())
            {
                return;
            }

            int mamon = int.Parse(txtMaMonHoc.Text.ToString());
            if(mamon != mon.maMon)
            {
                return;
            }

            int malop = int.Parse(cbbLop.SelectedValue.ToString());
            int mahocky = int.Parse(cbbHocKy.SelectedValue.ToString());
            int magv = int.Parse(cbbGVDay.SelectedValue.ToString());
            string tenmon = txtTenMonHoc.Text;

            if (tenmon == "")
            {
                MessageBox.Show("Bạn chưa nhập tên môn", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            Mon m = db.Mon.Where(i => i.maMon == mamon && i.trangThai == true).FirstOrDefault();
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn sửa thông tin môn " + m.tenMon+ " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                m.tenMon = tenmon;
                m.gvDay = magv;
                m.maHK = mahocky;
                m.maLop = malop;
                m.trangThai = true;
                db.SaveChanges();
                MessageBox.Show("Sửa thông tin thành công", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GiaoVien_Load(sender, e);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();
            int malop = -1, mahocky = -1, magv = -1;

            if (cbbLop.SelectedIndex != -1)
            {
                malop = int.Parse(cbbLop.SelectedValue.ToString());
            }
            if (cbbHocKy.SelectedIndex != -1)
            {
                mahocky = int.Parse(cbbHocKy.SelectedValue.ToString());
            }
            if (cbbGVDay.SelectedIndex != -1)
            {
                magv = int.Parse(cbbGVDay.SelectedValue.ToString());
            }

            List<Mon> list = new List<Mon>();
            if(txtTenMonHoc.Text == "")
            {
                list = db.Mon.Where(m => m.trangThai == true).ToList();
            }
            else
            {
                list = db.Mon.Where(m => m.trangThai == true && m.tenMon.ToUpper().Contains(txtTenMonHoc.Text.ToUpper())).ToList();
            }

            if(malop != -1)
            {
                list = list.Where(m => m.maLop == malop).ToList();
            }
            if(mahocky != -1)
            {
                list = list.Where(m => m.maHK == mahocky).ToList();
            }
            if (magv != -1)
            {
                list = list.Where(m => m.gvDay == magv).ToList();
            }

            foreach (Mon i in list)
            {
                HocKy hk = db.HocKy.Where(m => m.maHK == i.maHK).FirstOrDefault();
                GiaoVien gv = db.GiaoVien.Where(m => m.maGV == i.gvDay).FirstOrDefault();
                Lop l = db.Lop.Where(m => m.maLop == i.maLop).FirstOrDefault();
                dgvData.Rows.Add(i.maMon, i.tenMon, gv.ten, hk.tenHK, l.tenLop, l.nam);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int mamonhoc = int.Parse(txtMaMonHoc.Text);

            Mon x = db.Mon.Where(m => m.maMon == mamonhoc && m.trangThai == true).FirstOrDefault();

            if(x == null)
            {
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa môn " + x.tenMon + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if(dialogResult == DialogResult.Yes)
                {
                    x.trangThai = false;
                    db.SaveChanges();
                    MessageBox.Show("Xóa lớp " + x.tenMon + " thành công", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    GiaoVien_Load(sender, e);
                    return;
                }
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvData.CurrentRow.Index;

            int mamon = int.Parse(dgvData.Rows[index].Cells[0].Value.ToString());
            string tengv = dgvData.Rows[index].Cells[2].Value.ToString();
            string tenhk = dgvData.Rows[index].Cells[3].Value.ToString();
            string tenlop = dgvData.Rows[index].Cells[4].Value.ToString();

            Mon x = db.Mon.Where(m => m.maMon == mamon && m.trangThai == true).FirstOrDefault();
            mon = x;

            List<GiaoVien> lgv = db.GiaoVien.Where(m => m.trangThai == true).ToList();
            List<Lop> ll = db.Lop.Where(m => m.trangThai == true).ToList();
            List<HocKy> lhk = db.HocKy.ToList();

            int vt1 = -1, vt2 = -1, vt3 = -1;

            for(int i = 0; i < lgv.Count; i++)
            {
                if(lgv[i].ten == tengv)
                {
                    vt3 = i;
                    break;
                }
            }
            for (int i = 0; i < ll.Count; i++)
            {
                if (ll[i].tenLop == tenlop)
                {
                    vt1 = i;
                    break;
                }
            }
            for (int i = 0; i < lhk.Count; i++)
            {
                if (lhk[i].tenHK == tenhk)
                {
                    vt2 = i;
                    break;
                }
            }

            cbbGVDay.SelectedIndex = vt3;
            cbbHocKy.SelectedIndex = vt2;
            cbbLop.SelectedIndex = vt1;

            txtMaMonHoc.Text = mamon.ToString();
            txtTenMonHoc.Text = x.tenMon;
        }
    }
}
