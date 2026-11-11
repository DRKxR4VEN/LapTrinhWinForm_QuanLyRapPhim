using System.ComponentModel;

namespace QuanLyRapChieuPhim;

public static class Ui
{
    public static readonly Color MainBack = Color.FromArgb(245, 247, 250);
    public static readonly Color MainText = Color.FromArgb(35, 45, 60);
    public static readonly Color Accent = Color.FromArgb(35, 110, 220);

    /// <summary>True khi form đang được mở trong Visual Studio Designer.</summary>
    public static bool DesignTime => LicenseManager.UsageMode == LicenseUsageMode.Designtime;

    public static string Money(decimal value)
        => value.ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("vi-VN")) + " đ";

    /// <summary>Áp dụng màu nền, màu chữ và cỡ chữ đã lưu trong Registry.</summary>
    public static void ApplyAppearance(Form form)
    {
        if (DesignTime)
            return;

        var (back, fore, size) = AppData.LoadAppearance();
        form.BackColor = back;
        form.ForeColor = fore;
        form.Font = new Font("Segoe UI", size);

        Recolor(form, back, fore);
    }

    static void Recolor(Control root, Color back, Color fore)
    {
        foreach (Control control in root.Controls)
        {
            if (control.BackColor == MainBack)
                control.BackColor = back;

            if (control.ForeColor == MainText)
                control.ForeColor = fore;

            Recolor(control, back, fore);
        }
    }

    public static void ShowError(Exception ex)
    {
        FileHelper.Log(ex);
        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
