
namespace QuanLyRapChieuPhim;

public class TicketForm : Form
{
    readonly ComboBox screening = new()
    {
        DropDownStyle = ComboBoxStyle.DropDownList,
        Width = 320
    };

    readonly FlowLayoutPanel seats = new()
    {
        Dock = DockStyle.Fill,
        AutoScroll = true,
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false,
        Padding = new Padding(10),
        BackColor = Color.White
    };

    readonly RadioButton adult = new()
    {
        BackColor = Ui.MainBack,
        ForeColor = Ui.MainText,
        Font = new Font("Segoe UI", 9),
        Text = "Người lớn",
        Checked = true,
        AutoSize = true
    };

    readonly RadioButton student = new()
    {
        Text = "Học sinh - Sinh viên",
        AutoSize = true
    };

    readonly Label status = new()
    {
        Dock = DockStyle.Bottom,
        Height = 35,
        TextAlign = ContentAlignment.MiddleLeft
    };

    readonly HashSet<string> selected = new();
    readonly Dictionary<string, Button> seatButtons = new();

    CinemaRoom? currentRoom;
    Screening? currentScreening;

    public TicketForm()
    {
        Text = "Bán vé";
        Width = 1000;
        Height = 700;
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Ui.MainBack;
        ForeColor = Ui.MainText;
        Font = new Font("Segoe UI", 10);

        var top = Ui.Flow();

        top.Controls.Add(Ui.Lab("Suất chiếu"));
        top.Controls.Add(screening);
        top.Controls.Add(adult);
        top.Controls.Add(student);

        var sell = Ui.Btn("Bán vé đã chọn");
        sell.Click += (_, _) => Sell();
        top.Controls.Add(sell);

        Controls.Add(seats);
        Controls.Add(status);
        Controls.Add(top);

        screening.SelectedIndexChanged += (_, _) => BuildSeats();

        RefreshScreenings();
    }

    void RefreshScreenings()
    {
        screening.DataSource = AppData.Screenings
            .Where(s => s.StartTime > DateTime.Now.AddMinutes(-AppConfig.CleaningMinutes))
            .OrderBy(s => s.StartTime)
            .ToList();

        screening.DisplayMember = nameof(Screening.Display);

        BuildSeats();
    }

    void BuildSeats()
    {
        seats.Controls.Clear();
        selected.Clear();
        seatButtons.Clear();

        currentScreening = screening.SelectedItem as Screening;

        currentRoom = currentScreening == null
            ? null
            : AppData.Rooms.FirstOrDefault(
                r => r.Id == currentScreening.RoomId);

        if (currentRoom == null || currentScreening == null)
        {
            status.Text = "Chưa có suất chiếu/phòng chiếu.";
            return;
        }

        var sold = AppData.Tickets
            .Where(t => t.ScreeningId == currentScreening.Id)
            .Select(t => t.Seat)
            .ToHashSet();

        var screenLabel = new Label
        {
            Text = "MÀN HÌNH",
            AutoSize = false,
            Width = Math.Max(400, currentRoom.SeatsPerRow * 72 + 42),
            Height = 26,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.FromArgb(220, 230, 245),
            ForeColor = Ui.MainText,
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            Margin = new Padding(0, 0, 0, 8)
        };
        seats.Controls.Add(screenLabel);

        for (int r = 1; r <= currentRoom.Rows; r++)
        {
            var rowPanel = new FlowLayoutPanel
            {
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                WrapContents = false,
                BackColor = Color.White,
                Margin = new Padding(0, 2, 0, 2)
            };

            var rowLetter = (char)('A' + r - 1);
            bool vipRow = r <= currentRoom.VipRows;

            rowPanel.Controls.Add(new Label
            {
                Text = $"{rowLetter}{(vipRow ? " ★" : "")}",
                AutoSize = false,
                Width = 42,
                Height = 42,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = vipRow ? Color.DarkGoldenrod : Ui.MainText,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            });

            for (int c = 1; c <= currentRoom.SeatsPerRow; c++)
            {
                var name = $"{rowLetter}{c}";

                var b = new Button
                {
                    Text = name,
                    Width = 62,
                    Height = 42,
                    Margin = new Padding(4),
                    Tag = name,
                    Enabled = !sold.Contains(name),
                    ForeColor = Color.Black,
                    BackColor = sold.Contains(name)
                        ? Color.IndianRed
                        : vipRow ? Color.Gold : Color.LightGreen
                };

                b.Click += (_, _) => Toggle(name, b);

                seatButtons[name] = b;
                rowPanel.Controls.Add(b);
            }

            seats.Controls.Add(rowPanel);
        }

        status.Text =
            "Xanh lá: ghế trống | Vàng: ghế VIP | Đỏ: đã bán | Xanh dương: đang chọn";
    }

    void Toggle(string name, Button b)
    {
        if (!selected.Add(name))
        {
            selected.Remove(name);

            bool vip =
                (name[0] - 'A' + 1) <= currentRoom!.VipRows;

            b.BackColor = vip ? Color.Gold : Color.LightGreen;
        }
        else
        {
            b.BackColor = Color.DeepSkyBlue;
        }

        status.Text = $"Đang chọn {selected.Count} ghế.";
    }

    void Sell()
    {
        try
        {
            if (currentScreening == null || currentRoom == null)
                throw new Exception("Vui lòng chọn suất chiếu.");

            if (selected.Count == 0)
                throw new Exception("Vui lòng chọn ít nhất một ghế.");

            var movie = AppData.Movies.FirstOrDefault(
                m => m.Id == currentScreening.MovieId)
                ?? throw new Exception("Không tìm thấy phim.");

            var customer = student.Checked
                ? "Học sinh-SV"
                : "Người lớn";

            var discount = student.Checked
                ? AppConfig.StudentDiscount
                : AppConfig.AdultDiscount;

            var created = new List<Ticket>();

            foreach (var seat in selected)
            {
                if (AppData.Tickets.Any(
                    t => t.ScreeningId == currentScreening.Id
                    && t.Seat == seat))
                {
                    throw new Exception($"Ghế {seat} vừa được bán.");
                }

                bool vip = (seat[0] - 'A' + 1) <= currentRoom.VipRows;

                decimal price = AppConfig.BaseTicketPrice
                    * (vip ? 1.5m : 1m)
                    * (1 - discount);

                created.Add(new Ticket
                {
                    Id = "V" + Guid.NewGuid().ToString("N")[..8].ToUpper(),
                    ScreeningId = currentScreening.Id,
                    MovieTitle = movie.Title,
                    RoomName = currentRoom.Name,
                    ShowTime = currentScreening.StartTime,
                    Seat = seat,
                    IsVip = vip,
                    CustomerType = customer,
                    Price = decimal.Round(price, 0),
                    SoldAt = DateTime.Now
                });
            }

            AppData.Tickets.AddRange(created);
            AppData.SaveTickets();

            foreach (var t in created)
            {
                var path = Path.Combine(
                    FileHelper.DataDir,
                    $"Ve_{t.Id}.txt");

                File.WriteAllText(path,
                    $"VÉ XEM PHIM\r\n" +
                    $"Mã vé: {t.Id}\r\n" +
                    $"Phim: {t.MovieTitle}\r\n" +
                    $"Phòng: {t.RoomName}\r\n" +
                    $"Suất: {t.ShowTime:dd/MM/yyyy HH:mm}\r\n" +
                    $"Ghế: {t.Seat} {(t.IsVip ? "(VIP)" : "")}\r\n" +
                    $"Khách: {t.CustomerType}\r\n" +
                    $"Giá: {t.Price:N0} VNĐ");
            }

            MessageBox.Show(
                $"Bán thành công {created.Count} vé.\n" +
                "Vé được lưu trong thư mục Data.",
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            BuildSeats();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }
}
