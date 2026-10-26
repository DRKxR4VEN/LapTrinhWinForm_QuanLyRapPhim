using System.Globalization;
using System.Text;

namespace QuanLyRapChieuPhim;

/// <summary>
/// Phân hệ SV3 - Thống kê doanh thu.
/// - Thống kê theo phim và theo ngày bán vé.
/// - Biểu đồ cột vẽ bằng GDI+ (điểm nhấn).
/// - Xuất báo cáo ra CSV/TXT bằng SaveFileDialog.
/// - Tổng hợp cả tiền hoàn trả để ra doanh thu thực thu.
/// </summary>
public class RevenueForm : Form
{
    public sealed class MovieStat
    {
        public string Phim { get; set; } = "";
        public int SoVe { get; set; }
        public decimal TienVe { get; set; }
        public decimal TienCombo { get; set; }
        public decimal Tong => TienVe + TienCombo;
    }

    public sealed class DayStat
    {
        public DateTime Ngay { get; set; }
        public int SoVe { get; set; }
        public decimal TienVe { get; set; }
        public decimal TienCombo { get; set; }
        public decimal Tong => TienVe + TienCombo;
    }

    readonly DateTimePicker from = new()
    {
        Format = DateTimePickerFormat.Custom,
        CustomFormat = "dd/MM/yyyy",
        Width = 125
    };

    readonly DateTimePicker to = new()
    {
        Format = DateTimePickerFormat.Custom,
        CustomFormat = "dd/MM/yyyy",
        Width = 125
    };

    readonly DataGridView movieGrid = Ui.Grid();
    readonly DataGridView dayGrid = Ui.Grid();
    readonly TabControl tabs = new() { Dock = DockStyle.Fill, Font = new Font("Segoe UI", 9) };
    readonly TabPage movieTab = new("Theo phim");
    readonly TabPage dayTab = new("Theo ngày");

    readonly Panel chart = new()
    {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        BorderStyle = BorderStyle.FixedSingle,
        Margin = new Padding(6, 0, 0, 0)
    };

    readonly Label summary = new()
    {
        AutoSize = true,
        Font = new Font("Segoe UI", 10, FontStyle.Bold),
        ForeColor = Ui.MainText,
        Margin = new Padding(10, 8, 10, 4)
    };

    List<(string Label, decimal Value)> chartItems = new();
    List<MovieStat> movieRows = new();
    List<DayStat> dayRows = new();
    List<Refund> refundRows = new();

    int totalTickets;
    decimal totalTicketMoney;
    decimal totalComboMoney;
    decimal refundMoney;

    public RevenueForm()
    {
        Text = "Thống kê doanh thu";
        Width = 1000;
        Height = 640;
        MinimumSize = new Size(900, 560);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Ui.MainBack;
        ForeColor = Ui.MainText;
        Font = new Font("Segoe UI", 9);

        // ---------------- Bộ lọc và nút chức năng ----------------
        var top = Ui.Flow();
        top.Controls.Add(Ui.Lab("Từ ngày"));
        top.Controls.Add(from);
        top.Controls.Add(Ui.Lab("Đến ngày"));
        top.Controls.Add(to);

        var statButton = Ui.Btn("Thống kê");
        statButton.Click += (_, _) => LoadReport();
        top.Controls.Add(statButton);

        var csvButton = Ui.Btn("Xuất CSV");
        csvButton.BackColor = Color.FromArgb(30, 130, 80);
        csvButton.Click += (_, _) => ExportCsv();
        top.Controls.Add(csvButton);

        var txtButton = Ui.Btn("Xuất báo cáo TXT");
        txtButton.BackColor = Color.FromArgb(90, 100, 115);
        txtButton.Click += (_, _) => ExportTxt();
        top.Controls.Add(txtButton);

        // ---------------- Hai bảng số liệu ----------------
        movieTab.BackColor = Ui.MainBack;
        movieTab.Padding = new Padding(4);
        dayTab.BackColor = Ui.MainBack;
        dayTab.Padding = new Padding(4);
        movieTab.Controls.Add(movieGrid);
        dayTab.Controls.Add(dayGrid);
        tabs.TabPages.Add(movieTab);
        tabs.TabPages.Add(dayTab);
        tabs.SelectedIndexChanged += (_, _) => UpdateChart();

        var body = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            Padding = new Padding(8),
            BackColor = Ui.MainBack
        };
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 66f));
        body.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34f));
        body.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
        body.Controls.Add(tabs, 0, 0);
        body.Controls.Add(chart, 1, 0);

        var bottom = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            AutoSize = true,
            WrapContents = true,
            BackColor = Ui.MainBack
        };
        bottom.Controls.Add(summary);

        Controls.Add(body);
        Controls.Add(bottom);
        Controls.Add(top);

        // ---------------- Menu chuột phải trên bảng ----------------
        var context = new ContextMenuStrip();
        context.Items.Add("Xuất CSV", null, (_, _) => ExportCsv());
        context.Items.Add("Xuất báo cáo TXT", null, (_, _) => ExportTxt());
        context.Items.Add(new ToolStripSeparator());
        context.Items.Add("Làm mới", null, (_, _) => LoadReport());
        movieGrid.ContextMenuStrip = context;
        dayGrid.ContextMenuStrip = context;

        chart.Paint += DrawChart;
        chart.Resize += (_, _) => chart.Invalidate();

        from.Value = DateTime.Today.AddDays(-30);
        to.Value = DateTime.Today;

        LoadReport();

        Shown += (_, _) => UpdateChart();
    }

    void LoadReport()
    {
        if (from.Value.Date > to.Value.Date)
        {
            MessageBox.Show(
                "Ngày bắt đầu phải trước hoặc bằng ngày kết thúc.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Warning);
            return;
        }

        var tickets = AppData.Tickets
            .Where(t => t.SoldAt.Date >= from.Value.Date && t.SoldAt.Date <= to.Value.Date)
            .ToList();

        refundRows = AppData.Refunds
            .Where(r => r.RefundedAt.Date >= from.Value.Date && r.RefundedAt.Date <= to.Value.Date)
            .OrderBy(r => r.RefundedAt)
            .ToList();

        movieRows = tickets
            .GroupBy(t => t.MovieTitle)
            .Select(g => new MovieStat
            {
                Phim = g.Key,
                SoVe = g.Count(),
                TienVe = g.Sum(t => t.Price),
                TienCombo = g.Sum(t => t.ComboPrice)
            })
            .OrderByDescending(x => x.Tong)
            .ThenBy(x => x.Phim)
            .ToList();

        dayRows = tickets
            .GroupBy(t => t.SoldAt.Date)
            .Select(g => new DayStat
            {
                Ngay = g.Key,
                SoVe = g.Count(),
                TienVe = g.Sum(t => t.Price),
                TienCombo = g.Sum(t => t.ComboPrice)
            })
            .OrderBy(x => x.Ngay)
            .ToList();

        totalTickets = tickets.Count;
        totalTicketMoney = tickets.Sum(t => t.Price);
        totalComboMoney = tickets.Sum(t => t.ComboPrice);
        refundMoney = refundRows.Sum(r => r.Amount);

        movieGrid.DataSource = null;
        movieGrid.DataSource = movieRows;
        StyleGrid(movieGrid, byDay: false);

        dayGrid.DataSource = null;
        dayGrid.DataSource = dayRows;
        StyleGrid(dayGrid, byDay: true);

        summary.Text =
            $"Từ {from.Value:dd/MM/yyyy} đến {to.Value:dd/MM/yyyy}  •  " +
            $"{totalTickets} vé  •  Doanh thu {Ui.Money(totalTicketMoney + totalComboMoney)}  •  " +
            $"Đã trả {refundRows.Count} vé (-{Ui.Money(refundMoney)})  •  " +
            $"Thực thu {Ui.Money(totalTicketMoney + totalComboMoney - refundMoney)}";

        UpdateChart();
    }

    static void StyleGrid(DataGridView grid, bool byDay)
    {
        void Header(string column, string text, DataGridViewContentAlignment align)
        {
            if (grid.Columns[column] == null)
                return;

            grid.Columns[column]!.HeaderText = text;
            grid.Columns[column]!.DefaultCellStyle.Alignment = align;
        }

        if (byDay)
        {
            Header("Ngay", "Ngày", DataGridViewContentAlignment.MiddleCenter);
            if (grid.Columns["Ngay"] != null)
                grid.Columns["Ngay"]!.DefaultCellStyle.Format = "dd/MM/yyyy";
        }
        else
        {
            Header("Phim", "Phim", DataGridViewContentAlignment.MiddleLeft);
        }

        Header("SoVe", "Số vé", DataGridViewContentAlignment.MiddleRight);
        Header("TienVe", "Doanh thu vé", DataGridViewContentAlignment.MiddleRight);
        Header("TienCombo", "Bắp nước", DataGridViewContentAlignment.MiddleRight);
        Header("Tong", "Tổng cộng", DataGridViewContentAlignment.MiddleRight);

        foreach (var name in new[] { "TienVe", "TienCombo", "Tong" })
        {
            if (grid.Columns[name] != null)
                grid.Columns[name]!.DefaultCellStyle.Format = "N0";
        }
    }

    bool ByMovieTab() => tabs.SelectedIndex != 1;

    void UpdateChart()
    {
        var byMovie = ByMovieTab();

        chartItems = byMovie
            ? movieRows
                .OrderByDescending(x => x.Tong)
                .Take(8)
                .Select(x => (x.Phim, x.Tong))
                .ToList()
            : dayRows
                .OrderByDescending(x => x.Tong)
                .Take(8)
                .OrderBy(x => x.Ngay)
                .Select(x => ($"Ngày {x.Ngay:dd/MM}", x.Tong))
                .ToList();

        chart.Invalidate();
    }

    void DrawChart(object? sender, PaintEventArgs e)
    {
        var g = e.Graphics;
        g.Clear(Color.White);

        using var titleFont = new Font("Segoe UI", 9, FontStyle.Bold);
        using var textFont = new Font("Segoe UI", 9);
        using var valueFont = new Font("Segoe UI", 9, FontStyle.Bold);

        string title = ByMovieTab()
            ? "BIỂU ĐỒ DOANH THU THEO PHIM (TOP 8)"
            : "BIỂU ĐỒ DOANH THU THEO NGÀY";
        TextRenderer.DrawText(g, title, titleFont, new Point(10, 8), Ui.MainText);

        if (chartItems.Count == 0)
        {
            TextRenderer.DrawText(
                g,
                "Chưa có vé nào trong khoảng thời gian này.",
                textFont,
                new Point(10, 40),
                Color.Gray);
            return;
        }

        const int left = 10;
        const int top = 34;
        const int rowHeight = 30;
        const int valueWidth = 96;

        int labelWidth = Math.Min(120, Math.Max(60, chart.Width / 3));
        int barMax = Math.Max(20, chart.Width - left - labelWidth - valueWidth - 14);
        decimal max = chartItems.Max(x => x.Value);
        if (max <= 0)
            max = 1;

        using var barBrush = new SolidBrush(Ui.Accent);
        using var altBrush = new SolidBrush(Color.FromArgb(105, 170, 240));

        int y = top;
        for (int i = 0; i < chartItems.Count; i++)
        {
            var (label, value) = chartItems[i];

            TextRenderer.DrawText(
                g,
                label,
                textFont,
                new Rectangle(left, y + 6, labelWidth, 18),
                Ui.MainText,
                TextFormatFlags.EndEllipsis | TextFormatFlags.VerticalCenter);

            int width = (int)Math.Round(barMax * (double)(value / max));
            g.FillRectangle(
                i % 2 == 0 ? barBrush : altBrush,
                new Rectangle(left + labelWidth, y + 4, Math.Max(2, width), 20));

            TextRenderer.DrawText(
                g,
                Ui.Money(value),
                valueFont,
                new Rectangle(chart.Width - valueWidth - 8, y + 6, valueWidth, 18),
                Ui.MainText,
                TextFormatFlags.Right | TextFormatFlags.VerticalCenter);

            y += rowHeight;
        }
    }

    void ExportCsv()
    {
        using var dialog = new SaveFileDialog
        {
            Title = "Xuất báo cáo doanh thu (CSV)",
            Filter = "Tập tin CSV|*.csv",
            FileName = $"BaoCaoDoanhThu_{DateTime.Now:yyyyMMdd_HHmm}.csv"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        try
        {
            var sb = new StringBuilder();
            sb.AppendLine("BÁO CÁO DOANH THU");
            sb.AppendLine($"Từ ngày;{from.Value:dd/MM/yyyy}");
            sb.AppendLine($"Đến ngày;{to.Value:dd/MM/yyyy}");
            sb.AppendLine($"Số vé;{totalTickets}");
            sb.AppendLine($"Doanh thu vé;{Num(totalTicketMoney)}");
            sb.AppendLine($"Doanh thu bắp nước;{Num(totalComboMoney)}");
            sb.AppendLine($"Số vé đã trả;{refundRows.Count}");
            sb.AppendLine($"Tiền hoàn;{Num(refundMoney)}");
            sb.AppendLine($"Thực thu;{Num(totalTicketMoney + totalComboMoney - refundMoney)}");
            sb.AppendLine();

            if (ByMovieTab())
            {
                sb.AppendLine("Phim;Số vé;Doanh thu vé;Bắp nước;Tổng cộng");
                foreach (var row in movieRows)
                {
                    sb.AppendLine(
                        $"{row.Phim};{row.SoVe};{Num(row.TienVe)};" +
                        $"{Num(row.TienCombo)};{Num(row.Tong)}");
                }
            }
            else
            {
                sb.AppendLine("Ngày;Số vé;Doanh thu vé;Bắp nước;Tổng cộng");
                foreach (var row in dayRows)
                {
                    sb.AppendLine(
                        $"{row.Ngay:yyyy-MM-dd};{row.SoVe};{Num(row.TienVe)};" +
                        $"{Num(row.TienCombo)};{Num(row.Tong)}");
                }
            }

            File.WriteAllText(dialog.FileName, sb.ToString(), new UTF8Encoding(true));
            MessageBox.Show(
                "Đã xuất báo cáo:\n" + dialog.FileName,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    void ExportTxt()
    {
        using var dialog = new SaveFileDialog
        {
            Title = "Xuất báo cáo doanh thu (TXT)",
            Filter = "Tập tin văn bản|*.txt",
            FileName = $"BaoCaoDoanhThu_{DateTime.Now:yyyyMMdd_HHmm}.txt"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        try
        {
            var sb = new StringBuilder();
            sb.AppendLine(AppConfig.CinemaName);
            sb.AppendLine("BÁO CÁO DOANH THU");
            sb.AppendLine(new string('=', 62));
            sb.AppendLine($"Khoảng thời gian : {from.Value:dd/MM/yyyy} - {to.Value:dd/MM/yyyy}");
            sb.AppendLine($"Ngày lập báo cáo: {DateTime.Now:dd/MM/yyyy HH:mm}");
            sb.AppendLine();
            sb.AppendLine("TỔNG HỢP");
            sb.AppendLine($"- Số vé đã bán      : {totalTickets}");
            sb.AppendLine($"- Doanh thu vé      : {Ui.Money(totalTicketMoney)}");
            sb.AppendLine($"- Doanh thu bắp nước: {Ui.Money(totalComboMoney)}");
            sb.AppendLine($"- Số vé đã trả      : {refundRows.Count}");
            sb.AppendLine($"- Tiền hoàn trả     : {Ui.Money(refundMoney)}");
            sb.AppendLine($"- Thực thu          : {Ui.Money(totalTicketMoney + totalComboMoney - refundMoney)}");
            sb.AppendLine();
            sb.AppendLine("DOANH THU THEO PHIM");
            foreach (var row in movieRows)
            {
                sb.AppendLine(
                    $"- {row.Phim}: {row.SoVe} vé | Vé {Ui.Money(row.TienVe)} | " +
                    $"Bắp nước {Ui.Money(row.TienCombo)} | Tổng {Ui.Money(row.Tong)}");
            }

            sb.AppendLine();
            sb.AppendLine("DOANH THU THEO NGÀY");
            foreach (var row in dayRows)
            {
                sb.AppendLine(
                    $"- {row.Ngay:dd/MM/yyyy}: {row.SoVe} vé | Vé {Ui.Money(row.TienVe)} | " +
                    $"Bắp nước {Ui.Money(row.TienCombo)} | Tổng {Ui.Money(row.Tong)}");
            }

            sb.AppendLine();
            sb.AppendLine("DANH SÁCH VÉ ĐÃ TRẢ");
            if (refundRows.Count == 0)
            {
                sb.AppendLine("- Không có vé trả trong khoảng thời gian này.");
            }
            else
            {
                foreach (var r in refundRows)
                {
                    sb.AppendLine(
                        $"- {r.TicketId} | {r.MovieTitle} | Ghế {r.Seat} | " +
                        $"{Ui.Money(r.Amount)} | Trả lúc {r.RefundedAt:HH:mm dd/MM/yyyy}");
                }
            }

            sb.AppendLine();
            sb.AppendLine(new string('=', 62));
            sb.AppendLine("Hết báo cáo.");

            File.WriteAllText(dialog.FileName, sb.ToString(), new UTF8Encoding(true));
            MessageBox.Show(
                "Đã xuất báo cáo:\n" + dialog.FileName,
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    static string Num(decimal value) => value.ToString("0.##", CultureInfo.InvariantCulture);
}
