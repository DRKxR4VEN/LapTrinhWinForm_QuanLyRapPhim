namespace QuanLyRapChieuPhim;

public class RoomForm : Form
{
    readonly DataGridView grid = Ui.Grid();
    readonly TextBox id = Ui.Box(90), name = Ui.Box(180), rows = Ui.Box(80), seats = Ui.Box(90), vip = Ui.Box(80);

    public RoomForm()
    {
        Text = "Quản lý phòng chiếu";
        Width = 900;
        Height = 560;
        MinimumSize = new Size(760, 450);
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
            RowCount = 2,
            Padding = new Padding(10),
            BackColor = Ui.MainBack
        };
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
        fields.Controls.Add(Field("Mã phòng", id, 110));
        fields.Controls.Add(Field("Tên phòng", name, 200));
        fields.Controls.Add(Field("Số hàng", rows, 100));
        fields.Controls.Add(Field("Ghế / hàng", seats, 110));
        fields.Controls.Add(Field("Hàng VIP", vip, 100));

        var buttons = new FlowLayoutPanel
        {
            AutoSize = true,
            AutoSizeMode = AutoSizeMode.GrowAndShrink,
            WrapContents = true,
            BackColor = Ui.MainBack,
            Margin = new Padding(0, 6, 0, 0)
        };
        foreach (var text in new[] { "Thêm", "Sửa", "Xóa" })
        {
            var b = Ui.Btn(text);
            b.Click += (_, _) => Act(text);
            buttons.Controls.Add(b);
        }

        top.Controls.Add(fields, 0, 0);
        top.Controls.Add(buttons, 0, 1);

        Controls.Add(grid);
        Controls.Add(top);

        grid.SelectionChanged += (_, _) => LoadSelected();
        RefreshGrid();
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

    void Act(string action)
    {
        try
        {
            if (action == "Thêm")
            {
                if (AppData.Rooms.Any(x => x.Id == id.Text.Trim()))
                    throw new Exception("Mã phòng đã tồn tại.");
                AppData.Rooms.Add(Read());
            }
            else if (action == "Sửa")
            {
                var r = AppData.Rooms.FirstOrDefault(x => x.Id == id.Text.Trim())
                    ?? throw new Exception("Không tìm thấy phòng.");
                var n = Read();
                r.Name = n.Name;
                r.Rows = n.Rows;
                r.SeatsPerRow = n.SeatsPerRow;
                r.VipRows = n.VipRows;
            }
            else
            {
                var r = AppData.Rooms.FirstOrDefault(x => x.Id == id.Text.Trim())
                    ?? throw new Exception("Chọn phòng cần xóa.");
                if (AppData.Screenings.Any(x => x.RoomId == r.Id))
                    throw new Exception("Phòng đã có suất chiếu.");
                AppData.Rooms.Remove(r);
            }

            AppData.SaveRooms();
            RefreshGrid();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    CinemaRoom Read()
    {
        int Parse(TextBox t) => int.TryParse(t.Text, out var n) && n > 0
            ? n
            : throw new Exception("Số hàng và số ghế mỗi hàng phải là số dương.");

        var r = Parse(rows);
        var s = Parse(seats);

        if (!int.TryParse(vip.Text, out var v) || v < 0 || v > r)
            throw new Exception("Hàng VIP phải từ 0 đến số hàng.");

        if (string.IsNullOrWhiteSpace(id.Text) || string.IsNullOrWhiteSpace(name.Text))
            throw new Exception("Nhập mã và tên phòng.");

        return new CinemaRoom
        {
            Id = id.Text.Trim(),
            Name = name.Text.Trim(),
            Rows = r,
            SeatsPerRow = s,
            VipRows = v
        };
    }

    void RefreshGrid()
    {
        grid.DataSource = AppData.Rooms
            .Select(x => new { x.Id, x.Name, x.Rows, x.SeatsPerRow, x.VipRows, x.Capacity })
            .ToList();

        SetHeader("Id", "Mã phòng");
        SetHeader("Name", "Tên phòng");
        SetHeader("Rows", "Số hàng");
        SetHeader("SeatsPerRow", "Ghế / hàng");
        SetHeader("VipRows", "Hàng VIP");
        SetHeader("Capacity", "Sức chứa");
    }

    void SetHeader(string column, string text)
    {
        if (grid.Columns[column] != null)
            grid.Columns[column]!.HeaderText = text;
    }

    void LoadSelected()
    {
        if (grid.CurrentRow == null) return;

        var r = AppData.Rooms.FirstOrDefault(
            x => x.Id == grid.CurrentRow.Cells["Id"]?.Value?.ToString());
        if (r == null) return;

        id.Text = r.Id;
        name.Text = r.Name;
        rows.Text = r.Rows.ToString();
        seats.Text = r.SeatsPerRow.ToString();
        vip.Text = r.VipRows.ToString();
    }
}
