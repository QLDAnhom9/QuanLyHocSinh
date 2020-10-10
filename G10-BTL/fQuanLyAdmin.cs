using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace G10_BTL
{
    public partial class QuanLyAdmin : Form
    {
        public QuanLyAdmin()
        {
            InitializeComponent();
        }

        QuanLyTruongHocEntities db = new QuanLyTruongHocEntities();
        HocSinh hs = new HocSinh();
        GiaoVien gv = new GiaoVien();

        private void trangChuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.InitializeComponent();
            GUI.fTrangChu fhs = new GUI.fTrangChu();
            fhs.MdiParent = this;
            //hs.StartPosition = FormStartPosition.CenterParent;
            if (hs != null)
            {
                fhs.Tag = hs;
            }
            else
            {
                fhs.Tag = gv;
            }
            fhs.Show();
        }

        private void QuanLyAdmin_Load(object sender, EventArgs e)
        {
            hs = this.Tag as HocSinh;
            gv = this.Tag as GiaoVien;
            trangChuToolStripMenuItem_Click(sender, e);
        }

        private void quanLyHcSinhToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gv != null)
            {
                Lop l = db.Lop.Where(m => m.maGVCN == gv.maGV).FirstOrDefault();
                if (l == null && gv.ten != "admin")
                {
                    MessageBox.Show("Bạn không được sử dụng chức năng này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                this.Controls.Clear();
                this.InitializeComponent();
                GUI.fQuanlyHocSinh hs = new GUI.fQuanlyHocSinh();
                hs.Tag = gv;
                hs.MdiParent = this;
                //hs.StartPosition = FormStartPosition.CenterParent;
                hs.Show();
            }
            else
            {
                MessageBox.Show("Bạn không được sử dụng chức năng này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void quanLyGiaoVienToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gv != null && gv.ten == "admin")
            {
                this.Controls.Clear();
                this.InitializeComponent();
                GUI.fQuanlyGiaoVien hs = new GUI.fQuanlyGiaoVien();
                hs.MdiParent = this;
                //hs.StartPosition = FormStartPosition.CenterParent;
                hs.Show();
            }
            else
            {
                MessageBox.Show("Bạn không được sử dụng chức năng này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void lopHocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gv != null && gv.ten == "admin")
            {
                this.Controls.Clear();
                this.InitializeComponent();
                GUI.fLop hs = new GUI.fLop();
                hs.MdiParent = this;
                //hs.StartPosition = FormStartPosition.CenterParent;
                hs.Show();
            }
            else
            {
                MessageBox.Show("Bạn không được sử dụng chức năng này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void monHocToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gv != null && gv.ten == "admin")
            {
                this.Controls.Clear();
                this.InitializeComponent();
                GUI.fMonHoc hs = new GUI.fMonHoc();
                hs.MdiParent = this;
                //hs.StartPosition = FormStartPosition.CenterParent;
                hs.Show();
            }
            else
            {
                MessageBox.Show("Bạn không được sử dụng chức năng này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void ketQuaHocTapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            this.Controls.Clear();
            this.InitializeComponent();
            GUI.fKQHocTap h = new GUI.fKQHocTap();
            h.MdiParent = this;
            //hs.StartPosition = FormStartPosition.CenterParent;
            if (hs != null)
            {
                h.Tag = hs;
            }
            else
            {
                h.Tag = gv;
            }
            h.Show();
           
        }

        private void thoatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Bạn có muốn thoát ứng dụng không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if(dialogResult == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void quảnLýĐiểmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gv != null)
            {
                this.Controls.Clear();
                this.InitializeComponent();
                GUI.fQuanlyDiem d = new GUI.fQuanlyDiem();
                d.MdiParent = this;
                d.Tag = gv;
                //hs.StartPosition = FormStartPosition.CenterParent;
                d.Show();
            }
            else
            {
                MessageBox.Show("Bạn không được sử dụng chức năng này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
