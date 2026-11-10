namespace QuanLyRapChieuPhim;

public partial class FormDangNhap : Form
{
    readonly Dictionary<string, int> soLanSai = new();

    public TaiKhoan? NguoiDungHienTai { get; private set; }

    public FormDangNhap()
    {
        InitializeComponent();
        GiaoDien.ApDungGiaoDien(this);

        var ghiNhoDaLuu = DuLieu.DocGhiNhoDangNhap();
        oGhiNho.Checked = ghiNhoDaLuu.GhiNho;

        if (ghiNhoDaLuu.GhiNho && !string.IsNullOrWhiteSpace(ghiNhoDaLuu.NguoiDung))
        {
            oTaiKhoan.Text = ghiNhoDaLuu.NguoiDung;
            oMatKhau.Focus();
        }
        else
        {
            oTaiKhoan.Focus();
        }
    }

    void nutDangNhap_Click(object? sender, EventArgs e) => DangNhap();

    void DangNhap()
    {
        var tenDangNhap = oTaiKhoan.Text.Trim();

        if (string.IsNullOrWhiteSpace(tenDangNhap))
        {
            MessageBox.Show("Vui lòng nhập tài khoản.");
            oTaiKhoan.Focus();
            return;
        }

        if (soLanSai.GetValueOrDefault(tenDangNhap) >= 3)
        {
            MessageBox.Show(
                "Tài khoản bị khóa do nhập sai 3 lần.");
            return;
        }

        var taiKhoan = DuLieu.DanhSachTaiKhoan.FirstOrDefault(
            x => x.TenDangNhap == tenDangNhap &&
                 x.MatKhau == oMatKhau.Text);

        if (taiKhoan == null)
        {
            soLanSai[tenDangNhap] = soLanSai.GetValueOrDefault(tenDangNhap) + 1;

            MessageBox.Show(
                $"Sai tài khoản hoặc mật khẩu. " +
                $"Lần sai: {soLanSai[tenDangNhap]}/3");

            if (soLanSai[tenDangNhap] >= 3)
            {
                MessageBox.Show(
                    "Tài khoản đã bị khóa trong phiên chạy này.");
            }

            oMatKhau.Clear();
            oMatKhau.Focus();
            return;
        }

        DuLieu.LuuGhiNhoDangNhap(tenDangNhap, oGhiNho.Checked);
        NguoiDungHienTai = taiKhoan;
        DialogResult = DialogResult.OK;
        Close();
    }
}
