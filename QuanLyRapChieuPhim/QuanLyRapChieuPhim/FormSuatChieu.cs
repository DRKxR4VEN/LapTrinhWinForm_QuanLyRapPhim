using System.Globalization;

namespace QuanLyRapChieuPhim;

/// <summary>
/// Phân hệ SV2 - Quản lý suất chiếu.
/// - Lên lịch suất chiếu cho từng phòng.
/// - Chặn trùng giờ trong cùng phòng (thời lượng phim + thời gian dọn phòng).
/// - Hiển thị lịch dạng cây Ngày -> Phim -> Suất.
/// - Chỉ cho hủy suất chiếu chưa bán vé.
/// </summary>
public partial class FormSuatChieu : Form
{
    static readonly CultureInfo Vietnamese = CultureInfo.GetCultureInfo("vi-VN");

    public FormSuatChieu()
    {
        InitializeComponent();
        GiaoDien.ApDungGiaoDien(this);

        if (GiaoDien.DangThietKe)
            return;

        DatGioMacDinh();
        NapLai();
    }

    void nutThem_Click(object? sender, EventArgs e) => ThemSuat();

    void nutHuy_Click(object? sender, EventArgs e) => HuySuat();

    void nutLamMoi_Click(object? sender, EventArgs e) => NapLai();

    void oPhim_SelectedIndexChanged(object? sender, EventArgs e) => CapNhatXemTruoc();

    void oPhong_SelectedIndexChanged(object? sender, EventArgs e) => CapNhatXemTruoc();

    void oNgay_ValueChanged(object? sender, EventArgs e) => CapNhatXemTruoc();

    void oGio_ValueChanged(object? sender, EventArgs e) => CapNhatXemTruoc();

    void cayLich_AfterSelect(object? sender, TreeViewEventArgs e) => HienThiDangChon();

    /// <summary>Nạp lại danh sách phim/phòng và vẽ lại cây lịch.</summary>
    void NapLai()
    {
        NapLuaChon();
        NapCay();
        CapNhatXemTruoc();
    }

    void NapLuaChon()
    {
        var maPhimDaChon = (oPhim.SelectedItem as Phim)?.Ma;
        var maPhongDaChon = (oPhong.SelectedItem as PhongChieu)?.Ma;

        var dsPhim = DuLieu.DanhSachPhim.OrderBy(x => x.Ten).ToList();
        var dsPhong = DuLieu.DanhSachPhong.OrderBy(x => x.Ten).ToList();

        oPhim.DataSource = null;
        oPhim.DisplayMember = nameof(Phim.Ten);
        oPhim.ValueMember = nameof(Phim.Ma);
        oPhim.DataSource = dsPhim;

        oPhong.DataSource = null;
        oPhong.DisplayMember = nameof(PhongChieu.Ten);
        oPhong.ValueMember = nameof(PhongChieu.Ma);
        oPhong.DataSource = dsPhong;

        if (maPhimDaChon != null)
        {
            var chiSo = dsPhim.FindIndex(x => x.Ma == maPhimDaChon);
            if (chiSo >= 0 && chiSo < oPhim.Items.Count)
                oPhim.SelectedIndex = chiSo;
        }

        if (maPhongDaChon != null)
        {
            var chiSo = dsPhong.FindIndex(x => x.Ma == maPhongDaChon);
            if (chiSo >= 0 && chiSo < oPhong.Items.Count)
                oPhong.SelectedIndex = chiSo;
        }

        nutThem.Enabled = DuLieu.DanhSachPhim.Count > 0 && DuLieu.DanhSachPhong.Count > 0;

        if (DuLieu.DanhSachPhim.Count == 0 || DuLieu.DanhSachPhong.Count == 0)
        {
            goiY.Text = "Cần có ít nhất một phim và một phòng chiếu (nhập ở phân hệ Phim/Phòng chiếu) trước khi lên lịch.";
        }
        else
        {
            goiY.Text = $"Mỗi suất chiếu được cộng thêm {CauHinh.PhutDonPhong} phút dọn phòng khi kiểm tra trùng lịch.";
        }
    }

    void DatGioMacDinh()
    {
        var thoiDiemSom = DateTime.Now.AddMinutes(30);
        var soPhutCong = (30 - thoiDiemSom.Minute % 30) % 30;
        var goiYThoiGian = new DateTime(thoiDiemSom.Year, thoiDiemSom.Month, thoiDiemSom.Day, thoiDiemSom.Hour, thoiDiemSom.Minute, 0).AddMinutes(soPhutCong);

        oNgay.Value = goiYThoiGian.Date;
        oGio.Value = goiYThoiGian;
    }

    DateTime LayGioBatDau()
    {
        var ngay = oNgay.Value.Date;
        var gio = oGio.Value;
        return new DateTime(ngay.Year, ngay.Month, ngay.Day, gio.Hour, gio.Minute, 0);
    }

    void CapNhatXemTruoc()
    {
        if (oPhim.SelectedItem is not Phim phimDaChon)
        {
            thongTinPhim.Text = "Chưa có phim để lên lịch.";
            thongTinKetThuc.Text = "";
            return;
        }

        thongTinPhim.Text =
            $"Thời lượng phim: {phimDaChon.ThoiLuong} phút  •  " +
            $"Dọn phòng: {CauHinh.PhutDonPhong} phút";

        var batDau = LayGioBatDau();
        var ketThuc = batDau.AddMinutes(phimDaChon.ThoiLuong);
        var phongTrong = ketThuc.AddMinutes(CauHinh.PhutDonPhong);

        thongTinKetThuc.Text =
            $"Kết thúc phim: {ketThuc:HH:mm dd/MM/yyyy}  •  " +
            $"Phòng trống lại: {phongTrong:HH:mm}";
        thongTinKetThuc.ForeColor = batDau <= DateTime.Now
            ? Color.Firebrick
            : Color.FromArgb(35, 110, 220);
    }

    void ThemSuat()
    {
        try
        {
            if (oPhim.SelectedItem is not Phim phimDaChon)
            {
                oPhim.Focus();
                throw new Exception("Vui lòng chọn phim cho suất chiếu.");
            }

            if (oPhong.SelectedItem is not PhongChieu phongChon)
            {
                oPhong.Focus();
                throw new Exception("Vui lòng chọn phòng chiếu.");
            }

            if (phimDaChon.ThoiLuong <= 0)
                throw new Exception("Phim chưa có thời lượng hợp lệ. Hãy sửa lại ở phần Quản lý phim.");

            var gioBatDau = LayGioBatDau();
            if (gioBatDau <= DateTime.Now)
            {
                oGio.Focus();
                throw new Exception("Giờ bắt đầu phải sau thời điểm hiện tại.");
            }

            var suatMoi = new SuatChieu
            {
                Ma = MaSuatMoi(),
                MaPhim = phimDaChon.Ma,
                MaPhong = phongChon.Ma,
                GioBatDau = gioBatDau,
                TenPhim = phimDaChon.Ten,
                TenPhong = phongChon.Ten
            };

            var suatTrung = DuLieu.TimTrungLich(suatMoi, phimDaChon);
            if (suatTrung != null)
            {
                var phimTrung = DuLieu.DanhSachPhim.FirstOrDefault(x => x.Ma == suatTrung.MaPhim);
                var ketThucTrung = suatTrung.GioBatDau.AddMinutes(
                    (phimTrung?.ThoiLuong ?? 0) + CauHinh.PhutDonPhong);

                oGio.Focus();
                throw new Exception(
                    $"Phòng {phongChon.Ten} đã có suất chiếu trùng giờ.\n\n" +
                    $"Suất đang chiếm phòng: {phimTrung?.Ten ?? suatTrung.TenPhim}\n" +
                    $"Thời gian: {suatTrung.GioBatDau:HH:mm dd/MM/yyyy} - {ketThucTrung:HH:mm} " +
                    $"(đã gồm {CauHinh.PhutDonPhong} phút dọn phòng).\n\n" +
                    "Vui lòng chọn giờ khác hoặc phòng khác.");
            }

            DuLieu.DanhSachSuat.Add(suatMoi);
            DuLieu.LuuSuat();
            NapCay();
            ChonSuat(suatMoi.Ma);
        }
        catch (Exception ex)
        {
            GiaoDien.BaoLoi(ex);
        }
    }

    void HuySuat()
    {
        try
        {
            if (cayLich.SelectedNode?.Tag is not SuatChieu suat)
            {
                MessageBox.Show(
                    "Vui lòng chọn đúng dòng suất chiếu (giờ - phòng) trong cây lịch.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                cayLich.Focus();
                return;
            }

            var soVeDaBan = DuLieu.DanhSachVe.Count(t => t.MaSuat == suat.Ma);
            if (soVeDaBan > 0)
            {
                MessageBox.Show(
                    $"Suất chiếu này đã bán {soVeDaBan} vé nên không thể hủy.\n" +
                    "Chỉ được hủy suất chiếu chưa bán vé.",
                    "Không thể hủy",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var xacNhan = MessageBox.Show(
                $"Bạn có chắc muốn hủy suất chiếu?\n\n" +
                $"Phim: {suat.TenPhim}\n" +
                $"Phòng: {suat.TenPhong}\n" +
                $"Thời gian: {suat.GioBatDau:HH:mm dd/MM/yyyy}",
                "Xác nhận hủy suất chiếu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (xacNhan != DialogResult.Yes)
                return;

            if (!DuLieu.DanhSachSuat.Remove(suat))
                throw new Exception("Không tìm thấy suất chiếu cần hủy. Dữ liệu có thể đã thay đổi.");

            DuLieu.LuuSuat();
            NapCay();
            thongTinChon.Text = "Đã hủy suất chiếu. Chọn suất khác để xem chi tiết.";
            nutHuy.Enabled = false;
            cayLich.Focus();
        }
        catch (Exception ex)
        {
            GiaoDien.BaoLoi(ex);
        }
    }

    static string MaSuatMoi()
    {
        return "SC" + DateTime.Now.ToString("yyyyMMddHHmmssfff") +
               Guid.NewGuid().ToString("N")[..3].ToUpperInvariant();
    }

    void NapCay()
    {
        var maDaChon = (cayLich.SelectedNode?.Tag as SuatChieu)?.Ma;

        cayLich.BeginUpdate();
        try
        {
            cayLich.Nodes.Clear();

            var dsSuat = DuLieu.DanhSachSuat
                .OrderBy(x => x.GioBatDau)
                .ToList();

            if (dsSuat.Count == 0)
            {
                var nutTrong = cayLich.Nodes.Add("Chưa có suất chiếu nào");
                nutTrong.ForeColor = Color.Gray;
                nutTrong.Tag = null;
            }
            else
            {
                foreach (var nhomNgay in dsSuat.GroupBy(x => x.GioBatDau.Date))
                {
                    var nutNgay = cayLich.Nodes.Add(
                        $"{nhomNgay.Key.ToString("dddd, dd/MM/yyyy", Vietnamese)}  ({nhomNgay.Count()} suất)");
                    nutNgay.ForeColor = nhomNgay.Key < DateTime.Today
                        ? Color.Gray
                        : GiaoDien.MauNhan;

                    foreach (var nhomPhim in nhomNgay.GroupBy(x => x.MaPhim))
                    {
                        var phimTimThay = DuLieu.DanhSachPhim.FirstOrDefault(x => x.Ma == nhomPhim.Key);
                        var tenPhim = phimTimThay?.Ten ?? nhomPhim.First().TenPhim;
                        var thoiLuong = phimTimThay?.ThoiLuong ?? 0;

                        var nutPhim = nutNgay.Nodes.Add(
                            $"{tenPhim}" +
                            (thoiLuong > 0 ? $"  ({thoiLuong} phút)" : "") +
                            $"  •  {nhomPhim.Count()} suất");

                        nutPhim.ForeColor = nhomNgay.Key < DateTime.Today
                            ? Color.Gray
                            : GiaoDien.ChuChinh;

                        foreach (var suat in nhomPhim.OrderBy(x => x.GioBatDau))
                        {
                            var phongChon = DuLieu.DanhSachPhong.FirstOrDefault(x => x.Ma == suat.MaPhong);
                            var sucChua = phongChon?.SucChua ?? 0;
                            var soVeBan = DuLieu.DanhSachVe.Count(t => t.MaSuat == suat.Ma);
                            var ketThuc = thoiLuong > 0
                                ? suat.GioBatDau.AddMinutes(thoiLuong)
                                : suat.GioBatDau;
                            var gioTrong = ketThuc.AddMinutes(CauHinh.PhutDonPhong);

                            var nut = nutPhim.Nodes.Add(
                                $"{suat.GioBatDau:HH:mm} - {gioTrong:HH:mm}  |  " +
                                $"{phongChon?.Ten ?? suat.TenPhong}  |  " +
                                $"{soVeBan}/{sucChua} vé");

                            nut.Tag = suat;
                            nut.ToolTipText =
                                $"Phim: {tenPhim}\n" +
                                $"Phòng: {phongChon?.Ten ?? suat.TenPhong}\n" +
                                $"Bắt đầu: {suat.GioBatDau:HH:mm dd/MM/yyyy}\n" +
                                $"Kết thúc: {ketThuc:HH:mm} (dọn phòng đến {gioTrong:HH:mm})\n" +
                                $"Đã bán: {soVeBan}/{sucChua} vé";

                            if (gioTrong <= DateTime.Now)
                                nut.ForeColor = Color.Gray;
                            else if (suat.GioBatDau <= DateTime.Now)
                                nut.ForeColor = Color.DarkOrange;
                            else if (sucChua > 0 && soVeBan >= sucChua)
                                nut.ForeColor = Color.Firebrick;
                            else
                                nut.ForeColor = Color.FromArgb(30, 120, 70);
                        }
                    }
                }
            }

            cayLich.ExpandAll();
        }
        finally
        {
            cayLich.EndUpdate();
        }

        if (maDaChon != null)
            ChonSuat(maDaChon);
        else
            HienThiDangChon();
    }

    void ChonSuat(string maSuat)
    {
        var nut = TimNut(cayLich.Nodes, maSuat);
        if (nut == null)
        {
            HienThiDangChon();
            return;
        }

        cayLich.SelectedNode = nut;
        nut.EnsureVisible();
    }

    static TreeNode? TimNut(TreeNodeCollection nodes, string maSuat)
    {
        foreach (TreeNode nut in nodes)
        {
            if (nut.Tag is SuatChieu suat && suat.Ma == maSuat)
                return nut;

            var child = TimNut(nut.Nodes, maSuat);
            if (child != null)
                return child;
        }

        return null;
    }

    void HienThiDangChon()
    {
        if (cayLich.SelectedNode?.Tag is not SuatChieu suat)
        {
            thongTinChon.Text = "Chưa chọn suất chiếu nào.\nHãy chọn một dòng suất chiếu cụ thể trong cây lịch.";
            nutHuy.Enabled = false;
            return;
        }

        nutHuy.Enabled = true;

        var phimTimThay = DuLieu.DanhSachPhim.FirstOrDefault(x => x.Ma == suat.MaPhim);
        var phongTimThay = DuLieu.DanhSachPhong.FirstOrDefault(x => x.Ma == suat.MaPhong);
        var soVeBan = DuLieu.DanhSachVe.Count(t => t.MaSuat == suat.Ma);
        var sucChua = phongTimThay?.SucChua ?? 0;
        var ketThuc = suat.GioBatDau.AddMinutes(phimTimThay?.ThoiLuong ?? 0);
        var gioTrong = ketThuc.AddMinutes(CauHinh.PhutDonPhong);

        thongTinChon.Text =
            $"Phim: {phimTimThay?.Ten ?? suat.TenPhim}\r\n" +
            $"Phòng: {phongTimThay?.Ten ?? suat.TenPhong}\r\n" +
            $"Bắt đầu: {suat.GioBatDau:HH:mm dd/MM/yyyy}\r\n" +
            $"Kết thúc phim: {ketThuc:HH:mm}\r\n" +
            $"Phòng trống lại: {gioTrong:HH:mm} (+{CauHinh.PhutDonPhong} phút dọn phòng)\r\n" +
            $"Vé đã bán: {soVeBan}/{sucChua}";

        thongTinChon.ForeColor = soVeBan >= sucChua && sucChua > 0
            ? Color.Firebrick
            : GiaoDien.ChuChinh;
    }
}
