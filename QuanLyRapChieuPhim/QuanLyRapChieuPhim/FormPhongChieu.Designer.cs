#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class FormPhongChieu
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel bangTren;
        private FlowLayoutPanel luongTruong;
        private FlowLayoutPanel luongNut;
        private Panel khungMa;
        private Panel khungTen;
        private Panel khungSoHang;
        private Panel khungSoGhe;
        private Panel khungHangVip;
        private Label nhanMa;
        private Label nhanTen;
        private Label nhanSoHang;
        private Label nhanSoGhe;
        private Label nhanHangVip;
        private TextBox oMa;
        private TextBox oTenPhong;
        private TextBox oSoHang;
        private TextBox oSoGhe;
        private TextBox oHangVip;
        private DataGridView bang;
        private Button nutThem;
        private Button nutSua;
        private Button nutXoa;

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
            oTenPhong = new TextBox();
            khungSoHang = new Panel();
            nhanSoHang = new Label();
            oSoHang = new TextBox();
            khungSoGhe = new Panel();
            nhanSoGhe = new Label();
            oSoGhe = new TextBox();
            khungHangVip = new Panel();
            nhanHangVip = new Label();
            oHangVip = new TextBox();
            luongNut = new FlowLayoutPanel();
            nutThem = new Button();
            nutSua = new Button();
            nutXoa = new Button();
            bang = new DataGridView();
            var kieuOCot = new DataGridViewCellStyle();
            var kieuTieuDe = new DataGridViewCellStyle();
            bangTren.SuspendLayout();
            luongTruong.SuspendLayout();
            khungMa.SuspendLayout();
            khungTen.SuspendLayout();
            khungSoHang.SuspendLayout();
            khungSoGhe.SuspendLayout();
            khungHangVip.SuspendLayout();
            luongNut.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bang).BeginInit();
            SuspendLayout();

            // nhanMa
            nhanMa.AutoSize = true;
            nhanMa.ForeColor = Color.FromArgb(90, 100, 115);
            nhanMa.Location = new Point(0, 0);
            nhanMa.Text = "Mã phòng";

            // oMa
            oMa.BackColor = Color.White;
            oMa.ForeColor = Color.FromArgb(35, 45, 60);
            oMa.Location = new Point(0, 22);
            oMa.Size = new Size(102, 23);

            // khungMa
            khungMa.BackColor = Color.FromArgb(245, 247, 250);
            khungMa.Controls.Add(nhanMa);
            khungMa.Controls.Add(oMa);
            khungMa.Margin = new Padding(0, 0, 10, 4);
            khungMa.Size = new Size(110, 58);

            // nhanTen
            nhanTen.AutoSize = true;
            nhanTen.ForeColor = Color.FromArgb(90, 100, 115);
            nhanTen.Location = new Point(0, 0);
            nhanTen.Text = "Tên phòng";

            // oTenPhong
            oTenPhong.BackColor = Color.White;
            oTenPhong.ForeColor = Color.FromArgb(35, 45, 60);
            oTenPhong.Location = new Point(0, 22);
            oTenPhong.Size = new Size(192, 23);

            // khungTen
            khungTen.BackColor = Color.FromArgb(245, 247, 250);
            khungTen.Controls.Add(nhanTen);
            khungTen.Controls.Add(oTenPhong);
            khungTen.Margin = new Padding(0, 0, 10, 4);
            khungTen.Size = new Size(200, 58);

            // nhanSoHang
            nhanSoHang.AutoSize = true;
            nhanSoHang.ForeColor = Color.FromArgb(90, 100, 115);
            nhanSoHang.Location = new Point(0, 0);
            nhanSoHang.Text = "Số hàng";

            // oSoHang
            oSoHang.BackColor = Color.White;
            oSoHang.ForeColor = Color.FromArgb(35, 45, 60);
            oSoHang.Location = new Point(0, 22);
            oSoHang.Size = new Size(92, 23);

            // khungSoHang
            khungSoHang.BackColor = Color.FromArgb(245, 247, 250);
            khungSoHang.Controls.Add(nhanSoHang);
            khungSoHang.Controls.Add(oSoHang);
            khungSoHang.Margin = new Padding(0, 0, 10, 4);
            khungSoHang.Size = new Size(100, 58);

            // nhanSoGhe
            nhanSoGhe.AutoSize = true;
            nhanSoGhe.ForeColor = Color.FromArgb(90, 100, 115);
            nhanSoGhe.Location = new Point(0, 0);
            nhanSoGhe.Text = "Ghế / hàng";

            // oSoGhe
            oSoGhe.BackColor = Color.White;
            oSoGhe.ForeColor = Color.FromArgb(35, 45, 60);
            oSoGhe.Location = new Point(0, 22);
            oSoGhe.Size = new Size(102, 23);

            // khungSoGhe
            khungSoGhe.BackColor = Color.FromArgb(245, 247, 250);
            khungSoGhe.Controls.Add(nhanSoGhe);
            khungSoGhe.Controls.Add(oSoGhe);
            khungSoGhe.Margin = new Padding(0, 0, 10, 4);
            khungSoGhe.Size = new Size(110, 58);

            // nhanHangVip
            nhanHangVip.AutoSize = true;
            nhanHangVip.ForeColor = Color.FromArgb(90, 100, 115);
            nhanHangVip.Location = new Point(0, 0);
            nhanHangVip.Text = "Hàng VIP";

            // oHangVip
            oHangVip.BackColor = Color.White;
            oHangVip.ForeColor = Color.FromArgb(35, 45, 60);
            oHangVip.Location = new Point(0, 22);
            oHangVip.Size = new Size(92, 23);

            // khungHangVip
            khungHangVip.BackColor = Color.FromArgb(245, 247, 250);
            khungHangVip.Controls.Add(nhanHangVip);
            khungHangVip.Controls.Add(oHangVip);
            khungHangVip.Margin = new Padding(0, 0, 10, 4);
            khungHangVip.Size = new Size(100, 58);

            // luongTruong
            luongTruong.AutoSize = true;
            luongTruong.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            luongTruong.BackColor = Color.FromArgb(245, 247, 250);
            luongTruong.Controls.Add(khungMa);
            luongTruong.Controls.Add(khungTen);
            luongTruong.Controls.Add(khungSoHang);
            luongTruong.Controls.Add(khungSoGhe);
            luongTruong.Controls.Add(khungHangVip);
            luongTruong.Margin = new Padding(0);
            luongTruong.WrapContents = true;

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

            // luongNut
            luongNut.AutoSize = true;
            luongNut.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            luongNut.BackColor = Color.FromArgb(245, 247, 250);
            luongNut.Controls.Add(nutThem);
            luongNut.Controls.Add(nutSua);
            luongNut.Controls.Add(nutXoa);
            luongNut.Margin = new Padding(0, 6, 0, 0);
            luongNut.WrapContents = true;

            // bangTren
            bangTren.AutoSize = true;
            bangTren.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            bangTren.BackColor = Color.FromArgb(245, 247, 250);
            bangTren.ColumnCount = 1;
            bangTren.ColumnStyles.Add(new ColumnStyle());
            bangTren.Controls.Add(luongTruong, 0, 0);
            bangTren.Controls.Add(luongNut, 0, 1);
            bangTren.Dock = DockStyle.Top;
            bangTren.Padding = new Padding(10);
            bangTren.RowCount = 2;
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

            // FormPhongChieu
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(884, 521);
            Controls.Add(bang);
            Controls.Add(bangTren);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(35, 45, 60);
            MinimumSize = new Size(760, 450);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý phòng chiếu";
            bangTren.ResumeLayout(false);
            bangTren.PerformLayout();
            luongTruong.ResumeLayout(false);
            khungMa.ResumeLayout(false);
            khungMa.PerformLayout();
            khungTen.ResumeLayout(false);
            khungTen.PerformLayout();
            khungSoHang.ResumeLayout(false);
            khungSoHang.PerformLayout();
            khungSoGhe.ResumeLayout(false);
            khungSoGhe.PerformLayout();
            khungHangVip.ResumeLayout(false);
            khungHangVip.PerformLayout();
            luongNut.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bang).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
