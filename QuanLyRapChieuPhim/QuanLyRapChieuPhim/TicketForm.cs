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
public class TicketForm : Form
{
    static readonly Color SeatFree = Color.LightGreen;
    static readonly Color SeatVip = Color.Gold;
    static readonly Color SeatSelected = Color.DeepSkyBlue;
    static readonly Color SeatSold = Color.IndianRed;

    readonly ComboBox screening = new()
    {
        DropDownStyle = ComboBoxStyle.DropDownList,
        Width = 430
    };

    readonly ComboBox snack = new()
    {
        DropDownStyle = ComboBoxStyle.DropDownList,
        Width = 235
    };

    readonly RadioButton adult = new()
    {
        Text = "Người lớn",
        AutoSize = true,
        Checked = true,
        BackColor = Ui.MainBack,
        ForeColor = Ui.MainText,
        Margin = new Padding(2, 9, 14, 0)
    };

    readonly RadioButton student = new()
    {
        Text = "Học sinh - Sinh viên",
        AutoSize = true,
        BackColor = Ui.MainBack,
        ForeColor = Ui.MainText,
        Margin = new Padding(2, 9, 0, 0)
    };

    readonly Panel seatHost = new()
    {
        Dock = DockStyle.Fill,
        AutoScroll = true,
        BackColor = Color.White,
        Padding = new Padding(10)
    };

    readonly FlowLayoutPanel seats = new()
    {
        Location = new Point(0, 0),
        FlowDirection = FlowDirection.TopDown,
        WrapContents = false,
        AutoSize = true,
        AutoSizeMode = AutoSizeMode.GrowAndShrink,
        BackColor = Color.White
    };

    readonly Label legend = new()
    {
        AutoSize = true,
        TextAlign = ContentAlignment.MiddleLeft,
        ForeColor = Ui.MainText,
        Margin = new Padding(8, 10, 0, 0)
    };

    readonly Label status = new()
    {
        AutoSize = true,
        ForeColor = Ui.MainText,
        Margin = new Padding(10, 10, 18, 0)
    };

    readonly Label cart = new()
    {
        AutoSize = true,
        Font = new Font("Segoe UI", 10, FontStyle.Bold),
        ForeColor = Ui.Accent,
        Margin = new Padding(4, 7, 10, 0)
    };

    readonly ListView soldList = new()
    {
        Dock = DockStyle.Fill,
        View = View.Details,
        FullRowSelect = true,
        MultiSelect = false,
        HideSelection = false,
        ShowItemToolTips = true,
        BackColor = Color.White,
        ForeColor = Ui.MainText,
        BorderStyle = BorderStyle.FixedSingle
    };

    readonly Label soldInfo = new()
    {
        Dock = DockStyle.Bottom,
        Height = 56,
        ForeColor = Color.FromArgb(90, 100, 115),
        Padding = new Padding(2, 4, 2, 4)
    };

    readonly Button refundButton;
    readonly Button printButton;

    readonly System.Windows.Forms.Timer refreshTimer = new() { Interval = 20000 };

    readonly HashSet<string> selected = new();

    CinemaRoom? currentRoom;
    Screening? currentScreening;
    string loadedScreeningIds = "";
    bool binding;
    bool selling;

    public TicketForm()
    {
        Text = "Bán vé";
        Width = 1150;
        Height = 680;
        MinimumSize = new Size(1050, 600);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Ui.MainBack;
        ForeColor = Ui.MainText;
        Font = new Font("Segoe UI", 9);

        // ---------------- Thanh công cụ phía trên ----------------
        var top = Ui.Flow();

        top.Controls.Add(Ui.Lab("Suất chiếu"));
        top.Controls.Add(screening);

        var typePanel = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = false,
            BackColor = Ui.MainBack,
            Margin = new Padding(8, 0, 8, 0)
        };
        typePanel.Controls.Add(new Label
        {
            Text = "Loại vé",
            AutoSize = true,
            ForeColor = Ui.MainText,
            Margin = new Padding(0, 10, 4, 0)
        });
        typePanel.Controls.Add(adult);
        typePanel.Controls.Add(student);
        top.Controls.Add(typePanel);

        top.Controls.Add(Ui.Lab("Bắp nước"));
        top.Controls.Add(snack);

        var sell = Ui.Btn("Bán vé đã chọn");
        sell.Click += (_, _) => Sell();
        top.Controls.Add(sell);

        // ---------------- Danh sách vé đã bán + trả vé ----------------
        var right = new Panel
        {
            Dock = DockStyle.Right,
            Width = 340,
            BackColor = Ui.MainBack,
            Padding = new Padding(8)
        };

        var soldTitle = new Label
        {
            Dock = DockStyle.Top,
            Height = 28,
            Text = "VÉ ĐÃ BÁN",
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            ForeColor = Ui.MainText
        };

        refundButton = Ui.Btn("Trả vé đã chọn");
        refundButton.BackColor = Color.Firebrick;
        refundButton.Enabled = false;
        refundButton.Click += (_, _) => Refund();

        printButton = Ui.Btn("In lại vé");
        printButton.BackColor = Color.FromArgb(90, 100, 115);
        printButton.Click += (_, _) => PrintSelectedTicket();

        var actions = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            Height = 84,
            WrapContents = true,
            BackColor = Ui.MainBack
        };
        actions.Controls.Add(refundButton);
        actions.Controls.Add(printButton);

        soldList.Columns.Add("Ghế", 42);
        soldList.Columns.Add("Loại vé", 92);
        soldList.Columns.Add("Tổng", 80);
        soldList.Columns.Add("Bán lúc", 86);

        right.Controls.Add(soldList);
        right.Controls.Add(soldInfo);
        right.Controls.Add(actions);
        right.Controls.Add(soldTitle);

        // ---------------- Thanh trạng thái dưới ----------------
        var bottom = new FlowLayoutPanel
        {
            Dock = DockStyle.Bottom,
            AutoSize = true,
            WrapContents = true,
            BackColor = Ui.MainBack
        };
        bottom.Controls.Add(status);
        bottom.Controls.Add(cart);
        bottom.Controls.Add(legend);

        seatHost.Controls.Add(seats);

        Controls.Add(seatHost);
        Controls.Add(right);
        Controls.Add(bottom);
        Controls.Add(top);

        // ---------------- Combo bắp nước ----------------
        var snackOptions = new List<SnackCombo>
        {
            new() { Name = "Không dùng bắp nước", Price = 0 }
        };
        snackOptions.AddRange(AppConfig.SnackCombos);
        snack.DisplayMember = nameof(SnackCombo.Display);
        snack.DataSource = snackOptions;

        // ---------------- Sự kiện ----------------
        screening.SelectedIndexChanged += (_, _) =>
        {
            if (!binding)
                BuildSeats();
        };
        adult.CheckedChanged += (_, _) => UpdateCart();
        student.CheckedChanged += (_, _) => UpdateCart();
        snack.SelectedIndexChanged += (_, _) => UpdateCart();
        soldList.SelectedIndexChanged += (_, _) =>
            refundButton.Enabled = soldList.SelectedItems.Count > 0
                && soldList.SelectedItems[0].Tag is Ticket;
        soldList.DoubleClick += (_, _) => PrintSelectedTicket();

        refreshTimer.Tick += (_, _) =>
        {
            // Không vẽ lại sơ đồ ghế khi hộp thoại xác nhận bán vé đang mở.
            if (!selling)
                RefreshScreenings();
        };
        refreshTimer.Start();
        FormClosed += (_, _) => refreshTimer.Stop();

        RefreshScreenings();
    }

    /// <summary>
    /// Nạp danh sách suất chiếu chưa bắt đầu. Timer gọi lại hàm này mỗi 20 giây
    /// nên suất vừa tới giờ chiếu sẽ tự biến mất khỏi màn hình bán vé.
    /// </summary>
    void RefreshScreenings()
    {
        var list = AppData.Screenings
            .Where(s => s.StartTime > DateTime.Now)
            .OrderBy(s => s.StartTime)
            .ToList();

        var signature = string.Join("|", list.Select(x => x.Id));
        if (signature == loadedScreeningIds)
        {
            UpdateCountdown();
            return;
        }

        var previousId = (screening.SelectedItem as Screening)?.Id;
        var lostSelection = previousId != null && list.All(x => x.Id != previousId);
        loadedScreeningIds = signature;

        binding = true;
        screening.DataSource = null;
        screening.DisplayMember = nameof(Screening.Display);
        screening.DataSource = list;

        if (previousId != null)
        {
            var index = list.FindIndex(x => x.Id == previousId);
            if (index >= 0)
                screening.SelectedIndex = index;
        }

        if (screening.SelectedIndex < 0 && list.Count > 0)
            screening.SelectedIndex = 0;
        binding = false;

        BuildSeats();

        if (lostSelection)
            status.Text = "Suất chiếu vừa bắt đầu đã được ẩn khỏi danh sách bán vé.";
    }

    void BuildSeats()
    {
        while (seats.Controls.Count > 0)
        {
            var control = seats.Controls[0];
            seats.Controls.RemoveAt(0);
            control.Dispose();
        }

        selected.Clear();
        soldList.Items.Clear();
        refundButton.Enabled = false;
        cart.Text = "";

        currentScreening = screening.SelectedItem as Screening;
        currentRoom = currentScreening == null
            ? null
            : AppData.Rooms.FirstOrDefault(r => r.Id == currentScreening.RoomId);

        if (currentScreening == null || currentRoom == null)
        {
            status.Text = "Chưa có suất chiếu hoặc phòng chiếu để bán vé.";
            legend.Text = "";
            UpdateCountdown();
            return;
        }

        status.Text =
            $"{currentScreening.MovieTitle}  •  {currentRoom.Name}  •  " +
            $"{currentScreening.StartTime:HH:mm dd/MM/yyyy}";

        var sold = AppData.Tickets
            .Where(t => t.ScreeningId == currentScreening.Id)
            .Select(t => t.Seat)
            .ToHashSet();

        seats.Controls.Add(new Label
        {
            Text = "MÀN HÌNH",
            AutoSize = false,
            Width = Math.Max(420, currentRoom.SeatsPerRow * 60 + 42),
            Height = 26,
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.FromArgb(220, 230, 245),
            ForeColor = Ui.MainText,
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            Margin = new Padding(0, 0, 0, 10)
        });

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
                Height = 40,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = vipRow ? Color.DarkGoldenrod : Ui.MainText,
                Font = new Font("Segoe UI", 9, FontStyle.Bold)
            });

            for (int c = 1; c <= currentRoom.SeatsPerRow; c++)
            {
                var name = $"{rowLetter}{c}";
                bool isSold = sold.Contains(name);

                var b = new Button
                {
                    Text = name,
                    Width = 54,
                    Height = 40,
                    Margin = new Padding(3),
                    Tag = name,
                    Enabled = !isSold,
                    ForeColor = Color.Black,
                    BackColor = isSold ? SeatSold : vipRow ? SeatVip : SeatFree
                };

                b.Click += (_, _) => Toggle(name, b);

                rowPanel.Controls.Add(b);
            }

            seats.Controls.Add(rowPanel);
        }

        legend.Text =
            "Xanh lá: ghế trống  •  Vàng: ghế VIP  •  " +
            "Xanh dương: đang chọn  •  Đỏ: đã bán";

        LoadSoldTickets();
        UpdateCountdown();
        UpdateCart();
    }

    void Toggle(string name, Button b)
    {
        if (!selected.Add(name))
        {
            selected.Remove(name);
            b.BackColor = IsVipSeat(name) ? SeatVip : SeatFree;
        }
        else
        {
            b.BackColor = SeatSelected;
        }

        if (selected.Count == 0)
            status.Text = $"Chưa chọn ghế nào  •  {currentScreening?.MovieTitle}";
        else
            status.Text = $"Đang chọn {selected.Count} ghế: {SeatListText()}";

        UpdateCart();
    }

    string SeatListText() => string.Join(", ", selected
        .OrderBy(s => s[0])
        .ThenBy(s => int.Parse(s[1..])));

    bool IsVipSeat(string seat)
    {
        if (currentRoom == null || seat.Length == 0)
            return false;

        return seat[0] - 'A' + 1 <= currentRoom.VipRows;
    }

    SnackCombo? SelectedSnack => snack.SelectedItem as SnackCombo;

    decimal SeatPrice(string seat)
    {
        if (currentRoom == null)
            return 0;

        decimal discount = student.Checked
            ? AppConfig.StudentDiscount
            : AppConfig.AdultDiscount;

        decimal price = AppConfig.BaseTicketPrice
            * (IsVipSeat(seat) ? AppConfig.VipSurcharge : 1m)
            * (1 - discount);

        return decimal.Round(price, 0) + (SelectedSnack?.Price ?? 0);
    }

    void UpdateCart()
    {
        if (currentRoom == null || selected.Count == 0)
        {
            cart.Text = "Chưa chọn ghế nào.";
            return;
        }

        decimal total = selected.Sum(SeatPrice);
        cart.Text =
            $"{selected.Count} ghế: {SeatListText()}  •  Tạm tính {Ui.Money(total)}";
    }

    void LoadSoldTickets()
    {
        soldList.Items.Clear();
        refundButton.Enabled = false;

        if (currentScreening == null)
            return;

        foreach (var t in AppData.Tickets
                     .Where(x => x.ScreeningId == currentScreening.Id)
                     .OrderBy(x => x.Seat))
        {
            var item = new ListViewItem(new[]
            {
                t.Seat,
                t.CustomerType,
                Ui.Money(t.Total),
                t.SoldAt.ToString("HH:mm dd/MM")
            })
            {
                Tag = t,
                ToolTipText =
                    $"Mã vé: {t.Id}\n" +
                    $"Giá vé: {Ui.Money(t.Price)}\n" +
                    $"Bắp nước: {(string.IsNullOrWhiteSpace(t.ComboName) ? "Không" : t.ComboName)}\n" +
                    $"Phụ thu combo: {Ui.Money(t.ComboPrice)}"
            };

            soldList.Items.Add(item);
        }
    }

    void UpdateCountdown()
    {
        if (currentScreening == null)
        {
            soldInfo.Text =
                $"Quy định: chỉ nhận trả vé trước giờ chiếu " +
                $"{AppConfig.RefundBeforeHours} tiếng.";
            return;
        }

        var remaining = currentScreening.StartTime - DateTime.Now;
        string left = remaining <= TimeSpan.Zero
            ? "Suất chiếu đã bắt đầu."
            : remaining.TotalHours >= 1
                ? $"Còn {(int)remaining.TotalHours} giờ {remaining.Minutes} phút tới giờ chiếu."
                : $"Còn {Math.Max(0, remaining.Minutes)} phút tới giờ chiếu.";

        soldInfo.Text =
            $"Quy định: chỉ nhận trả vé trước giờ chiếu {AppConfig.RefundBeforeHours} tiếng.\n" +
            left;
    }

    void Sell()
    {
        try
        {
            if (currentScreening == null || currentRoom == null)
                throw new Exception("Vui lòng chọn suất chiếu.");

            if (currentScreening.StartTime <= DateTime.Now)
            {
                RefreshScreenings();
                throw new Exception("Suất chiếu đã bắt đầu nên không thể bán vé.");
            }

            if (selected.Count == 0)
                throw new Exception("Vui lòng chọn ít nhất một ghế trên sơ đồ.");

            var movie = AppData.Movies.FirstOrDefault(m => m.Id == currentScreening.MovieId)
                ?? throw new Exception("Không tìm thấy phim của suất chiếu.");

            var duplicate = selected.FirstOrDefault(seat => AppData.Tickets.Any(
                t => t.ScreeningId == currentScreening.Id && t.Seat == seat));

            if (duplicate != null)
            {
                BuildSeats();
                throw new Exception($"Ghế {duplicate} vừa được bán, vui lòng chọn ghế khác.");
            }

            string customer = student.Checked ? "Học sinh-SV" : "Người lớn";
            decimal discount = student.Checked
                ? AppConfig.StudentDiscount
                : AppConfig.AdultDiscount;
            var combo = SelectedSnack;

            var created = new List<Ticket>();

            foreach (var seat in selected)
            {
                bool vip = IsVipSeat(seat);
                decimal price = decimal.Round(
                    AppConfig.BaseTicketPrice
                    * (vip ? AppConfig.VipSurcharge : 1m)
                    * (1 - discount),
                    0);

                created.Add(new Ticket
                {
                    Id = AppData.NewTicketId(),
                    ScreeningId = currentScreening.Id,
                    MovieTitle = movie.Title,
                    RoomName = currentRoom.Name,
                    ShowTime = currentScreening.StartTime,
                    Seat = seat,
                    IsVip = vip,
                    CustomerType = customer,
                    Price = price,
                    SoldAt = DateTime.Now,
                    ComboName = combo?.Price > 0 ? combo.Name : "",
                    ComboPrice = combo?.Price ?? 0m
                });
            }

            decimal total = created.Sum(t => t.Total);

            selling = true;
            var confirm = MessageBox.Show(
                $"Xác nhận bán {created.Count} vé?\n\n" +
                $"Phim: {movie.Title}\n" +
                $"Suất: {currentScreening.StartTime:HH:mm dd/MM/yyyy} - {currentRoom.Name}\n" +
                $"Ghế: {SeatListText()}\n" +
                $"Loại vé: {customer}\n" +
                $"Bắp nước: {(combo?.Price > 0 ? combo.Name : "Không")}\n" +
                $"Tổng tiền: {Ui.Money(total)}",
                "Xác nhận bán vé",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button1);
            selling = false;

            if (confirm != DialogResult.Yes)
                return;

            AppData.Tickets.AddRange(created);
            AppData.SaveTickets();

            int failed = 0;
            foreach (var t in created)
            {
                if (!SaveTicketFile(t, Path.Combine(FileHelper.DataDir, $"Ve_{t.Id}.txt")))
                    failed++;
            }

            MessageBox.Show(
                $"Bán thành công {created.Count} vé, tổng {Ui.Money(total)}.\n" +
                (failed == 0
                    ? "Vé TXT đã được lưu trong thư mục Data."
                    : $"{failed} vé không ghi được tập tin, xem Data/log.txt."),
                "Thông báo",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);

            BuildSeats();
        }
        catch (Exception ex)
        {
            selling = false;
            Ui.ShowError(ex);
        }
    }

    void Refund()
    {
        try
        {
            if (soldList.SelectedItems.Count == 0
                || soldList.SelectedItems[0].Tag is not Ticket t)
            {
                throw new Exception("Vui lòng chọn vé cần trả trong danh sách vé đã bán.");
            }

            var remaining = t.ShowTime - DateTime.Now;
            var allowedBefore = TimeSpan.FromHours(AppConfig.RefundBeforeHours);

            if (remaining < allowedBefore)
            {
                string detail = remaining <= TimeSpan.Zero
                    ? "Suất chiếu đã bắt đầu."
                    : $"Suất này chỉ còn {Math.Max(0, (int)remaining.TotalMinutes)} phút nữa là chiếu.";

                throw new Exception(
                    $"Chỉ nhận trả vé trước giờ chiếu ít nhất " +
                    $"{AppConfig.RefundBeforeHours} tiếng.\n{detail}");
            }

            var confirm = MessageBox.Show(
                "Xác nhận trả vé?\n\n" +
                $"Mã vé: {t.Id}\n" +
                $"Phim: {t.MovieTitle}\n" +
                $"Ghế: {t.Seat}\n" +
                $"Loại vé: {t.CustomerType}\n" +
                $"Bắp nước: {(string.IsNullOrWhiteSpace(t.ComboName) ? "Không" : t.ComboName)}\n" +
                $"Số tiền hoàn: {Ui.Money(t.Total)}",
                "Xác nhận trả vé",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes)
                return;

            if (!AppData.Tickets.Remove(t))
                throw new Exception("Không tìm thấy vé cần trả, dữ liệu có thể đã thay đổi.");

            var refund = new Refund
            {
                TicketId = t.Id,
                ScreeningId = t.ScreeningId,
                MovieTitle = t.MovieTitle,
                RoomName = t.RoomName,
                Seat = t.Seat,
                CustomerType = t.CustomerType,
                ComboName = t.ComboName,
                Amount = t.Total,
                ShowTime = t.ShowTime,
                RefundedAt = DateTime.Now
            };

            AppData.Refunds.Add(refund);
            AppData.SaveTickets();
            AppData.SaveRefunds();
            FileHelper.Log(
                $"Trả vé {t.Id} - suất {t.ScreeningId} - ghế {t.Seat} - " +
                $"hoàn {t.Total:0} đ - lý do: khách trả trước giờ chiếu.");

            MessageBox.Show(
                $"Đã trả vé {t.Id}.\nHoàn tiền: {Ui.Money(t.Total)}",
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

    void PrintSelectedTicket()
    {
        try
        {
            if (soldList.SelectedItems.Count == 0
                || soldList.SelectedItems[0].Tag is not Ticket t)
            {
                MessageBox.Show(
                    "Vui lòng chọn vé cần in trong danh sách vé đã bán.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            using var dialog = new SaveFileDialog
            {
                Title = "Lưu vé xem phim",
                Filter = "Tập tin văn bản|*.txt",
                FileName = $"Ve_{t.Id}.txt",
                InitialDirectory = FileHelper.DataDir
            };

            if (dialog.ShowDialog() != DialogResult.OK)
                return;

            if (SaveTicketFile(t, dialog.FileName))
            {
                MessageBox.Show(
                    "Đã in vé ra tập tin:\n" + dialog.FileName,
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
            Ui.ShowError(ex);
        }
    }

    static bool SaveTicketFile(Ticket t, string path)
    {
        try
        {
            FileHelper.EnsureDataDir();
            File.WriteAllText(path, BuildTicketText(t), new UTF8Encoding(true));
            return true;
        }
        catch (Exception ex)
        {
            FileHelper.Log(ex);
            return false;
        }
    }

    static string BuildTicketText(Ticket t) =>
        $"{AppConfig.CinemaName}\r\n" +
        "====================================\r\n" +
        "           VÉ XEM PHIM\r\n" +
        "====================================\r\n" +
        $"Mã vé     : {t.Id}\r\n" +
        $"Phim      : {t.MovieTitle}\r\n" +
        $"Phòng     : {t.RoomName}\r\n" +
        $"Suất chiếu: {t.ShowTime:HH:mm dd/MM/yyyy}\r\n" +
        $"Ghế       : {t.Seat}{(t.IsVip ? " (VIP)" : "")}\r\n" +
        $"Loại vé   : {t.CustomerType}\r\n" +
        $"Bắp nước  : {(string.IsNullOrWhiteSpace(t.ComboName) ? "Không" : t.ComboName)}\r\n" +
        "------------------------------------\r\n" +
        $"Giá vé    : {Ui.Money(t.Price)}\r\n" +
        $"Phụ thu   : {Ui.Money(t.ComboPrice)}\r\n" +
        $"Tổng cộng : {Ui.Money(t.Total)}\r\n" +
        $"Ngày bán  : {t.SoldAt:HH:mm dd/MM/yyyy}\r\n" +
        "====================================\r\n" +
        "Vui lòng giữ vé để soát khi vào phòng.\r\n";
}
