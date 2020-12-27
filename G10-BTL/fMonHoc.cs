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
            cbbGVDay.ValueMember = Settings.MA_GV;
            cbbGVDay.DisplayMember = Settings.TEN_GV;

            cbbHocKy.DataSource = db.HocKy.ToList();
            cbbHocKy.DisplayMember = Settings.TEN_HK;
            cbbHocKy.ValueMember = Settings.MA_HK;

            cbbLop.DataSource = db.Lop.Where(m => m.trangThai == true).ToList();
            cbbLop.DisplayMember = Settings.TEN_LOP;
            cbbLop.ValueMember = Settings.MA_LOP;

            cbbLop.SelectedIndex = -1;
            cbbHocKy.SelectedIndex = -1;
            cbbGVDay.SelectedIndex = -1;

            if (db.Mon.Count() > 0)
            {
                txtMaMonHoc.Text = (db.Mon.Select(m => m.maMon).Max() + 1).ToString();
            }
            else
            {
                txtMaMonHoc.Text = Settings.MA_MH_MAC_DINH;
            }
            txtMaMonHoc.Enabled = false;
            txtTenMonHoc.Text = "";

            List<Mon> list = db.Mon.Where(m => m.trangThai == true).ToList();

            foreach (Mon i in list)
            {
                string tenGV = "", tenHK = "", tenLop = "", nam = "";
                if (i.maHK != null)
                    tenHK = db.HocKy.Where(m => m.maHK == i.maHK).Select(m => m.tenHK).FirstOrDefault().ToString();
                if (i.gvDay != null)
                    tenGV = db.GiaoVien.Where(m => m.maGV == i.gvDay).Select(m => m.ten).FirstOrDefault().ToString();
                if (i.maLop != null)
                {
                    tenLop = db.Lop.Where(m => m.maLop == i.maLop && m.trangThai == true).Select(m => m.tenLop).FirstOrDefault().ToString();
                    nam = db.Lop.Where(m => m.maLop == i.maLop && m.trangThai == true).Select(m => m.nam).FirstOrDefault().ToString();
                }
                dgvData.Rows.Add(i.maMon, i.tenMon, tenGV, tenHK, tenLop, nam, i.gvDay, i.maHK, i.maLop);
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
                MessageBox.Show("Bạn chưa " + Settings.CHON_LOP.ToLower(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbbHocKy.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa " + Settings.CHON_HOC_KY.ToLower(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (cbbGVDay.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa " + Settings.CHON_GV.ToLower(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
            if (dialogResult == DialogResult.Yes)
            {
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
            if (mamon != mon.maMon)
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
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn sửa thông tin môn " + m.tenMon + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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
            if (txtTenMonHoc.Text == "")
            {
                list = db.Mon.Where(m => m.trangThai == true).ToList();
            }
            else
            {
                list = db.Mon.Where(m => m.trangThai == true && m.tenMon.ToUpper().Contains(txtTenMonHoc.Text.ToUpper())).ToList();
            }

            if (malop != -1)
            {
                list = list.Where(m => m.maLop == malop).ToList();
            }
            if (mahocky != -1)
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

            if (x == null)
            {
                return;
            }
            else
            {
                DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa môn " + x.tenMon + " không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (dialogResult == DialogResult.Yes)
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
            int mamon = Int32.Parse(dgvData.Rows[index].Cells[0].Value.ToString());
            string tenmon, tengv, tenhk, tenlop;
            int magv = -1, mahk = -1, malop = -1;

            // lấy tên
            tenmon = dgvData.Rows[index].Cells[1].Value.ToString();
            tengv = dgvData.Rows[index].Cells[2].Value.ToString();
            tenhk = dgvData.Rows[index].Cells[3].Value.ToString();
            tenlop = dgvData.Rows[index].Cells[4].Value.ToString();
            try
            {
                // lấy mã
                magv = Int32.Parse(dgvData.Rows[index].Cells[6].Value.ToString());
                mahk = Int32.Parse(dgvData.Rows[index].Cells[7].Value.ToString());
                malop = Int32.Parse(dgvData.Rows[index].Cells[8].Value.ToString());
            }
            catch (Exception)
            {

            }

            cbbGVDay.SelectedIndex = magv == -1 ? -1 : cbbGVDay.FindString(tengv);
            cbbHocKy.SelectedIndex = mahk == -1 ? -1 : cbbHocKy.FindString(tenhk);
            cbbLop.SelectedIndex = malop == -1 ? -1 : cbbLop.FindString(tenlop);

            txtMaMonHoc.Text = mamon.ToString();
            txtTenMonHoc.Text = tenmon;
        }

        private void cbbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbLop.SelectedIndex == -1)
            {
                cbbLop.SelectedText = Settings.CHON_LOP;
            }
        }

        private void cbbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbHocKy.SelectedIndex == -1)
            {
                cbbHocKy.SelectedText = Settings.CHON_HOC_KY;
            }
        }

        private void cbbGVDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbGVDay.SelectedIndex == -1)
            {
                cbbGVDay.SelectedText = Settings.CHON_GV;
            }
        }
    }
}
