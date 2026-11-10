using System.ComponentModel;

namespace QuanLyRapChieuPhim;

public static class GiaoDien
{
    public static readonly Color NenChinh = Color.FromArgb(245, 247, 250);
    public static readonly Color ChuChinh = Color.FromArgb(35, 45, 60);
    public static readonly Color MauNhan = Color.FromArgb(35, 110, 220);

    /// <summary>True khi cửa sổ đang được mở trong Visual Studio Designer.</summary>
    public static bool DangThietKe => LicenseManager.UsageMode == LicenseUsageMode.Designtime;

    public static string Tien(decimal giaTri)
        => giaTri.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("vi-VN")) + " đ";

    /// <summary>Áp dụng màu nền, màu chữ và cỡ chữ đã lưu trong Registry.</summary>
    public static void ApDungGiaoDien(Form bieuMau)
    {
        if (DangThietKe)
            return;

        var (mauNen, mauChu, coChu) = DuLieu.DocGiaoDien();
        bieuMau.BackColor = mauNen;
        bieuMau.ForeColor = mauChu;
        bieuMau.Font = new Font("Segoe UI", coChu);

        DoiMau(bieuMau, mauNen, mauChu);
    }

    static void DoiMau(Control goc, Color mauNen, Color mauChu)
    {
        foreach (Control dieuKhien in goc.Controls)
        {
            if (dieuKhien.BackColor == NenChinh)
                dieuKhien.BackColor = mauNen;

            if (dieuKhien.ForeColor == ChuChinh)
                dieuKhien.ForeColor = mauChu;

            DoiMau(dieuKhien, mauNen, mauChu);
        }
    }

    public static void BaoLoi(Exception ex)
    {
        TepTin.GhiLoi(ex);
        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
