namespace QuanLyRapChieuPhim;

public partial class FormCauHinh : Form
{
    Color mauNen;
    Color mauChu;

    public FormCauHinh()
    {
        InitializeComponent();
        GiaoDien.ApDungGiaoDien(this);

        var hienTai = DuLieu.DocGiaoDien();
        mauNen = hienTai.Back;
        mauChu = hienTai.Fore;
        oCoChu.Value = Math.Clamp((decimal)hienTai.FontSize, oCoChu.Minimum, oCoChu.Maximum);
    }

    void nutMauNen_Click(object? sender, EventArgs e)
    {
        using var hopThoai = new ColorDialog { Color = mauNen, FullOpen = true };
        if (hopThoai.ShowDialog() == DialogResult.OK)
            mauNen = hopThoai.Color;
    }

    void nutMauChu_Click(object? sender, EventArgs e)
    {
        using var hopThoai = new ColorDialog { Color = mauChu, FullOpen = true };
        if (hopThoai.ShowDialog() == DialogResult.OK)
            mauChu = hopThoai.Color;
    }

    void nutLuu_Click(object? sender, EventArgs e)
    {
        DuLieu.LuuGiaoDien(mauNen, mauChu, (float)oCoChu.Value);
        MessageBox.Show(
            "Đã lưu tùy chọn giao diện vào Registry.\n" +
            "Đóng và mở lại ứng dụng để áp dụng toàn bộ.",
            "Thông báo",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
        Close();
    }
}
