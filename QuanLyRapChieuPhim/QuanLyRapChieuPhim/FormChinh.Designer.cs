#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class FormChinh
    {
        private System.ComponentModel.IContainer components = null;

        private MenuStrip thanhMenu;
        private ToolStripMenuItem menuHeThong;
        private ToolStripMenuItem mucSaoLuu;
        private ToolStripMenuItem mucKhoiPhuc;
        private ToolStripSeparator phanCachHeThong;
        private ToolStripMenuItem mucGiaoDien;
        private ToolStripMenuItem mucThoat;
        private ToolStripMenuItem menuQuanLy;
        private ToolStripMenuItem mucPhim;
        private ToolStripMenuItem mucPhong;
        private ToolStripMenuItem mucSuatChieu;
        private ToolStripMenuItem mucBanVe;
        private ToolStripMenuItem mucDoanhThu;
        private ToolStripMenuItem menuCuaSo;
        private ToolStripMenuItem mucXepChong;
        private ToolStripMenuItem mucXepNgang;
        private ToolStripMenuItem mucXepDoc;
        private ToolStripSeparator phanCachCuaSo;
        private ToolStripMenuItem mucXepBieuTuong;
        private Panel bangChao;
        private Label nhanTieuDe;
        private Label nhanPhuDe;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            thanhMenu = new MenuStrip();
            menuHeThong = new ToolStripMenuItem();
            mucSaoLuu = new ToolStripMenuItem();
            mucKhoiPhuc = new ToolStripMenuItem();
            phanCachHeThong = new ToolStripSeparator();
            mucGiaoDien = new ToolStripMenuItem();
            mucThoat = new ToolStripMenuItem();
            menuQuanLy = new ToolStripMenuItem();
            mucPhim = new ToolStripMenuItem();
            mucPhong = new ToolStripMenuItem();
            mucSuatChieu = new ToolStripMenuItem();
            mucBanVe = new ToolStripMenuItem();
            mucDoanhThu = new ToolStripMenuItem();
            menuCuaSo = new ToolStripMenuItem();
            mucXepChong = new ToolStripMenuItem();
            mucXepNgang = new ToolStripMenuItem();
            mucXepDoc = new ToolStripMenuItem();
            phanCachCuaSo = new ToolStripSeparator();
            mucXepBieuTuong = new ToolStripMenuItem();
            bangChao = new Panel();
            nhanTieuDe = new Label();
            nhanPhuDe = new Label();
            thanhMenu.SuspendLayout();
            bangChao.SuspendLayout();
            SuspendLayout();

            // menuHeThong
            menuHeThong.DropDownItems.AddRange(new ToolStripItem[]
            {
                mucSaoLuu, mucKhoiPhuc, phanCachHeThong, mucGiaoDien, mucThoat
            });
            menuHeThong.Text = "Hệ thống";

            // mucSaoLuu
            mucSaoLuu.Text = "Sao lưu dữ liệu";
            mucSaoLuu.Click += mucSaoLuu_Click;

            // mucKhoiPhuc
            mucKhoiPhuc.Text = "Khôi phục dữ liệu";
            mucKhoiPhuc.Click += mucKhoiPhuc_Click;

            // mucGiaoDien
            mucGiaoDien.Text = "Đổi giao diện";
            mucGiaoDien.Click += mucGiaoDien_Click;

            // mucThoat
            mucThoat.Text = "Thoát";
            mucThoat.Click += mucThoat_Click;

            // menuQuanLy
            menuQuanLy.DropDownItems.AddRange(new ToolStripItem[]
            {
                mucPhim, mucPhong, mucSuatChieu, mucBanVe, mucDoanhThu
            });
            menuQuanLy.Text = "Quản lý";

            // mucPhim
            mucPhim.Text = "Phim";
            mucPhim.Click += mucPhim_Click;

            // mucPhong
            mucPhong.Text = "Phòng chiếu";
            mucPhong.Click += mucPhong_Click;

            // mucSuatChieu
            mucSuatChieu.Text = "Suất chiếu";
            mucSuatChieu.Click += mucSuatChieu_Click;

            // mucBanVe
            mucBanVe.Text = "Bán vé";
            mucBanVe.Click += mucBanVe_Click;

            // mucDoanhThu
            mucDoanhThu.Text = "Doanh thu";
            mucDoanhThu.Click += mucDoanhThu_Click;

            // menuCuaSo
            menuCuaSo.DropDownItems.AddRange(new ToolStripItem[]
            {
                mucXepChong, mucXepNgang, mucXepDoc, phanCachCuaSo, mucXepBieuTuong
            });
            menuCuaSo.Text = "Cửa sổ";

            // mucXepChong
            mucXepChong.Text = "Sắp xếp chồng";
            mucXepChong.Click += mucXepChong_Click;

            // mucXepNgang
            mucXepNgang.Text = "Sắp xếp ngang";
            mucXepNgang.Click += mucXepNgang_Click;

            // mucXepDoc
            mucXepDoc.Text = "Sắp xếp dọc";
            mucXepDoc.Click += mucXepDoc_Click;

            // mucXepBieuTuong
            mucXepBieuTuong.Text = "Sắp xếp biểu tượng";
            mucXepBieuTuong.Click += mucXepBieuTuong_Click;

            // thanhMenu
            thanhMenu.BackColor = Color.White;
            thanhMenu.Font = new Font("Segoe UI", 10F);
            thanhMenu.Items.AddRange(new ToolStripItem[] { menuHeThong, menuQuanLy, menuCuaSo });
            thanhMenu.Location = new Point(0, 0);
            thanhMenu.MdiWindowListItem = menuCuaSo;
            thanhMenu.Name = "thanhMenu";
            thanhMenu.Padding = new Padding(8, 5, 0, 5);
            thanhMenu.Size = new Size(1184, 34);
            thanhMenu.TabIndex = 0;
            thanhMenu.Text = "thanhMenu";

            // nhanTieuDe
            nhanTieuDe.Dock = DockStyle.Top;
            nhanTieuDe.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            nhanTieuDe.ForeColor = Color.FromArgb(35, 45, 60);
            nhanTieuDe.Height = 60;
            nhanTieuDe.Text = "QUẢN LÝ RẠP CHIẾU PHIM";
            nhanTieuDe.TextAlign = ContentAlignment.MiddleCenter;

            // nhanPhuDe
            nhanPhuDe.Dock = DockStyle.Top;
            nhanPhuDe.Font = new Font("Segoe UI", 12F);
            nhanPhuDe.ForeColor = Color.FromArgb(90, 100, 115);
            nhanPhuDe.Height = 40;
            nhanPhuDe.Text = "Chọn chức năng trong menu “Quản lý” để bắt đầu";
            nhanPhuDe.TextAlign = ContentAlignment.TopCenter;

            // bangChao
            bangChao.BackColor = Color.FromArgb(245, 247, 250);
            bangChao.Controls.Add(nhanPhuDe);
            bangChao.Controls.Add(nhanTieuDe);
            bangChao.Dock = DockStyle.Fill;
            bangChao.Padding = new Padding(30);

            // FormChinh
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1184, 721);
            Controls.Add(bangChao);
            Controls.Add(thanhMenu);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.FromArgb(35, 45, 60);
            IsMdiContainer = true;
            MainMenuStrip = thanhMenu;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "QUẢN LÝ RẠP CHIẾU PHIM";
            FormClosing += FormChinh_FormClosing;
            MdiChildActivate += FormChinh_MdiChildActivate;
            thanhMenu.ResumeLayout(false);
            thanhMenu.PerformLayout();
            bangChao.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
