#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class FormDoanhThu
    {
        private System.ComponentModel.IContainer components = null;

        private FlowLayoutPanel bangTren;
        private Label nhanTuNgay;
        private DateTimePicker oTuNgay;
        private Label nhanDenNgay;
        private DateTimePicker oDenNgay;
        private Button nutThongKe;
        private Button nutXuatCsv;
        private Button nutXuatTxt;
        private TableLayoutPanel than;
        private TabControl tab;
        private TabPage tabPhim;
        private TabPage tabNgay;
        private DataGridView bangPhim;
        private DataGridView bangNgay;
        private Panel bieuDo;
        private FlowLayoutPanel bangDuoi;
        private Label tongHop;
        private ContextMenuStrip menuChuotPhai;
        private ToolStripMenuItem mucXuatCsv;
        private ToolStripMenuItem mucXuatTxt;
        private ToolStripSeparator phanCach;
        private ToolStripMenuItem mucLamMoi;

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
            nhanTuNgay = new Label();
            oTuNgay = new DateTimePicker();
            nhanDenNgay = new Label();
            oDenNgay = new DateTimePicker();
            nutThongKe = new Button();
            nutXuatCsv = new Button();
            nutXuatTxt = new Button();
            than = new TableLayoutPanel();
            tab = new TabControl();
            tabPhim = new TabPage();
            tabNgay = new TabPage();
            bangPhim = new DataGridView();
            bangNgay = new DataGridView();
            bieuDo = new Panel();
            bangDuoi = new FlowLayoutPanel();
            tongHop = new Label();
            menuChuotPhai = new ContextMenuStrip(components);
            mucXuatCsv = new ToolStripMenuItem();
            mucXuatTxt = new ToolStripMenuItem();
            phanCach = new ToolStripSeparator();
            mucLamMoi = new ToolStripMenuItem();
            var kieuOCotPhim = new DataGridViewCellStyle();
            var kieuTieuDePhim = new DataGridViewCellStyle();
            var kieuOCotNgay = new DataGridViewCellStyle();
            var kieuTieuDeNgay = new DataGridViewCellStyle();
            bangTren.SuspendLayout();
            than.SuspendLayout();
            tab.SuspendLayout();
            tabPhim.SuspendLayout();
            tabNgay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)bangPhim).BeginInit();
            ((System.ComponentModel.ISupportInitialize)bangNgay).BeginInit();
            bangDuoi.SuspendLayout();
            menuChuotPhai.SuspendLayout();
            SuspendLayout();

            // nhanTuNgay
            nhanTuNgay.AutoSize = true;
            nhanTuNgay.ForeColor = Color.FromArgb(35, 45, 60);
            nhanTuNgay.Margin = new Padding(5, 8, 3, 3);
            nhanTuNgay.Text = "Từ ngày";
            nhanTuNgay.TextAlign = ContentAlignment.MiddleLeft;

            // oTuNgay
            oTuNgay.CustomFormat = "dd/MM/yyyy";
            oTuNgay.Format = DateTimePickerFormat.Custom;
            oTuNgay.Margin = new Padding(3, 4, 3, 4);
            oTuNgay.Size = new Size(125, 23);

            // nhanDenNgay
            nhanDenNgay.AutoSize = true;
            nhanDenNgay.ForeColor = Color.FromArgb(35, 45, 60);
            nhanDenNgay.Margin = new Padding(5, 8, 3, 3);
            nhanDenNgay.Text = "Đến ngày";
            nhanDenNgay.TextAlign = ContentAlignment.MiddleLeft;

            // oDenNgay
            oDenNgay.CustomFormat = "dd/MM/yyyy";
            oDenNgay.Format = DateTimePickerFormat.Custom;
            oDenNgay.Margin = new Padding(3, 4, 3, 4);
            oDenNgay.Size = new Size(125, 23);

            // nutThongKe
            nutThongKe.AutoSize = true;
            nutThongKe.BackColor = Color.FromArgb(35, 110, 220);
            nutThongKe.Cursor = Cursors.Hand;
            nutThongKe.FlatStyle = FlatStyle.Flat;
            nutThongKe.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutThongKe.ForeColor = Color.White;
            nutThongKe.Margin = new Padding(8, 4, 4, 4);
            nutThongKe.Padding = new Padding(10, 6, 10, 6);
            nutThongKe.Text = "Thống kê";
            nutThongKe.UseVisualStyleBackColor = false;
            nutThongKe.Click += nutThongKe_Click;

            // nutXuatCsv
            nutXuatCsv.AutoSize = true;
            nutXuatCsv.BackColor = Color.FromArgb(30, 130, 80);
            nutXuatCsv.Cursor = Cursors.Hand;
            nutXuatCsv.FlatStyle = FlatStyle.Flat;
            nutXuatCsv.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutXuatCsv.ForeColor = Color.White;
            nutXuatCsv.Margin = new Padding(4);
            nutXuatCsv.Padding = new Padding(10, 6, 10, 6);
            nutXuatCsv.Text = "Xuất CSV";
            nutXuatCsv.UseVisualStyleBackColor = false;
            nutXuatCsv.Click += nutXuatCsv_Click;

            // nutXuatTxt
            nutXuatTxt.AutoSize = true;
            nutXuatTxt.BackColor = Color.FromArgb(90, 100, 115);
            nutXuatTxt.Cursor = Cursors.Hand;
            nutXuatTxt.FlatStyle = FlatStyle.Flat;
            nutXuatTxt.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            nutXuatTxt.ForeColor = Color.White;
            nutXuatTxt.Margin = new Padding(4);
            nutXuatTxt.Padding = new Padding(10, 6, 10, 6);
            nutXuatTxt.Text = "Xuất báo cáo TXT";
            nutXuatTxt.UseVisualStyleBackColor = false;
            nutXuatTxt.Click += nutXuatTxt_Click;

            // bangTren
            bangTren.AutoSize = true;
            bangTren.BackColor = Color.FromArgb(245, 247, 250);
            bangTren.Controls.Add(nhanTuNgay);
            bangTren.Controls.Add(oTuNgay);
            bangTren.Controls.Add(nhanDenNgay);
            bangTren.Controls.Add(oDenNgay);
            bangTren.Controls.Add(nutThongKe);
            bangTren.Controls.Add(nutXuatCsv);
            bangTren.Controls.Add(nutXuatTxt);
            bangTren.Dock = DockStyle.Top;
            bangTren.Padding = new Padding(10);

            // bangPhim
            bangPhim.AllowUserToAddRows = false;
            bangPhim.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bangPhim.BackgroundColor = Color.White;
            bangPhim.BorderStyle = BorderStyle.FixedSingle;
            kieuOCotPhim.BackColor = Color.White;
            kieuOCotPhim.ForeColor = Color.FromArgb(35, 45, 60);
            kieuOCotPhim.Padding = new Padding(4);
            kieuOCotPhim.SelectionBackColor = Color.FromArgb(205, 225, 250);
            kieuOCotPhim.SelectionForeColor = Color.FromArgb(35, 45, 60);
            bangPhim.DefaultCellStyle = kieuOCotPhim;
            bangPhim.ColumnHeadersHeight = 32;
            bangPhim.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            kieuTieuDePhim.BackColor = Color.FromArgb(230, 238, 249);
            kieuTieuDePhim.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            kieuTieuDePhim.ForeColor = Color.FromArgb(35, 45, 60);
            kieuTieuDePhim.Padding = new Padding(5);
            bangPhim.ColumnHeadersDefaultCellStyle = kieuTieuDePhim;
            bangPhim.ContextMenuStrip = menuChuotPhai;
            bangPhim.Dock = DockStyle.Fill;
            bangPhim.EnableHeadersVisualStyles = false;
            bangPhim.GridColor = Color.FromArgb(225, 229, 235);
            bangPhim.MultiSelect = false;
            bangPhim.ReadOnly = true;
            bangPhim.RowHeadersVisible = false;
            bangPhim.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // bangNgay
            bangNgay.AllowUserToAddRows = false;
            bangNgay.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            bangNgay.BackgroundColor = Color.White;
            bangNgay.BorderStyle = BorderStyle.FixedSingle;
            kieuOCotNgay.BackColor = Color.White;
            kieuOCotNgay.ForeColor = Color.FromArgb(35, 45, 60);
            kieuOCotNgay.Padding = new Padding(4);
            kieuOCotNgay.SelectionBackColor = Color.FromArgb(205, 225, 250);
            kieuOCotNgay.SelectionForeColor = Color.FromArgb(35, 45, 60);
            bangNgay.DefaultCellStyle = kieuOCotNgay;
            bangNgay.ColumnHeadersHeight = 32;
            bangNgay.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            kieuTieuDeNgay.BackColor = Color.FromArgb(230, 238, 249);
            kieuTieuDeNgay.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            kieuTieuDeNgay.ForeColor = Color.FromArgb(35, 45, 60);
            kieuTieuDeNgay.Padding = new Padding(5);
            bangNgay.ColumnHeadersDefaultCellStyle = kieuTieuDeNgay;
            bangNgay.ContextMenuStrip = menuChuotPhai;
            bangNgay.Dock = DockStyle.Fill;
            bangNgay.EnableHeadersVisualStyles = false;
            bangNgay.GridColor = Color.FromArgb(225, 229, 235);
            bangNgay.MultiSelect = false;
            bangNgay.ReadOnly = true;
            bangNgay.RowHeadersVisible = false;
            bangNgay.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // tabPhim
            tabPhim.BackColor = Color.FromArgb(245, 247, 250);
            tabPhim.Controls.Add(bangPhim);
            tabPhim.Padding = new Padding(4);
            tabPhim.Text = "Theo phim";

            // tabNgay
            tabNgay.BackColor = Color.FromArgb(245, 247, 250);
            tabNgay.Controls.Add(bangNgay);
            tabNgay.Padding = new Padding(4);
            tabNgay.Text = "Theo ngày";

            // tab
            tab.Controls.Add(tabPhim);
            tab.Controls.Add(tabNgay);
            tab.Dock = DockStyle.Fill;
            tab.Font = new Font("Segoe UI", 9F);
            tab.SelectedIndexChanged += tab_SelectedIndexChanged;

            // bieuDo
            bieuDo.BackColor = Color.White;
            bieuDo.BorderStyle = BorderStyle.FixedSingle;
            bieuDo.Dock = DockStyle.Fill;
            bieuDo.Margin = new Padding(6, 0, 0, 0);
            bieuDo.Paint += bieuDo_Paint;
            bieuDo.Resize += bieuDo_Resize;

            // than
            than.BackColor = Color.FromArgb(245, 247, 250);
            than.ColumnCount = 2;
            than.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66F));
            than.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            than.Controls.Add(tab, 0, 0);
            than.Controls.Add(bieuDo, 1, 0);
            than.Dock = DockStyle.Fill;
            than.Padding = new Padding(8);
            than.RowCount = 1;
            than.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // tongHop
            tongHop.AutoSize = true;
            tongHop.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            tongHop.ForeColor = Color.FromArgb(35, 45, 60);
            tongHop.Margin = new Padding(10, 8, 10, 4);

            // bangDuoi
            bangDuoi.AutoSize = true;
            bangDuoi.BackColor = Color.FromArgb(245, 247, 250);
            bangDuoi.Controls.Add(tongHop);
            bangDuoi.Dock = DockStyle.Bottom;

            // mucXuatCsv
            mucXuatCsv.Text = "Xuất CSV";
            mucXuatCsv.Click += mucXuatCsv_Click;

            // mucXuatTxt
            mucXuatTxt.Text = "Xuất báo cáo TXT";
            mucXuatTxt.Click += mucXuatTxt_Click;

            // mucLamMoi
            mucLamMoi.Text = "Làm mới";
            mucLamMoi.Click += mucLamMoi_Click;

            // menuChuotPhai
            menuChuotPhai.Items.AddRange(new ToolStripItem[]
            {
                mucXuatCsv,
                mucXuatTxt,
                phanCach,
                mucLamMoi
            });
            menuChuotPhai.Size = new Size(190, 76);

            // FormDoanhThu
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(984, 601);
            Controls.Add(than);
            Controls.Add(bangDuoi);
            Controls.Add(bangTren);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(35, 45, 60);
            MinimumSize = new Size(900, 560);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thống kê doanh thu";
            Shown += FormDoanhThu_Shown;
            bangTren.ResumeLayout(false);
            bangTren.PerformLayout();
            than.ResumeLayout(false);
            tab.ResumeLayout(false);
            tabPhim.ResumeLayout(false);
            tabNgay.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)bangPhim).EndInit();
            ((System.ComponentModel.ISupportInitialize)bangNgay).EndInit();
            bangDuoi.ResumeLayout(false);
            bangDuoi.PerformLayout();
            menuChuotPhai.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
