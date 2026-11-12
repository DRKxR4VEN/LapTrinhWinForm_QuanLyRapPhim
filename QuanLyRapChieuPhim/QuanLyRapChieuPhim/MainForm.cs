namespace QuanLyRapChieuPhim;

public partial class MainForm : Form
{
    AppUser user = new() { Username = "designer", Role = "QuanTri" };

    /// <summary>True khi người dùng chọn Đăng xuất để quay lại form đăng nhập.</summary>
    public bool LogoutRequested { get; private set; }

    /// <summary>Constructor rỗng dành cho Visual Studio Designer.</summary>
    public MainForm() : this(new AppUser { Username = "designer", Role = "QuanTri" })
    {
    }

    public MainForm(AppUser currentUser)
    {
        user = currentUser;

        InitializeComponent();

        Text = $"QUẢN LÝ RẠP CHIẾU PHIM - {user.Username} ({user.Role})";
        ApplyRole();

        if (Ui.DesignTime)
            return;

        Ui.ApplyAppearance(this);

        var saved = AppData.LoadWindowBounds();
        if (saved.HasValue)
        {
            StartPosition = FormStartPosition.Manual;
            Bounds = saved.Value;
        }

        var mdi = Controls.OfType<MdiClient>().FirstOrDefault();
        if (mdi != null)
            mdi.BackColor = BackColor;

        welcomePanel.BringToFront();
        menuStrip.BringToFront();
    }

    void ApplyRole()
    {
        bool admin = user.Role == "QuanTri";

        backupItem.Visible = admin;
        restoreItem.Visible = admin;
        systemSeparator.Visible = admin;

        movieItem.Visible = admin;
        roomItem.Visible = admin;
        screeningItem.Visible = admin;
        revenueItem.Visible = admin;
    }

    void Open(Form form)
    {
        if (user.Role != "QuanTri" && form is not TicketForm && form is not SettingsForm)
        {
            MessageBox.Show(
                "Bạn không có quyền truy cập chức năng này.",
                "Phân quyền",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            form.Dispose();
            return;
        }

        foreach (Form child in MdiChildren)
        {
            if (child.GetType() == form.GetType())
            {
                child.Activate();
                form.Dispose();
                return;
            }
        }

        form.MdiParent = this;
        form.Show();
    }

    void backupItem_Click(object? sender, EventArgs e) => Backup();

    void Backup()
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Chọn thư mục lưu bản sao dữ liệu"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        try
        {
            var dest = Path.Combine(
                dialog.SelectedPath,
                "Backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            Directory.CreateDirectory(dest);

            foreach (var file in Directory.GetFiles(FileHelper.DataDir))
                File.Copy(file, Path.Combine(dest, Path.GetFileName(file)), true);

            MessageBox.Show("Đã sao lưu: " + dest, "Thông báo");
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    void restoreItem_Click(object? sender, EventArgs e) => Restore();

    void Restore()
    {
        using var dialog = new FolderBrowserDialog
        {
            Description = "Chọn thư mục backup cần khôi phục"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        if (MessageBox.Show(
                "Khôi phục sẽ ghi đè dữ liệu hiện tại. Tiếp tục?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        try
        {
            foreach (var file in Directory.GetFiles(dialog.SelectedPath, "*.txt"))
                File.Copy(file, Path.Combine(FileHelper.DataDir, Path.GetFileName(file)), true);

            AppData.Initialize();
            MessageBox.Show("Khôi phục xong. Nên đóng và mở lại các Form.");
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    void appearanceItem_Click(object? sender, EventArgs e) => Open(new SettingsForm());

    void movieItem_Click(object? sender, EventArgs e) => Open(new MovieForm());

    void roomItem_Click(object? sender, EventArgs e) => Open(new RoomForm());

    void screeningItem_Click(object? sender, EventArgs e) => Open(new ScreeningForm());

    void ticketItem_Click(object? sender, EventArgs e) => Open(new TicketForm());

    void revenueItem_Click(object? sender, EventArgs e) => Open(new RevenueForm());

    void logoutItem_Click(object? sender, EventArgs e) => Logout();

    void exitItem_Click(object? sender, EventArgs e) => Close();

    void Logout()
    {
        var confirm = MessageBox.Show(
            $"Đăng xuất tài khoản \"{user.Username}\" và quay lại trang đăng nhập?",
            "Xác nhận đăng xuất",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question,
            MessageBoxDefaultButton.Button2);

        if (confirm != DialogResult.Yes)
            return;

        LogoutRequested = true;
        Close();
    }

    void cascadeItem_Click(object? sender, EventArgs e) => ArrangeWindows(MdiLayout.Cascade);

    // "Sắp xếp ngang" xếp các cửa sổ cạnh nhau theo hàng ngang (TileVertical),
    // "Sắp xếp dọc" xếp chồng lên nhau theo cột dọc (TileHorizontal).
    void tileHorizontalItem_Click(object? sender, EventArgs e) => ArrangeWindows(MdiLayout.TileVertical);

    void tileVerticalItem_Click(object? sender, EventArgs e) => ArrangeWindows(MdiLayout.TileHorizontal);

    void arrangeIconsItem_Click(object? sender, EventArgs e) => LayoutMdi(MdiLayout.ArrangeIcons);

    readonly Dictionary<Form, Size> designMinimums = new();

    /// <summary>
    /// Sắp xếp các form con trong vùng MDI. MinimumSize của từng form được tạm hạ xuống
    /// theo kích thước ô chia để LayoutMdi không đẩy cửa sổ tràn ra ngoài vùng làm việc.
    /// </summary>
    void ArrangeWindows(MdiLayout layout)
    {
        var children = MdiChildren;
        if (children.Length == 0)
            return;

        foreach (var child in children)
        {
            if (!designMinimums.ContainsKey(child))
                designMinimums[child] = child.MinimumSize;

            child.MinimumSize = Size.Empty;
        }

        LayoutMdi(layout);

        foreach (var child in children)
        {
            if (!designMinimums.TryGetValue(child, out var design) || child.WindowState != FormWindowState.Normal)
                continue;

            child.MinimumSize = new Size(
                Math.Min(design.Width, child.Width),
                Math.Min(design.Height, child.Height));
        }
    }

    void MainForm_MdiChildActivate(object? sender, EventArgs e)
    {
        welcomePanel.Visible = ActiveMdiChild == null;
    }

    void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (!LogoutRequested && e.CloseReason == CloseReason.UserClosing)
        {
            var confirm = MessageBox.Show(
                "Bạn có chắc muốn thoát chương trình?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }
        }

        if (!Ui.DesignTime)
            AppData.SaveWindowBounds(this);
    }
}
