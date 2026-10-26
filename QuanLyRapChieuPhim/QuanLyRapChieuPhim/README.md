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
   - Quản trị: `admin` / `123`
   - Nhân viên: `nhanvien` / `123`
5. Dữ liệu mẫu được copy vào thư mục `Data` cạnh file chạy; nếu thiếu, chương trình tự tạo thư mục/tập tin khi khởi động.

## Các chức năng
- Đăng nhập đọc `Data/users.txt`, khóa tài khoản sau 3 lần sai trong phiên chạy.
- Quản lý phim, chọn poster, lọc thể loại, nhập CSV.
- Quản lý phòng chiếu, số hàng/ghế và hàng VIP.
- Quản lý suất chiếu dạng cây Ngày -> Phim -> Suất; kiểm tra trùng lịch theo thời lượng phim + 15 phút dọn phòng; không hủy suất đã bán vé.
- Suất chiếu được lưu trong `Data/screenings.txt`, mỗi dòng:
  `MaSuat;MaPhim;MaPhong;NgayGioISO;TenPhim;TenPhong`.
- Chọn ghế động, trạng thái ghế, ghế VIP giá cao hơn 50%, giảm giá theo `App.config`, in vé ra `.txt`.
- Báo cáo doanh thu theo phim và ngày.
- MDI, cấu hình giao diện lưu Registry, sao lưu/khôi phục dữ liệu, ghi lỗi vào `Data/log.txt`.

## Định dạng dữ liệu trong thư mục Data
Tất cả tập tin `.txt` dùng UTF-8, mỗi dòng một bản ghi, các trường ngăn cách bằng `;`:
- `movies.txt`: `Ma;Ten;TheLoai;ThoiLuong;Poster`
- `rooms.txt`: `Ma;Ten;SoHang;SoGheMoiHang;HangVIP`
- `screenings.txt`: `Ma;MaPhim;MaPhong;NgayGioISO;TenPhim;TenPhong`
- `tickets.txt`: `Ma;MaSuat;TenPhim;TenPhong;NgayGioISO;Ghe;VIP;LoaiVe;Gia;NgayBanISO`
- `users.txt`: `TaiKhoan;MatKhau;VaiTro`

Bộ dữ liệu mẫu hiện có 6 phim, 4 phòng chiếu, 33 suất chiếu và 7 vé.

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

