namespace QuanLyRapChieuPhim;

public class LoginForm : Form
{
    readonly TextBox user = new()
    {
        Width = 180
    };

    readonly TextBox pass = new()
    {
        Width = 180,
        PasswordChar = '●'
    };

    readonly Dictionary<string, int> fails = new();

    public AppUser? CurrentUser { get; private set; }

    public LoginForm()
    {
        Text = "Đăng nhập - Quản lý rạp chiếu phim";
        Width = 400;
        Height = 250;
        StartPosition = FormStartPosition.CenterScreen;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;

        BackColor = Ui.MainBack;
        ForeColor = Ui.MainText;
        Font = new Font("Segoe UI", 10);

        var panel = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(18),
            ColumnCount = 2,
            RowCount = 3,
            BackColor = Ui.MainBack
        };

        panel.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 35));

        panel.ColumnStyles.Add(
            new ColumnStyle(SizeType.Percent, 65));

        panel.Controls.Add(Ui.Lab("Tài khoản"), 0, 0);
        panel.Controls.Add(user, 1, 0);

        panel.Controls.Add(Ui.Lab("Mật khẩu"), 0, 1);
        panel.Controls.Add(pass, 1, 1);

        var login = Ui.Btn("Đăng nhập");
        login.Click += (_, _) => DoLogin();

        panel.Controls.Add(login, 1, 2);

        Controls.Add(panel);

        var hint = new Label
        {
            BackColor = Ui.MainBack,
            ForeColor = Ui.MainText,
            Font = new Font("Segoe UI", 9),
            Text = "Mặc định: admin/123 hoặc nhanvien/123",
            Dock = DockStyle.Bottom,
            Height = 30,
            TextAlign = ContentAlignment.MiddleCenter
        };

        Controls.Add(hint);

        AcceptButton = login;
    }

    void DoLogin()
    {
        var name = user.Text.Trim();

        if (string.IsNullOrWhiteSpace(name))
        {
            MessageBox.Show("Vui lòng nhập tài khoản.");
            user.Focus();
            return;
        }

        if (fails.GetValueOrDefault(name) >= 3)
        {
            MessageBox.Show(
                "Tài khoản bị khóa do nhập sai 3 lần.");
            return;
        }

        var found = AppData.Users.FirstOrDefault(
            x => x.Username == name &&
                 x.Password == pass.Text);

        if (found == null)
        {
            fails[name] = fails.GetValueOrDefault(name) + 1;

            MessageBox.Show(
                $"Sai tài khoản hoặc mật khẩu. " +
                $"Lần sai: {fails[name]}/3");

            if (fails[name] >= 3)
            {
                MessageBox.Show(
                    "Tài khoản đã bị khóa trong phiên chạy này.");
            }

            pass.Clear();
            pass.Focus();
            return;
        }

        CurrentUser = found;
        DialogResult = DialogResult.OK;
        Close();
    }
}