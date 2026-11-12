namespace QuanLyRapChieuPhim;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        AppData.Initialize();

        while (true)
        {
            using var login = new LoginForm();
            if (login.ShowDialog() != DialogResult.OK)
                break;

            using var main = new MainForm(login.CurrentUser!);
            Application.Run(main);

            // Đăng xuất: quay lại form đăng nhập để chọn tài khoản khác.
            if (!main.LogoutRequested)
                break;
        }
    }
}
