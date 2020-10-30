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
    public partial class fLop : Form
    {
        public fLop()
        {
            InitializeComponent();
        }

        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();
        Lop ldc = new Lop();

        private void Lop_Load(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();
            List<Lop> list = db.Lop.Where(m=>m.trangThai == true).ToList();
            foreach (Lop i in list)
            {
                String tengv = db.GiaoVien.Where(m => m.maGV == i.maGVCN && m.trangThai == true).Select(m => m.ten).FirstOrDefault();
                List<HocSinh> sl = db.HocSinh.Where(m => m.maLop == i.maLop && m.trangThai == true).ToList();
                dgvData.Rows.Add(i.maLop, i.tenLop, tengv, sl.Count());
            }
            cbbGVCN.DataSource = db.GiaoVien.Where(m => m.trangThai == true).ToList();
            cbbGVCN.DisplayMember = "Ten";
            cbbGVCN.ValueMember = "MaGV";
            cbbGVCN.SelectedIndex = -1;
            
            if (db.Lop.Count() > 0)
            {
                txtMaLop.Text = (db.Lop.Max(m => m.maLop) + 1).ToString();
            }
            else
            {
                txtMaLop.Text = "1";
            }
            txtMaLop.Enabled = false;
            txtTenLop.Text = "";
        }

        private void btnThemMoi_Click(object sender, EventArgs e)
        {

            if(txtTenLop.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên lớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if(cbbGVCN.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn chưa chọn giáo viên chủ nhiệm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int malop = int.Parse(txtMaLop.Text);

            Lop lop = db.Lop.Where(m => m.maLop == malop && m.trangThai == true).FirstOrDefault();
            if (lop != null)
            {
                return;
            }

            string tenlop = txtTenLop.Text;
            int magv = int.Parse(cbbGVCN.SelectedValue.ToString());

            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thêm lớp " + tenlop + " và giáo viên chủ nhiệm là " + cbbGVCN.Text + " không?", "Tông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {

                bool ck = true;
                for (int i = 0; i < tenlop.Length; i++)
                {
                    if (tenlop[i] >= 'a' && tenlop[i] <= 'z' || tenlop[i] >= 'A' && tenlop[i] <= 'Z' || tenlop[i] >= '0' && tenlop[i] <= '9' || tenlop[i]==' ')
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
                    MessageBox.Show("Tên lớp không hợp lệ!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Lop x = db.Lop.Where(m => m.tenLop == tenlop && m.trangThai == true).FirstOrDefault();
                if(x!= null)
                {
                    MessageBox.Show("Tên lớp đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Lop y = db.Lop.Where(m => m.maGVCN == magv && m.trangThai == true).FirstOrDefault();
                if(y != null)
                {
                    MessageBox.Show("Giáo viên đã chủ nhiệm lớp khác rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                Lop l = new Lop();
                l.tenLop = tenlop;
                l.maGVCN = magv;
                l.trangThai = true;
                db.Lop.Add(l);
                db.SaveChanges();
                MessageBox.Show("Thêm mới lớp thành công", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Lop_Load(sender, e);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Lop_Load(sender, e);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            int malop = int.Parse(txtMaLop.Text.ToString().Trim());
            if (txtTenLop.Text == "")
            {
                MessageBox.Show("Bạn chưa nhập tên lớp", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (db.Lop.Where(m => m.maLop == malop).FirstOrDefault() == null){
                MessageBox.Show("Lớp không tồn tại ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string tenlop = txtTenLop.Text.Trim();
            int magv = int.Parse(cbbGVCN.SelectedValue.ToString().Trim());

            Lop x = db.Lop.Where(m => m.tenLop == tenlop && m.trangThai == true).FirstOrDefault();
            if (x != null && x.maLop!=malop)
            {
                MessageBox.Show("Tên lớp đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Lop y = db.Lop.Where(m => m.maGVCN == magv && m.trangThai == true).FirstOrDefault();
            if (y != null && y.maGVCN != magv)
            {
                MessageBox.Show("Giáo viên đã chủ nhiệm lớp khác rồi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Lop l = db.Lop.Where(m => m.maLop == malop).FirstOrDefault();
            l.tenLop = tenlop;
            l.maGVCN = magv;
            l.trangThai = true;
            db.SaveChanges();
            MessageBox.Show("Cập nhật thông tin lớp thành công", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Lop_Load(sender, e);
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            int malop = int.Parse(txtMaLop.Text);
            Lop l = db.Lop.Where(m => m.maLop == malop && m.trangThai == true).FirstOrDefault();
            if(l == null)
            {
                return;
            }
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn xóa lớp " + l.tenLop + " không?", "Tông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dialogResult == DialogResult.Yes)
            {
                l.trangThai = false;
                db.SaveChanges();
                MessageBox.Show("Xóa lớp "+ l.tenLop + " thành công", "Chúc mừng", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Lop_Load(sender, e);
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();
            int malop = int.Parse(txtMaLop.Text);
            string tenlop = txtTenLop.Text;
            int magv = -1;
            if(cbbGVCN.SelectedIndex != -1)
            {
                magv = int.Parse(cbbGVCN.SelectedValue.ToString());
            }
            List<Lop> list = new List<Lop>();
            if (magv != -1)
            {
                list = db.Lop.Where(m => m.maGVCN == magv && m.trangThai == true).ToList();
            }
            else
            {
                list = db.Lop.Where(m => m.trangThai == true).ToList();
            }
            if (tenlop != "")
            {
                list = list.Where(m => m.tenLop.Contains(tenlop)).ToList();
            }
            foreach(Lop i in list)
            {
                String tengv = db.GiaoVien.Where(m => m.maGV == i.maGVCN && m.trangThai == true).Select(m => m.ten).FirstOrDefault();
                dgvData.Rows.Add(i.maLop, i.tenLop, tengv, db.HocSinh.Where(m=>m.maLop == i.maLop && m.trangThai == true).Count());
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dgvData.CurrentRow.Index;
            int malop = int.Parse(dgvData.Rows[index].Cells[0].Value.ToString());

            Lop l = db.Lop.Where(m => m.maLop == malop).FirstOrDefault();
            txtMaLop.Text = l.maLop.ToString();
            txtTenLop.Text = l.tenLop;
            ldc = l;
            GiaoVien gv = db.GiaoVien.Where(m => m.maGV == l.maGVCN).FirstOrDefault();
            List<GiaoVien> lgv = db.GiaoVien.Where(m => m.trangThai == true).ToList();
            int vt = -1;
            if ( gv != null)
            for (int i = 0; i < lgv.Count; i++)
            {
                if (lgv[i].maGV == gv.maGV)
                {
                    vt = i;
                    goto abc;
                }
            }
        abc:
            cbbGVCN.SelectedIndex = vt;
        }

        private void cbbGVCN_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ( cbbGVCN.SelectedIndex == -1 )
            {
                cbbGVCN.SelectedText = "Chọn giáo viên";
            }
        }
    }
}
