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
        int index = 0, madiem;
        List<NhapDiem> listnd = new List<NhapDiem>();
        List<Diem> listdiem = new List<Diem>();
        bool update = true;

        public fQuanlyDiem()
        {
            InitializeComponent();
        }

        private void QuanlyDiem_Load(object sender, EventArgs e)
        {
            // Combobox Lớp
            cbbLop.DataSource = db.Lop.Where(m => m.trangThai == true).ToList();
            cbbLop.DisplayMember = Settings.TEN_LOP;
            cbbLop.ValueMember = Settings.MA_LOP;
            cbbLop.SelectedIndex = -1;
            cbbLop.SelectedText = Settings.CHON_LOP;
            // Combobox Học kỳ
            cbbHocKy.DataSource = db.HocKy.ToList();
            cbbHocKy.DisplayMember = Settings.TEN_HK;
            cbbHocKy.ValueMember = Settings.MA_HK;
            cbbHocKy.SelectedIndex = -1;
            cbbHocKy.SelectedText = Settings.CHON_HOC_KY;
            // Combobox Môn học
            cbbMonHoc.DataSource = db.Mon.ToList();
            cbbMonHoc.DisplayMember = Settings.TEN_MON;
            cbbMonHoc.ValueMember = Settings.MA_MON;
            cbbMonHoc.SelectedIndex = -1;
            cbbMonHoc.SelectedText = Settings.CHON_MON;
            // Combobox Học sinh
            cbbHocSinh.DataSource = db.HocSinh.Where(m => m.trangThai == true).ToList();
            cbbHocSinh.DisplayMember = Settings.TEN_HS;
            cbbHocSinh.ValueMember = Settings.MA_HS;
            cbbHocSinh.SelectedIndex = -1;
            cbbHocSinh.SelectedText = Settings.CHON_HS;

            // Tùy chỉnh vô hiệu hóa
            groupBox1.Enabled = true;
            cbbLop.Enabled = true;
            cbbHocSinh.Enabled = false;
            cbbHocKy.Enabled = false;
            cbbMonHoc.Enabled = false;
            btnThem.Enabled = true;

            loadData();
        }

        // Kiểm tra các trường khi một sự kiện được khởi động
        private bool check()
        {
            if (txtDiemCuoiKy.Text.Trim() == "" && txtDiemGiuaKy.Text.Trim() == "")
            {
                MessageBox.Show("Bạn chưa nhập điểm", "Xin kiểm tra lại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            float diemgiuaky, diemcuoiky;

            // Kiểm tra điểm đầu vào
            if (txtDiemGiuaKy.Text != "")
            {
                try
                {
                    diemgiuaky = float.Parse(txtDiemGiuaKy.Text);
                    if (diemgiuaky < 0 || diemgiuaky > 10)
                    {
                        MessageBox.Show("Phạm vi điểm từ 0 đến 10.", "Nhập lại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi nhập điểm, xin kiểm tra lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            if (txtDiemCuoiKy.Text.Trim() != "")
            {
                try
                {
                    diemcuoiky = float.Parse(txtDiemCuoiKy.Text);
                    if (diemcuoiky < 0 || diemcuoiky > 10)
                    {
                        MessageBox.Show("Phạm vi điểm từ 0 đến 10.", "Nhập lại", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return false;
                    }
                }
                catch (Exception)
                {
                    MessageBox.Show("Lỗi nhập điểm, xin kiểm tra lại", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
  
            return true;
        }

        // Xóa bản ghi được chọn
        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult select = MessageBox.Show("Điểm của học sinh này sẽ được xóa. Tiếp tục ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (select == DialogResult.OK)
            {
                Diem rm = new Diem();
                rm = db.Diem.Where(d => d.maDiem == madiem).First();
                db.Diem.Remove(rm);
                db.SaveChanges();
                QuanlyDiem_Load(sender, e);
            }
            else
                return;
        }

        private void dgvData_SelectionChanged(object sender, EventArgs e)
        {
            // Lấy vị trí dòng được chọn
            if (dgvData.Rows.Count > 0)
                index = dgvData.CurrentRow.Index;
            else
                index = 0;
        }

        private void loadData()
        {
            dgvData.Rows.Clear();
            listdiem = db.Diem.ToList();
            if (listdiem == null)
                MessageBox.Show("Danh sách đang trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                foreach (Diem a in listdiem)
                {
                    dgvData.Rows.Add(a.maHS.ToString(), a.HocSinh.ten.ToString(),
                        a.HocSinh.Lop.tenLop.ToString(), a.maMon.ToString(),
                        a.Mon.tenMon.ToString(), a.diemGiuaKy.ToString(),
                        a.diemCuoiKy.ToString(), a.Mon.maHK.ToString(),a.maDiem.ToString());
                }
        }

        private void cbbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (update)
            {
                try
                {
                    cbbMonHoc.Enabled = true;
                    // Lấy mã lớp được chọn
                    int malop = Int32.Parse(cbbLop.SelectedValue.ToString());

                    int mahk = Int32.Parse(cbbHocKy.SelectedValue.ToString());
                    // Đưa các môn học của học kỳ được chọn lên Combobox Môn học       
                    cbbMonHoc.DataSource = db.Mon.Where(m => m.trangThai == true && m.maHK == mahk && m.maLop == malop).ToList();
                    cbbMonHoc.SelectedIndex = -1;
                }
                catch (Exception)
                {
                    return;
                }
            }
            update = true;
        }

        private void cbbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (update)
            {
                try
                {
                    // Lấy mã lớp được chọn
                    int malop = Int32.Parse(cbbLop.SelectedValue.ToString());

                    // Đưa lên danh sách học sinh trong lớp này
                    cbbHocSinh.DataSource = db.HocSinh.Where(m => m.trangThai == true && m.maLop == malop).ToList();
                    cbbHocSinh.SelectedIndex = -1;
                    cbbHocSinh.Enabled = true;
                    cbbLop.Enabled = false;
                }
                catch (Exception)
                {
                    return;
                }
            }
            update = true;
        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // cho biết các combobox có cần gọi thao tác SelectedIndexChanged hay không
            update = false;

            // khởi tạo các biến lưu giá trị
            string tenhk, tenlop, tenmon;
            int mahs, mahk, mamon;

            try
            {
                // lấy mã
                mahs = Int32.Parse(dgvData.Rows[index].Cells[0].Value.ToString());
                mahk = Int32.Parse(dgvData.Rows[index].Cells[7].Value.ToString());
                mamon = Int32.Parse(dgvData.Rows[index].Cells[3].Value.ToString());
                madiem = Int32.Parse(dgvData.Rows[index].Cells[8].Value.ToString());
                // lấy tên
                cbbHocSinh.Text = dgvData.Rows[index].Cells[1].Value.ToString();              
                tenhk = db.HocKy.Where(m => m.maHK == mahk).Select(m => m.tenHK).FirstOrDefault();
                tenlop = dgvData.Rows[index].Cells[2].Value.ToString();
                tenmon = dgvData.Rows[index].Cells[4].Value.ToString();
                // lấy điểm
                txtDiemGiuaKy.Text = dgvData.Rows[index].Cells[5].Value.ToString();
                txtDiemCuoiKy.Text = dgvData.Rows[index].Cells[6].Value.ToString();             
            }
            catch (Exception)
            {
                return;
            }

            // đưa dữ liệu lên các combobox
            cbbHocKy.SelectedText = tenhk;
            cbbHocKy.SelectedValue = mahk;
            cbbMonHoc.SelectedText = tenmon;
            cbbMonHoc.SelectedValue = mamon;
            cbbLop.SelectedText = tenlop;
            cbbLop.SelectedValue = db.Lop.Where(l => l.tenLop == tenlop).Select(l => l.maLop).First();

            // không cho phép chỉnh sửa thông tin 
            groupBox1.Enabled = false;
            // không cho phép thêm
            btnThem.Enabled = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbbHocKy.Enabled = true;
            cbbHocKy.SelectedIndex = -1;
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            QuanlyDiem_Load(sender, e);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!check())
            {
                return;
            }
            

            Diem d = new Diem();
            d.maDiem = 0;
            d.maHS = Int32.Parse(cbbHocSinh.SelectedValue.ToString());
            d.maMon = Int32.Parse(cbbMonHoc.SelectedValue.ToString());
            d.diemGiuaKy = float.Parse(txtDiemGiuaKy.Text);
            d.diemCuoiKy = float.Parse(txtDiemCuoiKy.Text);

            db.Diem.Add(d);
            db.SaveChanges();

            MessageBox.Show("Nhập điểm thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            QuanlyDiem_Load(sender, e);
        }

        private void btnCapNhat_Click(object sender, EventArgs e)
        {
            DialogResult select = MessageBox.Show("Điểm sẽ được thay đổi. Tiếp tục ?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning);
            if (select == DialogResult.OK)
            {
                db.Diem.Where(d => d.maDiem == madiem).First().diemCuoiKy = float.Parse(txtDiemCuoiKy.Text);
                db.Diem.Where(d => d.maDiem == madiem).First().diemGiuaKy = float.Parse(txtDiemGiuaKy.Text);
                db.SaveChanges();
                QuanlyDiem_Load(sender, e);
            }
            else
                return;
        }

        private void fQuanlyDiem_DoubleClick(object sender, EventArgs e)
        {
            QuanlyDiem_Load(sender, e);
        }
    }
}
