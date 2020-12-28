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
    public partial class fKQHocTap : Form
    {
        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();
        List<Diem> listdiem = new List<Diem>();
        List<KetQuaHocTap> listkq = new List<KetQuaHocTap>();


        public fKQHocTap()
        {
            InitializeComponent();
        }

        private void fKQHocTap_Load(object sender, EventArgs e)
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

            Load_Data();
        }

        private void Load_Data()
        {
            dgvData.Rows.Clear();
            listdiem = db.Diem.ToList();
            if (listdiem == null)
                MessageBox.Show("Danh sách đang trống", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            else
                foreach (Diem a in listdiem)
                {
                    // tính điểm tổng kết và đánh giá
                    double diemTongKet = 0;
                    string danhgia = "";

                    if (a.Mon.tenMon == "Toán" || a.Mon.tenMon == "Tiếng Việt")
                    {
                        diemTongKet = ((double)a.diemGiuaKy + (double)a.diemCuoiKy * 2) / 3;
                        diemTongKet = Math.Round((double)diemTongKet, 2);
                        double? x = diemTongKet - (int)diemTongKet;
                        if (x >= 0.0 && x < 0.25)
                        {
                            diemTongKet = (int)diemTongKet;
                        }
                        else if (x >= 0.25 && x < 0.5)
                        {
                            diemTongKet = (int)diemTongKet + 0.5;
                        }
                        else if (x >= 0.5 && x < 0.75)
                        {
                            diemTongKet = (int)diemTongKet + 0.5;
                        }
                        else if (x >= 0.75 && x < 1.00)
                        {
                            diemTongKet = (int)diemTongKet + 1.0;
                        }
                        if (diemTongKet >= 8.0)
                        {
                            danhgia = "Giỏi";
                        }
                        else if (diemTongKet < 8.0 && diemTongKet >= 7.0)
                        {
                            danhgia = "Khá";
                        }
                        else if (diemTongKet < 7.0 && diemTongKet >= 6.0)
                        {
                            danhgia = "Khá";
                        }
                        else
                        {
                            danhgia = "Yếu";
                        }
                        a.danhGia = danhgia;
                    }
                    else
                    {
                        diemTongKet = (double)a.diemCuoiKy;
                        diemTongKet = Math.Round((double)diemTongKet, 2);
                        double? x = diemTongKet - (int)diemTongKet;
                        if (x >= 0.0 && x < 0.25)
                        {
                            diemTongKet = (int)diemTongKet;
                        }
                        else if (x >= 0.25 && x < 0.5)
                        {
                            diemTongKet = (int)diemTongKet + 0.5;
                        }
                        else if (x >= 0.5 && x < 0.75)
                        {
                            diemTongKet = (int)diemTongKet + 0.5;
                        }
                        else if (x >= 0.75 && x < 1.00)
                        {
                            diemTongKet = (int)diemTongKet + 1.0;
                        }
                        if (diemTongKet >= 9.0)
                        {
                            danhgia = "Giỏi";
                        }
                        else if (diemTongKet < 9.0 && diemTongKet >= 7.0)
                        {
                            danhgia = "Khá";
                        }
                        else if (diemTongKet < 7.0 && diemTongKet >= 5.0)
                        {
                            danhgia = "Trung Bình";
                        }
                        else
                        {
                            danhgia = "Yếu";
                        }
                        a.danhGia = danhgia;
                    }

                    KetQuaHocTap kq = new KetQuaHocTap((int)a.maHS, a.HocSinh.ten.ToString(),
                            a.HocSinh.Lop.tenLop, (int)a.maMon,
                            a.Mon.tenMon.ToString(), (double)a.diemGiuaKy,
                            (double)a.diemCuoiKy, diemTongKet, a.Mon.maHK.ToString(), a.danhGia);

                    listkq.Add(kq);
                    // đưa dữ liệu lên dataGridView
                    dgvData.Rows.Add(listkq.Last().getCollection());
                    dgvData.ClearSelection();
                }
        }

        private void cbbHocSinh_SelectedValueChanged(object sender, EventArgs e)
        {
            cbbHocKy.Enabled = true;
            cbbHocKy.SelectedIndex = -1;
            dgvData.Rows.Clear();
            foreach (KetQuaHocTap a in listkq)
            {
                // so sánh tên lớp giống nhau
                if (a.lop.Trim().ToUpper().Equals(cbbLop.Text.Trim().ToUpper()) && a.tenHocSinh.Trim().ToUpper().Equals(cbbHocSinh.Text.Trim().ToUpper()))
                {
                    dgvData.Rows.Add(a.getCollection());
                }
                else
                    continue;
            }
            dgvData.ClearSelection();
        }

        private void cbbMon_SelectedValueChanged(object sender, EventArgs e)
        {
            dgvData.Rows.Clear();
            foreach (KetQuaHocTap a in listkq)
            {
                // so sánh tên lớp giống nhau
                if (a.lop.Trim().ToUpper().Equals(cbbLop.Text.Trim().ToUpper()) && a.tenHocSinh.Trim().ToUpper().Equals(cbbHocSinh.Text.Trim().ToUpper()) && a.hocKy.Trim().Equals(cbbHocKy.SelectedValue.ToString().Trim()) && a.maMon == (int)cbbMonHoc.SelectedValue)
                {
                    dgvData.Rows.Add(a.getCollection());
                }
                else
                    continue;
            }
            dgvData.ClearSelection();
        }

        private void cbbLop_SelectedValueChanged(object sender, EventArgs e)
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
                
            }

            // Lọc dữ liệu trên datagridview
            dgvData.Rows.Clear();
            foreach (KetQuaHocTap a in listkq)
            {
                // so sánh tên lớp giống nhau
                if (a.lop.Trim().ToUpper().Equals(cbbLop.Text.Trim().ToUpper()))
                {
                    dgvData.Rows.Add(a.getCollection());
                }
                else
                    continue;
            }
            dgvData.ClearSelection();
        }

        private void fKQHocTap_DoubleClick(object sender, EventArgs e)
        {
            // Tùy chỉnh vô hiệu hóa
            groupBox1.Enabled = true;
            cbbLop.Enabled = true;
            cbbHocSinh.Enabled = false;
            cbbHocKy.Enabled = false;
            cbbMonHoc.Enabled = false;
        }

        private void cbbHocKy_SelectedValueChanged(object sender, EventArgs e)
        {
            try
            {
                cbbMonHoc.Enabled = true;
                // Lấy mã lớp được chọn
                int malop = (int)cbbLop.SelectedValue;
                MessageBox.Show("Wrong Parse", "OK"); 
                int mahk = (int)cbbHocKy.SelectedValue;
                // Đưa các môn học của học kỳ được chọn lên Combobox Môn học       
                cbbMonHoc.DataSource = db.Mon.Where(m => m.trangThai == true && m.maHK == mahk && m.maLop == malop).ToList();
                cbbMonHoc.SelectedIndex = -1;
            }
            catch (Exception)
            {              
                
            }
            dgvData.Rows.Clear();
            foreach (KetQuaHocTap a in listkq)
            {
                // so sánh tên lớp giống nhau
                // a.hocKy.Trim().Equals(cbbHocKy.SelectedValue.ToString().Trim())
                if (a.lop.Trim().ToUpper().Equals(cbbLop.Text.Trim().ToUpper()) && a.tenHocSinh.Trim().ToUpper().Equals(cbbHocSinh.Text.Trim().ToUpper()) && Int32.Parse(a.hocKy) == (int)cbbHocKy.SelectedValue)
                {
                    dgvData.Rows.Add(a.getCollection());
                }
                else
                    continue;
            }
            dgvData.ClearSelection();
        }
    }
}
