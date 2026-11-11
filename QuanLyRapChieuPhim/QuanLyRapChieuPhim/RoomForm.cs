namespace QuanLyRapChieuPhim;

public partial class RoomForm : Form
{
    public RoomForm()
    {
        InitializeComponent();
        Ui.ApplyAppearance(this);

        if (Ui.DesignTime)
            return;

        RefreshGrid();
    }

    void addButton_Click(object? sender, EventArgs e) => Execute("Thêm");

    void editButton_Click(object? sender, EventArgs e) => Execute("Sửa");

    void deleteButton_Click(object? sender, EventArgs e) => Execute("Xóa");

    void Execute(string action)
    {
        try
        {
            if (action == "Thêm")
            {
                RequireId();
                if (AppData.Rooms.Any(x => x.Id == id.Text.Trim()))
                {
                    id.Focus();
                    throw new Exception("Mã phòng đã tồn tại.");
                }

                AppData.Rooms.Add(Read());
            }
            else if (action == "Sửa")
            {
                RequireId();
                var room = AppData.Rooms.FirstOrDefault(x => x.Id == id.Text.Trim());
                if (room == null)
                {
                    id.Focus();
                    throw new Exception("Không tìm thấy phòng.");
                }

                var updated = Read();
                room.Name = updated.Name;
                room.Rows = updated.Rows;
                room.SeatsPerRow = updated.SeatsPerRow;
                room.VipRows = updated.VipRows;
            }
            else
            {
                RequireId();
                var room = AppData.Rooms.FirstOrDefault(x => x.Id == id.Text.Trim());
                if (room == null)
                {
                    id.Focus();
                    throw new Exception("Chọn phòng cần xóa.");
                }

                if (AppData.Screenings.Any(x => x.RoomId == room.Id))
                    throw new Exception("Phòng đã có suất chiếu.");

                if (MessageBox.Show(
                        $"Xóa phòng \"{room.Name}\"?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;

                AppData.Rooms.Remove(room);
            }

            AppData.SaveRooms();
            RefreshGrid();
        }
        catch (Exception ex)
        {
            Ui.ShowError(ex);
        }
    }

    void RequireId()
    {
        if (string.IsNullOrWhiteSpace(id.Text))
        {
            id.Focus();
            throw new Exception("Nhập mã phòng.");
        }
    }

    CinemaRoom Read()
    {
        if (string.IsNullOrWhiteSpace(name.Text))
        {
            name.Focus();
            throw new Exception("Nhập tên phòng.");
        }

        int Parse(TextBox input)
        {
            if (!int.TryParse(input.Text, out var value) || value <= 0)
            {
                input.Focus();
                throw new Exception("Số hàng và số ghế mỗi hàng phải là số dương.");
            }

            return value;
        }

        var rowCount = Parse(rows);
        var seatsPerRow = Parse(seats);

        if (!int.TryParse(vip.Text, out var vipRows) || vipRows < 0 || vipRows > rowCount)
        {
            vip.Focus();
            throw new Exception("Hàng VIP phải từ 0 đến số hàng.");
        }

        return new CinemaRoom
        {
            Id = id.Text.Trim(),
            Name = name.Text.Trim(),
            Rows = rowCount,
            SeatsPerRow = seatsPerRow,
            VipRows = vipRows
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

    void grid_SelectionChanged(object? sender, EventArgs e) => LoadSelected();

    void LoadSelected()
    {
        if (grid.CurrentRow == null)
            return;

        var room = AppData.Rooms.FirstOrDefault(
            x => x.Id == grid.CurrentRow.Cells["Id"]?.Value?.ToString());
        if (room == null)
            return;

        id.Text = room.Id;
        name.Text = room.Name;
        rows.Text = room.Rows.ToString();
        seats.Text = room.SeatsPerRow.ToString();
        vip.Text = room.VipRows.ToString();
    }
}
