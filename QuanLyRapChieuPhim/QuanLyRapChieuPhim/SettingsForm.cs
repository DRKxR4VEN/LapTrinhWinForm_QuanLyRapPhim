namespace QuanLyRapChieuPhim;

public partial class SettingsForm : Form
{
    Color back;
    Color fore;

    public SettingsForm()
    {
        InitializeComponent();
        Ui.ApplyAppearance(this);

        var current = AppData.LoadAppearance();
        back = current.Back;
        fore = current.Fore;
        fontSize.Value = Math.Clamp((decimal)current.FontSize, fontSize.Minimum, fontSize.Maximum);
    }

    void backButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new ColorDialog { Color = back, FullOpen = true };
        if (dialog.ShowDialog() == DialogResult.OK)
            back = dialog.Color;
    }

    void foreButton_Click(object? sender, EventArgs e)
    {
        using var dialog = new ColorDialog { Color = fore, FullOpen = true };
        if (dialog.ShowDialog() == DialogResult.OK)
            fore = dialog.Color;
    }

    void saveButton_Click(object? sender, EventArgs e)
    {
        AppData.SaveAppearance(back, fore, (float)fontSize.Value);
        MessageBox.Show(
            "Đã lưu tùy chọn giao diện vào Registry.\n" +
            "Đóng và mở lại ứng dụng để áp dụng toàn bộ.",
            "Thông báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        Close();
    }
}
