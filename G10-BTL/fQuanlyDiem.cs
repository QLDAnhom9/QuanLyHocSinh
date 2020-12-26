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
    public partial class fQuanlyDiem : Form
    {
        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();
        GiaoVien gv = new GiaoVien();
        int index = 0, mahschon = -1;
        List<NhapDiem> listnd = new List<NhapDiem>();
        List<Mon> listmon = new List<Mon>();
        int sl = 0;

        public fQuanlyDiem()
        {
            InitializeComponent();
            gv = this.Tag as GiaoVien;
        }

        private void QuanlyDiem_Load(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();
            gv = this.Tag as GiaoVien;

            
            listmon = db.Mon.Where(m => m.trangThai == true).ToList();
            if(gv.ten != Settings.ADMIN)
            {
                listmon = listmon.Where(m => m.gvDay == gv.maGV).ToList();
            }
            txtTenHocSinh.Text = "";

            cbbLop.DataSource = db.Lop.Where(m => m.trangThai == true).ToList();
            cbbLop.DisplayMember = Settings.TEN_LOP;
            cbbLop.ValueMember = Settings.MA_LOP;
            cbbLop.SelectedIndex = -1;
            cbbLop.SelectedText = Settings.CHON_LOP;

            cbbHocKy.DataSource = db.HocKy.ToList();
            cbbHocKy.DisplayMember = Settings.TEN_HK;
            cbbHocKy.ValueMember = Settings.MA_HK;
            cbbHocKy.SelectedIndex = -1;
            cbbHocKy.SelectedText = Settings.CHON_HOC_KY;

            cbbMonHoc.DataSource = listmon;
            cbbMonHoc.DisplayMember = Settings.TEN_MON;
            cbbMonHoc.ValueMember = Settings.MA_MON;
            cbbMonHoc.SelectedIndex = -1;
            cbbMonHoc.SelectedText = Settings.CHON_MON;
            sl++;
            foreach (Mon m in listmon)
            { 
                Lop l = db.Lop.Where(n => n.maLop == m.maLop && n.trangThai == true).FirstOrDefault();

                HocKy hk = db.HocKy.Where(n => n.maHK == m.maHK).FirstOrDefault();
                if (l == null)
                {
                    continue;
                }
                List<HocSinh> lisths = db.HocSinh.Where(n => n.maLop == l.maLop && n.trangThai == true).ToList();
                foreach(HocSinh hs in lisths)
                {
                    Diem d = db.Diem.Where(n => n.maMon == m.maMon && n.maHS == hs.maHS && n.trangThai == true).FirstOrDefault();
                    NhapDiem nd = new NhapDiem();
                    nd.maHS = hs.maHS;
                    nd.tenHS = hs.ten;
                    nd.maMon = m.maMon;
                    nd.tenMon = m.tenMon;
                    nd.hocKy = hk.tenHK;
                    nd.lop = l.tenLop;
                    if (d== null)
                    {
                        nd.diemGiuaKy = -1;
                        nd.diemCuoiKy = -1;
                        if (sl == 1)
                        {
                            listnd.Add(nd);
                        }
                        continue;
                    }
                    if (d.tgXoa == null)
                    {
                        nd.diemGiuaKy = d.diemGiuaKy;
                        nd.diemCuoiKy = d.diemCuoiKy;
                    }
                    else
                    {
                        if(d.diemGiuaKy == -1)
                        {
                            nd.diemGiuaKy = -1;
                            nd.diemCuoiKy = d.diemCuoiKy;
                        }
                        else
                        {
                            nd.diemCuoiKy = -1;
                            nd.diemGiuaKy = d.diemGiuaKy;
                        }
                    }
                    if (sl <= 1)
                    {
                        listnd.Add(nd);
                    }
                }
            }
            
            
            for (int i = 0; i < listnd.Count; i++)
            {
                bool ck = false;
                if (listnd[i].diemGiuaKy == -1 && listnd[i].diemCuoiKy != -1)
                {
                    dgvData.Rows.Add(listnd[i].maHS, listnd[i].tenHS, listnd[i].maMon, listnd[i].tenMon, "", listnd[i].diemCuoiKy, listnd[i].hocKy, listnd[i].lop);
                    ck = true;
                }
                if (listnd[i].diemCuoiKy == -1 && listnd[i].diemGiuaKy != -1)
                {
                    dgvData.Rows.Add(listnd[i].maHS, listnd[i].tenHS, listnd[i].maMon, listnd[i].tenMon, listnd[i].diemGiuaKy, "", listnd[i].hocKy, listnd[i].lop);
                    ck = true;
                }
                if (listnd[i].diemCuoiKy == -1 && listnd[i].diemGiuaKy == -1)
                {
                    dgvData.Rows.Add(listnd[i].maHS, listnd[i].tenHS, listnd[i].maMon, listnd[i].tenMon, "", "", listnd[i].hocKy, listnd[i].lop);
                    ck = true;
                }
                if (!ck)
                {
                    dgvData.Rows.Add(listnd[i].maHS, listnd[i].tenHS, listnd[i].maMon, listnd[i].tenMon, listnd[i].diemGiuaKy, listnd[i].diemCuoiKy, listnd[i].hocKy, listnd[i].lop);
                }
            }
        }

        private bool check()
        {
            string tenhs = txtTenHocSinh.Text;
            if(tenhs == "" || tenhs.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập tên học sinh", "Xin kiểm tra lại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            HocSinh hs = db.HocSinh.Where(m => m.ten == tenhs).FirstOrDefault();
            if(hs == null)
            {
                MessageBox.Show("Tên học sinh không đúng", "Xin kiểm tra lại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            double diemGiuaKy = 0, diemCuoiKy = 0;
            try
            {
                if (txtDiemGiuaKy.Enabled == true && txtDiemGiuaKy.Text.Trim() != "")
                {
                    diemGiuaKy = double.Parse(txtDiemGiuaKy.Text);
                }
                if (txtDiemCuoiKy.Enabled == true && txtDiemCuoiKy.Text.Trim() != "")
                {
                    diemCuoiKy = double.Parse(txtDiemCuoiKy.Text);
                }
            }
            catch(Exception)
            {
                return false;
            }
            if(diemCuoiKy>10.0 || diemCuoiKy < 0.0 || ((diemGiuaKy >10.0 || diemGiuaKy < 0.0)&&(txtDiemGiuaKy.Enabled == true)))
            {
                MessageBox.Show("Điểm không đúng", "Xin kiểm tra lại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnXacNhan_Click(object sender, EventArgs e)
        {
            if (!check())
            {
                return;
            }
            if (txtDiemCuoiKy.Text.Trim() == "" && txtDiemGiuaKy.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập điểm", "Xin kiểm tra lại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            float diemgiuaky = -1, diemcuoiky = -1;
            try
            {
                if (txtDiemGiuaKy.Text != "" && txtDiemGiuaKy.Text.Trim() != "")
                {
                    diemgiuaky = float.Parse(txtDiemGiuaKy.Text);
                }
                if (txtDiemCuoiKy.Text != "" && txtDiemCuoiKy.Text.Trim() != "")
                {
                    diemcuoiky = float.Parse(txtDiemCuoiKy.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Lỗi nhập điểm, xin kiểm tra lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int vt = -1;
            try
            {
                for (int i = 0; i < listnd.Count; i++)
                {
                    int mamon = int.Parse(cbbMonHoc.SelectedValue.ToString());
                    if (listnd[i].maHS == mahschon && listnd[i].maMon == mamon && listnd[i].hocKy == cbbHocKy.Text && listnd[i].lop == cbbLop.Text)
                    {
                        vt = i;
                        break;
                    }
                }
                if (diemcuoiky != -1)
                {
                    if (vt != -1)
                    {
                        listnd[vt].diemCuoiKy = diemcuoiky;
                    }
                }
                if (diemgiuaky != -1)
                {
                    if (vt != -1)
                    {
                        listnd[vt].diemGiuaKy = diemgiuaky;
                    }
                }
                MessageBox.Show("Nhập điểm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbMonHoc_SelectedIndexChanged(sender, e);
            }
            catch(Exception)
            {
                return;
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (!check())
            {
                return;
            }
            if(txtDiemGiuaKy.Text.Trim() != "" && txtDiemCuoiKy.Text.Trim() != "")
            {
                return;
            }
            float diemgiuaky = -1, diemcuoiky = -1;
            try
            {
                if (txtDiemGiuaKy.Text != "" && txtDiemGiuaKy.Text.Trim() != "")
                {
                    diemgiuaky = float.Parse(txtDiemGiuaKy.Text);
                }
                if (txtDiemCuoiKy.Text != "" && txtDiemCuoiKy.Text.Trim() != "")
                {
                    diemcuoiky = float.Parse(txtDiemCuoiKy.Text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Điểm không hợp lệ xin kiểm tra lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            int vt = -1;
            try
            {
                for (int i = 0; i < listnd.Count; i++)
                {
                    int mamon = int.Parse(cbbMonHoc.SelectedValue.ToString());
                    if (listnd[i].maHS == mahschon && listnd[i].maMon == mamon && listnd[i].hocKy == cbbHocKy.Text && listnd[i].lop == cbbLop.Text)
                    {
                        vt = i;
                        break;
                    }
                }
                if (diemcuoiky == -1)
                {
                    if (vt != -1)
                    {
                        listnd[vt].diemCuoiKy = diemcuoiky;
                    }
                }
                if (diemgiuaky == -1)
                {
                    if (vt != -1)
                    {
                        listnd[vt].diemGiuaKy = diemgiuaky;
                    }
                }
                MessageBox.Show("Xóa điểm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cbbMonHoc_SelectedIndexChanged(sender, e);
            }
            catch (Exception )
            {
                return;
            }
        }
        int dem = 0;

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            
        }

        private void btnLuuLai_Click(object sender, EventArgs e)
        {
            try
            {
                foreach(NhapDiem x in listnd)
                {
                    Diem y = db.Diem.Where(m => m.maHS == x.maHS && m.maMon == x.maMon).FirstOrDefault();
                    if(y == null)
                    {
                        Diem z = new Diem();
                        z.maHS = x.maHS;
                        z.maMon = x.maMon;
                        if (x.diemGiuaKy != -1)
                            z.diemGiuaKy = x.diemGiuaKy;
                        if (x.diemCuoiKy != -1)
                            z.diemCuoiKy = x.diemCuoiKy;
                        z.tgNhap = DateTime.Now;
                        z.trangThai = true;
                        db.Diem.Add(z);
                        db.SaveChanges();
                    }
                    else
                    {
                        y.diemCuoiKy = x.diemCuoiKy;
                        y.diemGiuaKy = x.diemGiuaKy;
                        db.SaveChanges();
                    }
                }
                MessageBox.Show("Lưu điểm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch(Exception)
            {
                return;
            }
        }

        private void loadData()
        {
            dgvData.Rows.Clear();
            try
            {
                int mamon = int.Parse(cbbMonHoc.SelectedValue.ToString());
                Mon x = new Mon();
                if (mamon != -1)
                    x = db.Mon.Where(m => m.trangThai == true && m.maMon == mamon).FirstOrDefault();
                else
                    x = db.Mon.Where(m => m.trangThai == true && m.gvDay == gv.maGV).FirstOrDefault();
                string tenlop = cbbLop.Text;
                string hocky = cbbHocKy.Text;

                List<NhapDiem> nhapdiem = new List<NhapDiem>();
                nhapdiem = listnd;
                if (tenlop.Trim() != "" && tenlop != Settings.CHON_LOP)
                {
                    nhapdiem = listnd.Where(n => n.lop == tenlop).ToList();
                }
                if (hocky.Trim() != "" && hocky != Settings.CHON_HOC_KY)
                {
                    nhapdiem = listnd.Where(n => n.hocKy == hocky).ToList();
                }
                nhapdiem = nhapdiem.Where(m => m.maMon == mamon).ToList();

                dgvData.Rows.Clear();

                for (int i = 0; i < nhapdiem.Count; i++)
                {
                    bool ck = false;
                    if (nhapdiem[i].diemGiuaKy == -1 && nhapdiem[i].diemCuoiKy != -1)
                    {
                        dgvData.Rows.Add(nhapdiem[i].maHS, nhapdiem[i].tenHS, nhapdiem[i].maMon, nhapdiem[i].tenMon, "", nhapdiem[i].diemCuoiKy, nhapdiem[i].hocKy, nhapdiem[i].lop);
                        ck = true;
                    }
                    if (listnd[i].diemCuoiKy == -1 && listnd[i].diemGiuaKy != -1)
                    {
                        dgvData.Rows.Add(nhapdiem[i].maHS, nhapdiem[i].tenHS, nhapdiem[i].maMon, nhapdiem[i].tenMon, nhapdiem[i].diemGiuaKy, "", nhapdiem[i].hocKy, nhapdiem[i].lop);
                        ck = true;
                    }
                    if (listnd[i].diemCuoiKy == -1 && listnd[i].diemGiuaKy == -1)
                    {
                        dgvData.Rows.Add(nhapdiem[i].maHS, nhapdiem[i].tenHS, nhapdiem[i].maMon, nhapdiem[i].tenMon, "", "", nhapdiem[i].hocKy, nhapdiem[i].lop);
                        ck = true;
                    }
                    if (!ck)
                    {
                        dgvData.Rows.Add(nhapdiem[i].maHS, nhapdiem[i].tenHS, nhapdiem[i].maMon, nhapdiem[i].tenMon, nhapdiem[i].diemGiuaKy, nhapdiem[i].diemCuoiKy, nhapdiem[i].hocKy, nhapdiem[i].lop);
                    }
                }
            }
            catch(Exception)
            {
                return;
            }
        }


        private void cbbMonHoc_SelectedIndexChanged(object sender, EventArgs e)
        {
            int mamon = -1;
            try
            {
                if(cbbMonHoc.Text.Trim() == "")
                {
                    dgvData.Rows.Clear();
                    return;
                }

                if(cbbMonHoc.Text.ToString() != "Toán" && cbbMonHoc.Text.ToString() != "Tiếng Việt")
                {
                    txtDiemGiuaKy.Enabled = false;
                }
                else
                {
                    txtDiemGiuaKy.Enabled = true;
                }

                mamon = int.Parse(cbbMonHoc.SelectedValue.ToString());
                if(mamon.ToString().Trim() != "")
                {
                    loadData();
                }
            }
            catch(Exception)
            {
                return;
            }
            
        }

        private void cbbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbbHocKy.SelectedIndex == -1)
            {
                return;
            }
            try
            {
                int mahk = int.Parse(cbbHocKy.SelectedValue.ToString());
                listmon = db.Mon.Where(m => m.trangThai == true && m.maHK == mahk).ToList();
                if (cbbLop.SelectedIndex != -1)
                {
                    int malop = int.Parse(cbbLop.SelectedValue.ToString());
                    listmon = listmon.Where(m => m.maLop == malop).ToList();
                }
                if (gv.ten != Settings.ADMIN)
                {
                    listmon = listmon.Where(m => m.gvDay == gv.maGV).ToList();
                }
                if(listmon.Count == 0)
                {
                    cbbMonHoc.SelectedIndex = -1;
                }
                cbbMonHoc.DataSource = listmon;
                loadData();
            }
            catch(Exception)
            {
                return;
            }
        }

        private void cbbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbbLop.SelectedIndex == -1)
            {
                return;
            }
            try
            {
                int malop = int.Parse(cbbLop.SelectedValue.ToString());
                listmon = db.Mon.Where(m => m.trangThai == true && m.maLop == malop).ToList();
                if (cbbHocKy.SelectedIndex != -1)
                {
                    int mahk = int.Parse(cbbHocKy.SelectedValue.ToString());
                    listmon = listmon.Where(m => m.maHK == mahk).ToList();
                }
                if (gv.ten != Settings.ADMIN)
                {
                    listmon = listmon.Where(m => m.gvDay == gv.maGV).ToList();
                }
                if (listmon.Count == 0)
                {
                    cbbMonHoc.SelectedIndex = -1;
                }
                cbbMonHoc.DataSource = listmon;
                
                loadData();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dem > 0)
            {
                if (dgvData.Rows.Count > 0)
                    index = dgvData.CurrentRow.Index;
                else
                    index = 0;
                try
                {
                    txtTenHocSinh.Text = dgvData.Rows[index].Cells[1].Value.ToString();
                }
                catch (Exception)
                {
                    return;
                }

                string tenhk = dgvData.Rows[index].Cells[6].Value.ToString();
                string tenlop = dgvData.Rows[index].Cells[7].Value.ToString();
                string tenmon = dgvData.Rows[index].Cells[3].Value.ToString();
                mahschon = int.Parse(dgvData.Rows[index].Cells[0].Value.ToString());

                int mahk = db.HocKy.Where(m => m.tenHK == tenhk).Select(m => m.maHK).FirstOrDefault();
                int malop = db.Lop.Where(m => m.tenLop == tenlop).Select(m => m.maLop).FirstOrDefault();
                int mamon = db.Mon.Where(m => m.tenMon == tenmon).Select(m => m.maMon).FirstOrDefault();

                cbbHocKy.SelectedText = tenhk;
                cbbHocKy.SelectedValue = mahk;

                cbbLop.SelectedText = tenlop;
                cbbLop.SelectedValue = malop;

                int vt = 0, j=0;

                //MessageBox.Show(tenmon.ToString());
                foreach(Mon i in listmon)
                {
                    if(i.tenMon == tenmon)
                    {
                        vt = j;
                        break;
                    }
                    j++;
                }
                //MessageBox.Show(vt.ToString());
                cbbMonHoc.SelectedIndex = vt;

                try
                {
                    txtDiemCuoiKy.Text = dgvData.Rows[index].Cells[5].Value.ToString();
                    txtDiemGiuaKy.Text = dgvData.Rows[index].Cells[4].Value.ToString();
                }
                catch(Exception)
                {
                    return;
                }
            }
            dem++;
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            QuanlyDiem_Load(sender, e);
        }
    }
}
