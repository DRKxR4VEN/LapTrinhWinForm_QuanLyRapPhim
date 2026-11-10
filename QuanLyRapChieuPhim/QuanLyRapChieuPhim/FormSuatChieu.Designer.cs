#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class FormSuatChieu
    {
        private System.ComponentModel.IContainer components = null;

        private TableLayoutPanel boCuc;
        private Panel bangTrai;
        private Label tieuDeLich;
        private Label goiYLich;
        private Panel khungCay;
        private TreeView cayLich;
        private Panel bangPhai;
        private Panel the;
        private FlowLayoutPanel bangForm;
        private Label tieuDeForm;
        private Panel khungPhim;
        private Label nhanPhim;
        private ComboBox oPhim;
        private Panel khungPhong;
        private Label nhanPhong;
        private ComboBox oPhong;
        private Panel khungNgay;
        private Label nhanNgay;
        private DateTimePicker oNgay;
        private DateTimePicker oGio;
        private Label thongTinPhim;
        private Label thongTinKetThuc;
        private FlowLayoutPanel luongNut;
        private Button nutThem;
        private Button nutHuy;
        private Button nutLamMoi;
        private Panel duongKe;
        private Label tieuDeChon;
        private Label thongTinChon;
        private Label goiY;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            boCuc = new TableLayoutPanel();
            bangTrai = new Panel();
            khungCay = new Panel();
            cayLich = new TreeView();
            goiYLich = new Label();
            tieuDeLich = new Label();
            bangPhai = new Panel();
            the = new Panel();
            bangForm = new FlowLayoutPanel();
            tieuDeForm = new Label();
            khungPhim = new Panel();
            nhanPhim = new Label();
            oPhim = new ComboBox();
            khungPhong = new Panel();
            nhanPhong = new Label();
            oPhong = new ComboBox();
            khungNgay = new Panel();
            nhanNgay = new Label();
            oNgay = new DateTimePicker();
            oGio = new DateTimePicker();
            thongTinPhim = new Label();
            thongTinKetThuc = new Label();
            luongNut = new FlowLayoutPanel();
            nutThem = new Button();
            nutHuy = new Button();
            nutLamMoi = new Button();
            duongKe = new Panel();
            tieuDeChon = new Label();
            thongTinChon = new Label();
            goiY = new Label();
            boCuc.SuspendLayout();
            bangTrai.SuspendLayout();
            khungCay.SuspendLayout();
            bangPhai.SuspendLayout();
            the.SuspendLayout();
            bangForm.SuspendLayout();
            khungPhim.SuspendLayout();
            khungPhong.SuspendLayout();
            khungNgay.SuspendLayout();
            luongNut.SuspendLayout();
            SuspendLayout();

            // tieuDeLich
            tieuDeLich.Dock = DockStyle.Top;
            tieuDeLich.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            tieuDeLich.ForeColor = Color.FromArgb(35, 45, 60);
            tieuDeLich.Height = 32;
            tieuDeLich.Text = "LỊCH CHIẾU";

            // goiYLich
            goiYLich.Dock = DockStyle.Bottom;
            goiYLich.ForeColor = Color.FromArgb(90, 100, 115);
            goiYLich.Height = 44;
            goiYLich.Text = "Cây lịch: Ngày → Phim → Suất. Chọn đúng dòng suất chiếu để xem chi tiết hoặc hủy.";
            goiYLich.TextAlign = ContentAlignment.MiddleLeft;

            // cayLich
            cayLich.BackColor = Color.White;
            cayLich.BorderStyle = BorderStyle.FixedSingle;
            cayLich.Dock = DockStyle.Fill;
            cayLich.ForeColor = Color.FromArgb(35, 45, 60);
            cayLich.FullRowSelect = true;
            cayLich.HideSelection = false;
            cayLich.ItemHeight = 24;
            cayLich.ShowNodeToolTips = true;
            cayLich.AfterSelect += cayLich_AfterSelect;

            // khungCay
            khungCay.Controls.Add(cayLich);
            khungCay.Dock = DockStyle.Fill;
            khungCay.Padding = new Padding(0, 4, 0, 4);

            // bangTrai
            bangTrai.BackColor = Color.FromArgb(245, 247, 250);
            bangTrai.Controls.Add(khungCay);
            bangTrai.Controls.Add(goiYLich);
            bangTrai.Controls.Add(tieuDeLich);
            bangTrai.Dock = DockStyle.Fill;
            bangTrai.Padding = new Padding(8);

            // tieuDeForm
            tieuDeForm.AutoSize = true;
            tieuDeForm.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            tieuDeForm.ForeColor = Color.FromArgb(35, 45, 60);
            tieuDeForm.Margin = new Padding(0, 0, 0, 8);
            tieuDeForm.Text = "LÊN LỊCH SUẤT CHIẾU";

            // nhanPhim
            nhanPhim.AutoSize = true;
            nhanPhim.ForeColor = Color.FromArgb(90, 100, 115);
            nhanPhim.Location = new Point(0, 0);
            nhanPhim.Text = "Phim";

            // oPhim
            oPhim.DropDownStyle = ComboBoxStyle.DropDownList;
            oPhim.Location = new Point(0, 22);
            oPhim.Size = new Size(360, 23);
            oPhim.SelectedIndexChanged += oPhim_SelectedIndexChanged;

            // khungPhim
            khungPhim.BackColor = Color.White;
            khungPhim.Controls.Add(nhanPhim);
            khungPhim.Controls.Add(oPhim);
            khungPhim.Margin = new Padding(0, 0, 0, 2);
            khungPhim.Size = new Size(380, 58);

            // nhanPhong
            nhanPhong.AutoSize = true;
            nhanPhong.ForeColor = Color.FromArgb(90, 100, 115);
            nhanPhong.Location = new Point(0, 0);
            nhanPhong.Text = "Phòng chiếu";

            // oPhong
            oPhong.DropDownStyle = ComboBoxStyle.DropDownList;
            oPhong.Location = new Point(0, 22);
            oPhong.Size = new Size(360, 23);
            oPhong.SelectedIndexChanged += oPhong_SelectedIndexChanged;

            // khungPhong
            khungPhong.BackColor = Color.White;
            khungPhong.Controls.Add(nhanPhong);
            khungPhong.Controls.Add(oPhong);
            khungPhong.Margin = new Padding(0, 0, 0, 2);
            khungPhong.Size = new Size(380, 58);

            // nhanNgay
            nhanNgay.AutoSize = true;
            nhanNgay.ForeColor = Color.FromArgb(90, 100, 115);
            nhanNgay.Location = new Point(0, 0);
            nhanNgay.Text = "Ngày và giờ bắt đầu";

            // oNgay
            oNgay.CustomFormat = "dd/MM/yyyy";
            oNgay.Format = DateTimePickerFormat.Custom;
            oNgay.Location = new Point(0, 22);
            oNgay.Size = new Size(225, 23);
            oNgay.ValueChanged += oNgay_ValueChanged;

            // oGio
            oGio.CustomFormat = "HH:mm";
            oGio.Format = DateTimePickerFormat.Custom;
            oGio.Location = new Point(235, 22);
            oGio.ShowUpDown = true;
            oGio.Size = new Size(125, 23);
            oGio.ValueChanged += oGio_ValueChanged;

            // khungNgay
            khungNgay.BackColor = Color.White;
            khungNgay.Controls.Add(nhanNgay);
            khungNgay.Controls.Add(oNgay);
            khungNgay.Controls.Add(oGio);
            khungNgay.Margin = new Padding(0, 0, 0, 2);
            khungNgay.Size = new Size(380, 58);

            // thongTinPhim
            thongTinPhim.AutoSize = true;
            thongTinPhim.ForeColor = Color.FromArgb(90, 100, 115);
            thongTinPhim.Margin = new Padding(0, 0, 0, 6);
            thongTinPhim.Text = "Chọn phim để xem thời lượng.";

            // thongTinKetThuc
            thongTinKetThuc.AutoSize = true;
            thongTinKetThuc.ForeColor = Color.FromArgb(35, 110, 220);
            thongTinKetThuc.Margin = new Padding(0, 0, 0, 8);
            thongTinKetThuc.MaximumSize = new Size(380, 0);

            // nutThem
            nutThem.AutoSize = true;
            nutThem.BackColor = Color.FromArgb(35, 110, 220);
            nutThem.Cursor = Cursors.Hand;
            nutThem.FlatStyle = FlatStyle.Flat;
            nutThem.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutThem.ForeColor = Color.White;
            nutThem.Margin = new Padding(4);
            nutThem.Padding = new Padding(10, 6, 10, 6);
            nutThem.Text = "Thêm suất chiếu";
            nutThem.UseVisualStyleBackColor = false;
            nutThem.Click += nutThem_Click;

            // nutHuy
            nutHuy.AutoSize = true;
            nutHuy.BackColor = Color.Firebrick;
            nutHuy.Cursor = Cursors.Hand;
            nutHuy.Enabled = false;
            nutHuy.FlatStyle = FlatStyle.Flat;
            nutHuy.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutHuy.ForeColor = Color.White;
            nutHuy.Margin = new Padding(4);
            nutHuy.Padding = new Padding(10, 6, 10, 6);
            nutHuy.Text = "Hủy suất đã chọn";
            nutHuy.UseVisualStyleBackColor = false;
            nutHuy.Click += nutHuy_Click;

            // nutLamMoi
            nutLamMoi.AutoSize = true;
            nutLamMoi.BackColor = Color.FromArgb(90, 100, 115);
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
            luongNut.BackColor = Color.White;
            luongNut.Controls.Add(nutThem);
            luongNut.Controls.Add(nutHuy);
            luongNut.Controls.Add(nutLamMoi);
            luongNut.Height = 104;
            luongNut.Margin = new Padding(0, 0, 0, 6);
            luongNut.WrapContents = true;
            luongNut.Width = 380;

            // duongKe
            duongKe.BackColor = Color.FromArgb(220, 225, 232);
            duongKe.Margin = new Padding(0, 6, 0, 8);
            duongKe.Size = new Size(60, 1);

            // tieuDeChon
            tieuDeChon.AutoSize = true;
            tieuDeChon.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            tieuDeChon.ForeColor = Color.FromArgb(35, 45, 60);
            tieuDeChon.Margin = new Padding(0, 0, 0, 2);
            tieuDeChon.Text = "THÔNG TIN SUẤT ĐANG CHỌN";

            // thongTinChon
            thongTinChon.AutoSize = true;
            thongTinChon.BackColor = Color.FromArgb(245, 247, 250);
            thongTinChon.BorderStyle = BorderStyle.FixedSingle;
            thongTinChon.ForeColor = Color.FromArgb(35, 45, 60);
            thongTinChon.Margin = new Padding(0, 8, 0, 0);
            thongTinChon.MaximumSize = new Size(380, 0);
            thongTinChon.Padding = new Padding(10);
            thongTinChon.Text = "Chưa chọn suất chiếu nào.";

            // goiY
            goiY.AutoSize = true;
            goiY.ForeColor = Color.FromArgb(90, 100, 115);
            goiY.Margin = new Padding(0, 4, 0, 0);
            goiY.MaximumSize = new Size(380, 0);
            goiY.Text = "Mỗi suất chiếu được cộng thêm 15 phút dọn phòng khi kiểm tra trùng lịch.";

            // bangForm
            bangForm.AutoScroll = true;
            bangForm.BackColor = Color.White;
            bangForm.Controls.Add(tieuDeForm);
            bangForm.Controls.Add(khungPhim);
            bangForm.Controls.Add(khungPhong);
            bangForm.Controls.Add(khungNgay);
            bangForm.Controls.Add(thongTinPhim);
            bangForm.Controls.Add(thongTinKetThuc);
            bangForm.Controls.Add(luongNut);
            bangForm.Controls.Add(duongKe);
            bangForm.Controls.Add(tieuDeChon);
            bangForm.Controls.Add(thongTinChon);
            bangForm.Controls.Add(goiY);
            bangForm.Dock = DockStyle.Fill;
            bangForm.FlowDirection = FlowDirection.TopDown;
            bangForm.WrapContents = false;

            // the
            the.AutoScroll = true;
            the.BackColor = Color.White;
            the.BorderStyle = BorderStyle.FixedSingle;
            the.Controls.Add(bangForm);
            the.Dock = DockStyle.Fill;
            the.Padding = new Padding(16, 12, 16, 12);

            // bangPhai
            bangPhai.BackColor = Color.FromArgb(245, 247, 250);
            bangPhai.Controls.Add(the);
            bangPhai.Dock = DockStyle.Fill;
            bangPhai.Padding = new Padding(8);

            // boCuc
            boCuc.BackColor = Color.FromArgb(245, 247, 250);
            boCuc.ColumnCount = 2;
            boCuc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57F));
            boCuc.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43F));
            boCuc.Controls.Add(bangTrai, 0, 0);
            boCuc.Controls.Add(bangPhai, 1, 0);
            boCuc.Dock = DockStyle.Fill;
            boCuc.Padding = new Padding(8);
            boCuc.RowCount = 1;
            boCuc.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // FormSuatChieu
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1084, 641);
            Controls.Add(boCuc);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(35, 45, 60);
            MinimumSize = new Size(1000, 600);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý suất chiếu";
            boCuc.ResumeLayout(false);
            bangTrai.ResumeLayout(false);
            khungCay.ResumeLayout(false);
            bangPhai.ResumeLayout(false);
            the.ResumeLayout(false);
            bangForm.ResumeLayout(false);
            bangForm.PerformLayout();
            khungPhim.ResumeLayout(false);
            khungPhim.PerformLayout();
            khungPhong.ResumeLayout(false);
            khungPhong.PerformLayout();
            khungNgay.ResumeLayout(false);
            khungNgay.PerformLayout();
            luongNut.ResumeLayout(false);
            ResumeLayout(false);
        }
    }
}
