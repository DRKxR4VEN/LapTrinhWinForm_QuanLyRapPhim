using System.Globalization;

namespace QuanLyRapChieuPhim;

public class Phim
{
    public string Ma { get; set; } = "";
    public string Ten { get; set; } = "";
    public string TheLoai { get; set; } = "";
    public int ThoiLuong { get; set; }
    public string DuongDanPoster { get; set; } = "";
    public override string ToString() => string.Join(";", Ma, Ten, TheLoai, ThoiLuong, DuongDanPoster);
    public static Phim TuDong(string s) { var p=s.Split(';'); return new Phim { Ma=p[0], Ten=p[1], TheLoai=p[2], ThoiLuong=int.Parse(p[3]), DuongDanPoster=p.Length>4?p[4]:"" }; }
}

public class PhongChieu
{
    public string Ma { get; set; } = "";
    public string Ten { get; set; } = "";
    public int SoHang { get; set; } = 5;
    public int SoGheMoiHang { get; set; } = 8;
    public int HangVip { get; set; } = 1;
    public int SucChua => SoHang * SoGheMoiHang;
    public override string ToString() => string.Join(";", Ma, Ten, SoHang, SoGheMoiHang, HangVip);
    public static PhongChieu TuDong(string s) { var p=s.Split(';'); return new PhongChieu { Ma=p[0], Ten=p[1], SoHang=int.Parse(p[2]), SoGheMoiHang=int.Parse(p[3]), HangVip=int.Parse(p[4]) }; }
}

public class SuatChieu
{
    public string Ma { get; set; } = "";
    public string MaPhim { get; set; } = "";
    public string MaPhong { get; set; } = "";
    public DateTime GioBatDau { get; set; }
    public string TenPhim { get; set; } = "";
    public string TenPhong { get; set; } = "";
    public DateTime GioKetThuc(int thoiLuong) => GioBatDau.AddMinutes(thoiLuong + CauHinh.PhutDonPhong);
    public string HienThi => $"{TenPhim} | {TenPhong} | {GioBatDau:HH:mm dd/MM/yyyy}";
    public override string ToString() => string.Join(";", Ma, MaPhim, MaPhong, GioBatDau.ToString("O"), TenPhim, TenPhong);
    public static SuatChieu TuDong(string s) { var p=s.Split(';'); return new SuatChieu { Ma=p[0], MaPhim=p[1], MaPhong=p[2], GioBatDau=DateTime.Parse(p[3], null, DateTimeStyles.RoundtripKind), TenPhim=p.Length>4?p[4]:"", TenPhong=p.Length>5?p[5]:"" }; }
}

public class Ve
{
    public string Ma { get; set; } = "";
    public string MaSuat { get; set; } = "";
    public string TenPhim { get; set; } = "";
    public string TenPhong { get; set; } = "";
    public DateTime GioChieu { get; set; }
    public string Ghe { get; set; } = "";
    public bool LaVip { get; set; }
    public string LoaiVe { get; set; } = "Người lớn";
    public decimal Gia { get; set; }
    public DateTime NgayBan { get; set; } = DateTime.Now;
    public string TenCombo { get; set; } = "";
    public decimal GiaCombo { get; set; }
    public decimal TongTien => Gia + GiaCombo;
    public override string ToString() => string.Join(";", Ma, MaSuat, TenPhim, TenPhong, GioChieu.ToString("O"), Ghe, LaVip, LoaiVe, Gia.ToString(CultureInfo.InvariantCulture), NgayBan.ToString("O"), TenCombo, GiaCombo.ToString(CultureInfo.InvariantCulture));
    public static Ve TuDong(string s)
    {
        var p = s.Split(';');
        if (p.Length < 10)
            throw new FormatException($"Vé cần tối thiểu 10 trường, dòng chỉ có {p.Length}.");

        return new Ve
        {
            Ma = p[0],
            MaSuat = p[1],
            TenPhim = p[2],
            TenPhong = p[3],
            GioChieu = DateTime.Parse(p[4], null, DateTimeStyles.RoundtripKind),
            Ghe = p[5],
            LaVip = bool.Parse(p[6]),
            LoaiVe = p[7],
            Gia = decimal.Parse(p[8], CultureInfo.InvariantCulture),
            NgayBan = DateTime.Parse(p[9], null, DateTimeStyles.RoundtripKind),
            // Hai trường combo được thêm sau, dữ liệu vé cũ chỉ có 10 trường.
            TenCombo = p.Length > 10 ? p[10] : "",
            GiaCombo = p.Length > 11 ? decimal.Parse(p[11], CultureInfo.InvariantCulture) : 0m
        };
    }
}

/// <summary>Một lựa chọn bắp nước bán kèm vé. Giá đọc từ App.config.</summary>
public class ComboBapNuoc
{
    public string Ten { get; set; } = "";
    public decimal Gia { get; set; }
    public string HienThi => Gia > 0
        ? $"{Ten} (+{Gia.ToString("N0", CultureInfo.GetCultureInfo("vi-VN"))} đ)"
        : Ten;
    public override string ToString() => HienThi;
}

/// <summary>Biên bản trả vé, lưu ở Data/trave.txt để đối soát doanh thu.</summary>
public class PhieuTraVe
{
    public string MaVe { get; set; } = "";
    public string MaSuat { get; set; } = "";
    public string TenPhim { get; set; } = "";
    public string TenPhong { get; set; } = "";
    public string Ghe { get; set; } = "";
    public string LoaiVe { get; set; } = "";
    public string TenCombo { get; set; } = "";
    public decimal SoTienHoan { get; set; }
    public DateTime GioChieu { get; set; }
    public DateTime NgayTra { get; set; } = DateTime.Now;
    public override string ToString() => string.Join(";", MaVe, MaSuat, TenPhim, TenPhong, Ghe, LoaiVe, TenCombo, SoTienHoan.ToString(CultureInfo.InvariantCulture), GioChieu.ToString("O"), NgayTra.ToString("O"));
    public static PhieuTraVe TuDong(string s)
    {
        var p = s.Split(';');
        if (p.Length < 10)
            throw new FormatException($"Biên bản trả vé cần 10 trường, dòng chỉ có {p.Length}.");

        return new PhieuTraVe
        {
            MaVe = p[0],
            MaSuat = p[1],
            TenPhim = p[2],
            TenPhong = p[3],
            Ghe = p[4],
            LoaiVe = p[5],
            TenCombo = p[6],
            SoTienHoan = decimal.Parse(p[7], CultureInfo.InvariantCulture),
            GioChieu = DateTime.Parse(p[8], null, DateTimeStyles.RoundtripKind),
            NgayTra = DateTime.Parse(p[9], null, DateTimeStyles.RoundtripKind)
        };
    }
}

public class TaiKhoan
{
    public string TenDangNhap { get; set; } = "";
    public string MatKhau { get; set; } = "";
    public string VaiTro { get; set; } = "NhanVien";
    public override string ToString() => $"{TenDangNhap};{MatKhau};{VaiTro}";
    public static TaiKhoan TuDong(string s) { var p=s.Split(';'); return new TaiKhoan { TenDangNhap=p[0], MatKhau=p[1], VaiTro=p[2] }; }
}
