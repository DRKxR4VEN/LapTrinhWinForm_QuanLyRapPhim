#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class FormPhim
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel bangTren;
        private FlowLayoutPanel luongTruong;
        private FlowLayoutPanel luongPoster;
        private FlowLayoutPanel luongNut;
        private Panel khungMa;
        private Panel khungTen;
        private Panel khungTheLoai;
        private Panel khungThoiLuong;
        private Panel khungPoster;
        private Panel noiDung;
        private Panel bangXemTruoc;
        private Panel bangDanhSach;
        private Label nhanMa;
        private Label nhanTen;
        private Label nhanTheLoai;
        private Label nhanThoiLuong;
        private Label nhanPoster;
        private Label tieuDeXemTruoc;
        private Label tieuDeDanhSach;
        private TextBox oMa;
        private TextBox oTen;
        private TextBox oTheLoai;
        private TextBox oThoiLuong;
        private TextBox oPoster;
        private PictureBox anhXemTruoc;
        private ListView danhSach;
        private ImageList anhPoster;
        private DataGridView bang;
        private Button nutChonPoster;
        private Button nutThem;
        private Button nutSua;
        private Button nutXoa;
        private Button nutNhapCsv;
        private Button nutLoc;
        private Button nutLamMoi;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            bangTren = new TableLayoutPanel();
            luongTruong = new FlowLayoutPanel();
            khungMa = new Panel();
            nhanMa = new Label();
            oMa = new TextBox();
            khungTen = new Panel();
            nhanTen = new Label();
            oTen = new TextBox();
            khungTheLoai = new Panel();
            nhanTheLoai = new Label();
            oTheLoai = new TextBox();
            khungThoiLuong = new Panel();
            nhanThoiLuong = new Label();
            oThoiLuong = new TextBox();
            luongPoster = new FlowLayoutPanel();
            khungPoster = new Panel();
            nhanPoster = new Label();
            oPoster = new TextBox();
            nutChonPoster = new Button();
            luongNut = new FlowLayoutPanel();
            nutThem = new Button();
            nutSua = new Button();
            nutXoa = new Button();
            nutNhapCsv = new Button();
            nutLoc = new Button();
            nutLamMoi = new Button();
            noiDung = new Panel();
            bang = new DataGridView();
            bangDanhSach = new Panel();
            danhSach = new ListView();
            anhPoster = new ImageList(components);
            tieuDeDanhSach = new Label();
            bangXemTruoc = new Panel();
            anhXemTruoc = new PictureBox();
            tieuDeXemTruoc = new Label();
            var kieuOCot = new DataGridViewCellStyle();
            var kieuTieuDe = new DataGridViewCellStyle();
            bangTren.SuspendLayout();
            luongTruong.SuspendLayout();
            khungMa.SuspendLayout();
            khungTen.SuspendLayout();
            khungTheLoai.SuspendLayout();
            khungThoiLuong.SuspendLayout();
            luongPoster.SuspendLayout();
            khungPoster.SuspendLayout();
            luongNut.SuspendLayout();
            noiDung.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bang).BeginInit();
            bangDanhSach.SuspendLayout();
            bangXemTruoc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)anhXemTruoc).BeginInit();
            SuspendLayout();

            // nhanMa
            nhanMa.AutoSize = true;
            nhanMa.ForeColor = Color.FromArgb(90, 100, 115);
            nhanMa.Location = new Point(0, 0);
            nhanMa.Text = "Mã phim";

            // oMa
            oMa.BackColor = Color.White;
            oMa.ForeColor = Color.FromArgb(35, 45, 60);
            oMa.Location = new Point(0, 22);
            oMa.Size = new Size(112, 23);

            // khungMa
            khungMa.BackColor = Color.FromArgb(245, 247, 250);
            khungMa.Controls.Add(nhanMa);
            khungMa.Controls.Add(oMa);
            khungMa.Margin = new Padding(0, 0, 10, 4);
            khungMa.Size = new Size(120, 58);

            // nhanTen
            nhanTen.AutoSize = true;
            nhanTen.ForeColor = Color.FromArgb(90, 100, 115);
            nhanTen.Location = new Point(0, 0);
            nhanTen.Text = "Tên phim";

            // oTen
            oTen.BackColor = Color.White;
            oTen.ForeColor = Color.FromArgb(35, 45, 60);
            oTen.Location = new Point(0, 22);
            oTen.Size = new Size(212, 23);

            // khungTen
            khungTen.BackColor = Color.FromArgb(245, 247, 250);
            khungTen.Controls.Add(nhanTen);
            khungTen.Controls.Add(oTen);
            khungTen.Margin = new Padding(0, 0, 10, 4);
            khungTen.Size = new Size(220, 58);

            // nhanTheLoai
            nhanTheLoai.AutoSize = true;
            nhanTheLoai.ForeColor = Color.FromArgb(90, 100, 115);
            nhanTheLoai.Location = new Point(0, 0);
            nhanTheLoai.Text = "Thể loại";

            // oTheLoai
            oTheLoai.BackColor = Color.White;
            oTheLoai.ForeColor = Color.FromArgb(35, 45, 60);
            oTheLoai.Location = new Point(0, 22);
            oTheLoai.Size = new Size(152, 23);

            // khungTheLoai
            khungTheLoai.BackColor = Color.FromArgb(245, 247, 250);
            khungTheLoai.Controls.Add(nhanTheLoai);
            khungTheLoai.Controls.Add(oTheLoai);
            khungTheLoai.Margin = new Padding(0, 0, 10, 4);
            khungTheLoai.Size = new Size(160, 58);

            // nhanThoiLuong
            nhanThoiLuong.AutoSize = true;
            nhanThoiLuong.ForeColor = Color.FromArgb(90, 100, 115);
            nhanThoiLuong.Location = new Point(0, 0);
            nhanThoiLuong.Text = "Thời lượng (phút)";

            // oThoiLuong
            oThoiLuong.BackColor = Color.White;
            oThoiLuong.ForeColor = Color.FromArgb(35, 45, 60);
            oThoiLuong.Location = new Point(0, 22);
            oThoiLuong.Size = new Size(127, 23);

            // khungThoiLuong
            khungThoiLuong.BackColor = Color.FromArgb(245, 247, 250);
            khungThoiLuong.Controls.Add(nhanThoiLuong);
            khungThoiLuong.Controls.Add(oThoiLuong);
            khungThoiLuong.Margin = new Padding(0, 0, 10, 4);
            khungThoiLuong.Size = new Size(135, 58);

            // luongTruong
            luongTruong.AutoSize = true;
            luongTruong.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            luongTruong.BackColor = Color.FromArgb(245, 247, 250);
            luongTruong.Controls.Add(khungMa);
            luongTruong.Controls.Add(khungTen);
            luongTruong.Controls.Add(khungTheLoai);
            luongTruong.Controls.Add(khungThoiLuong);
            luongTruong.Margin = new Padding(0);
            luongTruong.WrapContents = true;

            // nhanPoster
            nhanPoster.AutoSize = true;
            nhanPoster.ForeColor = Color.FromArgb(90, 100, 115);
            nhanPoster.Location = new Point(0, 0);
            nhanPoster.Text = "Đường dẫn poster";

            // oPoster
            oPoster.BackColor = Color.White;
            oPoster.ForeColor = Color.FromArgb(35, 45, 60);
            oPoster.Location = new Point(0, 22);
            oPoster.Size = new Size(552, 23);

            // khungPoster
            khungPoster.BackColor = Color.FromArgb(245, 247, 250);
            khungPoster.Controls.Add(nhanPoster);
            khungPoster.Controls.Add(oPoster);
            khungPoster.Margin = new Padding(0, 0, 10, 4);
            khungPoster.Size = new Size(560, 58);

            // nutChonPoster
            nutChonPoster.AutoSize = true;
            nutChonPoster.BackColor = Color.FromArgb(35, 110, 220);
            nutChonPoster.Cursor = Cursors.Hand;
            nutChonPoster.FlatStyle = FlatStyle.Flat;
            nutChonPoster.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutChonPoster.ForeColor = Color.White;
            nutChonPoster.Margin = new Padding(6, 22, 0, 0);
            nutChonPoster.Padding = new Padding(10, 6, 10, 6);
            nutChonPoster.Text = "Chọn poster";
            nutChonPoster.UseVisualStyleBackColor = false;
            nutChonPoster.Click += nutChonPoster_Click;

            // luongPoster
            luongPoster.AutoSize = true;
            luongPoster.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            luongPoster.BackColor = Color.FromArgb(245, 247, 250);
            luongPoster.Controls.Add(khungPoster);
            luongPoster.Controls.Add(nutChonPoster);
            luongPoster.Margin = new Padding(0);
            luongPoster.WrapContents = false;

            // nutThem
            nutThem.AutoSize = true;
            nutThem.BackColor = Color.FromArgb(35, 110, 220);
            nutThem.Cursor = Cursors.Hand;
            nutThem.FlatStyle = FlatStyle.Flat;
            nutThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutThem.ForeColor = Color.White;
            nutThem.Margin = new Padding(4);
            nutThem.Padding = new Padding(10, 6, 10, 6);
            nutThem.Text = "Thêm";
            nutThem.UseVisualStyleBackColor = false;
            nutThem.Click += nutThem_Click;

            // nutSua
            nutSua.AutoSize = true;
            nutSua.BackColor = Color.FromArgb(35, 110, 220);
            nutSua.Cursor = Cursors.Hand;
            nutSua.FlatStyle = FlatStyle.Flat;
            nutSua.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutSua.ForeColor = Color.White;
            nutSua.Margin = new Padding(4);
            nutSua.Padding = new Padding(10, 6, 10, 6);
            nutSua.Text = "Sửa";
            nutSua.UseVisualStyleBackColor = false;
            nutSua.Click += nutSua_Click;

            // nutXoa
            nutXoa.AutoSize = true;
            nutXoa.BackColor = Color.FromArgb(35, 110, 220);
            nutXoa.Cursor = Cursors.Hand;
            nutXoa.FlatStyle = FlatStyle.Flat;
            nutXoa.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutXoa.ForeColor = Color.White;
            nutXoa.Margin = new Padding(4);
            nutXoa.Padding = new Padding(10, 6, 10, 6);
            nutXoa.Text = "Xóa";
            nutXoa.UseVisualStyleBackColor = false;
            nutXoa.Click += nutXoa_Click;

            // nutNhapCsv
            nutNhapCsv.AutoSize = true;
            nutNhapCsv.BackColor = Color.FromArgb(35, 110, 220);
            nutNhapCsv.Cursor = Cursors.Hand;
            nutNhapCsv.FlatStyle = FlatStyle.Flat;
            nutNhapCsv.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutNhapCsv.ForeColor = Color.White;
            nutNhapCsv.Margin = new Padding(4);
            nutNhapCsv.Padding = new Padding(10, 6, 10, 6);
            nutNhapCsv.Text = "Nhập CSV";
            nutNhapCsv.UseVisualStyleBackColor = false;
            nutNhapCsv.Click += nutNhapCsv_Click;

            // nutLoc
            nutLoc.AutoSize = true;
            nutLoc.BackColor = Color.FromArgb(35, 110, 220);
            nutLoc.Cursor = Cursors.Hand;
            nutLoc.FlatStyle = FlatStyle.Flat;
            nutLoc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutLoc.ForeColor = Color.White;
            nutLoc.Margin = new Padding(4);
            nutLoc.Padding = new Padding(10, 6, 10, 6);
            nutLoc.Text = "Lọc thể loại";
            nutLoc.UseVisualStyleBackColor = false;
            nutLoc.Click += nutLoc_Click;

            // nutLamMoi
            nutLamMoi.AutoSize = true;
            nutLamMoi.BackColor = Color.FromArgb(35, 110, 220);
            nutLamMoi.Cursor = Cursors.Hand;
            nutLamMoi.FlatStyle = FlatStyle.Flat;
            nutLamMoi.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutLamMoi.ForeColor = Color.White;
            nutLamMoi.Margin = new Padding(4);
            nutLamMoi.Padding = new Padding(10, 6, 10, 6);
            nutLamMoi.Text = "Làm mới";
            nutLamMoi.UseVisualStyleBackColor = false;
            nutLamMoi.Click += nutLamMoi_Click;

            // luongNut
            luongNut.AutoSize = true;
            luongNut.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            luongNut.BackColor = Color.FromArgb(245, 247, 250);
            luongNut.Controls.Add(nutThem);
            luongNut.Controls.Add(nutSua);
            luongNut.Controls.Add(nutXoa);
            luongNut.Controls.Add(nutNhapCsv);
            luongNut.Controls.Add(nutLoc);
            luongNut.Controls.Add(nutLamMoi);
            luongNut.Margin = new Padding(0, 6, 0, 0);
            luongNut.WrapContents = true;

            // bangTren
            bangTren.AutoSize = true;
            bangTren.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bangTren.BackColor = Color.FromArgb(245, 247, 250);
            bangTren.ColumnCount = 1;
            bangTren.ColumnStyles.Add(new ColumnStyle());
            bangTren.Controls.Add(luongTruong, 0, 0);
            bangTren.Controls.Add(luongPoster, 0, 1);
            bangTren.Controls.Add(luongNut, 0, 2);
            bangTren.Dock = DockStyle.Top;
            bangTren.Padding = new Padding(10);
            bangTren.RowCount = 3;
            bangTren.RowStyles.Add(new RowStyle());
            bangTren.RowStyles.Add(new RowStyle());
            bangTren.RowStyles.Add(new RowStyle());

            // bang
            bang.AllowUserToAddRows = false;
            bang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bang.BackgroundColor = Color.White;
            bang.BorderStyle = BorderStyle.FixedSingle;
            kieuOCot.BackColor = Color.White;
            kieuOCot.ForeColor = Color.FromArgb(35, 45, 60);
            kieuOCot.Padding = new Padding(4);
            kieuOCot.SelectionBackColor = Color.FromArgb(205, 225, 250);
            kieuOCot.SelectionForeColor = Color.FromArgb(35, 45, 60);
            bang.DefaultCellStyle = kieuOCot;
            bang.ColumnHeadersHeight = 32;
            bang.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            kieuTieuDe.BackColor = Color.FromArgb(230, 238, 249);
            kieuTieuDe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            kieuTieuDe.ForeColor = Color.FromArgb(35, 45, 60);
            kieuTieuDe.Padding = new Padding(5);
            bang.ColumnHeadersDefaultCellStyle = kieuTieuDe;
            bang.Dock = DockStyle.Fill;
            bang.EnableHeadersVisualStyles = false;
            bang.GridColor = Color.FromArgb(225, 229, 235);
            bang.MultiSelect = false;
            bang.ReadOnly = true;
            bang.RowHeadersVisible = false;
            bang.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            bang.SelectionChanged += bang_SelectionChanged;

            // tieuDeDanhSach
            tieuDeDanhSach.Dock = DockStyle.Top;
            tieuDeDanhSach.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tieuDeDanhSach.ForeColor = Color.FromArgb(35, 45, 60);
            tieuDeDanhSach.Height = 24;
            tieuDeDanhSach.Text = "DANH SÁCH PHIM";

            // anhPoster
            anhPoster.ColorDepth = ColorDepth.Depth32Bit;
            anhPoster.ImageSize = new Size(64, 80);

            // danhSach
            danhSach.BackColor = Color.White;
            danhSach.BorderStyle = BorderStyle.None;
            danhSach.Dock = DockStyle.Fill;
            danhSach.ForeColor = Color.FromArgb(35, 45, 60);
            danhSach.LargeImageList = anhPoster;
            danhSach.View = View.LargeIcon;
            danhSach.ItemActivate += danhSach_ItemActivate;

            // bangDanhSach
            bangDanhSach.BackColor = Color.White;
            bangDanhSach.Controls.Add(danhSach);
            bangDanhSach.Controls.Add(tieuDeDanhSach);
            bangDanhSach.Dock = DockStyle.Right;
            bangDanhSach.Padding = new Padding(6);
            bangDanhSach.Width = 240;

            // anhXemTruoc
            anhXemTruoc.BackColor = Color.White;
            anhXemTruoc.BorderStyle = BorderStyle.FixedSingle;
            anhXemTruoc.Dock = DockStyle.Top;
            anhXemTruoc.Size = new Size(120, 150);
            anhXemTruoc.SizeMode = PictureBoxSizeMode.Zoom;

            // tieuDeXemTruoc
            tieuDeXemTruoc.Dock = DockStyle.Top;
            tieuDeXemTruoc.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tieuDeXemTruoc.ForeColor = Color.FromArgb(35, 45, 60);
            tieuDeXemTruoc.Height = 24;
            tieuDeXemTruoc.Text = "POSTER";

            // bangXemTruoc
            bangXemTruoc.BackColor = Color.White;
            bangXemTruoc.Controls.Add(anhXemTruoc);
            bangXemTruoc.Controls.Add(tieuDeXemTruoc);
            bangXemTruoc.Dock = DockStyle.Right;
            bangXemTruoc.Padding = new Padding(6);
            bangXemTruoc.Width = 150;

            // noiDung
            noiDung.BackColor = Color.White;
            noiDung.Controls.Add(bang);
            noiDung.Controls.Add(bangDanhSach);
            noiDung.Controls.Add(bangXemTruoc);
            noiDung.Dock = DockStyle.Fill;
            noiDung.Padding = new Padding(10);

            // FormPhim
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1104, 681);
            Controls.Add(noiDung);
            Controls.Add(bangTren);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(35, 45, 60);
            MinimumSize = new Size(900, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý phim";
            bangTren.ResumeLayout(false);
            bangTren.PerformLayout();
            luongTruong.ResumeLayout(false);
            khungMa.ResumeLayout(false);
            khungMa.PerformLayout();
            khungTen.ResumeLayout(false);
            khungTen.PerformLayout();
            khungTheLoai.ResumeLayout(false);
            khungTheLoai.PerformLayout();
            khungThoiLuong.ResumeLayout(false);
            khungThoiLuong.PerformLayout();
            luongPoster.ResumeLayout(false);
            khungPoster.ResumeLayout(false);
            khungPoster.PerformLayout();
            luongNut.ResumeLayout(false);
            noiDung.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bang).EndInit();
            bangDanhSach.ResumeLayout(false);
            bangXemTruoc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)anhXemTruoc).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
