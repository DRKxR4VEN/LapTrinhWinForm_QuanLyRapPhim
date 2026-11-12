#nullable disable

namespace QuanLyRapChieuPhim
{
    partial class MainForm
    {
        private System.ComponentModel.IContainer components = null;

        private MenuStrip menuStrip;
        private ToolStripMenuItem systemMenu;
        private ToolStripMenuItem backupItem;
        private ToolStripMenuItem restoreItem;
        private ToolStripSeparator systemSeparator;
        private ToolStripMenuItem appearanceItem;
        private ToolStripMenuItem logoutItem;
        private ToolStripSeparator exitSeparator;
        private ToolStripMenuItem exitItem;
        private ToolStripMenuItem manageMenu;
        private ToolStripMenuItem movieItem;
        private ToolStripMenuItem roomItem;
        private ToolStripMenuItem screeningItem;
        private ToolStripMenuItem ticketItem;
        private ToolStripMenuItem revenueItem;
        private ToolStripMenuItem windowMenu;
        private ToolStripMenuItem cascadeItem;
        private ToolStripMenuItem tileHorizontalItem;
        private ToolStripMenuItem tileVerticalItem;
        private ToolStripSeparator windowSeparator;
        private ToolStripMenuItem arrangeIconsItem;
        private Panel welcomePanel;
        private Label titleLabel;
        private Label subtitleLabel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
                components.Dispose();

            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            menuStrip = new MenuStrip();
            systemMenu = new ToolStripMenuItem();
            backupItem = new ToolStripMenuItem();
            restoreItem = new ToolStripMenuItem();
            systemSeparator = new ToolStripSeparator();
            appearanceItem = new ToolStripMenuItem();
            logoutItem = new ToolStripMenuItem();
            exitSeparator = new ToolStripSeparator();
            exitItem = new ToolStripMenuItem();
            manageMenu = new ToolStripMenuItem();
            movieItem = new ToolStripMenuItem();
            roomItem = new ToolStripMenuItem();
            screeningItem = new ToolStripMenuItem();
            ticketItem = new ToolStripMenuItem();
            revenueItem = new ToolStripMenuItem();
            windowMenu = new ToolStripMenuItem();
            cascadeItem = new ToolStripMenuItem();
            tileHorizontalItem = new ToolStripMenuItem();
            tileVerticalItem = new ToolStripMenuItem();
            windowSeparator = new ToolStripSeparator();
            arrangeIconsItem = new ToolStripMenuItem();
            welcomePanel = new Panel();
            titleLabel = new Label();
            subtitleLabel = new Label();
            menuStrip.SuspendLayout();
            welcomePanel.SuspendLayout();
            SuspendLayout();

            // systemMenu
            systemMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                backupItem, restoreItem, systemSeparator, appearanceItem,
                logoutItem, exitSeparator, exitItem
            });
            systemMenu.Text = "Hệ thống";

            // backupItem
            backupItem.Text = "Sao lưu dữ liệu";
            backupItem.Click += backupItem_Click;

            // restoreItem
            restoreItem.Text = "Khôi phục dữ liệu";
            restoreItem.Click += restoreItem_Click;

            // appearanceItem
            appearanceItem.Text = "Đổi giao diện";
            appearanceItem.Click += appearanceItem_Click;

            // logoutItem
            logoutItem.Text = "Đăng xuất";
            logoutItem.Click += logoutItem_Click;

            // exitItem
            exitItem.Text = "Thoát";
            exitItem.Click += exitItem_Click;

            // manageMenu
            manageMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                movieItem, roomItem, screeningItem, ticketItem, revenueItem
            });
            manageMenu.Text = "Quản lý";

            // movieItem
            movieItem.Text = "Phim";
            movieItem.Click += movieItem_Click;

            // roomItem
            roomItem.Text = "Phòng chiếu";
            roomItem.Click += roomItem_Click;

            // screeningItem
            screeningItem.Text = "Suất chiếu";
            screeningItem.Click += screeningItem_Click;

            // ticketItem
            ticketItem.Text = "Bán vé";
            ticketItem.Click += ticketItem_Click;

            // revenueItem
            revenueItem.Text = "Doanh thu";
            revenueItem.Click += revenueItem_Click;

            // windowMenu
            windowMenu.DropDownItems.AddRange(new ToolStripItem[]
            {
                cascadeItem, tileHorizontalItem, tileVerticalItem, windowSeparator, arrangeIconsItem
            });
            windowMenu.Text = "Cửa sổ";

            // cascadeItem
            cascadeItem.Text = "Sắp xếp chồng";
            cascadeItem.Click += cascadeItem_Click;

            // tileHorizontalItem
            tileHorizontalItem.Text = "Sắp xếp ngang";
            tileHorizontalItem.Click += tileHorizontalItem_Click;

            // tileVerticalItem
            tileVerticalItem.Text = "Sắp xếp dọc";
            tileVerticalItem.Click += tileVerticalItem_Click;

            // arrangeIconsItem
            arrangeIconsItem.Text = "Sắp xếp biểu tượng";
            arrangeIconsItem.Click += arrangeIconsItem_Click;

            // menuStrip
            menuStrip.BackColor = Color.White;
            menuStrip.Font = new Font("Segoe UI", 10F);
            menuStrip.Items.AddRange(new ToolStripItem[] { systemMenu, manageMenu, windowMenu });
            menuStrip.Location = new Point(0, 0);
            menuStrip.MdiWindowListItem = windowMenu;
            menuStrip.Name = "menuStrip";
            menuStrip.Padding = new Padding(8, 5, 0, 5);
            menuStrip.Size = new Size(1184, 34);
            menuStrip.TabIndex = 0;
            menuStrip.Text = "menuStrip";

            // titleLabel
            titleLabel.Dock = DockStyle.Top;
            titleLabel.Font = new Font("Segoe UI", 22F, FontStyle.Bold);
            titleLabel.ForeColor = Color.FromArgb(35, 45, 60);
            titleLabel.Height = 60;
            titleLabel.Text = "QUẢN LÝ RẠP CHIẾU PHIM";
            titleLabel.TextAlign = ContentAlignment.MiddleCenter;

            // subtitleLabel
            subtitleLabel.Dock = DockStyle.Top;
            subtitleLabel.Font = new Font("Segoe UI", 12F);
            subtitleLabel.ForeColor = Color.FromArgb(90, 100, 115);
            subtitleLabel.Height = 40;
            subtitleLabel.Text = "Chọn chức năng trong menu “Quản lý” để bắt đầu";
            subtitleLabel.TextAlign = ContentAlignment.TopCenter;

            // welcomePanel
            welcomePanel.BackColor = Color.FromArgb(245, 247, 250);
            welcomePanel.Controls.Add(subtitleLabel);
            welcomePanel.Controls.Add(titleLabel);
            welcomePanel.Dock = DockStyle.Fill;
            welcomePanel.Padding = new Padding(30);

            // MainForm
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(245, 247, 250);
            ClientSize = new Size(1184, 721);
            Controls.Add(welcomePanel);
            Controls.Add(menuStrip);
            Font = new Font("Segoe UI", 10F);
            ForeColor = Color.FromArgb(35, 45, 60);
            IsMdiContainer = true;
            MainMenuStrip = menuStrip;
            StartPosition = FormStartPosition.CenterScreen;
            Text = "QUẢN LÝ RẠP CHIẾU PHIM";
            FormClosing += MainForm_FormClosing;
            MdiChildActivate += MainForm_MdiChildActivate;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            welcomePanel.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }
    }
}
