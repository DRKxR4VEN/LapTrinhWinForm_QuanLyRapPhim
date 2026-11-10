namespace QuanLyRapChieuPhim;

public partial class FormChinh : Form
{
    TaiKhoan nguoiDung = new() { TenDangNhap = "designer", VaiTro = "QuanTri" };

    /// <summary>Constructor rỗng dành cho Visual Studio Designer.</summary>
    public FormChinh() : this(new TaiKhoan { TenDangNhap = "designer", VaiTro = "QuanTri" })
    {
    }

    public FormChinh(TaiKhoan nguoiDungHienTai)
    {
        nguoiDung = nguoiDungHienTai;

        InitializeComponent();

        Text = $"QUẢN LÝ RẠP CHIẾU PHIM - {nguoiDung.TenDangNhap} ({nguoiDung.VaiTro})";
        ApDungPhanQuyen();

        if (GiaoDien.DangThietKe)
            return;

        GiaoDien.ApDungGiaoDien(this);

        var viTriDaLuu = DuLieu.DocViTriCuaSo();
        if (viTriDaLuu.HasValue)
        {
            StartPosition = FormStartPosition.Manual;
            Bounds = viTriDaLuu.Value;
        }

        var vungMdi = Controls.OfType<MdiClient>().FirstOrDefault();
        if (vungMdi != null)
            vungMdi.BackColor = BackColor;

        bangChao.BringToFront();
        thanhMenu.BringToFront();
    }

    void ApDungPhanQuyen()
    {
        bool laQuanTri = nguoiDung.VaiTro == "QuanTri";

        mucSaoLuu.Visible = laQuanTri;
        mucKhoiPhuc.Visible = laQuanTri;
        phanCachHeThong.Visible = laQuanTri;

        mucPhim.Visible = laQuanTri;
        mucPhong.Visible = laQuanTri;
        mucSuatChieu.Visible = laQuanTri;
        mucDoanhThu.Visible = laQuanTri;
    }

    void MoFormCon(Form form)
    {
        if (nguoiDung.VaiTro != "QuanTri" && form is not FormBanVe && form is not FormCauHinh)
        {
            MessageBox.Show(
                "Bạn không có quyền truy cập chức năng này.",
                "Phân quyền",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            form.Dispose();
            return;
        }

        foreach (Form formCon in MdiChildren)
        {
            if (formCon.GetType() == form.GetType())
            {
                formCon.Activate();
                form.Dispose();
                return;
            }
        }

        form.MdiParent = this;
        form.Show();
    }

    void mucSaoLuu_Click(object? sender, EventArgs e) => SaoLuu();

    void SaoLuu()
    {
        using var hopThoai = new FolderBrowserDialog
        {
            Description = "Chọn thư mục lưu bản sao dữ liệu"
        };

        if (hopThoai.ShowDialog() != DialogResult.OK)
            return;

        try
        {
            var thuMucDich = Path.Combine(
                hopThoai.SelectedPath,
                "Backup_" + DateTime.Now.ToString("yyyyMMdd_HHmmss"));

            Directory.CreateDirectory(thuMucDich);

            foreach (var tep in Directory.GetFiles(TepTin.ThuMuc))
                File.Copy(tep, Path.Combine(thuMucDich, Path.GetFileName(tep)), true);

            MessageBox.Show("Đã sao lưu: " + thuMucDich, "Thông báo");
        }
        catch (Exception ex)
        {
            GiaoDien.BaoLoi(ex);
        }
    }

    void mucKhoiPhuc_Click(object? sender, EventArgs e) => KhoiPhuc();

    void KhoiPhuc()
    {
        using var hopThoai = new FolderBrowserDialog
        {
            Description = "Chọn thư mục backup cần khôi phục"
        };

        if (hopThoai.ShowDialog() != DialogResult.OK)
            return;

        if (MessageBox.Show(
                "Khôi phục sẽ ghi đè dữ liệu hiện tại. Tiếp tục?",
                "Xác nhận",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) != DialogResult.Yes)
            return;

        try
        {
            foreach (var tep in Directory.GetFiles(hopThoai.SelectedPath, "*.txt"))
                File.Copy(tep, Path.Combine(TepTin.ThuMuc, Path.GetFileName(tep)), true);

            DuLieu.KhoiTao();
            MessageBox.Show("Khôi phục xong. Nên đóng và mở lại các Form.");
        }
        catch (Exception ex)
        {
            GiaoDien.BaoLoi(ex);
        }
    }

    void mucGiaoDien_Click(object? sender, EventArgs e) => MoFormCon(new FormCauHinh());

    void mucPhim_Click(object? sender, EventArgs e) => MoFormCon(new FormPhim());

    void mucPhong_Click(object? sender, EventArgs e) => MoFormCon(new FormPhongChieu());

    void mucSuatChieu_Click(object? sender, EventArgs e) => MoFormCon(new FormSuatChieu());

    void mucBanVe_Click(object? sender, EventArgs e) => MoFormCon(new FormBanVe());

    void mucDoanhThu_Click(object? sender, EventArgs e) => MoFormCon(new FormDoanhThu());

    void mucThoat_Click(object? sender, EventArgs e) => Close();

    void mucXepChong_Click(object? sender, EventArgs e) => LayoutMdi(MdiLayout.Cascade);

    void mucXepNgang_Click(object? sender, EventArgs e) => LayoutMdi(MdiLayout.TileHorizontal);

    void mucXepDoc_Click(object? sender, EventArgs e) => LayoutMdi(MdiLayout.TileVertical);

    void mucXepBieuTuong_Click(object? sender, EventArgs e) => LayoutMdi(MdiLayout.ArrangeIcons);

    void FormChinh_MdiChildActivate(object? sender, EventArgs e)
    {
        bangChao.Visible = ActiveMdiChild == null;
    }

    void FormChinh_FormClosing(object? sender, FormClosingEventArgs e)
    {
        if (e.CloseReason == CloseReason.UserClosing)
        {
            var xacNhan = MessageBox.Show(
                "Bạn có chắc muốn thoát chương trình?",
                "Xác nhận thoát",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (xacNhan != DialogResult.Yes)
            {
                e.Cancel = true;
                return;
            }
        }

        if (!GiaoDien.DangThietKe)
            DuLieu.LuuViTriCuaSo(this);
    }
}
