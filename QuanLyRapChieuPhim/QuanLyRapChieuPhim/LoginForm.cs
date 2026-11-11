namespace QuanLyRapChieuPhim;

public partial class LoginForm : Form
{
    readonly Dictionary<string, int> fails = new();

    public AppUser? CurrentUser { get; private set; }

    public LoginForm()
    {
        InitializeComponent();
        Ui.ApplyAppearance(this);

        var memory = AppData.LoadLoginMemory();
        remember.Checked = memory.Remember;

        if (memory.Remember && !string.IsNullOrWhiteSpace(memory.User))
        {
            user.Text = memory.User;
            pass.Focus();
        }
        else
        {
            user.Focus();
        }
    }

    void loginButton_Click(object? sender, EventArgs e) => DoLogin();

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

        AppData.SaveLoginMemory(name, remember.Checked);
        CurrentUser = found;
        DialogResult = DialogResult.OK;
        Close();
    }
}
