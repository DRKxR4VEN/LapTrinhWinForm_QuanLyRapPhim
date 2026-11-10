using System.Text;

namespace QuanLyRapChieuPhim;

/// <summary>
/// Phân hệ SV3 - Bán vé và doanh thu.
/// - Chọn suất chiếu, sơ đồ ghế tạo bằng Button trong code với 4 trạng thái.
/// - Loại vé người lớn / học sinh - sinh viên, mức giảm đọc từ App.config.
/// - Combo bắp nước bán kèm (nâng cao).
/// - Timer tự ẩn suất đã bắt đầu chiếu (nâng cao).
/// - Trả vé trước giờ chiếu 1 tiếng (nâng cao).
/// - In vé ra tập tin TXT.
/// </summary>
public partial class FormBanVe : Form
{
    static readonly Color MauGheTrong = Color.LightGreen;
    static readonly Color MauGheVip = Color.Gold;
    static readonly Color MauGheDangChon = Color.DeepSkyBlue;
    static readonly Color MauGheDaBan = Color.IndianRed;

    readonly HashSet<string> gheDaChon = new();

    PhongChieu? phongHienTai;
    SuatChieu? suatHienTai;
    string chuoiSuatDaNap = "";
    bool dangGan;
    bool dangBan;

    public FormBanVe()
    {
        InitializeComponent();
        GiaoDien.ApDungGiaoDien(this);

        if (GiaoDien.DangThietKe)
            return;

        var dsCombo = new List<ComboBapNuoc>
        {
            new() { Ten = "Không dùng bắp nước", Gia = 0 }
        };
        dsCombo.AddRange(CauHinh.DanhSachCombo);
        oBapNuoc.DisplayMember = nameof(ComboBapNuoc.HienThi);
        oBapNuoc.DataSource = dsCombo;

        henGio.Start();
        NapSuat();
    }

    void oSuat_SelectedIndexChanged(object? sender, EventArgs e)
    {
        if (!dangGan)
            VeSoDoGhe();
    }

    void oNguoiLon_CheckedChanged(object? sender, EventArgs e) => CapNhatTamTinh();

    void oHocSinh_CheckedChanged(object? sender, EventArgs e) => CapNhatTamTinh();

    void oBapNuoc_SelectedIndexChanged(object? sender, EventArgs e) => CapNhatTamTinh();

    void danhSachDaBan_SelectedIndexChanged(object? sender, EventArgs e)
    {
        nutTraVe.Enabled = danhSachDaBan.SelectedItems.Count > 0
            && danhSachDaBan.SelectedItems[0].Tag is Ve;
    }

    void danhSachDaBan_DoubleClick(object? sender, EventArgs e) => InVeDangChon();

    void nutBan_Click(object? sender, EventArgs e) => BanVe();

    void nutTraVe_Click(object? sender, EventArgs e) => TraVe();

    void nutInVe_Click(object? sender, EventArgs e) => InVeDangChon();

    void henGio_Tick(object? sender, EventArgs e)
    {
        // Không vẽ lại sơ đồ ghế khi hộp thoại xác nhận bán vé đang mở.
        if (!dangBan)
            NapSuat();
    }

    void FormBanVe_FormClosed(object? sender, FormClosedEventArgs e) => henGio.Stop();

    /// <summary>
    /// Nạp danh sách suất chiếu chưa bắt đầu. Timer gọi lại hàm này mỗi 20 giây
    /// nên suất vừa tới giờ chiếu sẽ tự biến mất khỏi màn hình bán vé.
    /// </summary>
    void NapSuat()
    {
        var dsSuat = DuLieu.DanhSachSuat
            .Where(s => s.GioBatDau > DateTime.Now)
            .OrderBy(s => s.GioBatDau)
            .ToList();

        var chuoiMa = string.Join("|", dsSuat.Select(x => x.Ma));
        if (chuoiMa == chuoiSuatDaNap)
        {
            CapNhatDemNguoc();
            return;
        }

        var maTruoc = (oSuat.SelectedItem as SuatChieu)?.Ma;
        var matLuaChon = maTruoc != null && dsSuat.All(x => x.Ma != maTruoc);
        chuoiSuatDaNap = chuoiMa;

        dangGan = true;
        oSuat.DataSource = null;
        oSuat.DisplayMember = nameof(SuatChieu.HienThi);
        oSuat.DataSource = dsSuat;

        if (maTruoc != null)
        {
            var chiSo = dsSuat.FindIndex(x => x.Ma == maTruoc);
            if (chiSo >= 0)
                oSuat.SelectedIndex = chiSo;
        }

        if (oSuat.SelectedIndex < 0 && dsSuat.Count > 0)
            oSuat.SelectedIndex = 0;
        dangGan = false;

        VeSoDoGhe();

        if (matLuaChon)
            trangThai.Text = "Suất chiếu vừa bắt đầu đã được ẩn khỏi danh sách bán vé.";
    }

    void VeSoDoGhe()
    {
        while (luongGhe.Controls.Count > 0)
        {
            var dieuKhien = luongGhe.Controls[0];
            luongGhe.Controls.RemoveAt(0);
            dieuKhien.Dispose();
        }

        gheDaChon.Clear();
        danhSachDaBan.Items.Clear();
        nutTraVe.Enabled = false;
        tamTinh.Text = "";

        suatHienTai = oSuat.SelectedItem as SuatChieu;
        phongHienTai = suatHienTai == null
            ? null
            : DuLieu.DanhSachPhong.FirstOrDefault(r => r.Ma == suatHienTai.MaPhong);

        if (suatHienTai == null || phongHienTai == null)
        {
            trangThai.Text = "Chưa có suất chiếu hoặc phòng chiếu để bán vé.";
            chuThich.Text = "";
            CapNhatDemNguoc();
            return;
        }

        trangThai.Text =
            $"{suatHienTai.TenPhim}  •  {phongHienTai.Ten}  •  " +
            $"{suatHienTai.GioBatDau:HH:mm dd/MM/yyyy}";

        var gheDaBan = DuLieu.DanhSachVe
            .Where(t => t.MaSuat == suatHienTai.Ma)
            .Select(t => t.Ghe)
            .ToHashSet();

        luongGhe.Controls.Add(new Label
        {
            Text = "MÀN HÌNH",
            AutoSize = false,
            Width = Math.Max(420, phongHienTai.SoGheMoiHang * 60 + 42),
            Height = 26,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.FromArgb(220, 230, 245),
            ForeColor = GiaoDien.ChuChinh,
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            Margin = new Padding(0, 0, 0, 10)
        });

        for (int r = 1; r <= phongHienTai.SoHang; r++)
        {
            var hangGhe = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false,
                BackColor = Color.White,
                Margin = new Padding(0, 2, 0, 2)
            };

            var chuHang = (char)('A' + r - 1);
            bool hangVipGhe = r <= phongHienTai.HangVip;

            hangGhe.Controls.Add(new Label
            {
                Text = $"{chuHang}{(hangVipGhe ? " ★" : "")}",
                AutoSize = false,
                Width = 42,
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = hangVipGhe ? Color.DarkGoldenrod : GiaoDien.ChuChinh,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            });

            for (int c = 1; c <= phongHienTai.SoGheMoiHang; c++)
            {
                var tenGhe = $"{chuHang}{c}";
                bool daBan = gheDaBan.Contains(tenGhe);

                var nutGhe = new Button
                {
                    Text = tenGhe,
                    Width = 54,
                    Height = 40,
                    Margin = new Padding(3),
                    Tag = tenGhe,
                    Enabled = !daBan,
                    ForeColor = Color.Black,
                    BackColor = daBan ? MauGheDaBan : hangVipGhe ? MauGheVip : MauGheTrong
                };

                nutGhe.Click += (_, _) => BatTatGhe(tenGhe, nutGhe);
                hangGhe.Controls.Add(nutGhe);
            }

            luongGhe.Controls.Add(hangGhe);
        }

        chuThich.Text =
            "Xanh lá: ghế trống  •  Vàng: ghế VIP  •  " +
            "Xanh dương: đang chọn  •  Đỏ: đã bán";

        NapVeDaBan();
        CapNhatDemNguoc();
        CapNhatTamTinh();
    }

    void BatTatGhe(string ghe, Button nutGhe)
    {
        if (!gheDaChon.Add(ghe))
        {
            gheDaChon.Remove(ghe);
            nutGhe.BackColor = LaGheVip(ghe) ? MauGheVip : MauGheTrong;
        }
        else
        {
            nutGhe.BackColor = MauGheDangChon;
        }

        if (gheDaChon.Count == 0)
            trangThai.Text = $"Chưa chọn ghế nào  •  {suatHienTai?.TenPhim}";
        else
            trangThai.Text = $"Đang chọn {gheDaChon.Count} ghế: {ChuoiGhe()}";

        CapNhatTamTinh();
    }

    string ChuoiGhe() => string.Join(", ", gheDaChon
        .OrderBy(s => s[0])
        .ThenBy(s => int.Parse(s[1..])));

    bool LaGheVip(string ghe)
    {
        if (phongHienTai == null || ghe.Length == 0)
            return false;

        return ghe[0] - 'A' + 1 <= phongHienTai.HangVip;
    }

    ComboBapNuoc? ComboDangChon => oBapNuoc.SelectedItem as ComboBapNuoc;

    decimal TinhGiaVe(string ghe)
    {
        if (phongHienTai == null)
            return 0;

        decimal mucGiam = oHocSinh.Checked
            ? CauHinh.GiamGiaHocSinh
            : CauHinh.GiamGiaNguoiLon;

        decimal gia = CauHinh.GiaVeGoc
            * (LaGheVip(ghe) ? CauHinh.PhuThuVip : 1m)
            * (1 - mucGiam);

        return decimal.Round(gia, 0) + (ComboDangChon?.Gia ?? 0);
    }

    void CapNhatTamTinh()
    {
        if (phongHienTai == null || gheDaChon.Count == 0)
        {
            tamTinh.Text = "Chưa chọn ghế nào.";
            return;
        }

        decimal tong = gheDaChon.Sum(TinhGiaVe);
        tamTinh.Text =
            $"{gheDaChon.Count} ghế: {ChuoiGhe()}  •  Tạm tính {GiaoDien.Tien(tong)}";
    }

    void NapVeDaBan()
    {
        danhSachDaBan.Items.Clear();
        nutTraVe.Enabled = false;

        if (suatHienTai == null)
            return;

        foreach (var ve in DuLieu.DanhSachVe
                     .Where(x => x.MaSuat == suatHienTai.Ma)
                     .OrderBy(x => x.Ghe))
        {
            var dongVe = new ListViewItem(new[]
            {
                ve.Ghe,
                ve.LoaiVe,
                GiaoDien.Tien(ve.TongTien),
                ve.NgayBan.ToString("HH:mm dd/MM")
            })
            {
                Tag = ve,
                ToolTipText =
                    $"Mã vé: {ve.Ma}\n" +
                    $"Giá vé: {GiaoDien.Tien(ve.Gia)}\n" +
                    $"Bắp nước: {(string.IsNullOrWhiteSpace(ve.TenCombo) ? "Không" : ve.TenCombo)}\n" +
                    $"Phụ thu combo: {GiaoDien.Tien(ve.GiaCombo)}"
            };

            danhSachDaBan.Items.Add(dongVe);
        }
    }

    void CapNhatDemNguoc()
    {
        if (suatHienTai == null)
        {
            thongTinTraVe.Text =
                $"Quy định: chỉ nhận trả vé trước giờ chiếu " +
                $"{CauHinh.GioTruocKhiTra} tiếng.";
            return;
        }

        var thoiGianCon = suatHienTai.GioBatDau - DateTime.Now;
        string conLai = thoiGianCon <= TimeSpan.Zero
            ? "Suất chiếu đã bắt đầu."
            : thoiGianCon.TotalHours >= 1
                ? $"Còn {(int)thoiGianCon.TotalHours} giờ {thoiGianCon.Minutes} phút tới giờ chiếu."
                : $"Còn {Math.Max(0, thoiGianCon.Minutes)} phút tới giờ chiếu.";

        thongTinTraVe.Text =
            $"Quy định: chỉ nhận trả vé trước giờ chiếu {CauHinh.GioTruocKhiTra} tiếng.\n" +
            conLai;
    }

    void BanVe()
    {
        try
        {
            if (suatHienTai == null || phongHienTai == null)
            {
                oSuat.Focus();
                throw new Exception("Vui lòng chọn suất chiếu.");
            }

            if (suatHienTai.GioBatDau <= DateTime.Now)
            {
                NapSuat();
                throw new Exception("Suất chiếu đã bắt đầu nên không thể bán vé.");
            }

            if (gheDaChon.Count == 0)
                throw new Exception("Vui lòng chọn ít nhất một ghế trên sơ đồ.");

            var phim = DuLieu.DanhSachPhim.FirstOrDefault(m => m.Ma == suatHienTai.MaPhim)
                ?? throw new Exception("Không tìm thấy phim của suất chiếu.");

            var gheTrung = gheDaChon.FirstOrDefault(ghe => DuLieu.DanhSachVe.Any(
                t => t.MaSuat == suatHienTai.Ma && t.Ghe == ghe));

            if (gheTrung != null)
            {
                VeSoDoGhe();
                throw new Exception($"Ghế {gheTrung} vừa được bán, vui lòng chọn ghế khác.");
            }

            string loaiKhach = oHocSinh.Checked ? "Học sinh-SV" : "Người lớn";
            decimal mucGiam = oHocSinh.Checked
                ? CauHinh.GiamGiaHocSinh
                : CauHinh.GiamGiaNguoiLon;
            var comboBapNuoc = ComboDangChon;

            var dsVeMoi = new List<Ve>();

            foreach (var ghe in gheDaChon)
            {
                bool laVip = LaGheVip(ghe);
                decimal gia = decimal.Round(
                    CauHinh.GiaVeGoc
                    * (laVip ? CauHinh.PhuThuVip : 1m)
                    * (1 - mucGiam),
                    0);

                dsVeMoi.Add(new Ve
                {
                    Ma = DuLieu.MaVeMoi(),
                    MaSuat = suatHienTai.Ma,
                    TenPhim = phim.Ten,
                    TenPhong = phongHienTai.Ten,
                    GioChieu = suatHienTai.GioBatDau,
                    Ghe = ghe,
                    LaVip = laVip,
                    LoaiVe = loaiKhach,
                    Gia = gia,
                    NgayBan = DateTime.Now,
                    TenCombo = comboBapNuoc?.Gia > 0 ? comboBapNuoc.Ten : "",
                    GiaCombo = comboBapNuoc?.Gia ?? 0m
                });
            }

            decimal tong = dsVeMoi.Sum(t => t.TongTien);

            dangBan = true;
            var xacNhan = MessageBox.Show(
                $"Xác nhận bán {dsVeMoi.Count} vé?\n\n" +
                $"Phim: {phim.Ten}\n" +
                $"Suất: {suatHienTai.GioBatDau:HH:mm dd/MM/yyyy} - {phongHienTai.Ten}\n" +
                $"Ghế: {ChuoiGhe()}\n" +
                $"Loại vé: {loaiKhach}\n" +
                $"Bắp nước: {(comboBapNuoc?.Gia > 0 ? comboBapNuoc.Ten : "Không")}\n" +
                $"Tổng tiền: {GiaoDien.Tien(tong)}",
                "Xác nhận bán vé",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);
            dangBan = false;

            if (xacNhan != DialogResult.Yes)
                return;

            DuLieu.DanhSachVe.AddRange(dsVeMoi);
            DuLieu.LuuVe();

            int soLoi = 0;
            foreach (var ve in dsVeMoi)
            {
                if (!LuuTepVe(ve, Path.Combine(TepTin.ThuMuc, $"Ve_{ve.Ma}.txt")))
                    soLoi++;
            }

            MessageBox.Show(
                $"Bán thành công {dsVeMoi.Count} vé, tổng {GiaoDien.Tien(tong)}.\n" +
                (soLoi == 0
                    ? "Vé TXT đã được lưu trong thư mục Data."
                    : $"{soLoi} vé không ghi được tập tin, xem Data/log.txt."),
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            VeSoDoGhe();
        }
        catch (Exception ex)
        {
            dangBan = false;
            GiaoDien.BaoLoi(ex);
        }
    }

    void TraVe()
    {
        try
        {
            if (danhSachDaBan.SelectedItems.Count == 0
                || danhSachDaBan.SelectedItems[0].Tag is not Ve ve)
            {
                danhSachDaBan.Focus();
                throw new Exception("Vui lòng chọn vé cần trả trong danh sách vé đã bán.");
            }

            var thoiGianCon = ve.GioChieu - DateTime.Now;
            var mocChoPhep = TimeSpan.FromHours(CauHinh.GioTruocKhiTra);

            if (thoiGianCon < mocChoPhep)
            {
                string chiTiet = thoiGianCon <= TimeSpan.Zero
                    ? "Suất chiếu đã bắt đầu."
                    : $"Suất này chỉ còn {Math.Max(0, (int)thoiGianCon.TotalMinutes)} phút nữa là chiếu.";

                throw new Exception(
                    $"Chỉ nhận trả vé trước giờ chiếu ít nhất " +
                    $"{CauHinh.GioTruocKhiTra} tiếng.\n{chiTiet}");
            }

            var xacNhan = MessageBox.Show(
                "Xác nhận trả vé?\n\n" +
                $"Mã vé: {ve.Ma}\n" +
                $"Phim: {ve.TenPhim}\n" +
                $"Ghế: {ve.Ghe}\n" +
                $"Loại vé: {ve.LoaiVe}\n" +
                $"Bắp nước: {(string.IsNullOrWhiteSpace(ve.TenCombo) ? "Không" : ve.TenCombo)}\n" +
                $"Số tiền hoàn: {GiaoDien.Tien(ve.TongTien)}",
                "Xác nhận trả vé",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (xacNhan != DialogResult.Yes)
                return;

            if (!DuLieu.DanhSachVe.Remove(ve))
                throw new Exception("Không tìm thấy vé cần trả, dữ liệu có thể đã thay đổi.");

            var phieuTra = new PhieuTraVe
            {
                MaVe = ve.Ma,
                MaSuat = ve.MaSuat,
                TenPhim = ve.TenPhim,
                TenPhong = ve.TenPhong,
                Ghe = ve.Ghe,
                LoaiVe = ve.LoaiVe,
                TenCombo = ve.TenCombo,
                SoTienHoan = ve.TongTien,
                GioChieu = ve.GioChieu,
                NgayTra = DateTime.Now
            };

            DuLieu.DanhSachTraVe.Add(phieuTra);
            DuLieu.LuuVe();
            DuLieu.LuuTraVe();
            TepTin.GhiLoi(
                $"Trả vé {ve.Ma} - suất {ve.MaSuat} - ghế {ve.Ghe} - " +
                $"hoàn {ve.TongTien:0} đ - lý do: khách trả trước giờ chiếu.");

            MessageBox.Show(
                $"Đã trả vé {ve.Ma}.\nHoàn tiền: {GiaoDien.Tien(ve.TongTien)}",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            VeSoDoGhe();
        }
        catch (Exception ex)
        {
            GiaoDien.BaoLoi(ex);
        }
    }

    void InVeDangChon()
    {
        try
        {
            if (danhSachDaBan.SelectedItems.Count == 0
                || danhSachDaBan.SelectedItems[0].Tag is not Ve ve)
            {
                MessageBox.Show(
                    "Vui lòng chọn vé cần in trong danh sách vé đã bán.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using var hopThoai = new SaveFileDialog
            {
                Title = "Lưu vé xem phim",
                Filter = "Tập tin văn bản|*.txt",
                FileName = $"Ve_{ve.Ma}.txt",
                InitialDirectory = TepTin.ThuMuc
            };

            if (hopThoai.ShowDialog() != DialogResult.OK)
                return;

            if (LuuTepVe(ve, hopThoai.FileName))
            {
                MessageBox.Show(
                    "Đã in vé ra tập tin:\n" + hopThoai.FileName,
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show(
                    "Không ghi được tập tin vé, xem Data/log.txt.",
                    "Lỗi",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        catch (Exception ex)
        {
            GiaoDien.BaoLoi(ex);
        }
    }

    static bool LuuTepVe(Ve ve, string duongDan)
    {
        try
        {
            TepTin.TaoThuMuc();
            File.WriteAllText(duongDan, TaoNoiDungVe(ve), new UTF8Encoding(true));
            return true;
        }
        catch (Exception ex)
        {
            TepTin.GhiLoi(ex);
            return false;
        }
    }

    static string TaoNoiDungVe(Ve ve) =>
        $"{CauHinh.TenRap}\r\n" +
        "====================================\r\n" +
        "           VÉ XEM PHIM\r\n" +
        "====================================\r\n" +
        $"Mã vé     : {ve.Ma}\r\n" +
        $"Phim      : {ve.TenPhim}\r\n" +
        $"Phòng     : {ve.TenPhong}\r\n" +
        $"Suất chiếu: {ve.GioChieu:HH:mm dd/MM/yyyy}\r\n" +
        $"Ghế       : {ve.Ghe}{(ve.LaVip ? " (VIP)" : "")}\r\n" +
        $"Loại vé   : {ve.LoaiVe}\r\n" +
        $"Bắp nước  : {(string.IsNullOrWhiteSpace(ve.TenCombo) ? "Không" : ve.TenCombo)}\r\n" +
        "------------------------------------\r\n" +
        $"Giá vé    : {GiaoDien.Tien(ve.Gia)}\r\n" +
        $"Phụ thu   : {GiaoDien.Tien(ve.GiaCombo)}\r\n" +
        $"Tổng cộng : {GiaoDien.Tien(ve.TongTien)}\r\n" +
        $"Ngày bán  : {ve.NgayBan:HH:mm dd/MM/yyyy}\r\n" +
        "====================================\r\n" +
        "Vui lòng giữ vé để soát khi vào phòng.\r\n";
}
