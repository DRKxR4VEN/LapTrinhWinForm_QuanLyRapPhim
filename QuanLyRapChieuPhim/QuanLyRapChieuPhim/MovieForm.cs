namespace QuanLyRapChieuPhim;

public partial class MovieForm : Form
{
    string genreFilter = "";

    public MovieForm()
    {
        InitializeComponent();
        Ui.ApplyAppearance(this);

        if (Ui.DesignTime)
            return;

        RefreshData();
    }

    void pickPosterButton_Click(object? sender, EventArgs e) => PickPoster();

    void addButton_Click(object? sender, EventArgs e) => Execute("Thêm");

    void editButton_Click(object? sender, EventArgs e) => Execute("Sửa");

    void deleteButton_Click(object? sender, EventArgs e) => Execute("Xóa");

    void importButton_Click(object? sender, EventArgs e) => Execute("Nhập CSV");

    void filterButton_Click(object? sender, EventArgs e) => Execute("Lọc thể loại");

    void refreshButton_Click(object? sender, EventArgs e) => Execute("Làm mới");

    void PickPoster()
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "Ảnh|*.png;*.jpg;*.jpeg;*.bmp"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        poster.Text = dialog.FileName;
        try
        {
            using var stream = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read);
            preview.Image = Image.FromStream(stream);
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    void Execute(string action)
    {
        try
        {
            if (action == "Thêm")
            {
                RequireIdAndTitle();
                if (AppData.Movies.Any(x => x.Id == id.Text.Trim()))
                {
                    id.Focus();
                    throw new Exception("Mã phim đã tồn tại.");
                }

                AppData.Movies.Add(ReadInput());
            }
            else if (action == "Sửa")
            {
                RequireIdAndTitle();
                var movie = AppData.Movies.FirstOrDefault(x => x.Id == id.Text.Trim());
                if (movie == null)
                {
                    id.Focus();
                    throw new Exception("Không tìm thấy phim.");
                }

                var updated = ReadInput();
                movie.Title = updated.Title;
                movie.Genre = updated.Genre;
                movie.DurationMinutes = updated.DurationMinutes;
                movie.PosterPath = updated.PosterPath;
                movie.AgeLimit = updated.AgeLimit;
            }
            else if (action == "Xóa")
            {
                if (string.IsNullOrWhiteSpace(id.Text))
                {
                    id.Focus();
                    throw new Exception("Chọn phim cần xóa.");
                }

                var movie = AppData.Movies.FirstOrDefault(x => x.Id == id.Text.Trim());
                if (movie == null)
                {
                    id.Focus();
                    throw new Exception("Không tìm thấy phim.");
                }

                if (AppData.Screenings.Any(s => s.MovieId == movie.Id))
                    throw new Exception("Phim đã có suất chiếu, không thể xóa.");

                if (MessageBox.Show(
                        $"Xóa phim \"{movie.Title}\"?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;

                AppData.Movies.Remove(movie);
            }
            else if (action == "Nhập CSV")
            {
                ImportCsv();
            }
            else if (action == "Lọc thể loại")
            {
                var genreText = Microsoft.VisualBasic.Interaction.InputBox(
                    "Nhập thể loại cần lọc (để trống để bỏ lọc):",
                    "Lọc phim",
                    genreFilter);

                genreFilter = genreText.Trim();
                RefreshData();
                return;
            }
            else if (action == "Làm mới")
            {
                id.Clear();
                title.Clear();
                genre.Clear();
                duration.Clear();
                age.Clear();
                poster.Clear();
                genreFilter = "";
                preview.Image = PosterPlaceholder("Phim", 120, 150, 24);
            }

            AppData.SaveMovies();
            RefreshData();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    void ImportCsv()
    {
        using var dialog = new OpenFileDialog
        {
            Filter = "CSV|*.csv",
            Title = "Nhập danh sách phim từ CSV"
        };

        if (dialog.ShowDialog() != DialogResult.OK)
            return;

        int added = 0;
        int skipped = 0;

        foreach (var line in File.ReadAllLines(dialog.FileName))
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                skipped++;
                continue;
            }

            var parts = line.Split(',');
            if (parts.Length < 4 ||
                string.IsNullOrWhiteSpace(parts[0]) ||
                string.IsNullOrWhiteSpace(parts[1]) ||
                AppData.Movies.Any(m => m.Id == parts[0].Trim()) ||
                !int.TryParse(parts[3].Trim(), out var minutes) ||
                minutes <= 0)
            {
                skipped++;
                continue;
            }

            // Hỗ trợ cả CSV 4 cột (Ma,Ten,TheLoai,ThoiLuong) lẫn 5-6 cột có
            // giới hạn tuổi (P/T13/T16/T18 hoặc số) và đường dẫn poster.
            var ageLimit = 0;
            var posterPath = "";

            if (parts.Length > 4)
            {
                if (LooksLikeAge(parts[4]))
                {
                    if (!TryParseAgeLimit(parts[4], out ageLimit))
                    {
                        skipped++;
                        continue;
                    }

                    if (parts.Length > 5 && !string.IsNullOrWhiteSpace(parts[5]))
                        posterPath = parts[5].Trim();
                }
                else
                {
                    posterPath = parts[4].Trim();
                    if (parts.Length > 5 && !TryParseAgeLimit(parts[5], out ageLimit))
                    {
                        skipped++;
                        continue;
                    }
                }
            }

            AppData.Movies.Add(new Movie
            {
                Id = parts[0].Trim(),
                Title = parts[1].Trim(),
                Genre = parts[2].Trim(),
                DurationMinutes = minutes,
                PosterPath = posterPath,
                AgeLimit = ageLimit
            });
            added++;
        }

        if (skipped > 0)
        {
            FileHelper.Log(
                $"Nhập CSV {dialog.FileName}: thêm {added} phim, " +
                $"bỏ qua {skipped} dòng trống/trùng/sai định dạng.");
        }

        MessageBox.Show(
            $"Đã nhập {added} phim từ CSV." +
            (skipped > 0 ? $"\nBỏ qua {skipped} dòng không hợp lệ." : ""),
            "Nhập CSV",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    void RequireIdAndTitle()
    {
        if (string.IsNullOrWhiteSpace(id.Text))
        {
            id.Focus();
            throw new Exception("Nhập mã phim.");
        }

        if (string.IsNullOrWhiteSpace(title.Text))
        {
            title.Focus();
            throw new Exception("Nhập tên phim.");
        }
    }

    Movie ReadInput()
    {
        if (!int.TryParse(duration.Text, out var minutes) || minutes <= 0)
        {
            duration.Focus();
            throw new Exception("Thời lượng phải là số phút dương.");
        }

        return new Movie
        {
            Id = id.Text.Trim(),
            Title = title.Text.Trim(),
            Genre = genre.Text.Trim(),
            DurationMinutes = minutes,
            PosterPath = poster.Text.Trim(),
            AgeLimit = ParseAgeLimit()
        };
    }

    int ParseAgeLimit()
    {
        if (TryParseAgeLimit(age.Text, out var limit))
            return limit;

        age.Focus();
        throw new Exception(
            "Giới hạn tuổi phải là P (mọi lứa tuổi) hoặc số từ 1 đến 18.");
    }

    static bool TryParseAgeLimit(string text, out int limit)
    {
        limit = 0;
        text = text.Trim();

        if (text.Length == 0 ||
            text.Equals("P", StringComparison.OrdinalIgnoreCase) ||
            text.Equals("K", StringComparison.OrdinalIgnoreCase))
            return true;

        if (text.StartsWith("T", StringComparison.OrdinalIgnoreCase))
            text = text[1..].Trim();

        return int.TryParse(text, out limit) && limit is >= 1 and <= 18;
    }

    static bool LooksLikeAge(string text)
    {
        text = text.Trim();
        return text.Length == 0 ||
               text.Equals("P", StringComparison.OrdinalIgnoreCase) ||
               text.Equals("K", StringComparison.OrdinalIgnoreCase) ||
               text.StartsWith("T", StringComparison.OrdinalIgnoreCase) ||
               int.TryParse(text, out _);
    }

    static string AgeLabel(int limit) => limit <= 0 ? "P" : $"T{limit}";

    void RefreshData()
    {
        var movies = string.IsNullOrWhiteSpace(genreFilter)
            ? AppData.Movies
            : AppData.Movies
                .Where(x => x.Genre.Contains(genreFilter, StringComparison.OrdinalIgnoreCase))
                .ToList();

        grid.DataSource = movies
            .Select(x => new
            {
                x.Id,
                x.Title,
                x.Genre,
                x.DurationMinutes,
                AgeLimit = AgeLabel(x.AgeLimit)
            })
            .ToList();

        ApplyHeaders();

        listTitle.Text = string.IsNullOrWhiteSpace(genreFilter)
            ? "DANH SÁCH PHIM"
            : $"DANH SÁCH PHIM • {genreFilter}";

        list.Items.Clear();
        posterImages.Images.Clear();

        foreach (var movie in movies)
        {
            try
            {
                if (File.Exists(movie.PosterPath))
                {
                    using var stream = new FileStream(movie.PosterPath, FileMode.Open, FileAccess.Read);
                    posterImages.Images.Add(Image.FromStream(stream));
                }
                else
                {
                    posterImages.Images.Add(PosterPlaceholder(movie.Title, 64, 80, 20));
                }
            }
            catch
            {
                posterImages.Images.Add(PosterPlaceholder(movie.Title, 64, 80, 20));
            }

            list.Items.Add(new ListViewItem(movie.Title, posterImages.Images.Count - 1)
            {
                Tag = movie
            });
        }
    }

    void ApplyHeaders()
    {
        SetHeader("Id", "Mã phim");
        SetHeader("Title", "Tên phim");
        SetHeader("Genre", "Thể loại");
        SetHeader("DurationMinutes", "Thời lượng");
        SetHeader("AgeLimit", "Giới hạn tuổi");
    }

    void SetHeader(string column, string text)
    {
        if (grid.Columns[column] != null)
            grid.Columns[column]!.HeaderText = text;
    }

    void grid_SelectionChanged(object? sender, EventArgs e) => LoadSelected();

    void LoadSelected()
    {
        if (grid.CurrentRow == null)
            return;

        var key = grid.CurrentRow.Cells["Id"]?.Value?.ToString();
        var movie = AppData.Movies.FirstOrDefault(x => x.Id == key);
        if (movie != null)
            ShowMovie(movie);
    }

    void list_ItemActivate(object? sender, EventArgs e) => ActivateListItem();

    void ActivateListItem()
    {
        if (list.SelectedItems.Count == 0 ||
            list.SelectedItems[0].Tag is not Movie movie)
            return;

        ShowMovie(movie);
    }

    void ShowMovie(Movie movie)
    {
        id.Text = movie.Id;
        title.Text = movie.Title;
        genre.Text = movie.Genre;
        duration.Text = movie.DurationMinutes.ToString();
        age.Text = AgeLabel(movie.AgeLimit);
        poster.Text = movie.PosterPath;

        try
        {
            if (File.Exists(movie.PosterPath))
            {
                using var stream = new FileStream(movie.PosterPath, FileMode.Open, FileAccess.Read);
                preview.Image = Image.FromStream(stream);
            }
            else
            {
                preview.Image = PosterPlaceholder(movie.Title, 120, 150, 28);
            }
        }
        catch
        {
            preview.Image = PosterPlaceholder(movie.Title, 120, 150, 28);
        }
    }

    static Image PosterPlaceholder(string title, int width, int height, float fontSize)
    {
        var palette = new[]
        {
            Color.FromArgb(225, 238, 255),
            Color.FromArgb(255, 235, 225),
            Color.FromArgb(230, 248, 233),
            Color.FromArgb(245, 232, 250),
            Color.FromArgb(255, 248, 215)
        };

        var bitmap = new Bitmap(width, height);
        using var graphics = Graphics.FromImage(bitmap);
        graphics.Clear(palette[(title.GetHashCode() & int.MaxValue) % palette.Length]);

        var initials = string.Concat(
            title.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                 .Take(2)
                 .Select(x => char.ToUpperInvariant(x[0])));

        using var font = new Font("Segoe UI", fontSize, FontStyle.Bold);
        TextRenderer.DrawText(
            graphics,
            initials,
            font,
            new Rectangle(0, 0, width, height),
            Ui.Accent,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);

        return bitmap;
    }
}
