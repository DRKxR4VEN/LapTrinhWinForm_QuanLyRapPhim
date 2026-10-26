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
