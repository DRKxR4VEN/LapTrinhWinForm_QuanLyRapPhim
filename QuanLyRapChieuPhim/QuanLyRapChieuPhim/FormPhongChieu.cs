namespace QuanLyRapChieuPhim;

public partial class FormPhongChieu : Form
{
    public FormPhongChieu()
    {
        InitializeComponent();
        GiaoDien.ApDungGiaoDien(this);

        if (GiaoDien.DangThietKe)
            return;

        NapBang();
    }

    void nutThem_Click(object? sender, EventArgs e) => ThucHien("Thêm");

    void nutSua_Click(object? sender, EventArgs e) => ThucHien("Sửa");

    void nutXoa_Click(object? sender, EventArgs e) => ThucHien("Xóa");

    void ThucHien(string thaoTac)
    {
        try
        {
            if (thaoTac == "Thêm")
            {
                KiemTraMa();
                if (DuLieu.DanhSachPhong.Any(x => x.Ma == oMa.Text.Trim()))
                {
                    oMa.Focus();
                    throw new Exception("Mã phòng đã tồn tại.");
                }

                DuLieu.DanhSachPhong.Add(DocNhapLieu());
            }
            else if (thaoTac == "Sửa")
            {
                KiemTraMa();
                var phong = DuLieu.DanhSachPhong.FirstOrDefault(x => x.Ma == oMa.Text.Trim());
                if (phong == null)
                {
                    oMa.Focus();
                    throw new Exception("Không tìm thấy phòng.");
                }

                var phongMoi = DocNhapLieu();
                phong.Ten = phongMoi.Ten;
                phong.SoHang = phongMoi.SoHang;
                phong.SoGheMoiHang = phongMoi.SoGheMoiHang;
                phong.HangVip = phongMoi.HangVip;
            }
            else
            {
                KiemTraMa();
                var phong = DuLieu.DanhSachPhong.FirstOrDefault(x => x.Ma == oMa.Text.Trim());
                if (phong == null)
                {
                    oMa.Focus();
                    throw new Exception("Chọn phòng cần xóa.");
                }

                if (DuLieu.DanhSachSuat.Any(x => x.MaPhong == phong.Ma))
                    throw new Exception("Phòng đã có suất chiếu.");

                if (MessageBox.Show(
                        $"Xóa phòng \"{phong.Ten}\"?",
                        "Xác nhận xóa",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning,
                        MessageBoxDefaultButton.Button2) != DialogResult.Yes)
                    return;

                DuLieu.DanhSachPhong.Remove(phong);
            }

            DuLieu.LuuPhong();
            NapBang();
        }
        catch (Exception ex)
        {
            GiaoDien.BaoLoi(ex);
        }
    }

    void KiemTraMa()
    {
        if (string.IsNullOrWhiteSpace(oMa.Text))
        {
            oMa.Focus();
            throw new Exception("Nhập mã phòng.");
        }
    }

    PhongChieu DocNhapLieu()
    {
        if (string.IsNullOrWhiteSpace(oTenPhong.Text))
        {
            oTenPhong.Focus();
            throw new Exception("Nhập tên phòng.");
        }

        int DocSo(TextBox input)
        {
            if (!int.TryParse(input.Text, out var soLuong) || soLuong <= 0)
            {
                input.Focus();
                throw new Exception("Số hàng và số ghế mỗi hàng phải là số dương.");
            }

            return soLuong;
        }

        var soHang = DocSo(oSoHang);
        var soGheMoiHang = DocSo(oSoGhe);

        if (!int.TryParse(oHangVip.Text, out var soHangVip) || soHangVip < 0 || soHangVip > soHang)
        {
            oHangVip.Focus();
            throw new Exception("Hàng VIP phải từ 0 đến số hàng.");
        }

        return new PhongChieu
        {
            Ma = oMa.Text.Trim(),
            Ten = oTenPhong.Text.Trim(),
            SoHang = soHang,
            SoGheMoiHang = soGheMoiHang,
            HangVip = soHangVip
        };
    }

    void NapBang()
    {
        bang.DataSource = DuLieu.DanhSachPhong
            .Select(x => new { x.Ma, x.Ten, x.SoHang, x.SoGheMoiHang, x.HangVip, x.SucChua })
            .ToList();

        DatTieuDe("Ma", "Mã phòng");
        DatTieuDe("Ten", "Tên phòng");
        DatTieuDe("SoHang", "Số hàng");
        DatTieuDe("SoGheMoiHang", "Ghế / hàng");
        DatTieuDe("HangVip", "Hàng VIP");
        DatTieuDe("SucChua", "Sức chứa");
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

        var phong = DuLieu.DanhSachPhong.FirstOrDefault(
            x => x.Ma == bang.CurrentRow.Cells["Ma"]?.Value?.ToString());
        if (phong == null)
            return;

        oMa.Text = phong.Ma;
        oTenPhong.Text = phong.Ten;
        oSoHang.Text = phong.SoHang.ToString();
        oSoGhe.Text = phong.SoGheMoiHang.ToString();
        oHangVip.Text = phong.HangVip.ToString();
    }
}
