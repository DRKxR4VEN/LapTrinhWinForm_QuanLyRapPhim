#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class RevenueForm
    {
        private System.ComponentModel.IContainer components = null;

        private FlowLayoutPanel topPanel;
        private Label lblFrom;
        private DateTimePicker from;
        private Label lblTo;
        private DateTimePicker to;
        private Button statButton;
        private Button csvButton;
        private Button txtButton;
        private TableLayoutPanel body;
        private TabControl tabs;
        private TabPage movieTab;
        private TabPage dayTab;
        private DataGridView movieGrid;
        private DataGridView dayGrid;
        private Panel chart;
        private FlowLayoutPanel bottomPanel;
        private Label summary;
        private ContextMenuStrip contextMenu;
        private ToolStripMenuItem contextCsvItem;
        private ToolStripMenuItem contextTxtItem;
        private ToolStripSeparator contextSeparator;
        private ToolStripMenuItem contextRefreshItem;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            topPanel = new FlowLayoutPanel();
            lblFrom = new Label();
            from = new DateTimePicker();
            lblTo = new Label();
            to = new DateTimePicker();
            statButton = new Button();
            csvButton = new Button();
            txtButton = new Button();
            body = new TableLayoutPanel();
            tabs = new TabControl();
            movieTab = new TabPage();
            dayTab = new TabPage();
            movieGrid = new DataGridView();
            dayGrid = new DataGridView();
            chart = new Panel();
            bottomPanel = new FlowLayoutPanel();
            summary = new Label();
            contextMenu = new ContextMenuStrip(components);
            contextCsvItem = new ToolStripMenuItem();
            contextTxtItem = new ToolStripMenuItem();
            contextSeparator = new ToolStripSeparator();
            contextRefreshItem = new ToolStripMenuItem();
            var movieCellStyle = new DataGridViewCellStyle();
            var movieHeaderStyle = new DataGridViewCellStyle();
            var dayCellStyle = new DataGridViewCellStyle();
            var dayHeaderStyle = new DataGridViewCellStyle();
            topPanel.SuspendLayout();
            body.SuspendLayout();
            tabs.SuspendLayout();
            movieTab.SuspendLayout();
            dayTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)movieGrid).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dayGrid).BeginInit();
            bottomPanel.SuspendLayout();
            contextMenu.SuspendLayout();
            SuspendLayout();

            // lblFrom
            lblFrom.AutoSize = true;
            lblFrom.ForeColor = Color.FromArgb(35, 45, 60);
            lblFrom.Margin = new Padding(5, 8, 3, 3);
            lblFrom.Text = "Từ ngày";
            lblFrom.TextAlign = ContentAlignment.MiddleLeft;

            // from
            from.CustomFormat = "dd/MM/yyyy";
            from.Format = DateTimePickerFormat.Custom;
            from.Margin = new Padding(3, 4, 3, 4);
            from.Size = new Size(125, 23);

            // lblTo
            lblTo.AutoSize = true;
            lblTo.ForeColor = Color.FromArgb(35, 45, 60);
            lblTo.Margin = new Padding(5, 8, 3, 3);
            lblTo.Text = "Đến ngày";
            lblTo.TextAlign = ContentAlignment.MiddleLeft;

            // to
            to.CustomFormat = "dd/MM/yyyy";
            to.Format = DateTimePickerFormat.Custom;
            to.Margin = new Padding(3, 4, 3, 4);
            to.Size = new Size(125, 23);

            // statButton
            statButton.AutoSize = true;
            statButton.BackColor = Color.FromArgb(35, 110, 220);
            statButton.Cursor = Cursors.Hand;
            statButton.FlatStyle = FlatStyle.Flat;
            statButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            statButton.ForeColor = Color.White;
            statButton.Margin = new Padding(8, 4, 4, 4);
            statButton.Padding = new Padding(10, 6, 10, 6);
            statButton.Text = "Thống kê";
            statButton.UseVisualStyleBackColor = false;
            statButton.Click += statButton_Click;

            // csvButton
            csvButton.AutoSize = true;
            csvButton.BackColor = Color.FromArgb(30, 130, 80);
            csvButton.Cursor = Cursors.Hand;
            csvButton.FlatStyle = FlatStyle.Flat;
            csvButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            csvButton.ForeColor = Color.White;
            csvButton.Margin = new Padding(4);
            csvButton.Padding = new Padding(10, 6, 10, 6);
            csvButton.Text = "Xuất CSV";
            csvButton.UseVisualStyleBackColor = false;
            csvButton.Click += csvButton_Click;

            // txtButton
            txtButton.AutoSize = true;
            txtButton.BackColor = Color.FromArgb(90, 100, 115);
            txtButton.Cursor = Cursors.Hand;
            txtButton.FlatStyle = FlatStyle.Flat;
            txtButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            txtButton.ForeColor = Color.White;
            txtButton.Margin = new Padding(4);
            txtButton.Padding = new Padding(10, 6, 10, 6);
            txtButton.Text = "Xuất báo cáo TXT";
            txtButton.UseVisualStyleBackColor = false;
            txtButton.Click += txtButton_Click;

            // topPanel
            topPanel.AutoSize = true;
            topPanel.BackColor = Color.FromArgb(245, 247, 250);
            topPanel.Controls.Add(lblFrom);
            topPanel.Controls.Add(from);
            topPanel.Controls.Add(lblTo);
            topPanel.Controls.Add(to);
            topPanel.Controls.Add(statButton);
            topPanel.Controls.Add(csvButton);
            topPanel.Controls.Add(txtButton);
            topPanel.Dock = DockStyle.Top;
            topPanel.Padding = new Padding(10);

            // movieGrid
            movieGrid.AllowUserToAddRows = false;
            movieGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            movieGrid.BackgroundColor = Color.White;
            movieGrid.BorderStyle = BorderStyle.FixedSingle;
            movieCellStyle.BackColor = Color.White;
            movieCellStyle.ForeColor = Color.FromArgb(35, 45, 60);
            movieCellStyle.Padding = new Padding(4);
            movieCellStyle.SelectionBackColor = Color.FromArgb(205, 225, 250);
            movieCellStyle.SelectionForeColor = Color.FromArgb(35, 45, 60);
            movieGrid.DefaultCellStyle = movieCellStyle;
            movieGrid.ColumnHeadersHeight = 32;
            movieGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            movieHeaderStyle.BackColor = Color.FromArgb(230, 238, 249);
            movieHeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            movieHeaderStyle.ForeColor = Color.FromArgb(35, 45, 60);
            movieHeaderStyle.Padding = new Padding(5);
            movieGrid.ColumnHeadersDefaultCellStyle = movieHeaderStyle;
            movieGrid.ContextMenuStrip = contextMenu;
            movieGrid.Dock = DockStyle.Fill;
            movieGrid.EnableHeadersVisualStyles = false;
            movieGrid.GridColor = Color.FromArgb(225, 229, 235);
            movieGrid.MultiSelect = false;
            movieGrid.ReadOnly = true;
            movieGrid.RowHeadersVisible = false;
            movieGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // dayGrid
            dayGrid.AllowUserToAddRows = false;
            dayGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dayGrid.BackgroundColor = Color.White;
            dayGrid.BorderStyle = BorderStyle.FixedSingle;
            dayCellStyle.BackColor = Color.White;
            dayCellStyle.ForeColor = Color.FromArgb(35, 45, 60);
            dayCellStyle.Padding = new Padding(4);
            dayCellStyle.SelectionBackColor = Color.FromArgb(205, 225, 250);
            dayCellStyle.SelectionForeColor = Color.FromArgb(35, 45, 60);
            dayGrid.DefaultCellStyle = dayCellStyle;
            dayGrid.ColumnHeadersHeight = 32;
            dayGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dayHeaderStyle.BackColor = Color.FromArgb(230, 238, 249);
            dayHeaderStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            dayHeaderStyle.ForeColor = Color.FromArgb(35, 45, 60);
            dayHeaderStyle.Padding = new Padding(5);
            dayGrid.ColumnHeadersDefaultCellStyle = dayHeaderStyle;
            dayGrid.ContextMenuStrip = contextMenu;
            dayGrid.Dock = DockStyle.Fill;
            dayGrid.EnableHeadersVisualStyles = false;
            dayGrid.GridColor = Color.FromArgb(225, 229, 235);
            dayGrid.MultiSelect = false;
            dayGrid.ReadOnly = true;
            dayGrid.RowHeadersVisible = false;
            dayGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            // movieTab
            movieTab.BackColor = Color.FromArgb(245, 247, 250);
            movieTab.Controls.Add(movieGrid);
            movieTab.Padding = new Padding(4);
            movieTab.Text = "Theo phim";

            // dayTab
            dayTab.BackColor = Color.FromArgb(245, 247, 250);
            dayTab.Controls.Add(dayGrid);
            dayTab.Padding = new Padding(4);
            dayTab.Text = "Theo ngày";

            // tabs
            tabs.Controls.Add(movieTab);
            tabs.Controls.Add(dayTab);
            tabs.Dock = DockStyle.Fill;
            tabs.Font = new Font("Segoe UI", 9F);
            tabs.SelectedIndexChanged += tabs_SelectedIndexChanged;

            // chart
            chart.BackColor = Color.White;
            chart.BorderStyle = BorderStyle.FixedSingle;
            chart.Dock = DockStyle.Fill;
            chart.Margin = new Padding(6, 0, 0, 0);
            chart.Paint += chart_Paint;
            chart.Resize += chart_Resize;

            // body
            body.BackColor = Color.FromArgb(245, 247, 250);
            body.ColumnCount = 2;
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66F));
            body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            body.Controls.Add(tabs, 0, 0);
            body.Controls.Add(chart, 1, 0);
            body.Dock = DockStyle.Fill;
            body.Padding = new Padding(8);
            body.RowCount = 1;
            body.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            // summary
            summary.AutoSize = true;
            summary.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            summary.ForeColor = Color.FromArgb(35, 45, 60);
            summary.Margin = new Padding(10, 8, 10, 4);

            // bottomPanel
            bottomPanel.AutoSize = true;
            bottomPanel.BackColor = Color.FromArgb(245, 247, 250);
            bottomPanel.Controls.Add(summary);
            bottomPanel.Dock = DockStyle.Bottom;

            // contextCsvItem
            contextCsvItem.Text = "Xuất CSV";
            contextCsvItem.Click += contextCsv_Click;

            // contextTxtItem
            contextTxtItem.Text = "Xuất báo cáo TXT";
            contextTxtItem.Click += contextTxt_Click;

            // contextRefreshItem
            contextRefreshItem.Text = "Làm mới";
            contextRefreshItem.Click += contextRefresh_Click;

            // contextMenu
            contextMenu.Items.AddRange(new ToolStripItem[]
            {
                contextCsvItem,
                contextTxtItem,
                contextSeparator,
                contextRefreshItem
            });
            contextMenu.Size = new Size(190, 76);

            // RevenueForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(984, 601);
            Controls.Add(body);
            Controls.Add(bottomPanel);
            Controls.Add(topPanel);
            Font = new Font("Segoe UI", 9F);
            ForeColor = Color.FromArgb(35, 45, 60);
            MinimumSize = new Size(900, 560);
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Thống kê doanh thu";
            Shown += RevenueForm_Shown;
            topPanel.ResumeLayout(false);
            topPanel.PerformLayout();
            body.ResumeLayout(false);
            tabs.ResumeLayout(false);
            movieTab.ResumeLayout(false);
            dayTab.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)movieGrid).EndInit();
            ((System.ComponentModel.ISupportInitialize)dayGrid).EndInit();
            bottomPanel.ResumeLayout(false);
            bottomPanel.PerformLayout();
            contextMenu.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
