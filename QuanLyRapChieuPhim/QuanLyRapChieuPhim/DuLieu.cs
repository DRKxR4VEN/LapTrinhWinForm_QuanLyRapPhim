using Microsoft.Win32;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace QuanLyRapChieuPhim;

public static class CauHinh
{
    static readonly string DuongDanCauHinh = Path.Combine(AppContext.BaseDirectory, "App.config");
    static string Lay(string khoa, string macDinh)
    {
        try { return XDocument.Load(DuongDanCauHinh).Descendants("add").FirstOrDefault(x => (string?)x.Attribute("key") == khoa)?.Attribute("value")?.Value ?? macDinh; }
        catch { return macDinh; }
    }
    public static decimal GiaVeGoc => decimal.TryParse(Lay("BaseTicketPrice","80000"), NumberStyles.Any, CultureInfo.InvariantCulture, out var n) ? n : 80000m;
    public static decimal GiamGiaHocSinh => decimal.TryParse(Lay("StudentDiscount","0.15"), NumberStyles.Any, CultureInfo.InvariantCulture, out var n) ? n : .15m;
    public static decimal GiamGiaNguoiLon => decimal.TryParse(Lay("AdultDiscount","0"), NumberStyles.Any, CultureInfo.InvariantCulture, out var n) ? n : 0m;
    public static int PhutDonPhong => int.TryParse(Lay("CleaningMinutes","15"), out var n) ? n : 15;

    public static string TenRap => Lay("CinemaName", "RẠP CHIẾU PHIM SAO MAI");
    public static string ThuMucDuLieu => Lay("DataFolder", "Data");
    public static decimal PhuThuVip => decimal.TryParse(Lay("VipSurcharge","1.5"), NumberStyles.Any, CultureInfo.InvariantCulture, out var n) && n >= 1m ? n : 1.5m;
    public static int GioTruocKhiTra => int.TryParse(Lay("RefundBeforeHours","1"), out var n) && n >= 0 ? n : 1;

    static IReadOnlyList<ComboBapNuoc>? danhSachCombo;

    /// <summary>Danh sách combo bắp nước, mỗi mục có dạng "Tên:Giá" và ngăn cách bằng dấu phẩy.</summary>
    public static IReadOnlyList<ComboBapNuoc> DanhSachCombo
    {
        get
        {
            if (danhSachCombo != null)
                return danhSachCombo;

            var ketQua = new List<ComboBapNuoc>();
            foreach (var phan in Lay("SnackCombos", "").Split(',', StringSplitOptions.RemoveEmptyEntries))
            {
                var viTri = phan.LastIndexOf(':');
                if (viTri <= 0)
                    continue;

                var ten = phan[..viTri].Trim();
                if (ten.Length == 0)
                    continue;

                if (decimal.TryParse(phan[(viTri + 1)..].Trim(), NumberStyles.Any, CultureInfo.InvariantCulture, out var gia) && gia >= 0)
                    ketQua.Add(new ComboBapNuoc { Ten = ten, Gia = gia });
            }

            return danhSachCombo = ketQua;
        }
    }
}

public static class TepTin
{
    public static string ThuMuc
    {
        get
        {
            var thuMuc = CauHinh.ThuMucDuLieu;
            return Path.IsPathRooted(thuMuc)
                ? thuMuc
                : Path.Combine(AppContext.BaseDirectory, thuMuc);
        }
    }

    public static void TaoThuMuc() => Directory.CreateDirectory(ThuMuc);

    /// <summary>
    /// Đọc từng dòng bằng StreamReader. Dòng sai định dạng chỉ bị bỏ qua và ghi log,
    /// không làm hỏng toàn bộ dữ liệu của tập tin.
    /// </summary>
    public static List<T> Doc<T>(string tenTep, Func<string, T> chuyenDoi)
    {
        var ketQua = new List<T>();
        var duongDan = Path.Combine(ThuMuc, tenTep);

        try
        {
            TaoThuMuc();

            // Tự tạo tập tin rỗng nếu chưa có để lần chạy sau có thể ghi trực tiếp.
            if (!File.Exists(duongDan))
            {
                using (new StreamWriter(duongDan, false, new UTF8Encoding(false))) { }
                return ketQua;
            }

            using var docTep = new StreamReader(duongDan, Encoding.UTF8, true);
            string? dong;
            var soDong = 0;

            while ((dong = docTep.ReadLine()) != null)
            {
                soDong++;
                if (string.IsNullOrWhiteSpace(dong)) continue;

                try
                {
                    ketQua.Add(chuyenDoi(dong));
                }
                catch (Exception ex)
                {
                    GhiLoi(new Exception(
                        $"Bỏ qua dòng {soDong} trong {tenTep} vì sai định dạng: {dong}",
                        ex));
                }
            }
        }
        catch (Exception ex)
        {
            GhiLoi(ex);
            MessageBox.Show(
                $"Không đọc được {tenTep}: {ex.Message}",
                "Lỗi đọc dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        return ketQua;
    }

    public static void Ghi<T>(string tenTep, IEnumerable<T> cacDong)
    {
        try
        {
            TaoThuMuc();

            using var ghiTep = new StreamWriter(
                Path.Combine(ThuMuc, tenTep),
                false,
                new UTF8Encoding(false));

            foreach (var dong in cacDong)
                ghiTep.WriteLine(dong?.ToString() ?? "");
        }
        catch (Exception ex)
        {
            GhiLoi(ex);
            MessageBox.Show(
                $"Không ghi được {tenTep}: {ex.Message}",
                "Lỗi ghi dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    public static void GhiLoi(Exception ex) => GhiLoi(ex.ToString());

    /// <summary>Ghi một thông điệp (không phải ngoại lệ) vào Data/log.txt.</summary>
    public static void GhiLoi(string thongDiep)
    {
        try
        {
            TaoThuMuc();

            using var ghiTep = new StreamWriter(
                Path.Combine(ThuMuc, "log.txt"),
                true,
                new UTF8Encoding(false));

            ghiTep.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {thongDiep}");
        }
        catch
        {
            // Không để lỗi ghi log làm dừng chương trình.
        }
    }
}

public static class DuLieu
{
    public static List<Phim> DanhSachPhim = new();
    public static List<PhongChieu> DanhSachPhong = new();
    public static List<SuatChieu> DanhSachSuat = new();
    public static List<Ve> DanhSachVe = new();
    public static List<PhieuTraVe> DanhSachTraVe = new();
    public static List<TaiKhoan> DanhSachTaiKhoan = new();
    public static void KhoiTao()
    {
        TepTin.TaoThuMuc();
        DanhSachPhim=TepTin.Doc("movies.txt", Phim.TuDong);
        DanhSachPhong=TepTin.Doc("rooms.txt", PhongChieu.TuDong);
        DanhSachSuat=TepTin.Doc("screenings.txt", SuatChieu.TuDong);
        DanhSachVe=TepTin.Doc("tickets.txt", Ve.TuDong);
        DanhSachTraVe=TepTin.Doc("trave.txt", PhieuTraVe.TuDong);
        DanhSachTaiKhoan=TepTin.Doc("users.txt", TaiKhoan.TuDong);
        if (DanhSachTaiKhoan.Count==0)
        {
            DanhSachTaiKhoan.Add(new TaiKhoan { TenDangNhap="admin", MatKhau="123", VaiTro="QuanTri" });
            DanhSachTaiKhoan.Add(new TaiKhoan { TenDangNhap="nhanvien", MatKhau="123", VaiTro="NhanVien" });
            LuuTaiKhoan();
        }
    }
    public static void LuuPhim()=>TepTin.Ghi("movies.txt",DanhSachPhim);
    public static void LuuPhong()=>TepTin.Ghi("rooms.txt",DanhSachPhong);
    public static void LuuSuat()=>TepTin.Ghi("screenings.txt",DanhSachSuat);
    public static void LuuVe()=>TepTin.Ghi("tickets.txt",DanhSachVe);
    public static void LuuTraVe()=>TepTin.Ghi("trave.txt",DanhSachTraVe);
    public static void LuuTaiKhoan()=>TepTin.Ghi("users.txt",DanhSachTaiKhoan);

    /// <summary>Sinh mã vé mới, không trùng với vé đã có.</summary>
    public static string MaVeMoi()
    {
        string ma;
        do
        {
            ma = "V" + Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        }
        while (DanhSachVe.Any(t => t.Ma == ma) || DanhSachTraVe.Any(r => r.MaVe == ma));

        return ma;
    }
    /// <summary>
    /// Tìm suất chiếu bị trùng giờ trong cùng phòng.
    /// Khoảng chiếm phòng = thời lượng phim + số phút dọn phòng trong App.config.
    /// </summary>
    public static SuatChieu? TimTrungLich(SuatChieu suatMoi, Phim phim)
    {
        var batDauMoi = suatMoi.GioBatDau;
        var ketThucMoi = batDauMoi.AddMinutes(
            phim.ThoiLuong + CauHinh.PhutDonPhong);

        foreach (var suat in DanhSachSuat.Where(
            x => x.MaPhong == suatMoi.MaPhong && x.Ma != suatMoi.Ma))
        {
            var phimCu = DanhSachPhim.FirstOrDefault(x => x.Ma == suat.MaPhim);

            if (phimCu == null)
            {
                // Không tính được giờ kết thúc nếu dữ liệu phim bị thiếu.
                TepTin.GhiLoi(new Exception(
                    $"Không tìm thấy phim {suat.MaPhim} của suất {suat.Ma}; " +
                    "bỏ qua suất này khi kiểm tra trùng lịch."));
                continue;
            }

            var batDauCu = suat.GioBatDau;
            var ketThucCu = batDauCu.AddMinutes(
                phimCu.ThoiLuong + CauHinh.PhutDonPhong);

            // Hai khoảng [start, end) giao nhau.
            if (batDauMoi < ketThucCu && batDauCu < ketThucMoi)
                return suat;
        }

        return null;
    }

    public static bool CoTrungLich(SuatChieu suatMoi, Phim phim)
        => TimTrungLich(suatMoi, phim) != null;
    public static bool DaBanVe(string maSuat)=>DanhSachVe.Any(t=>t.MaSuat==maSuat);
    public static string KhoaGhe(string maSuat,string ghe)=>maSuat+"|"+ghe;

    const string DuongDanRegistry = @"Software\QuanLyRapChieuPhim";

    public static (Color Back, Color Fore, float FontSize) DocGiaoDien()
    {
        try
        {
            using var khoa=Registry.CurrentUser.OpenSubKey(DuongDanRegistry);
            var mauNen=Color.FromArgb(Convert.ToInt32(khoa?.GetValue("BackColor",GiaoDien.NenChinh.ToArgb()) ?? GiaoDien.NenChinh.ToArgb()));
            var mauChu=Color.FromArgb(Convert.ToInt32(khoa?.GetValue("ForeColor",GiaoDien.ChuChinh.ToArgb()) ?? GiaoDien.ChuChinh.ToArgb()));
            var coChu=Convert.ToSingle(khoa?.GetValue("FontSize","9") ?? "9");
            if (coChu < 8f || coChu > 20f)
                coChu = 9f;
            return (mauNen,mauChu,coChu);
        } catch { return (GiaoDien.NenChinh,GiaoDien.ChuChinh,9f); }
    }
    public static void LuuGiaoDien(Color mauNen, Color mauChu, float coChu)
    {
        try { using var khoa=Registry.CurrentUser.CreateSubKey(DuongDanRegistry); khoa.SetValue("BackColor",mauNen.ToArgb()); khoa.SetValue("ForeColor",mauChu.ToArgb()); khoa.SetValue("FontSize",coChu); }
        catch(Exception ex) { TepTin.GhiLoi(ex); MessageBox.Show("Không lưu được Registry: "+ex.Message); }
    }

    /// <summary>Ghi nhớ tài khoản đăng nhập gần nhất. Không lưu mật khẩu.</summary>
    public static (string NguoiDung, bool GhiNho) DocGhiNhoDangNhap()
    {
        try
        {
            using var khoa = Registry.CurrentUser.OpenSubKey(DuongDanRegistry);
            var ghiNho = Convert.ToInt32(khoa?.GetValue("RememberLogin", 0) ?? 0) == 1;
            var nguoiDung = khoa?.GetValue("LoginUser")?.ToString() ?? "";
            return (nguoiDung, ghiNho);
        }
        catch { return ("", false); }
    }

    public static void LuuGhiNhoDangNhap(string nguoiDung, bool ghiNho)
    {
        try
        {
            using var khoa = Registry.CurrentUser.CreateSubKey(DuongDanRegistry);
            khoa.SetValue("RememberLogin", ghiNho ? 1 : 0);
            khoa.SetValue("LoginUser", ghiNho ? nguoiDung : "");
        }
        catch (Exception ex)
        {
            TepTin.GhiLoi(ex);
        }
    }

    /// <summary>Khôi phục vị trí/kích thước cửa sổ chính đã lưu trong Registry.</summary>
    public static Rectangle? DocViTriCuaSo()
    {
        try
        {
            using var khoa = Registry.CurrentUser.OpenSubKey(DuongDanRegistry);
            if (khoa?.GetValue("WindowWidth") is null || khoa.GetValue("WindowHeight") is null)
                return null;

            var viTri = new Rectangle(
                Convert.ToInt32(khoa.GetValue("WindowX", 0)),
                Convert.ToInt32(khoa.GetValue("WindowY", 0)),
                Convert.ToInt32(khoa.GetValue("WindowWidth")),
                Convert.ToInt32(khoa.GetValue("WindowHeight")));

            if (viTri.Width < 600 || viTri.Height < 400)
                return null;

            bool hienThi = Screen.AllScreens.Any(suat => suat.WorkingArea.IntersectsWith(viTri));
            return hienThi ? viTri : (Rectangle?)null;
        }
        catch { return null; }
    }

    public static void LuuViTriCuaSo(Form bieuMau)
    {
        try
        {
            var viTri = bieuMau.WindowState == FormWindowState.Normal ? bieuMau.Bounds : bieuMau.RestoreBounds;
            if (viTri.Width < 600 || viTri.Height < 400)
                return;

            using var khoa = Registry.CurrentUser.CreateSubKey(DuongDanRegistry);
            khoa.SetValue("WindowX", viTri.X);
            khoa.SetValue("WindowY", viTri.Y);
            khoa.SetValue("WindowWidth", viTri.Width);
            khoa.SetValue("WindowHeight", viTri.Height);
        }
        catch (Exception ex)
        {
            TepTin.GhiLoi(ex);
        }
    }
}
