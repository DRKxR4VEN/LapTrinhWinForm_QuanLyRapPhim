# SV2 – Quản lý suất chiếu: toàn bộ code và giải thích

> Tài liệu này tổng hợp phần **SV2 – Lên lịch suất chiếu** theo đúng source hiện tại của đồ án.
> Code được trích nguyên văn từ các file trong project, không chỉnh sửa.

## 1. Yêu cầu của SV2

Theo đề bài:

- Lên lịch suất chiếu cho từng phòng.
- Không cho hai suất chồng giờ trong cùng một phòng.
- Thời gian chiếm phòng = **thời lượng phim + 15 phút dọn phòng**.
- Hiển thị lịch dưới dạng cây **Ngày → Phim → Suất** bằng `TreeView`.
- Chỉ được hủy suất chiếu **chưa bán vé**.

## 2. Các file liên quan

| File | Vai trò |
|---|---|
| `ScreeningForm.cs` | Toàn bộ giao diện và xử lý của SV2 |
| `Models.cs` | Lớp `Screening`, `Movie`, `CinemaRoom` |
| `AppData.cs` | Đọc/ghi dữ liệu, kiểm tra trùng lịch |
| `App.config` | Cấu hình `CleaningMinutes = 15` |
| `Data/screenings.txt` | Dữ liệu suất chiếu |
| `Data/movies.txt` | Dữ liệu phim, dùng để lấy thời lượng |
| `Data/rooms.txt` | Dữ liệu phòng chiếu |
| `Data/tickets.txt` | Dùng để kiểm tra suất đã bán vé chưa |
| `MainForm.cs` | Menu mở form Quản lý suất chiếu |

## 3. Cấu trúc dữ liệu

### 3.1. `Data/screenings.txt`

Mỗi dòng là một suất chiếu, các trường cách nhau bởi dấu `;`:

```text
Ma;MaPhim;MaPhong;NgayGio;TenPhim;TenPhong
```

Ví dụ:

```text
SC20261026180000A1B;PC001;PC01;2026-10-26T18:00:00.0000000+07:00;Conan;Phòng 1
```

- `Ma`: mã suất chiếu.
- `MaPhim`: mã phim.
- `MaPhong`: mã phòng.
- `NgayGio`: thời gian bắt đầu, lưu theo chuẩn `O` (round-trip).
- `TenPhim`, `TenPhong`: lưu kèm để hiển thị khi dữ liệu phim/phòng bị thiếu.

### 3.2. Các cấu hình liên quan

`App.config`:

```xml
<add key="CleaningMinutes" value="15" />
```

`CleaningMinutes` được đọc trong `AppConfig`:

```csharp
public static int CleaningMinutes => int.TryParse(Get("CleaningMinutes","15"), out var n) ? n : 15;
```

## 4. Toàn bộ code

### 4.1. `App.config`

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <appSettings>
    <add key="CinemaName" value="RẠP CHIẾU PHIM SAO MAI" />
    <add key="DataFolder" value="Data" />
    <add key="BaseTicketPrice" value="80000" />
    <add key="VipSurcharge" value="1.5" />
    <add key="StudentDiscount" value="0.15" />
    <add key="AdultDiscount" value="0" />
    <add key="CleaningMinutes" value="15" />
    <add key="RefundBeforeHours" value="1" />
    <add key="SnackCombos" value="Bắp + nước nhỏ:55000,Bắp + nước lớn:75000,Combo đôi:110000" />
  </appSettings>
</configuration>

```

### 4.2. `Models.cs` – các lớp `Movie`, `CinemaRoom`, `Screening`

```csharp
public class Movie
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Genre { get; set; } = "";
    public int DurationMinutes { get; set; }
    public string PosterPath { get; set; } = "";
    public override string ToString() => string.Join(";", Id, Title, Genre, DurationMinutes, PosterPath);
    public static Movie FromLine(string s) { var p=s.Split(';'); return new Movie { Id=p[0], Title=p[1], Genre=p[2], DurationMinutes=int.Parse(p[3]), PosterPath=p.Length>4?p[4]:"" }; }
}

public class CinemaRoom
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Rows { get; set; } = 5;
    public int SeatsPerRow { get; set; } = 8;
    public int VipRows { get; set; } = 1;
    public int Capacity => Rows * SeatsPerRow;
    public override string ToString() => string.Join(";", Id, Name, Rows, SeatsPerRow, VipRows);
    public static CinemaRoom FromLine(string s) { var p=s.Split(';'); return new CinemaRoom { Id=p[0], Name=p[1], Rows=int.Parse(p[2]), SeatsPerRow=int.Parse(p[3]), VipRows=int.Parse(p[4]) }; }
}

public class Screening
{
    public string Id { get; set; } = "";
    public string MovieId { get; set; } = "";
    public string RoomId { get; set; } = "";
    public DateTime StartTime { get; set; }
    public string MovieTitle { get; set; } = "";
    public string RoomName { get; set; } = "";
    public DateTime EndTime(int duration) => StartTime.AddMinutes(duration + AppConfig.CleaningMinutes);
    public string Display => $"{MovieTitle} | {RoomName} | {StartTime:HH:mm dd/MM/yyyy}";
    public override string ToString() => string.Join(";", Id, MovieId, RoomId, StartTime.ToString("O"), MovieTitle, RoomName);
    public static Screening FromLine(string s) { var p=s.Split(';'); return new Screening { Id=p[0], MovieId=p[1], RoomId=p[2], StartTime=DateTime.Parse(p[3], null, DateTimeStyles.RoundtripKind), MovieTitle=p.Length>4?p[4]:"", RoomName=p.Length>5?p[5]:"" }; }
}
```

### 4.3. `AppData.cs` – khởi tạo, lưu dữ liệu và kiểm tra trùng lịch

```csharp
    public static void Initialize()
    {
        FileHelper.EnsureDataDir();
        Movies=FileHelper.Read("movies.txt", Movie.FromLine);
        Rooms=FileHelper.Read("rooms.txt", CinemaRoom.FromLine);
        Screenings=FileHelper.Read("screenings.txt", Screening.FromLine);
        Tickets=FileHelper.Read("tickets.txt", Ticket.FromLine);
        Refunds=FileHelper.Read("trave.txt", Refund.FromLine);
        Users=FileHelper.Read("users.txt", AppUser.FromLine);
        if (Users.Count==0)
        {
            Users.Add(new AppUser { Username="admin", Password="123", Role="QuanTri" });
            Users.Add(new AppUser { Username="nhanvien", Password="123", Role="NhanVien" });
            SaveUsers();
        }
    }
    public static void SaveMovies()=>FileHelper.Write("movies.txt",Movies);
    public static void SaveRooms()=>FileHelper.Write("rooms.txt",Rooms);
    public static void SaveScreenings()=>FileHelper.Write("screenings.txt",Screenings);
    public static void SaveTickets()=>FileHelper.Write("tickets.txt",Tickets);
    public static void SaveRefunds()=>FileHelper.Write("trave.txt",Refunds);
    public static void SaveUsers()=>FileHelper.Write("users.txt",Users);

    /// <summary>Sinh mã vé mới, không trùng với vé đã có.</summary>
    public static string NewTicketId()
    {
        string id;
        do
        {
            id = "V" + Guid.NewGuid().ToString("N")[..8].ToUpperInvariant();
        }
        while (Tickets.Any(t => t.Id == id) || Refunds.Any(r => r.TicketId == id));

        return id;
    }
    /// <summary>
    /// Tìm suất chiếu bị trùng giờ trong cùng phòng.
    /// Khoảng chiếm phòng = thời lượng phim + số phút dọn phòng trong App.config.
    /// </summary>
    public static Screening? FindConflict(Screening candidate, Movie movie)
    {
        var candidateStart = candidate.StartTime;
        var candidateEnd = candidateStart.AddMinutes(
            movie.DurationMinutes + AppConfig.CleaningMinutes);

        foreach (var s in Screenings.Where(
            x => x.RoomId == candidate.RoomId && x.Id != candidate.Id))
        {
            var oldMovie = Movies.FirstOrDefault(x => x.Id == s.MovieId);

            if (oldMovie == null)
            {
                // Không tính được giờ kết thúc nếu dữ liệu phim bị thiếu.
                FileHelper.Log(new Exception(
                    $"Không tìm thấy phim {s.MovieId} của suất {s.Id}; " +
                    "bỏ qua suất này khi kiểm tra trùng lịch."));
                continue;
            }

            var oldStart = s.StartTime;
            var oldEnd = oldStart.AddMinutes(
                oldMovie.DurationMinutes + AppConfig.CleaningMinutes);

            // Hai khoảng [start, end) giao nhau.
            if (candidateStart < oldEnd && oldStart < candidateEnd)
                return s;
        }

        return null;
    }

    public static bool HasConflict(Screening candidate, Movie movie)
        => FindConflict(candidate, movie) != null;
    public static bool HasSoldTickets(string screeningId)=>Tickets.Any(t=>t.ScreeningId==screeningId);
```

### 4.4. `ScreeningForm.cs` – toàn bộ code SV2

```csharp
using System.Globalization;

namespace QuanLyRapChieuPhim;

/// <summary>
/// Phân hệ SV2 - Quản lý suất chiếu.
/// - Lên lịch suất chiếu cho từng phòng.
/// - Chặn trùng giờ trong cùng phòng (thời lượng phim + thời gian dọn phòng).
/// - Hiển thị lịch dạng cây Ngày -> Phim -> Suất.
/// - Chỉ cho hủy suất chiếu chưa bán vé.
/// </summary>
public class ScreeningForm : Form
{
    static readonly CultureInfo Vietnamese = CultureInfo.GetCultureInfo("vi-VN");

    readonly TreeView tree = new()
    {
        Dock = DockStyle.Fill,
        BackColor = Color.White,
        ForeColor = Ui.MainText,
        BorderStyle = BorderStyle.FixedSingle,
        HideSelection = false,
        FullRowSelect = true,
        ItemHeight = 24,
        ShowNodeToolTips = true
    };

    readonly ComboBox movie = new()
    {
        DropDownStyle = ComboBoxStyle.DropDownList,
        Width = 360
    };

    readonly ComboBox room = new()
    {
        DropDownStyle = ComboBoxStyle.DropDownList,
        Width = 360
    };

    readonly DateTimePicker startDate = new()
    {
        Format = DateTimePickerFormat.Custom,
        CustomFormat = "dd/MM/yyyy",
        Width = 225
    };

    readonly DateTimePicker startTime = new()
    {
        Format = DateTimePickerFormat.Custom,
        CustomFormat = "HH:mm",
        ShowUpDown = true,
        Width = 125
    };

    readonly Label movieInfo = new()
    {
        AutoSize = true,
        ForeColor = Color.FromArgb(90, 100, 115),
        Margin = new Padding(0, 0, 0, 6)
    };

    readonly Label endInfo = new()
    {
        AutoSize = true,
        MaximumSize = new Size(380, 0),
        ForeColor = Color.FromArgb(35, 110, 220),
        Margin = new Padding(0, 0, 0, 8)
    };

    readonly Label selectionInfo = new()
    {
        AutoSize = true,
        MaximumSize = new Size(380, 0),
        Padding = new Padding(10),
        BackColor = Color.FromArgb(245, 247, 250),
        ForeColor = Ui.MainText,
        BorderStyle = BorderStyle.FixedSingle,
        Margin = new Padding(0, 8, 0, 0)
    };

    readonly Label hint = new()
    {
        AutoSize = true,
        ForeColor = Color.FromArgb(90, 100, 115),
        MaximumSize = new Size(380, 0),
        Margin = new Padding(0, 4, 0, 0)
    };

    readonly Button addButton;
    readonly Button cancelButton;
    readonly Button refreshButton;

    public ScreeningForm()
    {
        Text = "Quản lý suất chiếu";
        Width = 1100;
        Height = 680;
        MinimumSize = new Size(1000, 600);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Ui.MainBack;
        ForeColor = Ui.MainText;
        Font = new Font("Segoe UI", 9);

        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            ColumnCount = 2,
            RowCount = 1,
            Padding = new Padding(8),
            BackColor = Ui.MainBack
        };
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 57f));
        layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 43f));
        layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));

        // ---------------- Cột trái: cây lịch chiếu ----------------
        var left = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(8),
            BackColor = Ui.MainBack
        };

        var leftTitle = new Label
        {
            Text = "LỊCH CHIẾU",
            Dock = DockStyle.Top,
            Height = 32,
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            ForeColor = Ui.MainText
        };

        var leftHint = new Label
        {
            Text = "Cây lịch: Ngày → Phim → Suất. Chọn đúng dòng suất chiếu để xem chi tiết hoặc hủy.",
            Dock = DockStyle.Bottom,
            Height = 44,
            ForeColor = Color.FromArgb(90, 100, 115),
            TextAlign = ContentAlignment.MiddleLeft
        };

        var treeHost = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 4, 0, 4) };
        treeHost.Controls.Add(tree);

        left.Controls.Add(treeHost);
        left.Controls.Add(leftHint);
        left.Controls.Add(leftTitle);

        // ---------------- Cột phải: form lên lịch ----------------
        var right = new Panel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(8),
            BackColor = Ui.MainBack
        };

        var card = new Panel
        {
            Dock = DockStyle.Fill,
            BackColor = Color.White,
            Padding = new Padding(16, 12, 16, 12),
            BorderStyle = BorderStyle.FixedSingle,
            AutoScroll = true
        };

        var form = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false,
            AutoScroll = true,
            BackColor = Color.White
        };

        var formTitle = new Label
        {
            Text = "LÊN LỊCH SUẤT CHIẾU",
            AutoSize = true,
            Font = new Font("Segoe UI", 12, FontStyle.Bold),
            ForeColor = Ui.MainText,
            Margin = new Padding(0, 0, 0, 8)
        };

        form.Controls.Add(formTitle);
        form.Controls.Add(Field("Phim", movie));
        form.Controls.Add(Field("Phòng chiếu", room));
        form.Controls.Add(DateTimeField());

        movieInfo.Text = "Chọn phim để xem thời lượng.";
        endInfo.Text = "";
        form.Controls.Add(movieInfo);
        form.Controls.Add(endInfo);

        var buttons = new FlowLayoutPanel
        {
            AutoSize = false,
            Width = 380,
            Height = 104,
            WrapContents = true,
            BackColor = Color.White,
            Margin = new Padding(0, 0, 0, 6)
        };

        addButton = Ui.Btn("Thêm suất chiếu");
        addButton.Click += (_, _) => AddScreening();

        cancelButton = Ui.Btn("Hủy suất đã chọn");
        cancelButton.BackColor = Color.Firebrick;
        cancelButton.Enabled = false;
        cancelButton.Click += (_, _) => CancelScreening();

        refreshButton = Ui.Btn("Làm mới");
        refreshButton.BackColor = Color.FromArgb(90, 100, 115);
        refreshButton.Click += (_, _) => ReloadData();

        buttons.Controls.Add(addButton);
        buttons.Controls.Add(cancelButton);
        buttons.Controls.Add(refreshButton);
        form.Controls.Add(buttons);

        var separator = new Panel
        {
            Width = 60,
            Height = 1,
            BackColor = Color.FromArgb(220, 225, 232),
            Margin = new Padding(0, 6, 0, 8)
        };
        form.Controls.Add(separator);

        var selectionTitle = new Label
        {
            Text = "THÔNG TIN SUẤT ĐANG CHỌN",
            AutoSize = true,
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            ForeColor = Ui.MainText,
            Margin = new Padding(0, 0, 0, 2)
        };
        form.Controls.Add(selectionTitle);

        selectionInfo.Text = "Chưa chọn suất chiếu nào.";
        form.Controls.Add(selectionInfo);

        hint.Text = $"Mỗi suất chiếu được cộng thêm {AppConfig.CleaningMinutes} phút dọn phòng khi kiểm tra trùng lịch.";
        form.Controls.Add(hint);

        card.Controls.Add(form);
        right.Controls.Add(card);

        layout.Controls.Add(left, 0, 0);
        layout.Controls.Add(right, 1, 0);
        Controls.Add(layout);

        movie.SelectedIndexChanged += (_, _) => UpdatePreview();
        room.SelectedIndexChanged += (_, _) => UpdatePreview();
        startDate.ValueChanged += (_, _) => UpdatePreview();
        startTime.ValueChanged += (_, _) => UpdatePreview();
        tree.AfterSelect += (_, _) => ShowSelection();

        SetDefaultStart();
        ReloadData();
    }

    static Panel Field(string caption, Control input)
    {
        var wrap = new Panel
        {
            Width = 380,
            Height = 58,
            Margin = new Padding(0, 0, 0, 2),
            BackColor = Color.White
        };

        var label = new Label
        {
            Text = caption,
            AutoSize = true,
            Location = new Point(0, 0),
            ForeColor = Color.FromArgb(90, 100, 115)
        };

        input.Location = new Point(0, 22);
        input.Width = 360;

        wrap.Controls.Add(label);
        wrap.Controls.Add(input);
        return wrap;
    }

    Panel DateTimeField()
    {
        var wrap = new Panel
        {
            Width = 380,
            Height = 58,
            Margin = new Padding(0, 0, 0, 2),
            BackColor = Color.White
        };

        var label = new Label
        {
            Text = "Ngày và giờ bắt đầu",
            AutoSize = true,
            Location = new Point(0, 0),
            ForeColor = Color.FromArgb(90, 100, 115)
        };

        startDate.Location = new Point(0, 22);
        startTime.Location = new Point(235, 22);

        wrap.Controls.Add(label);
        wrap.Controls.Add(startDate);
        wrap.Controls.Add(startTime);
        return wrap;
    }

    /// <summary>Nạp lại danh sách phim/phòng và vẽ lại cây lịch.</summary>
    void ReloadData()
    {
        RefreshChoices();
        RefreshTree();
        UpdatePreview();
    }

    void RefreshChoices()
    {
        var selectedMovieId = (movie.SelectedItem as Movie)?.Id;
        var selectedRoomId = (room.SelectedItem as CinemaRoom)?.Id;

        var movies = AppData.Movies.OrderBy(x => x.Title).ToList();
        var rooms = AppData.Rooms.OrderBy(x => x.Name).ToList();

        movie.DataSource = null;
        movie.DisplayMember = nameof(Movie.Title);
        movie.ValueMember = nameof(Movie.Id);
        movie.DataSource = movies;

        room.DataSource = null;
        room.DisplayMember = nameof(CinemaRoom.Name);
        room.ValueMember = nameof(CinemaRoom.Id);
        room.DataSource = rooms;

        if (selectedMovieId != null)
        {
            var index = movies.FindIndex(x => x.Id == selectedMovieId);
            if (index >= 0 && index < movie.Items.Count) movie.SelectedIndex = index;
        }

        if (selectedRoomId != null)
        {
            var index = rooms.FindIndex(x => x.Id == selectedRoomId);
            if (index >= 0 && index < room.Items.Count) room.SelectedIndex = index;
        }

        // Không để người dùng bấm thêm khi còn thiếu dữ liệu nền.
        addButton.Enabled = AppData.Movies.Count > 0 && AppData.Rooms.Count > 0;

        if (AppData.Movies.Count == 0 || AppData.Rooms.Count == 0)
        {
            hint.Text = "Cần có ít nhất một phim và một phòng chiếu (nhập ở phân hệ Phim/Phòng chiếu) trước khi lên lịch.";
        }
        else
        {
            hint.Text = $"Mỗi suất chiếu được cộng thêm {AppConfig.CleaningMinutes} phút dọn phòng khi kiểm tra trùng lịch.";
        }
    }

    void SetDefaultStart()
    {
        // Gợi ý một mốc thời gian tròn 30 phút trong tương lai gần.
        var t = DateTime.Now.AddMinutes(30);
        var add = (30 - t.Minute % 30) % 30;
        var suggestion = new DateTime(t.Year, t.Month, t.Day, t.Hour, t.Minute, 0).AddMinutes(add);

        startDate.Value = suggestion.Date;
        startTime.Value = suggestion;
    }

    DateTime GetStartTime()
    {
        var d = startDate.Value.Date;
        var t = startTime.Value;
        return new DateTime(d.Year, d.Month, d.Day, t.Hour, t.Minute, 0);
    }

    void UpdatePreview()
    {
        var m = movie.SelectedItem as Movie;
        if (m == null)
        {
            movieInfo.Text = "Chưa có phim để lên lịch.";
            endInfo.Text = "";
            return;
        }

        movieInfo.Text = $"Thời lượng phim: {m.DurationMinutes} phút  •  Dọn phòng: {AppConfig.CleaningMinutes} phút";

        var start = GetStartTime();
        var finish = start.AddMinutes(m.DurationMinutes);
        var roomFree = finish.AddMinutes(AppConfig.CleaningMinutes);

        endInfo.Text = $"Kết thúc phim: {finish:HH:mm dd/MM/yyyy}  •  Phòng trống lại: {roomFree:HH:mm}";
        endInfo.ForeColor = start <= DateTime.Now
            ? Color.Firebrick
            : Color.FromArgb(35, 110, 220);
    }

    void AddScreening()
    {
        try
        {
            if (movie.SelectedItem is not Movie m)
                throw new Exception("Vui lòng chọn phim cho suất chiếu.");

            if (room.SelectedItem is not CinemaRoom selectedRoom)
                throw new Exception("Vui lòng chọn phòng chiếu.");

            if (m.DurationMinutes <= 0)
                throw new Exception("Phim chưa có thời lượng hợp lệ. Hãy sửa lại ở phần Quản lý phim.");

            var startAt = GetStartTime();
            if (startAt <= DateTime.Now)
                throw new Exception("Giờ bắt đầu phải sau thời điểm hiện tại.");

            var candidate = new Screening
            {
                Id = NextScreeningId(),
                MovieId = m.Id,
                RoomId = selectedRoom.Id,
                StartTime = startAt,
                MovieTitle = m.Title,
                RoomName = selectedRoom.Name
            };

            var conflict = AppData.FindConflict(candidate, m);
            if (conflict != null)
            {
                var conflictMovie = AppData.Movies.FirstOrDefault(x => x.Id == conflict.MovieId);
                var conflictEnd = conflict.StartTime.AddMinutes(
                    (conflictMovie?.DurationMinutes ?? 0) + AppConfig.CleaningMinutes);

                throw new Exception(
                    $"Phòng {selectedRoom.Name} đã có suất chiếu trùng giờ.\n\n" +
                    $"Suất đang chiếm phòng: {conflictMovie?.Title ?? conflict.MovieTitle}\n" +
                    $"Thời gian: {conflict.StartTime:HH:mm dd/MM/yyyy} - {conflictEnd:HH:mm} " +
                    $"(đã gồm {AppConfig.CleaningMinutes} phút dọn phòng).\n\n" +
                    "Vui lòng chọn giờ khác hoặc phòng khác.");
            }

            AppData.Screenings.Add(candidate);
            AppData.SaveScreenings();
            RefreshTree();
            SelectScreening(candidate.Id);
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    void CancelScreening()
    {
        try
        {
            if (tree.SelectedNode?.Tag is not Screening s)
            {
                MessageBox.Show(
                    "Vui lòng chọn đúng dòng suất chiếu (giờ - phòng) trong cây lịch.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                tree.Focus();
                return;
            }

            var soldCount = AppData.Tickets.Count(t => t.ScreeningId == s.Id);
            if (soldCount > 0)
            {
                MessageBox.Show(
                    $"Suất chiếu này đã bán {soldCount} vé nên không thể hủy.\n" +
                    "Chỉ được hủy suất chiếu chưa bán vé.",
                    "Không thể hủy",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn hủy suất chiếu?\n\n" +
                $"Phim: {s.MovieTitle}\n" +
                $"Phòng: {s.RoomName}\n" +
                $"Thời gian: {s.StartTime:HH:mm dd/MM/yyyy}",
                "Xác nhận hủy suất chiếu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes)
                return;

            if (!AppData.Screenings.Remove(s))
                throw new Exception("Không tìm thấy suất chiếu cần hủy. Dữ liệu có thể đã thay đổi.");

            AppData.SaveScreenings();
            RefreshTree();
            selectionInfo.Text = "Đã hủy suất chiếu. Chọn suất khác để xem chi tiết.";
            cancelButton.Enabled = false;
            tree.Focus();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    static string NextScreeningId()
    {
        // Timestamp + hậu tố ngẫu nhiên để không trùng mã suất chiếu.
        return "SC" + DateTime.Now.ToString("yyyyMMddHHmmssfff") +
               Guid.NewGuid().ToString("N")[..3].ToUpperInvariant();
    }

    void RefreshTree()
    {
        var selectedId = (tree.SelectedNode?.Tag as Screening)?.Id;

        tree.BeginUpdate();
        try
        {
            tree.Nodes.Clear();

            var screenings = AppData.Screenings
                .OrderBy(x => x.StartTime)
                .ToList();

            if (screenings.Count == 0)
            {
                var empty = tree.Nodes.Add("Chưa có suất chiếu nào");
                empty.ForeColor = Color.Gray;
                empty.Tag = null;
            }
            else
            {
                foreach (var day in screenings.GroupBy(x => x.StartTime.Date))
                {
                    var dayNode = tree.Nodes.Add(
                        $"{day.Key.ToString("dddd, dd/MM/yyyy", Vietnamese)}  ({day.Count()} suất)");
                    dayNode.ForeColor = day.Key < DateTime.Today
                        ? Color.Gray
                        : Ui.Accent;

                    foreach (var byMovie in day.GroupBy(x => x.MovieId))
                    {
                        var m = AppData.Movies.FirstOrDefault(x => x.Id == byMovie.Key);
                        var title = m?.Title ?? byMovie.First().MovieTitle;
                        var duration = m?.DurationMinutes ?? 0;

                        var movieNode = dayNode.Nodes.Add(
                            $"{title}" +
                            (duration > 0 ? $"  ({duration} phút)" : "") +
                            $"  •  {byMovie.Count()} suất");

                        movieNode.ForeColor = day.Key < DateTime.Today
                            ? Color.Gray
                            : Ui.MainText;

                        foreach (var s in byMovie.OrderBy(x => x.StartTime))
                        {
                            var selectedRoom = AppData.Rooms.FirstOrDefault(x => x.Id == s.RoomId);
                            var capacity = selectedRoom?.Capacity ?? 0;
                            var sold = AppData.Tickets.Count(t => t.ScreeningId == s.Id);
                            var finish = duration > 0
                                ? s.StartTime.AddMinutes(duration)
                                : s.StartTime;
                            var free = finish.AddMinutes(AppConfig.CleaningMinutes);

                            var node = movieNode.Nodes.Add(
                                $"{s.StartTime:HH:mm} - {free:HH:mm}  |  " +
                                $"{selectedRoom?.Name ?? s.RoomName}  |  " +
                                $"{sold}/{capacity} vé");

                            node.Tag = s;
                            node.ToolTipText =
                                $"Phim: {title}\n" +
                                $"Phòng: {selectedRoom?.Name ?? s.RoomName}\n" +
                                $"Bắt đầu: {s.StartTime:HH:mm dd/MM/yyyy}\n" +
                                $"Kết thúc: {finish:HH:mm} (dọn phòng đến {free:HH:mm})\n" +
                                $"Đã bán: {sold}/{capacity} vé";

                            if (free <= DateTime.Now)
                                node.ForeColor = Color.Gray;
                            else if (s.StartTime <= DateTime.Now)
                                node.ForeColor = Color.DarkOrange;
                            else if (capacity > 0 && sold >= capacity)
                                node.ForeColor = Color.Firebrick;
                            else
                                node.ForeColor = Color.FromArgb(30, 120, 70);
                        }
                    }
                }
            }

            tree.ExpandAll();
        }
        finally
        {
            tree.EndUpdate();
        }

        if (selectedId != null)
            SelectScreening(selectedId);
        else
            ShowSelection();
    }

    void SelectScreening(string screeningId)
    {
        var node = FindNode(tree.Nodes, screeningId);
        if (node == null)
        {
            ShowSelection();
            return;
        }

        tree.SelectedNode = node;
        node.EnsureVisible();
    }

    static TreeNode? FindNode(TreeNodeCollection nodes, string screeningId)
    {
        foreach (TreeNode node in nodes)
        {
            if (node.Tag is Screening s && s.Id == screeningId)
                return node;

            var child = FindNode(node.Nodes, screeningId);
            if (child != null)
                return child;
        }

        return null;
    }

    void ShowSelection()
    {
        if (tree.SelectedNode?.Tag is not Screening s)
        {
            selectionInfo.Text = "Chưa chọn suất chiếu nào.\nHãy chọn một dòng suất chiếu cụ thể trong cây lịch.";
            cancelButton.Enabled = false;
            return;
        }

        cancelButton.Enabled = true;

        var m = AppData.Movies.FirstOrDefault(x => x.Id == s.MovieId);
        var r = AppData.Rooms.FirstOrDefault(x => x.Id == s.RoomId);
        var sold = AppData.Tickets.Count(t => t.ScreeningId == s.Id);
        var capacity = r?.Capacity ?? 0;
        var finish = s.StartTime.AddMinutes(m?.DurationMinutes ?? 0);
        var free = finish.AddMinutes(AppConfig.CleaningMinutes);

        selectionInfo.Text =
            $"Phim: {m?.Title ?? s.MovieTitle}\r\n" +
            $"Phòng: {r?.Name ?? s.RoomName}\r\n" +
            $"Bắt đầu: {s.StartTime:HH:mm dd/MM/yyyy}\r\n" +
            $"Kết thúc phim: {finish:HH:mm}\r\n" +
            $"Phòng trống lại: {free:HH:mm} (+{AppConfig.CleaningMinutes} phút dọn phòng)\r\n" +
            $"Vé đã bán: {sold}/{capacity}";

        selectionInfo.ForeColor = sold >= capacity && capacity > 0
            ? Color.Firebrick
            : Ui.MainText;
    }
}

```

### 4.5. `MainForm.cs` – menu mở form SV2

```csharp
        var manage=new ToolStripMenuItem("Quản lý");
        if(isAdmin)
        {
            manage.DropDownItems.Add(new ToolStripMenuItem("Phim",null,(_,_)=>Open(new MovieForm())));
            manage.DropDownItems.Add(new ToolStripMenuItem("Phòng chiếu",null,(_,_)=>Open(new RoomForm())));
            manage.DropDownItems.Add(new ToolStripMenuItem("Suất chiếu",null,(_,_)=>Open(new ScreeningForm())));
        }
        manage.DropDownItems.Add(new ToolStripMenuItem("Bán vé",null,(_,_)=>Open(new TicketForm())));
        if(isAdmin)
            manage.DropDownItems.Add(new ToolStripMenuItem("Doanh thu",null,(_,_)=>Open(new RevenueForm())));
        menu.Items.Add(sys); menu.Items.Add(manage); MainMenuStrip=menu; Controls.Add(menu);
        var mdi=Controls.OfType<MdiClient>().FirstOrDefault();
```


## 5. Giải thích chi tiết

### 5.1. Kiến trúc tổng quát

`ScreeningForm` là form con MDI, được mở từ menu **Quản lý → Suất chiếu**.

Giao diện chia làm 2 cột:

- **Cột trái (57%)**: `TreeView` hiển thị lịch chiếu theo 3 cấp:
  - Cấp 1: Ngày
  - Cấp 2: Phim
  - Cấp 3: Suất chiếu
- **Cột phải (43%)**: form lên lịch gồm phim, phòng, ngày, giờ, nút thêm/hủy/làm mới và thông tin suất đang chọn.

Dữ liệu dùng chung từ `AppData`:

- `AppData.Movies`: danh sách phim.
- `AppData.Rooms`: danh sách phòng.
- `AppData.Screenings`: danh sách suất chiếu.
- `AppData.Tickets`: dùng để đếm số vé đã bán của một suất.

### 5.2. Khởi tạo giao diện

Trong constructor `ScreeningForm()`:

- Tạo `TableLayoutPanel` 2 cột.
- Tạo `TreeView` ở cột trái.
- Tạo `FlowLayoutPanel` ở cột phải để xếp các field.
- Gắn sự kiện:
  - `movie.SelectedIndexChanged` → `UpdatePreview()`
  - `room.SelectedIndexChanged` → `UpdatePreview()`
  - `startDate.ValueChanged` → `UpdatePreview()`
  - `startTime.ValueChanged` → `UpdatePreview()`
  - `tree.AfterSelect` → `ShowSelection()`
  - Nút **Thêm suất chiếu** → `AddScreening()`
  - Nút **Hủy suất đã chọn** → `CancelScreening()`
  - Nút **Làm mới** → `ReloadData()`

Hai hàm phụ trợ:

- `Field(string caption, Control input)`: tạo một nhãn + control nhập.
- `DateTimeField()`: tạo cặp `DateTimePicker` ngày và giờ.

### 5.3. Nạp dữ liệu và giữ lựa chọn

`ReloadData()` gọi 3 hàm:

```csharp
RefreshChoices();
RefreshTree();
UpdatePreview();
```

`RefreshChoices()`:

- Lấy danh sách phim/phòng từ `AppData`.
- Sắp xếp theo tên để dễ chọn.
- Gán `DisplayMember` và `ValueMember` cho `ComboBox`.
- Cố gắng giữ lại phim/phòng đang chọn bằng cách lưu `Id` trước khi nạp lại.
- Nếu chưa có phim hoặc phòng thì disable nút **Thêm suất chiếu** và hiện hướng dẫn.

`SetDefaultStart()`:

- Lấy thời gian hiện tại + 30 phút.
- Làm tròn đến mốc 30 phút gần nhất.
- Gán vào `DateTimePicker` để người dùng có gợi ý ban đầu.

`GetStartTime()`:

- Kết hợp ngày từ `startDate` và giờ từ `startTime`.
- Trả về một `DateTime` mới, bỏ phần giây/mili giây.

### 5.4. Xem trước giờ kết thúc và giờ phòng trống

`UpdatePreview()`:

```csharp
var start = GetStartTime();
var finish = start.AddMinutes(m.DurationMinutes);
var roomFree = finish.AddMinutes(AppConfig.CleaningMinutes);
```

Ý nghĩa:

- `finish`: giờ phim kết thúc.
- `roomFree`: giờ phòng sẵn sàng cho suất tiếp theo, sau khi cộng thêm 15 phút dọn phòng.
- Nếu `start <= DateTime.Now` thì chữ chuyển màu đỏ để cảnh báo giờ đã qua.

### 5.5. Thêm suất chiếu – `AddScreening()`

Các bước kiểm tra:

1. Phải chọn phim.
2. Phải chọn phòng.
3. Phim phải có thời lượng > 0.
4. Giờ bắt đầu phải sau thời điểm hiện tại.

Sau đó tạo đối tượng `Screening`:

```csharp
var candidate = new Screening
{
    Id = NextScreeningId(),
    MovieId = m.Id,
    RoomId = selectedRoom.Id,
    StartTime = startAt,
    MovieTitle = m.Title,
    RoomName = selectedRoom.Name
};
```

Gọi kiểm tra trùng:

```csharp
var conflict = AppData.FindConflict(candidate, m);
if (conflict != null)
{
    // Hiện thông báo suất bị trùng và không thêm
}
```

Nếu không trùng:

```csharp
AppData.Screenings.Add(candidate);
AppData.SaveScreenings();
RefreshTree();
SelectScreening(candidate.Id);
```

Tức là:

- Thêm vào danh sách trong bộ nhớ.
- Ghi xuống `Data/screenings.txt`.
- Vẽ lại cây.
- Chọn đúng suất vừa thêm.

### 5.6. Thuật toán kiểm tra trùng lịch – `FindConflict()`

Đây là phần quan trọng nhất của SV2.

Mỗi suất chiếu chiếm phòng trong khoảng:

```text
[StartTime, StartTime + DurationMinutes + CleaningMinutes)
```

Với suất mới:

```csharp
var candidateStart = candidate.StartTime;
var candidateEnd = candidateStart.AddMinutes(
    movie.DurationMinutes + AppConfig.CleaningMinutes);
```

Với suất cũ:

```csharp
var oldStart = s.StartTime;
var oldEnd = oldStart.AddMinutes(
    oldMovie.DurationMinutes + AppConfig.CleaningMinutes);
```

Hai khoảng thời gian giao nhau khi:

```csharp
candidateStart < oldEnd && oldStart < candidateEnd
```

Đây là công thức chuẩn cho hai khoảng nửa mở `[start, end)`:

- Nếu suất mới bắt đầu **đúng bằng** giờ phòng trống của suất cũ thì **không trùng**.
- Nếu suất mới bắt đầu **sớm hơn** giờ phòng trống thì **trùng**.

Ví dụ:

- Phim cũ dài 120 phút, bắt đầu 18:00.
- Thời gian chiếm phòng: 18:00 → 20:15 (120 + 15 phút dọn phòng).
- Suất mới bắt đầu 20:15 → hợp lệ.
- Suất mới bắt đầu 20:14 → trùng, bị chặn.
- Suất mới ở phòng khác cùng giờ → hợp lệ.

Một điểm đáng chú ý:

```csharp
foreach (var s in Screenings.Where(
    x => x.RoomId == candidate.RoomId && x.Id != candidate.Id))
```

- Chỉ xét các suất **cùng phòng**.
- Bỏ qua chính suất đang xét (hữu ích nếu sau này thêm chức năng sửa suất chiếu).

Nếu phim của suất cũ bị thiếu dữ liệu, code ghi log và bỏ qua suất đó thay vì làm hỏng toàn bộ chức năng.

### 5.7. Vẽ cây lịch chiếu – `RefreshTree()`

Cây được tạo lại từ danh sách `AppData.Screenings`:

```text
Ngày
 └── Phim
      └── Suất chiếu
```

Cách nhóm:

```csharp
foreach (var day in screenings.GroupBy(x => x.StartTime.Date))
{
    // Tạo node ngày
    foreach (var byMovie in day.GroupBy(x => x.MovieId))
    {
        // Tạo node phim
        foreach (var s in byMovie.OrderBy(x => x.StartTime))
        {
            // Tạo node suất
        }
    }
}
```

Mỗi node suất chiếu hiển thị:

```text
HH:mm - HH:mm | Tên phòng | số vé đã bán / sức chứa
```

Ví dụ:

```text
18:00 - 20:15 | Phòng 1 | 12/40 vé
```

Node suất chiếu được gán:

```csharp
node.Tag = s;
```

Đây là mấu chốt để khi người dùng chọn node, form biết node đó đại diện cho `Screening` nào. Các node ngày và node phim không có `Tag` là `Screening`.

Màu sắc:

- Xám: suất đã qua.
- Cam: suất đang chiếu.
- Đỏ: đã bán hết vé.
- Xanh: còn vé, chưa chiếu.

Sau khi dựng cây:

- `tree.ExpandAll()` để mở toàn bộ.
- Nếu trước đó có suất đang chọn, dùng `FindNode()` để chọn lại.
- `FindNode()` đệ quy tìm node có `Tag` là `Screening` trùng `Id`.

### 5.8. Hủy suất chiếu – `CancelScreening()`

Điều kiện bắt buộc: **chỉ hủy suất chưa bán vé**.

Các bước:

1. Lấy node đang chọn:

```csharp
if (tree.SelectedNode?.Tag is not Screening s)
{
    // Yêu cầu chọn đúng dòng suất chiếu
}
```

2. Đếm số vé đã bán:

```csharp
var soldCount = AppData.Tickets.Count(t => t.ScreeningId == s.Id);
```

3. Nếu `soldCount > 0`:

```csharp
MessageBox.Show(
    $"Suất chiếu này đã bán {soldCount} vé nên không thể hủy.\n" +
    "Chỉ được hủy suất chiếu chưa bán vé.");
return;
```

4. Nếu chưa bán vé, hiện `MessageBox` xác nhận Yes/No.
5. Nếu chọn Yes:

```csharp
AppData.Screenings.Remove(s);
AppData.SaveScreenings();
RefreshTree();
```

6. Cập nhật lại thông tin và disable nút hủy.

Lưu ý: vé đã trả được xóa khỏi `tickets.txt`, nên `AppData.Tickets` chỉ còn các vé đang hiệu lực. Vì vậy một suất đã bán rồi trả hết vé sẽ được xem là “chưa bán vé” tại thời điểm hủy.

### 5.9. Hiển thị thông tin suất đang chọn – `ShowSelection()`

Khi người dùng chọn một node suất chiếu:

- Bật nút **Hủy suất đã chọn**.
- Tìm phim và phòng tương ứng trong `AppData`.
- Đếm số vé đã bán.
- Tính giờ kết thúc phim và giờ phòng trống lại.
- Hiển thị:
  - Phim
  - Phòng
  - Giờ bắt đầu
  - Giờ kết thúc phim
  - Giờ phòng trống lại
  - Số vé đã bán / sức chứa

Nếu đã bán hết vé, thông tin chuyển màu đỏ.

### 5.10. Sinh mã suất chiếu – `NextScreeningId()`

```csharp
return "SC" + DateTime.Now.ToString("yyyyMMddHHmmssfff") +
       Guid.NewGuid().ToString("N")[..3].ToUpperInvariant();
```

- Tiền tố `SC`.
- Thời gian đến mili giây.
- 3 ký tự ngẫu nhiên từ `Guid`.

Cách này gần như không trùng mã, kể cả khi thêm nhiều suất trong cùng một giây.

### 5.11. Ghi dữ liệu và xử lý lỗi

Khi thêm hoặc hủy:

```csharp
AppData.SaveScreenings();
```

`SaveScreenings()` gọi:

```csharp
FileHelper.Write("screenings.txt", Screenings);
```

`FileHelper.Write()`:

- Tạo thư mục `Data` nếu chưa có.
- Ghi UTF-8, không BOM.
- Mỗi dòng là `Screening.ToString()`, các trường cách nhau bởi `;`.

Khi khởi động, `AppData.Initialize()` đọc:

```csharp
Screenings = FileHelper.Read("screenings.txt", Screening.FromLine);
```

`FileHelper.Read()`:

- Tự tạo file rỗng nếu chưa có.
- Đọc từng dòng.
- Dòng sai định dạng bị bỏ qua và ghi vào `Data/log.txt`.
- Không làm hỏng toàn bộ danh sách.

Các thao tác thêm/hủy đều nằm trong `try/catch`; lỗi được đưa qua `Ui.ShowError(ex)`, hàm này ghi log và hiện `MessageBox`.

### 5.12. Một số giới hạn hiện tại

- Chưa có chức năng **sửa** suất chiếu; chỉ thêm và hủy.
- Có thể hủy suất đã qua nếu suất đó chưa bán vé; đề chỉ yêu cầu “chưa bán vé” nên điều này không sai, nhưng có thể chặn thêm nếu muốn.
- Kiểm tra trùng chỉ xét cùng phòng, đúng yêu cầu.
- Nếu thời lượng phim bị sửa sau khi đã lên lịch, việc kiểm tra trùng dùng thời lượng hiện tại của phim; dữ liệu suất chiếu không lưu lại thời lượng tại thời điểm tạo.
- `NextScreeningId()` không kiểm tra trùng tuyệt đối, nhưng xác suất trùng rất thấp.
- `ReloadData()` không đọc lại file từ đĩa; nó dùng dữ liệu trong bộ nhớ `AppData`. Vì các form dùng chung `AppData`, dữ liệu vẫn đồng bộ trong cùng phiên chạy.

## 6. Bảng test case gợi ý cho SV2

| STT | Trường hợp | Dữ liệu | Kết quả mong đợi |
|---|---|---|---|
| 1 | Thêm suất hợp lệ | Phim 120 phút, phòng trống, giờ tương lai | Thêm thành công, cây có suất mới |
| 2 | Trùng giờ cùng phòng | Suất cũ 18:00–20:15, suất mới 19:00 cùng phòng | Báo trùng, không thêm |
| 3 | Đúng giờ phòng trống | Suất cũ 18:00–20:15, suất mới 20:15 cùng phòng | Thêm thành công |
| 4 | Sát giờ phòng trống | Suất mới 20:14 cùng phòng | Báo trùng |
| 5 | Cùng giờ khác phòng | Suất mới 19:00 phòng khác | Thêm thành công |
| 6 | Giờ đã qua | Chọn giờ trước hiện tại | Báo lỗi, không thêm |
| 7 | Phim chưa có thời lượng | Phim có `DurationMinutes <= 0` | Báo lỗi |
| 8 | Thiếu phim/phòng | Chưa chọn phim hoặc phòng | Báo lỗi |
| 9 | Cây lịch | Có nhiều ngày, nhiều phim | Đúng thứ tự Ngày → Phim → Suất |
| 10 | Hủy suất chưa bán | Suất không có vé | Hủy thành công, cây cập nhật |
| 11 | Hủy suất đã bán | Suất có ít nhất 1 vé | Báo không thể hủy |
| 12 | Hủy khi chưa chọn | Không chọn node suất | Báo chọn đúng dòng suất |
| 13 | Lưu và mở lại | Thêm suất, đóng app, mở lại | Suất vẫn còn trong `screenings.txt` |
| 14 | Dòng dữ liệu sai | Thêm dòng thiếu trường vào `screenings.txt` | Bỏ qua dòng lỗi, ghi `log.txt`, app vẫn chạy |
| 15 | Thiếu file dữ liệu | Xóa `screenings.txt` | App tự tạo file rỗng, không crash |
| 16 | Phân quyền | Đăng nhập nhân viên | Không thấy menu Quản lý suất chiếu |

## 7. Kịch bản demo SV2

1. Đăng nhập `admin / 123`.
2. Mở **Quản lý → Suất chiếu**.
3. Chọn một phim, một phòng, ngày/giờ trong tương lai.
4. Bấm **Thêm suất chiếu**.
5. Chỉ vào cây: Ngày → Phim → Suất vừa thêm.
6. Thử thêm một suất khác **cùng phòng, chồng giờ** → hiện thông báo trùng.
7. Thử thêm suất **cùng giờ nhưng khác phòng** → thành công.
8. Chọn một suất chưa bán vé → bấm **Hủy suất đã chọn** → xác nhận.
9. Chọn một suất đã bán vé → bấm hủy → hiện thông báo không thể hủy.
10. Đóng app, mở lại → lịch vẫn còn.

## 8. Câu hỏi vấn đáp gợi ý

**Hỏi: Vì sao phải cộng 15 phút dọn phòng?**  
Đáp: Vì sau khi phim kết thúc, phòng cần được dọn dẹp trước khi đón khách của suất tiếp theo. Vì vậy thời gian phòng bị chiếm = thời lượng phim + 15 phút.

**Hỏi: Hai suất được xem là trùng khi nào?**  
Đáp: Khi khoảng `[start, end)` của suất mới giao với khoảng của suất cũ trong cùng phòng. Code dùng điều kiện `candidateStart < oldEnd && oldStart < candidateEnd`.

**Hỏi: Vì sao dùng `TreeView`?**  
Đáp: Đề yêu cầu hiển thị lịch theo cấu trúc Ngày → Phim → Suất; `TreeView` phù hợp để biểu diễn dữ liệu phân cấp.

**Hỏi: Làm sao biết node nào là suất chiếu?**  
Đáp: Mỗi node suất chiếu được gán `node.Tag = s`, trong đó `s` là đối tượng `Screening`. Khi hủy, code kiểm tra `tree.SelectedNode?.Tag is Screening s`.

**Hỏi: Vì sao chỉ cho hủy suất chưa bán vé?**  
Đáp: Nếu đã bán vé mà hủy suất sẽ ảnh hưởng đến khách hàng. Code đếm số vé trong `AppData.Tickets` theo `ScreeningId`; nếu lớn hơn 0 thì chặn hủy.

**Hỏi: Dữ liệu suất chiếu được lưu ở đâu?**  
Đáp: Lưu trong `Data/screenings.txt`, mỗi dòng một suất, các trường cách nhau bởi `;`. Khi thêm/hủy, gọi `AppData.SaveScreenings()` để ghi lại file.

**Hỏi: Nếu file dữ liệu bị sai một dòng thì sao?**  
Đáp: `FileHelper.Read()` bắt lỗi từng dòng, bỏ qua dòng sai và ghi vào `Data/log.txt`; các dòng hợp lệ vẫn được nạp.

**Hỏi: Vì sao phải kiểm tra `startAt <= DateTime.Now`?**  
Đáp: Không cho lên lịch suất chiếu đã bắt đầu hoặc đã qua, tránh dữ liệu vô nghĩa.

**Hỏi: Nếu phim không còn trong `movies.txt` thì sao?**  
Đáp: Khi kiểm tra trùng, code ghi log và bỏ qua suất cũ đó vì không tính được thời lượng. Cây lịch vẫn dùng `MovieTitle` lưu kèm trong suất chiếu để hiển thị.

**Hỏi: Làm sao giữ được suất đang chọn sau khi vẽ lại cây?**  
Đáp: Trước khi xóa cây, lưu `selectedId`; sau khi dựng lại, gọi `FindNode()` đệ quy để tìm node có `Tag` là `Screening` trùng `Id`, rồi gán `tree.SelectedNode`.
