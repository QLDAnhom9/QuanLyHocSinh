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
        GiaoVien gv = new GiaoVien();
        HocSinh hs = new HocSinh();
        List<KetQuaHocTap> list = new List<KetQuaHocTap>();
        List<HocSinh> lhs, lhs1 = new List<HocSinh>();
        List<Mon> mon = new List<Mon>();

        public fKQHocTap()
        {
            InitializeComponent();
        }

        private void fKQHocTap_Load(object sender, EventArgs e)
        {
            gv = this.Tag as GiaoVien;
            hs = this.Tag as HocSinh;
            lhs = new List<HocSinh>();

            cbbHocKy.Items.Clear();
            cbbHocSinh.Items.Clear();
            cbbLop.Items.Clear();
            cbbMon.Items.Clear();

            List<HocKy> hk = db.HocKy.ToList();
            cbbHocKy.Items.Add("Tất cả");
            cbbHocKy.SelectedIndex = 0;
            foreach (HocKy i in hk)
            {
                cbbHocKy.Items.Add(i.tenHK);
            }

            List<Lop> lop = new List<Lop>();
            bool ck = true;
            cbbHocSinh.Items.Add("Tất cả");
            cbbHocSinh.SelectedIndex = 0;
            if (gv != null && gv.ten != "admin")
            {
                List<Lop> l = db.Lop.Where(m => m.trangThai == true).ToList();
                List<Mon> listmon = db.Mon.Where(m => m.gvDay == gv.maGV).ToList();
                foreach (Lop i in l)
                {
                    Mon x = listmon.Where(m => m.maLop == i.maLop).FirstOrDefault();
                    if (x != null)
                    {
                        lop.Add(i);
                    }
                }
            }
            else if (gv != null && gv.ten == "admin")
            {
                lop = db.Lop.Where(m => m.trangThai == true).ToList();
            }
            else if (hs != null)
            {
                lop = db.Lop.Where(m => m.maLop == hs.maLop && m.trangThai == true).ToList();
                ck = false;
                cbbHocSinh.Items.Add(hs.ten);
            }
            cbbLop.Items.Add("Tất cả");
            cbbLop.SelectedIndex = 0;
            foreach (Lop i in lop)
            {
                cbbLop.Items.Add(i.tenLop);
                if (ck)
                {
                    List<HocSinh> y = db.HocSinh.Where(m => m.maLop == i.maLop && m.trangThai == true).ToList();
                    foreach (HocSinh j in y)
                    {
                        lhs.Add(j);
                        cbbHocSinh.Items.Add(j.ten + "(" + i.tenLop + ")");
                    }
                }
            }

            mon = db.Mon.Where(m=>m.trangThai == true).ToList();
            
            if (gv!=null && gv.ten != "admin")
            {
                mon = mon.Where(m => m.gvDay == gv.maGV && m.trangThai == true).ToList();
            }
            if (hs != null)
            {
                Lop x = db.Lop.Where(m => m.maLop == hs.maLop && m.trangThai == true).FirstOrDefault();
                mon = mon.Where(m => m.maLop == x.maLop).ToList();
            }
            cbbMon.Items.Add("Tất cả");
            cbbMon.SelectedIndex = 0;
            foreach (Mon i in mon)
            {
                Lop x = db.Lop.Where(m => m.maLop == i.maLop && m.trangThai == true).FirstOrDefault();
                if (x != null)
                {
                    cbbMon.Items.Add(i.tenMon + "(" + x.tenLop + ")");
                }
            }
        }

        private void Load_Data()
        {
            try
            {
                dgvData.Rows.Clear();
                list = (from i in db.Diem
                        join j in db.Mon on i.maMon equals j.maMon
                        join k in db.HocSinh on i.maHS equals k.maHS
                        join m in db.HocKy on j.maHK equals m.maHK
                        join n in db.Lop on k.maLop equals n.maLop
                        where n.trangThai == true && k.trangThai == true && j.trangThai == true && i.trangThai == true
                        select new KetQuaHocTap
                        {
                            tenHocSinh = k.ten,
                            mon = j.tenMon,
                            diemGiuaKy = i.diemGiuaKy,
                            diemCuoiKy = i.diemCuoiKy,
                            hocKy = m.tenHK,
                            lop = n.tenLop,
                            diemTongKet = 0,
                            maGV = j.gvDay,
                            maLop = n.maLop,
                            maHS = k.maHS
                        }).ToList();
                if (gv != null && gv.ten != "admin")
                {
                    list = list.Where(m => m.maGV == gv.maGV).ToList();
                    
                }
                else if (hs != null)
                {
                    int malop = int.Parse(db.Lop.Where(m => m.maLop == hs.maLop).Select(m => m.maLop).FirstOrDefault().ToString());
                    list = list.Where(m => m.maLop == malop && m.maHS == hs.maHS).ToList();
                }
                try
                {
                    foreach (KetQuaHocTap i in list)
                    {
                        string danhgia = "";
                        if (i.mon == "Toán" || i.mon == "Tiếng Việt")
                        {
                            if (i.diemGiuaKy != -1 && i.diemCuoiKy != -1 && i.diemCuoiKy.ToString() != "" && i.diemGiuaKy.ToString() != "")
                            {
                                i.diemTongKet = (i.diemGiuaKy + i.diemCuoiKy * 2) / 3;
                                i.diemTongKet = Math.Round((double)i.diemTongKet, 2);
                                double? x = i.diemTongKet - (int)i.diemTongKet;
                                if (x >= 0.0 && x < 0.25)
                                {
                                    i.diemTongKet = (int)i.diemTongKet;
                                }
                                else if (x >= 0.25 && x < 0.5)
                                {
                                    i.diemTongKet = (int)i.diemTongKet + 0.5;
                                }
                                else if (x >= 0.5 && x < 0.75)
                                {
                                    i.diemTongKet = (int)i.diemTongKet + 0.5;
                                }
                                else if (x >= 0.75 && x < 1.00)
                                {
                                    i.diemTongKet = (int)i.diemTongKet + 1.0;
                                }
                                if (i.diemTongKet >= 8.0)
                                {
                                    danhgia = "Giỏi";
                                }
                                else if (i.diemTongKet < 8.0 && i.diemTongKet >= 7.0)
                                {
                                    danhgia = "Khá";
                                }
                                else if (i.diemTongKet < 7.0 && i.diemTongKet >= 6.0)
                                {
                                    danhgia = "Khá";
                                }
                                else
                                {
                                    danhgia = "Yếu";
                                }
                                i.danhGia = danhgia;
                            }
                            else
                            {
                                i.diemTongKet = -1;
                            }
                        }
                        else
                        {
                            if (i.diemCuoiKy != -1 && i.diemCuoiKy.ToString() != "")
                            {
                                i.diemTongKet = i.diemCuoiKy;
                                i.diemTongKet = Math.Round((double)i.diemTongKet, 2);
                                double? x = i.diemTongKet - (int)i.diemTongKet;
                                if(x>=0.0 && x < 0.25)
                                {
                                    i.diemTongKet = (int)i.diemTongKet;
                                }
                                else if (x >= 0.25 && x < 0.5)
                                {
                                    i.diemTongKet = (int)i.diemTongKet + 0.5;
                                }
                                else if (x >= 0.5 && x < 0.75)
                                {
                                    i.diemTongKet = (int)i.diemTongKet + 0.5;
                                }
                                else if (x >= 0.75 && x < 1.00)
                                {
                                    i.diemTongKet = (int)i.diemTongKet + 1.0;
                                }
                                if (i.diemTongKet >= 9.0)
                                {
                                    danhgia = "Giỏi";
                                }
                                else if (i.diemTongKet < 9.0 && i.diemTongKet >= 7.0)
                                {
                                    danhgia = "Khá";
                                }
                                else if (i.diemTongKet < 7.0 && i.diemTongKet >= 5.0)
                                {
                                    danhgia = "Trung Bình";
                                }
                                else
                                {
                                    danhgia = "Yếu";
                                }
                                i.danhGia = danhgia;
                            }
                            else
                            {
                                i.diemTongKet = -1;
                            }
                        }
                    }

                    if (cbbLop.SelectedIndex != 0)
                    {
                        int idLop = int.Parse(cbbLop.SelectedIndex.ToString());
                        list = list.Where(m => m.lop == (db.Lop.Where(n => n.maLop == idLop).Select(n => n.tenLop).FirstOrDefault())).ToList();
                    }

                    if (cbbHocKy.SelectedIndex != 0)
                    {
                        int idHocKy = int.Parse(cbbHocKy.SelectedIndex.ToString());
                        list = list.Where(m => m.hocKy == (db.HocKy.Where(n => n.maHK == idHocKy).Select(n => n.tenHK).FirstOrDefault())).ToList();
                    }
                    if (cbbHocSinh.SelectedIndex != 0 && cbbHocSinh.SelectedIndex !=-1)
                    {
                        string tenhs1 = cbbHocSinh.Text, tenhs = "";
                        foreach (char i in tenhs1)
                        {
                            if (i != '(')
                            {
                                tenhs += i;
                            }
                            else
                            {
                                break;
                            }
                        }
                        list = list.Where(m => m.tenHocSinh == tenhs).ToList();
                    }
                    if (cbbMon.SelectedIndex != 0 && cbbMon.SelectedIndex !=-1)
                    {
                        string tenmon1 = cbbMon.Text, tenmon = "";
                        foreach (char i in tenmon1)
                        {
                            if (i != '(')
                            {
                                tenmon += i;
                            }
                            else
                            {
                                break;
                            }
                        }
                        list = list.Where(m => m.mon == tenmon).ToList();
                    }                                                
                    foreach (KetQuaHocTap i in list)
                    {
                        if (i.diemCuoiKy != -1 && i.diemGiuaKy != -1)
                        {
                            if (i.diemTongKet == -1)
                            {
                                dgvData.Rows.Add(i.tenHocSinh, i.mon, i.diemGiuaKy, i.diemCuoiKy, "", i.hocKy, i.lop, i.danhGia);
                            }
                            else
                            {
                                dgvData.Rows.Add(i.tenHocSinh, i.mon, i.diemGiuaKy, i.diemCuoiKy, i.diemTongKet, i.hocKy, i.lop, i.danhGia);
                            }
                        }
                        else if (i.diemCuoiKy != -1 && i.diemGiuaKy == -1)
                        {
                            dgvData.Rows.Add(i.tenHocSinh, i.mon, "", i.diemCuoiKy, "", i.hocKy, i.lop, i.danhGia);
                        }
                        else if (i.diemCuoiKy == -1 && i.diemGiuaKy != -1)
                        {
                            dgvData.Rows.Add(i.tenHocSinh, i.mon, i.diemGiuaKy, "", "", i.hocKy, i.lop, i.danhGia);
                        }
                    }
                }
                catch (Exception)
                {
                    return;
                }
            }
            catch (Exception)
            {
                return;
            }
        }

        private void cbbHocSinh_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Load_Data();
            }
            catch(Exception)
            {
                return;
            }
        }

        private void cbbHocKy_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cbbMon.Items.Clear();
                cbbMon.Items.Add("Tất cả");
                cbbMon.SelectedIndex = 0;

                Lop x = db.Lop.Where(m => m.tenLop == cbbLop.Text && m.trangThai == true).FirstOrDefault();
                List<Mon> mon1 = new List<Mon>();
                if (x != null)
                {
                    mon1 = mon.Where(m => m.maLop == x.maLop).ToList();
                }
                else
                {
                    mon1 = mon;
                }
                if(cbbHocKy.SelectedIndex == 0)
                {
                    mon1 = mon;
                }
                else
                {
                    mon1 = mon.Where(m => m.maHK == cbbHocKy.SelectedIndex).ToList();
                }

                foreach (Mon i in mon1)
                {
                    cbbMon.Items.Add(i.tenMon);
                }

                Load_Data();
            }
            catch (Exception)
            {
                return;
            }
        }

        private void cbbMon_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Load_Data();
            }
            catch(Exception)
            {
                return;
            }
        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cbbLop_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lhs1 = lhs;
                
                cbbMon.Items.Clear();
                cbbHocSinh.Items.Clear();
                cbbHocSinh.Items.Add("Tất cả");
                cbbHocSinh.SelectedIndex = 0;

                Lop x = db.Lop.Where(m => m.tenLop == cbbLop.Text && m.trangThai == true).FirstOrDefault();
                lhs1 = lhs1.Where(m => m.maLop == x.maLop).ToList();
                List<Mon> mon1 = mon.Where(m => m.maLop == x.maLop).ToList();
                
                if(cbbLop.SelectedIndex ==0)
                {
                    lhs1 = lhs;
                    mon1 = mon;
                }

                foreach (HocSinh i in lhs1)
                {
                    cbbHocSinh.Items.Add(i.ten);
                }
                foreach (Mon i in mon1)
                {
                    cbbMon.Items.Add(i.tenMon);
                }

                Load_Data();
            }
            catch(Exception)
            {
                return;
            }
        }
    }
}
