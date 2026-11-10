using System.Globalization;

namespace QuanLyRapChieuPhim;

/// <summary>
/// Phân hệ SV2 - Quản lý suất chiếu.
/// - Lên lịch suất chiếu cho từng phòng.
/// - Chặn trùng giờ trong cùng phòng (thời lượng phim + thời gian dọn phòng).
/// - Hiển thị lịch dạng cây Ngày -> Phim -> Suất.
/// - Chỉ cho hủy suất chiếu chưa bán vé.
/// </summary>
public partial class ScreeningForm : Form
{
    static readonly CultureInfo Vietnamese = CultureInfo.GetCultureInfo("vi-VN");

    public ScreeningForm()
    {
        InitializeComponent();
        Ui.ApplyAppearance(this);

        if (Ui.DesignTime)
            return;

        SetDefaultStart();
        ReloadData();
    }

    void addButton_Click(object? sender, EventArgs e) => AddScreening();

    void cancelButton_Click(object? sender, EventArgs e) => CancelScreening();

    void refreshButton_Click(object? sender, EventArgs e) => ReloadData();

    void movie_SelectedIndexChanged(object? sender, EventArgs e) => UpdatePreview();

    void room_SelectedIndexChanged(object? sender, EventArgs e) => UpdatePreview();

    void startDate_ValueChanged(object? sender, EventArgs e) => UpdatePreview();

    void startTime_ValueChanged(object? sender, EventArgs e) => UpdatePreview();

    void tree_AfterSelect(object? sender, TreeViewEventArgs e) => ShowSelection();

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
            if (index >= 0 && index < movie.Items.Count)
                movie.SelectedIndex = index;
        }

        if (selectedRoomId != null)
        {
            var index = rooms.FindIndex(x => x.Id == selectedRoomId);
            if (index >= 0 && index < room.Items.Count)
                room.SelectedIndex = index;
        }

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
        var soon = DateTime.Now.AddMinutes(30);
        var add = (30 - soon.Minute % 30) % 30;
        var suggestion = new DateTime(soon.Year, soon.Month, soon.Day, soon.Hour, soon.Minute, 0).AddMinutes(add);

        startDate.Value = suggestion.Date;
        startTime.Value = suggestion;
    }

    DateTime GetStartTime()
    {
        var date = startDate.Value.Date;
        var time = startTime.Value;
        return new DateTime(date.Year, date.Month, date.Day, time.Hour, time.Minute, 0);
    }

    void UpdatePreview()
    {
        if (movie.SelectedItem is not Movie selectedMovie)
        {
            movieInfo.Text = "Chưa có phim để lên lịch.";
            endInfo.Text = "";
            return;
        }

        movieInfo.Text =
            $"Thời lượng phim: {selectedMovie.DurationMinutes} phút  •  " +
            $"Dọn phòng: {AppConfig.CleaningMinutes} phút";

        var start = GetStartTime();
        var finish = start.AddMinutes(selectedMovie.DurationMinutes);
        var roomFree = finish.AddMinutes(AppConfig.CleaningMinutes);

        endInfo.Text =
            $"Kết thúc phim: {finish:HH:mm dd/MM/yyyy}  •  " +
            $"Phòng trống lại: {roomFree:HH:mm}";
        endInfo.ForeColor = start <= DateTime.Now
            ? Color.Firebrick
            : Color.FromArgb(35, 110, 220);
    }

    void AddScreening()
    {
        try
        {
            if (movie.SelectedItem is not Movie selectedMovie)
            {
                movie.Focus();
                throw new Exception("Vui lòng chọn phim cho suất chiếu.");
            }

            if (room.SelectedItem is not CinemaRoom selectedRoom)
            {
                room.Focus();
                throw new Exception("Vui lòng chọn phòng chiếu.");
            }

            if (selectedMovie.DurationMinutes <= 0)
                throw new Exception("Phim chưa có thời lượng hợp lệ. Hãy sửa lại ở phần Quản lý phim.");

            var startAt = GetStartTime();
            if (startAt <= DateTime.Now)
            {
                startTime.Focus();
                throw new Exception("Giờ bắt đầu phải sau thời điểm hiện tại.");
            }

            var candidate = new Screening
            {
                Id = NextScreeningId(),
                MovieId = selectedMovie.Id,
                RoomId = selectedRoom.Id,
                StartTime = startAt,
                MovieTitle = selectedMovie.Title,
                RoomName = selectedRoom.Name
            };

            var conflict = AppData.FindConflict(candidate, selectedMovie);
            if (conflict != null)
            {
                var conflictMovie = AppData.Movies.FirstOrDefault(x => x.Id == conflict.MovieId);
                var conflictEnd = conflict.StartTime.AddMinutes(
                    (conflictMovie?.DurationMinutes ?? 0) + AppConfig.CleaningMinutes);

                startTime.Focus();
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
            if (tree.SelectedNode?.Tag is not Screening screening)
            {
                MessageBox.Show(
                    "Vui lòng chọn đúng dòng suất chiếu (giờ - phòng) trong cây lịch.",
                    "Thông báo",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                tree.Focus();
                return;
            }

            var soldCount = AppData.Tickets.Count(t => t.ScreeningId == screening.Id);
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
                $"Phim: {screening.MovieTitle}\n" +
                $"Phòng: {screening.RoomName}\n" +
                $"Thời gian: {screening.StartTime:HH:mm dd/MM/yyyy}",
                "Xác nhận hủy suất chiếu",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2);

            if (confirm != DialogResult.Yes)
                return;

            if (!AppData.Screenings.Remove(screening))
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
                        var foundMovie = AppData.Movies.FirstOrDefault(x => x.Id == byMovie.Key);
                        var title = foundMovie?.Title ?? byMovie.First().MovieTitle;
                        var duration = foundMovie?.DurationMinutes ?? 0;

                        var movieNode = dayNode.Nodes.Add(
                            $"{title}" +
                            (duration > 0 ? $"  ({duration} phút)" : "") +
                            $"  •  {byMovie.Count()} suất");

                        movieNode.ForeColor = day.Key < DateTime.Today
                            ? Color.Gray
                            : Ui.MainText;

                        foreach (var screening in byMovie.OrderBy(x => x.StartTime))
                        {
                            var selectedRoom = AppData.Rooms.FirstOrDefault(x => x.Id == screening.RoomId);
                            var capacity = selectedRoom?.Capacity ?? 0;
                            var sold = AppData.Tickets.Count(t => t.ScreeningId == screening.Id);
                            var finish = duration > 0
                                ? screening.StartTime.AddMinutes(duration)
                                : screening.StartTime;
                            var free = finish.AddMinutes(AppConfig.CleaningMinutes);

                            var node = movieNode.Nodes.Add(
                                $"{screening.StartTime:HH:mm} - {free:HH:mm}  |  " +
                                $"{selectedRoom?.Name ?? screening.RoomName}  |  " +
                                $"{sold}/{capacity} vé");

                            node.Tag = screening;
                            node.ToolTipText =
                                $"Phim: {title}\n" +
                                $"Phòng: {selectedRoom?.Name ?? screening.RoomName}\n" +
                                $"Bắt đầu: {screening.StartTime:HH:mm dd/MM/yyyy}\n" +
                                $"Kết thúc: {finish:HH:mm} (dọn phòng đến {free:HH:mm})\n" +
                                $"Đã bán: {sold}/{capacity} vé";

                            if (free <= DateTime.Now)
                                node.ForeColor = Color.Gray;
                            else if (screening.StartTime <= DateTime.Now)
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
            if (node.Tag is Screening screening && screening.Id == screeningId)
                return node;

            var child = FindNode(node.Nodes, screeningId);
            if (child != null)
                return child;
        }

        return null;
    }

    void ShowSelection()
    {
        if (tree.SelectedNode?.Tag is not Screening screening)
        {
            selectionInfo.Text = "Chưa chọn suất chiếu nào.\nHãy chọn một dòng suất chiếu cụ thể trong cây lịch.";
            cancelButton.Enabled = false;
            return;
        }

        cancelButton.Enabled = true;

        var foundMovie = AppData.Movies.FirstOrDefault(x => x.Id == screening.MovieId);
        var foundRoom = AppData.Rooms.FirstOrDefault(x => x.Id == screening.RoomId);
        var sold = AppData.Tickets.Count(t => t.ScreeningId == screening.Id);
        var capacity = foundRoom?.Capacity ?? 0;
        var finish = screening.StartTime.AddMinutes(foundMovie?.DurationMinutes ?? 0);
        var free = finish.AddMinutes(AppConfig.CleaningMinutes);

        selectionInfo.Text =
            $"Phim: {foundMovie?.Title ?? screening.MovieTitle}\r\n" +
            $"Phòng: {foundRoom?.Name ?? screening.RoomName}\r\n" +
            $"Bắt đầu: {screening.StartTime:HH:mm dd/MM/yyyy}\r\n" +
            $"Kết thúc phim: {finish:HH:mm}\r\n" +
            $"Phòng trống lại: {free:HH:mm} (+{AppConfig.CleaningMinutes} phút dọn phòng)\r\n" +
            $"Vé đã bán: {sold}/{capacity}";

        selectionInfo.ForeColor = sold >= capacity && capacity > 0
            ? Color.Firebrick
            : Ui.MainText;
    }
}
