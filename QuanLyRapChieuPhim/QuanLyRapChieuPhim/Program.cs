namespace QuanLyRapChieuPhim;

internal static class Program
{
    [STAThread]
    static void Main()
    {
        ApplicationConfiguration.Initialize();
        DuLieu.KhoiTao();
        using var dangNhap = new FormDangNhap();
        if (dangNhap.ShowDialog() == DialogResult.OK)
            Application.Run(new FormChinh(dangNhap.NguoiDungHienTai!));
    }
}
