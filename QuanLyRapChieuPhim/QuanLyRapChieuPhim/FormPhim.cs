namespace QuanLyRapChieuPhim;

public partial class FormPhim : Form
{
    public FormPhim()
    {
        InitializeComponent();
        GiaoDien.ApDungGiaoDien(this);

        if (GiaoDien.DangThietKe)
            return;

        NapDuLieu();
    }

    void nutChonPoster_Click(object? sender, EventArgs e) => ChonPoster();

    void nutThem_Click(object? sender, EventArgs e) => ThucHien("Thêm");

    void nutSua_Click(object? sender, EventArgs e) => ThucHien("Sửa");

    void nutXoa_Click(object? sender, EventArgs e) => ThucHien("Xóa");

    void nutNhapCsv_Click(object? sender, EventArgs e) => ThucHien("Nhập CSV");

    void nutLoc_Click(object? sender, EventArgs e) => ThucHien("Lọc thể loại");

    void nutLamMoi_Click(object? sender, EventArgs e) => ThucHien("Làm mới");

    void ChonPoster()
    {
        using var hopThoai = new OpenFileDialog
        {
            Filter = "Ảnh|*.png;*.jpg;*.jpeg;*.bmp"
        };

        if (hopThoai.ShowDialog() != DialogResult.OK)
            return;

        oPoster.Text = hopThoai.FileName;
        try
        {
            using var luong = new FileStream(hopThoai.FileName, FileMode.Open, FileAccess.Read);
            anhXemTruoc.Image = Image.FromStream(luong);
        }
        catch (Exception ex)
        {
            GiaoDien.BaoLoi(ex);
        }
    }

    void ThucHien(string thaoTac)
    {
        try
        {
            if (thaoTac == "Thêm")
            {
                KiemTraMaVaTen();
                if (DuLieu.DanhSachPhim.Any(x => x.Ma == oMa.Text.Trim()))
                {
                    oMa.Focus();
                    throw new Exception("Mã phim đã tồn tại.");
                }

                DuLieu.DanhSachPhim.Add(DocNhapLieu());
            }
            else if (thaoTac == "Sửa")
            {
                KiemTraMaVaTen();
                var phim = DuLieu.DanhSachPhim.FirstOrDefault(x => x.Ma == oMa.Text.Trim());
                if (phim == null)
                {
                    oMa.Focus();
                    throw new Exception("Không tìm thấy phim.");
                }

                var phimMoi = DocNhapLieu();
                phim.Ten = phimMoi.Ten;
                phim.TheLoai = phimMoi.TheLoai;
                phim.ThoiLuong = phimMoi.ThoiLuong;
                phim.DuongDanPoster = phimMoi.DuongDanPoster;
            }
            else if (thaoTac == "Xóa")
            {
                if (string.IsNullOrWhiteSpace(oMa.Text))
                {
                    oMa.Focus();
                    throw new Exception("Chọn phim cần xóa.");
                }

                var phim = DuLieu.DanhSachPhim.FirstOrDefault(x => x.Ma == oMa.Text.Trim());
                if (phim == null)
                {
                    oMa.Focus();
                    throw new Exception("Không tìm thấy phim.");
                }

                if (DuLieu.DanhSachSuat.Any(s => s.MaPhim == phim.Ma))
                    throw new Exception("Phim đã có suất chiếu, không thể xóa.");

                if (MessageBox.Show(
                        $"Xóa phim \"{phim.Ten}\"?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;

                DuLieu.DanhSachPhim.Remove(phim);
            }
            else if (thaoTac == "Nhập CSV")
            {
                NhapCsv();
            }
            else if (thaoTac == "Lọc thể loại")
            {
                var theLoaiLoc = Microsoft.VisualBasic.Interaction.InputBox(
                    "Nhập thể loại cần lọc:", "Lọc phim");

                bang.DataSource = DuLieu.DanhSachPhim
                    .Where(x => x.TheLoai.Contains(theLoaiLoc, StringComparison.OrdinalIgnoreCase))
                    .Select(x => new { x.Ma, x.Ten, x.TheLoai, x.ThoiLuong })
                    .ToList();
                DatTieuDeCot();
                return;
            }
            else if (thaoTac == "Làm mới")
            {
                oMa.Clear();
                oTen.Clear();
                oTheLoai.Clear();
                oThoiLuong.Clear();
                oPoster.Clear();
                anhXemTruoc.Image = AnhTam("Phim", 120, 150, 24);
            }

            DuLieu.LuuPhim();
            NapDuLieu();
        }
        catch (Exception ex)
        {
            GiaoDien.BaoLoi(ex);
        }
    }

    void NhapCsv()
    {
        using var hopThoai = new OpenFileDialog
        {
            Filter = "CSV|*.csv",
            Title = "Nhập danh sách phim từ CSV"
        };

        if (hopThoai.ShowDialog() != DialogResult.OK)
            return;

        int soThem = 0;
        int soBoQua = 0;

        foreach (var dong in File.ReadAllLines(hopThoai.FileName))
        {
            if (string.IsNullOrWhiteSpace(dong))
            {
                soBoQua++;
                continue;
            }

            var phan = dong.Split(',');
            if (phan.Length >= 4 &&
                !string.IsNullOrWhiteSpace(phan[0]) &&
                !string.IsNullOrWhiteSpace(phan[1]) &&
                !DuLieu.DanhSachPhim.Any(m => m.Ma == phan[0].Trim()) &&
                int.TryParse(phan[3], out var soPhut) &&
                soPhut > 0)
            {
                DuLieu.DanhSachPhim.Add(new Phim
                {
                    Ma = phan[0].Trim(),
                    Ten = phan[1].Trim(),
                    TheLoai = phan[2].Trim(),
                    ThoiLuong = soPhut,
                    DuongDanPoster = phan.Length > 4 ? phan[4].Trim() : ""
                });
                soThem++;
            }
            else
            {
                soBoQua++;
            }
        }

        if (soBoQua > 0)
        {
            TepTin.GhiLoi(
                $"Nhập CSV {hopThoai.FileName}: thêm {soThem} phim, " +
                $"bỏ qua {soBoQua} dòng trống/trùng/sai định dạng.");
        }

        MessageBox.Show(
            $"Đã nhập {soThem} phim từ CSV." +
            (soBoQua > 0 ? $"\nBỏ qua {soBoQua} dòng không hợp lệ." : ""),
            "Nhập CSV",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    void KiemTraMaVaTen()
    {
        if (string.IsNullOrWhiteSpace(oMa.Text))
        {
            oMa.Focus();
            throw new Exception("Nhập mã phim.");
        }

        if (string.IsNullOrWhiteSpace(oTen.Text))
        {
            oTen.Focus();
            throw new Exception("Nhập tên phim.");
        }
    }

    Phim DocNhapLieu()
    {
        if (!int.TryParse(oThoiLuong.Text, out var soPhut) || soPhut <= 0)
        {
            oThoiLuong.Focus();
            throw new Exception("Thời lượng phải là số phút dương.");
        }

        return new Phim
        {
            Ma = oMa.Text.Trim(),
            Ten = oTen.Text.Trim(),
            TheLoai = oTheLoai.Text.Trim(),
            ThoiLuong = soPhut,
            DuongDanPoster = oPoster.Text.Trim()
        };
    }

    void NapDuLieu()
    {
        bang.DataSource = DuLieu.DanhSachPhim
            .Select(x => new { x.Ma, x.Ten, x.TheLoai, x.ThoiLuong })
            .ToList();

        DatTieuDeCot();

        danhSach.Items.Clear();
        anhPoster.Images.Clear();

        foreach (var phim in DuLieu.DanhSachPhim)
        {
            try
            {
                if (File.Exists(phim.DuongDanPoster))
                {
                    using var luong = new FileStream(phim.DuongDanPoster, FileMode.Open, FileAccess.Read);
                    anhPoster.Images.Add(Image.FromStream(luong));
                }
                else
                {
                    anhPoster.Images.Add(AnhTam(phim.Ten, 64, 80, 20));
                }
            }
            catch
            {
                anhPoster.Images.Add(AnhTam(phim.Ten, 64, 80, 20));
            }

            danhSach.Items.Add(new ListViewItem(phim.Ten, anhPoster.Images.Count - 1));
        }
    }

    void DatTieuDeCot()
    {
        DatTieuDe("Ma", "Mã phim");
        DatTieuDe("Ten", "Tên phim");
        DatTieuDe("TheLoai", "Thể loại");
        DatTieuDe("ThoiLuong", "Thời lượng");
    }

    void DatTieuDe(string cot, string tieuDe)
    {
        if (bang.Columns[cot] != null)
            bang.Columns[cot]!.HeaderText = tieuDe;
    }

    void bang_SelectionChanged(object? sender, EventArgs e) => NapDongDangChon();

    void NapDongDangChon()
    {
        if (bang.CurrentRow == null)
            return;

        var khoa = bang.CurrentRow.Cells["Ma"]?.Value?.ToString();
        var phim = DuLieu.DanhSachPhim.FirstOrDefault(x => x.Ma == khoa);
        if (phim != null)
            HienThiPhim(phim);
    }

    void danhSach_ItemActivate(object? sender, EventArgs e) => MoDongDangChon();

    void MoDongDangChon()
    {
        if (danhSach.SelectedIndices.Count == 0)
            return;

        var chiSo = danhSach.SelectedIndices[0];
        if (chiSo < 0 || chiSo >= DuLieu.DanhSachPhim.Count)
            return;

        HienThiPhim(DuLieu.DanhSachPhim[chiSo]);
    }

    void HienThiPhim(Phim phim)
    {
        oMa.Text = phim.Ma;
        oTen.Text = phim.Ten;
        oTheLoai.Text = phim.TheLoai;
        oThoiLuong.Text = phim.ThoiLuong.ToString();
        oPoster.Text = phim.DuongDanPoster;

        try
        {
            if (File.Exists(phim.DuongDanPoster))
            {
                using var luong = new FileStream(phim.DuongDanPoster, FileMode.Open, FileAccess.Read);
                anhXemTruoc.Image = Image.FromStream(luong);
            }
            else
            {
                anhXemTruoc.Image = AnhTam(phim.Ten, 120, 150, 28);
            }
        }
        catch
        {
            anhXemTruoc.Image = AnhTam(phim.Ten, 120, 150, 28);
        }
    }

    static Image AnhTam(string tenPhim, int rong, int cao, float coChu)
    {
        var bangMau = new[]
        {
            Color.FromArgb(225, 238, 255),
            Color.FromArgb(255, 235, 225),
            Color.FromArgb(230, 248, 233),
            Color.FromArgb(245, 232, 250),
            Color.FromArgb(255, 248, 215)
        };

        var anhTam = new Bitmap(rong, cao);
        using var hoaSi = Graphics.FromImage(anhTam);
        hoaSi.Clear(bangMau[(tenPhim.GetHashCode() & int.MaxValue) % bangMau.Length]);

        var chuVietTat = string.Concat(
            tenPhim.Split(' ', StringSplitOptions.RemoveEmptyEntries)
                 .Take(2)
                 .Select(x => char.ToUpperInvariant(x[0])));

        using var phong = new Font("Segoe UI", coChu, FontStyle.Bold);
        TextRenderer.DrawText(
            hoaSi,
            chuVietTat,
            phong,
            new Rectangle(0, 0, rong, cao),
            GiaoDien.MauNhan,
            TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.NoPrefix);

        return anhTam;
    }
}
