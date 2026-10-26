# Quản lý rạp chiếu phim - C# WinForms

## Yêu cầu
- Windows 10/11
- Visual Studio 2022 với workload **.NET desktop development**
- .NET 8 SDK

## Chạy chương trình
1. Giải nén thư mục.
2. Mở `QuanLyRapChieuPhim.csproj` bằng Visual Studio.
3. Chọn cấu hình `Debug`, nhấn F5.
4. Tài khoản mặc định:
   - Quản trị: `admin` / `123` – toàn quyền.
   - Nhân viên: `nhanvien` / `123` – chỉ **Bán vé** và **Đổi giao diện**.
5. Dữ liệu mẫu được copy vào thư mục `Data` cạnh file chạy; nếu thiếu, chương trình tự tạo thư mục/tập tin khi khởi động.

## Phân quyền
- **Quản trị:** sao lưu/khôi phục, quản lý phim, phòng chiếu, suất chiếu, bán vé, xem doanh thu, đổi giao diện.
- **Nhân viên:** bán vé và đổi giao diện; không thấy các mục quản lý phim, phòng chiếu, suất chiếu, doanh thu, sao lưu/khôi phục.

## Các chức năng
- Đăng nhập đọc `Data/users.txt`, khóa tài khoản sau 3 lần sai trong phiên chạy.
- Quản lý phim, chọn poster, lọc thể loại, nhập CSV.
- Quản lý phòng chiếu, số hàng/ghế và hàng VIP.
- Quản lý suất chiếu dạng cây Ngày -> Phim -> Suất; kiểm tra trùng lịch theo thời lượng phim + 15 phút dọn phòng; không hủy suất đã bán vé.
- Suất chiếu được lưu trong `Data/screenings.txt`, mỗi dòng:
  `MaSuat;MaPhim;MaPhong;NgayGioISO;TenPhim;TenPhong`.
- MDI, cấu hình giao diện lưu Registry, sao lưu/khôi phục dữ liệu, ghi lỗi vào `Data/log.txt`.

### Phân hệ SV3 - Bán vé & doanh thu
- Chọn suất chiếu, sơ đồ ghế tạo bằng `Button` trong code với 4 trạng thái:
  trống (xanh lá), VIP (vàng), đang chọn (xanh dương), đã bán (đỏ).
  Ghế đã bán bị khóa nên không thể bán trùng.
- Loại vé người lớn / học sinh - sinh viên (`RadioButton`), mức giảm đọc từ `App.config`.
  Giá vé = giá gốc × phụ thu VIP × (1 - mức giảm).
- Xác nhận trước khi bán, in mỗi vé ra `Data/Ve_<MaVe>.txt` (UTF-8);
  nút **In lại vé** dùng `SaveFileDialog` để lưu vé đã bán ra vị trí khác.
- **Nâng cao 1 - combo bắp nước:** `ComboBox` chọn combo bán kèm, giá trong `App.config`.
- **Nâng cao 2 - trả vé trước giờ chiếu:** danh sách vé đã bán của suất đang chọn
  (`ListView` Details); chỉ nhận trả khi còn ít nhất 1 tiếng trước giờ chiếu,
  có `MessageBox` xác nhận, hoàn tiền và ghi biên bản vào `Data/trave.txt`.
- **Nâng cao 3 - Timer tự ẩn suất đã chiếu:** `Timer` 20 giây làm mới danh sách suất;
  suất vừa tới giờ chiếu tự biến mất khỏi màn hình bán vé.
- Thống kê doanh thu 2 chế độ: **theo phim** và **theo ngày**, có biểu đồ cột vẽ bằng GDI+,
  tổng hợp doanh thu vé + bắp nước - tiền hoàn.
- Xuất báo cáo bằng `SaveFileDialog` ra **CSV** (tab đang xem) hoặc **TXT** (đầy đủ 2 bảng,
  tổng hợp và danh sách vé trả).

## Cấu hình trong App.config
| Khóa | Ý nghĩa | Giá trị mặc định |
|---|---|---|
| `CinemaName` | Tên đơn vị in trên vé/báo cáo | RẠP CHIẾU PHIM SAO MAI |
| `DataFolder` | Thư mục dữ liệu (tương đối hoặc tuyệt đối) | Data |
| `BaseTicketPrice` | Giá vé gốc | 80000 |
| `VipSurcharge` | Hệ số phụ thu ghế VIP | 1.5 |
| `StudentDiscount` | Mức giảm học sinh - sinh viên | 0.15 |
| `AdultDiscount` | Mức giảm người lớn | 0 |
| `CleaningMinutes` | Phút dọn phòng giữa hai suất | 15 |
| `RefundBeforeHours` | Số giờ tối thiểu được trả vé | 1 |
| `SnackCombos` | Danh sách combo, dạng `Tên:Giá` ngăn cách bằng dấu phẩy | Bắp + nước nhỏ:55000,... |

## Định dạng dữ liệu trong thư mục Data
Tất cả tập tin `.txt` dùng UTF-8, mỗi dòng một bản ghi, các trường ngăn cách bằng `;`:
- `movies.txt`: `Ma;Ten;TheLoai;ThoiLuong;Poster`
- `rooms.txt`: `Ma;Ten;SoHang;SoGheMoiHang;HangVIP`
- `screenings.txt`: `Ma;MaPhim;MaPhong;NgayGioISO;TenPhim;TenPhong`
- `tickets.txt`: `Ma;MaSuat;TenPhim;TenPhong;NgayGioISO;Ghe;VIP;LoaiVe;Gia;NgayBanISO;Combo;GiaCombo`
  (hai trường `Combo;GiaCombo` được thêm ở phân hệ SV3; dữ liệu vé cũ chỉ có 10 trường vẫn đọc được)
- `trave.txt`: `MaVe;MaSuat;TenPhim;TenPhong;Ghe;LoaiVe;Combo;SoTienHoan;GioChieuISO;ThoiGianTraISO`
- `users.txt`: `TaiKhoan;MatKhau;VaiTro`

Bộ dữ liệu mẫu hiện có 6 phim, 4 phòng chiếu, 33 suất chiếu, 53 vé và 3 biên bản trả vé.


## Định dạng CSV nhập phim
Tạo file CSV với dòng đầu là tiêu đề, các dòng sau theo thứ tự:
`MaPhim,TenPhim,TheLoai,ThoiLuongPhut`
Ví dụ:
`P001,Lat Mat,Han dong,120`

Lưu ý: bản demo sử dụng dấu phẩy để tách cột; tên phim/thể loại không nên chứa dấu phẩy.

## Định dạng users.txt
Mỗi dòng: `username;password;role`
Ví dụ:
`admin;123;QuanTri`
`nhanvien;123;NhanVien`

