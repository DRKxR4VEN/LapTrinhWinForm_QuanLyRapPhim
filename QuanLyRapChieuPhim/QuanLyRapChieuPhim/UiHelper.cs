using System.ComponentModel;

namespace QuanLyRapChieuPhim;

public static class Ui
{
    public static readonly Color MainBack = Color.FromArgb(245, 247, 250);
    public static readonly Color MainText = Color.FromArgb(35, 45, 60);
    public static readonly Color Accent = Color.FromArgb(35, 110, 220);
    public static readonly Color SecondaryText = Color.FromArgb(90, 100, 115);

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

        // Nền/chữ do người dùng chọn có thể quá tương phản thấp (ví dụ nền magenta
        // với chữ xanh đen); đổi chữ sang đen/trắng để vẫn đọc được.
        if (ContrastRatio(back, fore) < 4.5)
            fore = Luminance(back) > 0.179 ? Color.Black : Color.White;

        form.BackColor = back;
        form.ForeColor = fore;
        form.Font = new Font("Segoe UI", size);

        Recolor(form, back, fore);
    }

    public static double ContrastRatio(Color a, Color b)
    {
        var first = Luminance(a);
        var second = Luminance(b);
        return (Math.Max(first, second) + 0.05) / (Math.Min(first, second) + 0.05);
    }

    static double Luminance(Color color)
    {
        static double Channel(int value)
        {
            var channel = value / 255.0;
            return channel <= 0.03928
                ? channel / 12.92
                : Math.Pow((channel + 0.055) / 1.055, 2.4);
        }

        return 0.2126 * Channel(color.R)
             + 0.7152 * Channel(color.G)
             + 0.0722 * Channel(color.B);
    }

    static void Recolor(Control root, Color back, Color fore)
    {
        foreach (Control control in root.Controls)
        {
            if (control.BackColor == MainBack)
                control.BackColor = back;

            // Ô nhập liệu có nền trắng riêng nên không dùng màu chữ của theme
            // nếu màu đó quá nhạt trên nền thật của control.
            var background = control.BackColor == Color.Transparent
                ? root.BackColor
                : control.BackColor;

            if (control.ForeColor == MainText)
            {
                control.ForeColor = ContrastRatio(background, fore) >= 4.5
                    ? fore
                    : Luminance(background) > 0.179 ? Color.Black : Color.White;
            }
            else if (control.ForeColor == SecondaryText &&
                     ContrastRatio(background, SecondaryText) < 3)
            {
                control.ForeColor = Luminance(background) > 0.179
                    ? SecondaryText
                    : Color.FromArgb(200, 205, 215);
            }

            Recolor(control, back, fore);
        }
    }

    public static void ShowError(Exception ex)
    {
        FileHelper.Log(ex);
        MessageBox.Show(ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }
}
