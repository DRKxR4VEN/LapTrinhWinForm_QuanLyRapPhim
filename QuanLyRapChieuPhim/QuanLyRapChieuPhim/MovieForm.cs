namespace QuanLyRapChieuPhim;

public class MovieForm : Form
{
    readonly DataGridView grid = Ui.Grid();
    readonly TextBox id = Ui.Box(110), title = Ui.Box(210), genre = Ui.Box(150), duration = Ui.Box(120), poster = Ui.Box(520);
    readonly PictureBox preview = new()
    {
        Width = 120,
        Height = 150,
        SizeMode = PictureBoxSizeMode.Zoom,
        BorderStyle = BorderStyle.FixedSingle,
        BackColor = Color.White,
        Dock = DockStyle.Top
    };
    readonly ListView list = new()
    {
        Dock = DockStyle.Fill,
        View = View.LargeIcon,
        LargeImageList = new ImageList { ImageSize = new Size(64, 80), ColorDepth = ColorDepth.Depth32Bit },
        BackColor = Color.White,
        ForeColor = Ui.MainText,
        BorderStyle = BorderStyle.None
    };

    public MovieForm()
    {
        Text = "Quản lý phim";
        Width = 1120;
        Height = 720;
        MinimumSize = new Size(900, 600);
        StartPosition = FormStartPosition.CenterScreen;
        BackColor = Ui.MainBack;
        ForeColor = Ui.MainText;
        Font = new Font("Segoe UI", 9);

        var top = new TableLayoutPanel
        {
            Dock = DockStyle.Top,
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            ColumnCount = 1,
            RowCount = 3,
            Padding = new Padding(10),
            BackColor = Ui.MainBack
        };
        top.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        top.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        top.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        var fields = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = true,
            BackColor = Ui.MainBack,
            Margin = new Padding(0)
        };
        fields.Controls.Add(Field("Mã phim", id, 120));
        fields.Controls.Add(Field("Tên phim", title, 220));
        fields.Controls.Add(Field("Thể loại", genre, 160));
        fields.Controls.Add(Field("Thời lượng (phút)", duration, 135));

        var posterRow = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = false,
            BackColor = Ui.MainBack,
            Margin = new Padding(0)
        };
        posterRow.Controls.Add(Field("Đường dẫn poster", poster, 560));

        var pick = Ui.Btn("Chọn poster");
        pick.Margin = new Padding(6, 22, 0, 0);
        pick.Click += (_, _) => PickPoster();
        posterRow.Controls.Add(pick);

        var buttons = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = true,
            BackColor = Ui.MainBack,
            Margin = new Padding(0, 6, 0, 0)
        };
        foreach (var text in new[] { "Thêm", "Sửa", "Xóa", "Nhập CSV", "Lọc thể loại", "Làm mới" })
        {
            var b = Ui.Btn(text);
            b.Click += (_, _) => Action(text);
            buttons.Controls.Add(b);
        }

        top.Controls.Add(fields, 0, 0);
        top.Controls.Add(posterRow, 0, 1);
        top.Controls.Add(buttons, 0, 2);

        var content = new Panel { Dock = DockStyle.Fill, BackColor = Color.White, Padding = new Padding(10) };

        var previewPanel = new Panel
        {
            Dock = DockStyle.Right,
            Width = 150,
            BackColor = Color.White,
            Padding = new Padding(6)
        };
        var previewTitle = new Label
        {
            Text = "POSTER",
            Dock = DockStyle.Top,
            Height = 24,
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            ForeColor = Ui.MainText
        };
        previewPanel.Controls.Add(preview);
        previewPanel.Controls.Add(previewTitle);

        var listPanel = new Panel
        {
            Dock = DockStyle.Right,
            Width = 240,
            BackColor = Color.White,
            Padding = new Padding(6)
        };
        var listTitle = new Label
        {
            Text = "DANH SÁCH PHIM",
            Dock = DockStyle.Top,
            Height = 24,
            Font = new Font("Segoe UI", 9, FontStyle.Bold),
            ForeColor = Ui.MainText
        };
        listPanel.Controls.Add(list);
        listPanel.Controls.Add(listTitle);

        content.Controls.Add(grid);
        content.Controls.Add(listPanel);
        content.Controls.Add(previewPanel);

        Controls.Add(content);
        Controls.Add(top);

        RefreshData();
        grid.SelectionChanged += (_, _) => LoadSelected();
        list.ItemActivate += (_, _) => ActivateListItem();
    }

    static Panel Field(string caption, Control input, int width)
    {
        var wrap = new Panel
        {
            Width = width,
            Height = 58,
            Margin = new Padding(0, 0, 10, 4),
            BackColor = Ui.MainBack
        };

        var label = new Label
        {
            Text = caption,
            AutoSize = true,
            Location = new Point(0, 0),
            ForeColor = Color.FromArgb(90, 100, 115)
        };

        input.Location = new Point(0, 22);
        input.Width = width - 8;

        wrap.Controls.Add(label);
        wrap.Controls.Add(input);
        return wrap;
    }

    void PickPoster()
    {
        using var d = new OpenFileDialog { Filter = "Ảnh|*.png;*.jpg;*.jpeg;*.bmp" };
        if (d.ShowDialog() != DialogResult.OK) return;

        poster.Text = d.FileName;
        try
        {
            using var fs = new FileStream(d.FileName, FileMode.Open, FileAccess.Read);
            preview.Image = Image.FromStream(fs);
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    void Action(string action)
    {
        try
        {
            if (action == "Thêm")
            {
                if (string.IsNullOrWhiteSpace(id.Text) || string.IsNullOrWhiteSpace(title.Text))
                    throw new Exception("Nhập mã và tên phim.");
                if (AppData.Movies.Any(x => x.Id == id.Text.Trim()))
                    throw new Exception("Mã phim đã tồn tại.");
                AppData.Movies.Add(ReadInput());
            }
            else if (action == "Sửa")
            {
                var m = AppData.Movies.FirstOrDefault(x => x.Id == id.Text.Trim())
                    ?? throw new Exception("Không tìm thấy phim.");
                var n = ReadInput();
                m.Title = n.Title;
                m.Genre = n.Genre;
                m.DurationMinutes = n.DurationMinutes;
                m.PosterPath = n.PosterPath;
            }
            else if (action == "Xóa")
            {
                var m = AppData.Movies.FirstOrDefault(x => x.Id == id.Text.Trim())
                    ?? throw new Exception("Chọn phim cần xóa.");
                if (AppData.Screenings.Any(s => s.MovieId == m.Id))
                    throw new Exception("Phim đã có suất chiếu, không thể xóa.");
                AppData.Movies.Remove(m);
            }
            else if (action == "Nhập CSV")
            {
                using var d = new OpenFileDialog { Filter = "CSV|*.csv" };
                if (d.ShowDialog() != DialogResult.OK) return;

                foreach (var line in File.ReadAllLines(d.FileName).Skip(1))
                {
                    var p = line.Split(',');
                    if (p.Length >= 4 &&
                        !AppData.Movies.Any(m => m.Id == p[0].Trim()) &&
                        int.TryParse(p[3], out var minutes) && minutes > 0)
                    {
                        AppData.Movies.Add(new Movie
                        {
                            Id = p[0].Trim(),
                            Title = p[1].Trim(),
                            Genre = p[2].Trim(),
                            DurationMinutes = minutes
                        });
                    }
                }
            }
            else if (action == "Lọc thể loại")
            {
                var g = Microsoft.VisualBasic.Interaction.InputBox("Nhập thể loại cần lọc:", "Lọc phim");
                grid.DataSource = AppData.Movies
                    .Where(x => x.Genre.Contains(g, StringComparison.OrdinalIgnoreCase))
                    .Select(x => new { x.Id, x.Title, x.Genre, x.DurationMinutes })
                    .ToList();
                ApplyHeaders();
                return;
            }
            else if (action == "Làm mới")
            {
                id.Clear();
                title.Clear();
                genre.Clear();
                duration.Clear();
                poster.Clear();
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

    Movie ReadInput() => new()
    {
        Id = id.Text.Trim(),
        Title = title.Text.Trim(),
        Genre = genre.Text.Trim(),
        DurationMinutes = int.TryParse(duration.Text, out var n) && n > 0
            ? n
            : throw new Exception("Thời lượng phải là số phút dương."),
        PosterPath = poster.Text.Trim()
    };

    void RefreshData()
    {
        grid.DataSource = AppData.Movies
            .Select(x => new { x.Id, x.Title, x.Genre, x.DurationMinutes })
            .ToList();

        ApplyHeaders();

        list.Items.Clear();
        list.LargeImageList!.Images.Clear();

        foreach (var m in AppData.Movies)
        {
            try
            {
                if (File.Exists(m.PosterPath))
                {
                    using var fs = new FileStream(m.PosterPath, FileMode.Open, FileAccess.Read);
                    list.LargeImageList.Images.Add(Image.FromStream(fs));
                }
                else
                {
                    list.LargeImageList.Images.Add(PosterPlaceholder(m.Title, 64, 80, 20));
                }
            }
            catch
            {
                list.LargeImageList.Images.Add(PosterPlaceholder(m.Title, 64, 80, 20));
            }

            list.Items.Add(new ListViewItem(m.Title, list.LargeImageList.Images.Count - 1));
        }
    }

    void ApplyHeaders()
    {
        SetHeader("Id", "Mã phim");
        SetHeader("Title", "Tên phim");
        SetHeader("Genre", "Thể loại");
        SetHeader("DurationMinutes", "Thời lượng");
    }

    void SetHeader(string column, string text)
    {
        if (grid.Columns[column] != null)
            grid.Columns[column]!.HeaderText = text;
    }

    void LoadSelected()
    {
        if (grid.CurrentRow == null) return;

        var key = grid.CurrentRow.Cells["Id"]?.Value?.ToString();
        var m = AppData.Movies.FirstOrDefault(x => x.Id == key);
        if (m != null) ShowMovie(m);
    }

    void ActivateListItem()
    {
        if (list.SelectedIndices.Count == 0) return;

        var index = list.SelectedIndices[0];
        if (index < 0 || index >= AppData.Movies.Count) return;

        ShowMovie(AppData.Movies[index]);
    }

    void ShowMovie(Movie m)
    {
        id.Text = m.Id;
        title.Text = m.Title;
        genre.Text = m.Genre;
        duration.Text = m.DurationMinutes.ToString();
        poster.Text = m.PosterPath;

        try
        {
            if (File.Exists(m.PosterPath))
            {
                using var fs = new FileStream(m.PosterPath, FileMode.Open, FileAccess.Read);
                preview.Image = Image.FromStream(fs);
            }
            else
            {
                preview.Image = PosterPlaceholder(m.Title, 120, 150, 28);
            }
        }
        catch
        {
            preview.Image = PosterPlaceholder(m.Title, 120, 150, 28);
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

        var bmp = new Bitmap(width, height);
        using var g = Graphics.FromImage(bmp);
        g.Clear(palette[(title.GetHashCode() & int.MaxValue) % palette.Length]);

        var initials = string.Concat(
            title.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                 .Take(2)
                 .Select(x => char.ToUpperInvariant(x[0])));

        using var font = new Font("Segoe UI", fontSize, FontStyle.Bold);
        TextRenderer.DrawText(
            g,
            initials,
            font,
            new Rectangle(0, 0, width, height),
            Ui.Accent,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);

        return bmp;
    }
}
