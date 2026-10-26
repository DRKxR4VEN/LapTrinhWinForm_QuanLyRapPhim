namespace QuanLyRapChieuPhim;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        AppData.Initialize();
        using var login = new LoginForm();
        if (login.ShowDialog() == DialogResult.OK)
            Application.Run(new MainForm(login.CurrentUser!));
    }
}
