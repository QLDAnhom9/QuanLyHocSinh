using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace G10_BTL
{
    class KetQuaHocTap
    {
        public int? maHS { get; set; }
        public string tenHocSinh { get; set; }
        public int? maMon { get; set; }
        public string mon { get; set; }
        public string lop { get; set; }
        public double? diemGiuaKy { get; set; }
        public double? diemCuoiKy { get; set; }
        public double? diemTongKet { get; set; }
        public string hocKy { get; set; }
        public string danhGia { get; set; }

        public KetQuaHocTap(int mahs, string tenhs, string lop, int mamon, string mon, double diemgiuaky, double diemcuoiky, double diemtk, string hocky, string danhgia)
        {
            this.maHS = mahs;
            this.tenHocSinh = tenhs;
            this.lop = lop;
            this.maMon = mamon;
            this.mon = mon;
            this.diemGiuaKy = diemgiuaky;
            this.diemCuoiKy = diemcuoiky;
            this.diemTongKet = diemtk;
            this.hocKy = hocky;
            this.danhGia = danhgia;
        }

        public string[] getCollection()
        {

            string[] cl = new string[] { this.maHS.ToString(),
            this.tenHocSinh,
            this.lop,
            this.maMon.ToString(),
            this.mon,
            this.diemGiuaKy.ToString(),
            this.diemCuoiKy.ToString() ,
            this.diemTongKet.ToString(),
            this.hocKy,
            this.danhGia};
            return cl;
        }
    }
}
