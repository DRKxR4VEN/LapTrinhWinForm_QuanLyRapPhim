using System.Globalization;
using System.Text;

namespace QuanLyRapChieuPhim;

/// <tongHop>
/// Phân hệ SV3 - Thống kê doanh thu.
/// - Thống kê theo phim và theo ngày bán vé.
/// - Biểu đồ cột vẽ bằng GDI+ (điểm nhấn).
/// - Xuất báo cáo ra CSV/TXT bằng SaveFileDialog.
/// - Tổng hợp cả tiền hoàn trả để ra doanh thu thực thu.
/// </tongHop>
public partial class FormDoanhThu : Form
{
    public sealed class ThongKePhim
    {
        public string Phim { get; set; } = "";
        public int SoVe { get; set; }
        public decimal TienVe { get; set; }
        public decimal TienCombo { get; set; }
        public decimal Tong => TienVe + TienCombo;
    }

    public sealed class ThongKeNgay
    {
        public DateTime Ngay { get; set; }
        public int SoVe { get; set; }
        public decimal TienVe { get; set; }
        public decimal TienCombo { get; set; }
        public decimal Tong => TienVe + TienCombo;
    }

    List<(string Label, decimal Value)> mucBieuDo = new();
    List<ThongKePhim> dongTheoPhim = new();
    List<ThongKeNgay> dongTheoNgay = new();
    List<PhieuTraVe> dongTraVe = new();

    int tongVe;
    decimal tongTienVe;
    decimal tongTienCombo;
    decimal tongTienHoan;

    public FormDoanhThu()
    {
        InitializeComponent();
        GiaoDien.ApDungGiaoDien(this);

        if (GiaoDien.DangThietKe)
            return;

        oTuNgay.Value = DateTime.Today.AddDays(-30);
        oDenNgay.Value = DateTime.Today;

        NapBaoCao();
    }

    void nutThongKe_Click(object? sender, EventArgs e) => NapBaoCao();

    void nutXuatCsv_Click(object? sender, EventArgs e) => XuatCsv();

    void nutXuatTxt_Click(object? sender, EventArgs e) => XuatTxt();

    void tab_SelectedIndexChanged(object? sender, EventArgs e) => CapNhatBieuDo();

    void bieuDo_Paint(object? sender, PaintEventArgs e) => VeBieuDo(e);

    void bieuDo_Resize(object? sender, EventArgs e) => bieuDo.Invalidate();

    void mucXuatCsv_Click(object? sender, EventArgs e) => XuatCsv();

    void mucXuatTxt_Click(object? sender, EventArgs e) => XuatTxt();

    void mucLamMoi_Click(object? sender, EventArgs e) => NapBaoCao();

    void FormDoanhThu_Shown(object? sender, EventArgs e) => CapNhatBieuDo();

    void NapBaoCao()
    {
        if (oTuNgay.Value.Date > oDenNgay.Value.Date)
        {
            MessageBox.Show(
                "Ngày bắt đầu phải trước hoặc bằng ngày kết thúc.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        var dsVe = DuLieu.DanhSachVe
            .Where(t => t.NgayBan.Date >= oTuNgay.Value.Date && t.NgayBan.Date <= oDenNgay.Value.Date)
            .ToList();

        dongTraVe = DuLieu.DanhSachTraVe
            .Where(r => r.NgayTra.Date >= oTuNgay.Value.Date && r.NgayTra.Date <= oDenNgay.Value.Date)
            .OrderBy(r => r.NgayTra)
            .ToList();

        dongTheoPhim = dsVe
            .GroupBy(t => t.TenPhim)
            .Select(g => new ThongKePhim
            {
                Phim = g.Key,
                SoVe = g.Count(),
                TienVe = g.Sum(t => t.Gia),
                TienCombo = g.Sum(t => t.GiaCombo)
            })
            .OrderByDescending(x => x.Tong)
            .ThenBy(x => x.Phim)
            .ToList();

        dongTheoNgay = dsVe
            .GroupBy(t => t.NgayBan.Date)
            .Select(g => new ThongKeNgay
            {
                Ngay = g.Key,
                SoVe = g.Count(),
                TienVe = g.Sum(t => t.Gia),
                TienCombo = g.Sum(t => t.GiaCombo)
            })
            .OrderBy(x => x.Ngay)
            .ToList();

        tongVe = dsVe.Count;
        tongTienVe = dsVe.Sum(t => t.Gia);
        tongTienCombo = dsVe.Sum(t => t.GiaCombo);
        tongTienHoan = dongTraVe.Sum(r => r.SoTienHoan);

        bangPhim.DataSource = null;
        bangPhim.DataSource = dongTheoPhim;
        DinhDangBang(bangPhim, theoNgay: false);

        bangNgay.DataSource = null;
        bangNgay.DataSource = dongTheoNgay;
        DinhDangBang(bangNgay, theoNgay: true);

        tongHop.Text =
            $"Từ {oTuNgay.Value:dd/MM/yyyy} đến {oDenNgay.Value:dd/MM/yyyy}  •  " +
            $"{tongVe} vé  •  Doanh thu {GiaoDien.Tien(tongTienVe + tongTienCombo)}  •  " +
            $"Đã trả {dongTraVe.Count} vé (-{GiaoDien.Tien(tongTienHoan)})  •  " +
            $"Thực thu {GiaoDien.Tien(tongTienVe + tongTienCombo - tongTienHoan)}";

        CapNhatBieuDo();
    }

    static void DinhDangBang(DataGridView bang, bool theoNgay)
    {
        void Header(string cot, string chu, DataGridViewContentAlignment canLe)
        {
            if (bang.Columns[cot] == null)
                return;

            bang.Columns[cot]!.HeaderText = chu;
            bang.Columns[cot]!.DefaultCellStyle.Alignment = canLe;
        }

        if (theoNgay)
        {
            Header("Ngay", "Ngày", DataGridViewContentAlignment.MiddleCenter);
            if (bang.Columns["Ngay"] != null)
                bang.Columns["Ngay"]!.DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        else
        {
            Header("Phim", "Phim", DataGridViewContentAlignment.MiddleLeft);
        }

        Header("SoVe", "Số vé", DataGridViewContentAlignment.MiddleRight);
        Header("TienVe", "Doanh thu vé", DataGridViewContentAlignment.MiddleRight);
        Header("TienCombo", "Bắp nước", DataGridViewContentAlignment.MiddleRight);
        Header("Tong", "Tổng cộng", DataGridViewContentAlignment.MiddleRight);

        foreach (var tenCot in new[] { "TienVe", "TienCombo", "Tong" })
        {
            if (bang.Columns[tenCot] != null)
                bang.Columns[tenCot]!.DefaultCellStyle.Format = "N0";
        }
    }

    bool DangXemTheoPhim() => tab.SelectedIndex != 1;

    void CapNhatBieuDo()
    {
        var theoPhim = DangXemTheoPhim();

        mucBieuDo = theoPhim
            ? dongTheoPhim
                .OrderByDescending(x => x.Tong)
                .Take(8)
                .Select(x => (x.Phim, x.Tong))
                .ToList()
            : dongTheoNgay
                .OrderByDescending(x => x.Tong)
                .Take(8)
                .OrderBy(x => x.Ngay)
                .Select(x => ($"Ngày {x.Ngay:dd/MM}", x.Tong))
                .ToList();

        bieuDo.Invalidate();
    }

    void VeBieuDo(PaintEventArgs e)
    {
        var g = e.Graphics;
        g.Clear(Color.White);

        using var fontTieuDe = new Font("Segoe UI", 9, FontStyle.Bold);
        using var fontChu = new Font("Segoe UI", 9);
        using var fontGiaTri = new Font("Segoe UI", 9, FontStyle.Bold);

        string tieuDe = DangXemTheoPhim()
            ? "BIỂU ĐỒ DOANH THU THEO PHIM (TOP 8)"
            : "BIỂU ĐỒ DOANH THU THEO NGÀY";
        TextRenderer.DrawText(g, tieuDe, fontTieuDe, new Point(10, 8), GiaoDien.ChuChinh);

        if (mucBieuDo.Count == 0)
        {
            TextRenderer.DrawText(
                g,
                "Chưa có vé nào trong khoảng thời gian này.",
                fontChu,
                new Point(10, 40),
                Color.Gray);
            return;
        }

        const int le = 10;
        const int tren = 34;
        const int caoDong = 30;
        const int rongGiaTri = 96;

        int rongNhan = Math.Min(120, Math.Max(60, bieuDo.Width / 3));
        int rongToiDa = Math.Max(20, bieuDo.Width - le - rongNhan - rongGiaTri - 14);
        decimal lonNhat = mucBieuDo.Max(x => x.Value);
        if (lonNhat <= 0)
            lonNhat = 1;

        using var coChinh = new SolidBrush(GiaoDien.MauNhan);
        using var coPhu = new SolidBrush(Color.FromArgb(105, 170, 240));

        int y = tren;
        for (int i = 0; i < mucBieuDo.Count; i++)
        {
            var (nhan, giaTri) = mucBieuDo[i];

            TextRenderer.DrawText(
                g,
                nhan,
                fontChu,
                new Rectangle(le, y + 6, rongNhan, 18),
                GiaoDien.ChuChinh,
                TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);

            int rong = (int)Math.Round(rongToiDa * (double)(giaTri / lonNhat));
            g.FillRectangle(
                i % 2 == 0 ? coChinh : coPhu,
                new Rectangle(le + rongNhan, y + 4, Math.Max(2, rong), 20));

            TextRenderer.DrawText(
                g,
                GiaoDien.Tien(giaTri),
                fontGiaTri,
                new Rectangle(bieuDo.Width - rongGiaTri - 8, y + 6, rongGiaTri, 18),
                GiaoDien.ChuChinh,
                TextFormatFlags.Right | TextFormatFlags.VerticalCenter);

            y += caoDong;
        }
    }

    void XuatCsv()
    {
        using var hopThoai = new SaveFileDialog
        {
            Title = "Xuất báo cáo doanh thu (CSV)",
            Filter = "Tập tin CSV|*.csv",
            FileName = $"BaoCaoDoanhThu_{DateTime.Now:yyyyMMdd_HHmm}.csv"
        };

        if (hopThoai.ShowDialog() != DialogResult.OK)
            return;

        try
        {
            var bo = new StringBuilder();
            bo.AppendLine("BÁO CÁO DOANH THU");
            bo.AppendLine($"Từ ngày;{oTuNgay.Value:dd/MM/yyyy}");
            bo.AppendLine($"Đến ngày;{oDenNgay.Value:dd/MM/yyyy}");
            bo.AppendLine($"Số vé;{tongVe}");
            bo.AppendLine($"Doanh thu vé;{So(tongTienVe)}");
            bo.AppendLine($"Doanh thu bắp nước;{So(tongTienCombo)}");
            bo.AppendLine($"Số vé đã trả;{dongTraVe.Count}");
            bo.AppendLine($"Tiền hoàn;{So(tongTienHoan)}");
            bo.AppendLine($"Thực thu;{So(tongTienVe + tongTienCombo - tongTienHoan)}");
            bo.AppendLine();

            if (DangXemTheoPhim())
            {
                bo.AppendLine("Phim;Số vé;Doanh thu vé;Bắp nước;Tổng cộng");
                foreach (var dong in dongTheoPhim)
                {
                    bo.AppendLine(
                        $"{dong.Phim};{dong.SoVe};{So(dong.TienVe)};" +
                        $"{So(dong.TienCombo)};{So(dong.Tong)}");
                }
            }
            else
            {
                bo.AppendLine("Ngày;Số vé;Doanh thu vé;Bắp nước;Tổng cộng");
                foreach (var dong in dongTheoNgay)
                {
                    bo.AppendLine(
                        $"{dong.Ngay:yyyy-MM-dd};{dong.SoVe};{So(dong.TienVe)};" +
                        $"{So(dong.TienCombo)};{So(dong.Tong)}");
                }
            }

            File.WriteAllText(hopThoai.FileName, bo.ToString(), new UTF8Encoding(true));
            MessageBox.Show(
                "Đã xuất báo cáo:\n" + hopThoai.FileName,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            GiaoDien.BaoLoi(ex);
        }
    }

    void XuatTxt()
    {
        using var hopThoai = new SaveFileDialog
        {
            Title = "Xuất báo cáo doanh thu (TXT)",
            Filter = "Tập tin văn bản|*.txt",
            FileName = $"BaoCaoDoanhThu_{DateTime.Now:yyyyMMdd_HHmm}.txt"
        };

        if (hopThoai.ShowDialog() != DialogResult.OK)
            return;

        try
        {
            var bo = new StringBuilder();
            bo.AppendLine(CauHinh.TenRap);
            bo.AppendLine("BÁO CÁO DOANH THU");
            bo.AppendLine(new string('=', 62));
            bo.AppendLine($"Khoảng thời gian : {oTuNgay.Value:dd/MM/yyyy} - {oDenNgay.Value:dd/MM/yyyy}");
            bo.AppendLine($"Ngày lập báo cáo: {DateTime.Now:dd/MM/yyyy HH:mm}");
            bo.AppendLine();
            bo.AppendLine("TỔNG HỢP");
            bo.AppendLine($"- Số vé đã bán      : {tongVe}");
            bo.AppendLine($"- Doanh thu vé      : {GiaoDien.Tien(tongTienVe)}");
            bo.AppendLine($"- Doanh thu bắp nước: {GiaoDien.Tien(tongTienCombo)}");
            bo.AppendLine($"- Số vé đã trả      : {dongTraVe.Count}");
            bo.AppendLine($"- Tiền hoàn trả     : {GiaoDien.Tien(tongTienHoan)}");
            bo.AppendLine($"- Thực thu          : {GiaoDien.Tien(tongTienVe + tongTienCombo - tongTienHoan)}");
            bo.AppendLine();
            bo.AppendLine("DOANH THU THEO PHIM");
            foreach (var dong in dongTheoPhim)
            {
                bo.AppendLine(
                    $"- {dong.Phim}: {dong.SoVe} vé | Vé {GiaoDien.Tien(dong.TienVe)} | " +
                    $"Bắp nước {GiaoDien.Tien(dong.TienCombo)} | Tổng {GiaoDien.Tien(dong.Tong)}");
            }

            bo.AppendLine();
            bo.AppendLine("DOANH THU THEO NGÀY");
            foreach (var dong in dongTheoNgay)
            {
                bo.AppendLine(
                    $"- {dong.Ngay:dd/MM/yyyy}: {dong.SoVe} vé | Vé {GiaoDien.Tien(dong.TienVe)} | " +
                    $"Bắp nước {GiaoDien.Tien(dong.TienCombo)} | Tổng {GiaoDien.Tien(dong.Tong)}");
            }

            bo.AppendLine();
            bo.AppendLine("DANH SÁCH VÉ ĐÃ TRẢ");
            if (dongTraVe.Count == 0)
            {
                bo.AppendLine("- Không có vé trả trong khoảng thời gian này.");
            }
            else
            {
                foreach (var r in dongTraVe)
                {
                    bo.AppendLine(
                        $"- {r.MaVe} | {r.TenPhim} | Ghế {r.Ghe} | " +
                        $"{GiaoDien.Tien(r.SoTienHoan)} | Trả lúc {r.NgayTra:HH:mm dd/MM/yyyy}");
                }
            }

            bo.AppendLine();
            bo.AppendLine(new string('=', 62));
            bo.AppendLine("Hết báo cáo.");

            File.WriteAllText(hopThoai.FileName, bo.ToString(), new UTF8Encoding(true));
            MessageBox.Show(
                "Đã xuất báo cáo:\n" + hopThoai.FileName,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            GiaoDien.BaoLoi(ex);
        }
    }

    static string So(decimal giaTri) => giaTri.ToString("0.##", CultureInfo.InvariantCulture);
}
