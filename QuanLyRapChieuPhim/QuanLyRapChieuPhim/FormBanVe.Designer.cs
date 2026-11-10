#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class FormBanVe
    {
        private System.ComponentModel.IContainer components = null;

        private FlowLayoutPanel bangTren;
        private Label nhanSuat;
        private ComboBox oSuat;
        private FlowLayoutPanel bangLoaiVe;
        private Label nhanLoaiVe;
        private RadioButton oNguoiLon;
        private RadioButton oHocSinh;
        private Label nhanBapNuoc;
        private ComboBox oBapNuoc;
        private Button nutBan;
        private Panel bangPhai;
        private Label tieuDeDaBan;
        private ListView danhSachDaBan;
        private Label thongTinTraVe;
        private FlowLayoutPanel luongNut;
        private Button nutTraVe;
        private Button nutInVe;
        private FlowLayoutPanel bangDuoi;
        private Label trangThai;
        private Label tamTinh;
        private Label chuThich;
        private Panel khungGhe;
        private FlowLayoutPanel luongGhe;
        private System.Windows.Forms.Timer henGio;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            bangTren = new FlowLayoutPanel();
            bangLoaiVe = new FlowLayoutPanel();
            oNguoiLon = new RadioButton();
            oHocSinh = new RadioButton();
            nhanSuat = new Label();
            oSuat = new ComboBox();
            nhanLoaiVe = new Label();
            nhanBapNuoc = new Label();
            oBapNuoc = new ComboBox();
            nutBan = new Button();
            bangPhai = new Panel();
            danhSachDaBan = new ListView();
            thongTinTraVe = new Label();
            luongNut = new FlowLayoutPanel();
            nutTraVe = new Button();
            nutInVe = new Button();
            tieuDeDaBan = new Label();
            bangDuoi = new FlowLayoutPanel();
            trangThai = new Label();
            tamTinh = new Label();
            chuThich = new Label();
            khungGhe = new Panel();
            luongGhe = new FlowLayoutPanel();
            henGio = new System.Windows.Forms.Timer(components);
            bangTren.SuspendLayout();
            bangLoaiVe.SuspendLayout();
            bangPhai.SuspendLayout();
            luongNut.SuspendLayout();
            bangDuoi.SuspendLayout();
            khungGhe.SuspendLayout();
            SuspendLayout();

            // nhanSuat
            nhanSuat.AutoSize = true;
            nhanSuat.ForeColor = Color.FromArgb(35, 45, 60);
            nhanSuat.Margin = new Padding(5, 8, 3, 3);
            nhanSuat.Text = "Suất chiếu";
            nhanSuat.TextAlign = ContentAlignment.MiddleLeft;

            // oSuat
            oSuat.DropDownStyle = ComboBoxStyle.DropDownList;
            oSuat.Margin = new Padding(3, 4, 3, 4);
            oSuat.Size = new Size(430, 23);
            oSuat.SelectedIndexChanged += oSuat_SelectedIndexChanged;

            // nhanLoaiVe
            nhanLoaiVe.AutoSize = true;
            nhanLoaiVe.ForeColor = Color.FromArgb(35, 45, 60);
            nhanLoaiVe.Margin = new Padding(0, 10, 4, 0);
            nhanLoaiVe.Text = "Loại vé";

            // oNguoiLon
            oNguoiLon.AutoSize = true;
            oNguoiLon.BackColor = Color.FromArgb(245, 247, 250);
            oNguoiLon.Checked = true;
            oNguoiLon.ForeColor = Color.FromArgb(35, 45, 60);
            oNguoiLon.Margin = new Padding(2, 9, 14, 0);
            oNguoiLon.Text = "Người lớn";
            oNguoiLon.CheckedChanged += oNguoiLon_CheckedChanged;

            // oHocSinh
            oHocSinh.AutoSize = true;
            oHocSinh.BackColor = Color.FromArgb(245, 247, 250);
            oHocSinh.ForeColor = Color.FromArgb(35, 45, 60);
            oHocSinh.Margin = new Padding(2, 9, 0, 0);
            oHocSinh.Text = "Học sinh - Sinh viên";
            oHocSinh.CheckedChanged += oHocSinh_CheckedChanged;

            // bangLoaiVe
            bangLoaiVe.AutoSize = true;
            bangLoaiVe.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bangLoaiVe.BackColor = Color.FromArgb(245, 247, 250);
            bangLoaiVe.Controls.Add(nhanLoaiVe);
            bangLoaiVe.Controls.Add(oNguoiLon);
            bangLoaiVe.Controls.Add(oHocSinh);
            bangLoaiVe.Margin = new Padding(8, 0, 8, 0);
            bangLoaiVe.WrapContents = false;

            // nhanBapNuoc
            nhanBapNuoc.AutoSize = true;
            nhanBapNuoc.ForeColor = Color.FromArgb(35, 45, 60);
            nhanBapNuoc.Margin = new Padding(5, 8, 3, 3);
            nhanBapNuoc.Text = "Bắp nước";
            nhanBapNuoc.TextAlign = ContentAlignment.MiddleLeft;

            // oBapNuoc
            oBapNuoc.DropDownStyle = ComboBoxStyle.DropDownList;
            oBapNuoc.Margin = new Padding(3, 4, 3, 4);
            oBapNuoc.Size = new Size(235, 23);
            oBapNuoc.SelectedIndexChanged += oBapNuoc_SelectedIndexChanged;

            // nutBan
            nutBan.AutoSize = true;
            nutBan.BackColor = Color.FromArgb(35, 110, 220);
            nutBan.Cursor = Cursors.Hand;
            nutBan.FlatStyle = FlatStyle.Flat;
            nutBan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutBan.ForeColor = Color.White;
            nutBan.Margin = new Padding(4);
            nutBan.Padding = new Padding(10, 6, 10, 6);
            nutBan.Text = "Bán vé đã chọn";
            nutBan.UseVisualStyleBackColor = false;
            nutBan.Click += nutBan_Click;

            // bangTren
            bangTren.AutoSize = true;
            bangTren.BackColor = Color.FromArgb(245, 247, 250);
            bangTren.Controls.Add(nhanSuat);
            bangTren.Controls.Add(oSuat);
            bangTren.Controls.Add(bangLoaiVe);
            bangTren.Controls.Add(nhanBapNuoc);
            bangTren.Controls.Add(oBapNuoc);
            bangTren.Controls.Add(nutBan);
            bangTren.Dock = DockStyle.Top;
            bangTren.Padding = new Padding(10);

            // tieuDeDaBan
            tieuDeDaBan.Dock = DockStyle.Top;
            tieuDeDaBan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            tieuDeDaBan.ForeColor = Color.FromArgb(35, 45, 60);
            tieuDeDaBan.Height = 28;
            tieuDeDaBan.Text = "VÉ ĐÃ BÁN";

            // danhSachDaBan
            danhSachDaBan.BackColor = Color.White;
            danhSachDaBan.BorderStyle = BorderStyle.FixedSingle;
            danhSachDaBan.Columns.Add("Ghế", 42);
            danhSachDaBan.Columns.Add("Loại vé", 92);
            danhSachDaBan.Columns.Add("Tổng", 80);
            danhSachDaBan.Columns.Add("Bán lúc", 86);
            danhSachDaBan.Dock = DockStyle.Fill;
            danhSachDaBan.ForeColor = Color.FromArgb(35, 45, 60);
            danhSachDaBan.FullRowSelect = true;
            danhSachDaBan.HideSelection = false;
            danhSachDaBan.MultiSelect = false;
            danhSachDaBan.ShowItemToolTips = true;
            danhSachDaBan.View = View.Details;
            danhSachDaBan.SelectedIndexChanged += danhSachDaBan_SelectedIndexChanged;
            danhSachDaBan.DoubleClick += danhSachDaBan_DoubleClick;

            // thongTinTraVe
            thongTinTraVe.Dock = DockStyle.Bottom;
            thongTinTraVe.ForeColor = Color.FromArgb(90, 100, 115);
            thongTinTraVe.Height = 56;
            thongTinTraVe.Padding = new Padding(2, 4, 2, 4);

            // nutTraVe
            nutTraVe.AutoSize = true;
            nutTraVe.BackColor = Color.Firebrick;
            nutTraVe.Cursor = Cursors.Hand;
            nutTraVe.Enabled = false;
            nutTraVe.FlatStyle = FlatStyle.Flat;
            nutTraVe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutTraVe.ForeColor = Color.White;
            nutTraVe.Margin = new Padding(4);
            nutTraVe.Padding = new Padding(10, 6, 10, 6);
            nutTraVe.Text = "Trả vé đã chọn";
            nutTraVe.UseVisualStyleBackColor = false;
            nutTraVe.Click += nutTraVe_Click;

            // nutInVe
            nutInVe.AutoSize = true;
            nutInVe.BackColor = Color.FromArgb(90, 100, 115);
            nutInVe.Cursor = Cursors.Hand;
            nutInVe.FlatStyle = FlatStyle.Flat;
            nutInVe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutInVe.ForeColor = Color.White;
            nutInVe.Margin = new Padding(4);
            nutInVe.Padding = new Padding(10, 6, 10, 6);
            nutInVe.Text = "In lại vé";
            nutInVe.UseVisualStyleBackColor = false;
            nutInVe.Click += nutInVe_Click;

            // luongNut
            luongNut.BackColor = Color.FromArgb(245, 247, 250);
            luongNut.Controls.Add(nutTraVe);
            luongNut.Controls.Add(nutInVe);
            luongNut.Dock = DockStyle.Bottom;
            luongNut.Height = 84;
            luongNut.WrapContents = true;

            // bangPhai
            bangPhai.BackColor = Color.FromArgb(245, 247, 250);
            bangPhai.Controls.Add(danhSachDaBan);
            bangPhai.Controls.Add(thongTinTraVe);
            bangPhai.Controls.Add(luongNut);
            bangPhai.Controls.Add(tieuDeDaBan);
            bangPhai.Dock = DockStyle.Right;
            bangPhai.Padding = new Padding(8);
            bangPhai.Width = 340;

            // trangThai
            trangThai.AutoSize = true;
            trangThai.ForeColor = Color.FromArgb(35, 45, 60);
            trangThai.Margin = new Padding(10, 10, 18, 0);

            // tamTinh
            tamTinh.AutoSize = true;
            tamTinh.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            tamTinh.ForeColor = Color.FromArgb(35, 110, 220);
            tamTinh.Margin = new Padding(4, 7, 10, 0);

            // chuThich
            chuThich.AutoSize = true;
            chuThich.ForeColor = Color.FromArgb(35, 45, 60);
            chuThich.Margin = new Padding(8, 10, 0, 0);
            chuThich.TextAlign = ContentAlignment.MiddleLeft;

            // bangDuoi
            bangDuoi.AutoSize = true;
            bangDuoi.BackColor = Color.FromArgb(245, 247, 250);
            bangDuoi.Controls.Add(trangThai);
            bangDuoi.Controls.Add(tamTinh);
            bangDuoi.Controls.Add(chuThich);
            bangDuoi.Dock = DockStyle.Bottom;

            // luongGhe
            luongGhe.AutoSize = true;
            luongGhe.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            luongGhe.BackColor = Color.White;
            luongGhe.FlowDirection = FlowDirection.TopDown;
            luongGhe.Location = new Point(0, 0);
            luongGhe.WrapContents = false;

            // khungGhe
            khungGhe.AutoScroll = true;
            khungGhe.BackColor = Color.White;
            khungGhe.Controls.Add(luongGhe);
            khungGhe.Dock = DockStyle.Fill;
            khungGhe.Padding = new Padding(10);

            // henGio
            henGio.Interval = 20000;
            henGio.Tick += henGio_Tick;

            // FormBanVe
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1134, 641);
            Controls.Add(khungGhe);
            Controls.Add(bangPhai);
            Controls.Add(bangDuoi);
            Controls.Add(bangTren);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(35, 45, 60);
            MinimumSize = new Size(1050, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Bán vé";
            FormClosed += FormBanVe_FormClosed;
            bangTren.ResumeLayout(false);
            bangTren.PerformLayout();
            bangLoaiVe.ResumeLayout(false);
            bangLoaiVe.PerformLayout();
            bangPhai.ResumeLayout(false);
            luongNut.ResumeLayout(false);
            luongNut.PerformLayout();
            bangDuoi.ResumeLayout(false);
            bangDuoi.PerformLayout();
            khungGhe.ResumeLayout(false);
            khungGhe.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
