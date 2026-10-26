using Microsoft.Win32;
using System.Globalization;
using System.Text;
using System.Xml.Linq;

namespace QuanLyRapChieuPhim;

public static class AppConfig
{
    static readonly string ConfigPath = Path.Combine(AppContext.BaseDirectory, "App.config");
    static string Get(string key, string fallback)
    {
        try { return XDocument.Load(ConfigPath).Descendants("add").FirstOrDefault(x => (string?)x.Attribute("key") == key)?.Attribute("value")?.Value ?? fallback; }
        catch { return fallback; }
    }
    public static decimal BaseTicketPrice => decimal.TryParse(Get("BaseTicketPrice","80000"), NumberStyles.Any, CultureInfo.InvariantCulture, out var n) ? n : 80000m;
    public static decimal StudentDiscount => decimal.TryParse(Get("StudentDiscount","0.15"), NumberStyles.Any, CultureInfo.InvariantCulture, out var n) ? n : .15m;
    public static decimal AdultDiscount => decimal.TryParse(Get("AdultDiscount","0"), NumberStyles.Any, CultureInfo.InvariantCulture, out var n) ? n : 0m;
    public static int CleaningMinutes => int.TryParse(Get("CleaningMinutes","15"), out var n) ? n : 15;
}

public static class FileHelper
{
    public static string DataDir => Path.Combine(AppContext.BaseDirectory, "Data");

    public static void EnsureDataDir() => Directory.CreateDirectory(DataDir);

    /// <summary>
    /// Đọc từng dòng bằng StreamReader. Dòng sai định dạng chỉ bị bỏ qua và ghi log,
    /// không làm hỏng toàn bộ dữ liệu của tập tin.
    /// </summary>
    public static List<T> Read<T>(string file, Func<string, T> parser)
    {
        var result = new List<T>();
        var path = Path.Combine(DataDir, file);

        try
        {
            EnsureDataDir();

            // Tự tạo tập tin rỗng nếu chưa có để lần chạy sau có thể ghi trực tiếp.
            if (!File.Exists(path))
            {
                using (new StreamWriter(path, false, new UTF8Encoding(false))) { }
                return result;
            }

            using var reader = new StreamReader(path, Encoding.UTF8, true);
            string? line;
            var lineNumber = 0;

            while ((line = reader.ReadLine()) != null)
            {
                lineNumber++;
                if (string.IsNullOrWhiteSpace(line)) continue;

                try
                {
                    result.Add(parser(line));
                }
                catch (Exception ex)
                {
                    Log(new Exception(
                        $"Bỏ qua dòng {lineNumber} trong {file} vì sai định dạng: {line}",
                        ex));
                }
            }
        }
        catch (Exception ex)
        {
            Log(ex);
            MessageBox.Show(
                $"Không đọc được {file}: {ex.Message}",
                "Lỗi đọc dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }

        return result;
    }

    public static void Write<T>(string file, IEnumerable<T> rows)
    {
        try
        {
            EnsureDataDir();

            using var writer = new StreamWriter(
                Path.Combine(DataDir, file),
                false,
                new UTF8Encoding(false));

            foreach (var row in rows)
                writer.WriteLine(row?.ToString() ?? "");
        }
        catch (Exception ex)
        {
            Log(ex);
            MessageBox.Show(
                $"Không ghi được {file}: {ex.Message}",
                "Lỗi ghi dữ liệu",
                MessageBoxButtons.OK,
                MessageBoxIcon.Error);
        }
    }

    public static void Log(Exception ex)
    {
        try
        {
            EnsureDataDir();

            using var writer = new StreamWriter(
                Path.Combine(DataDir, "log.txt"),
                true,
                new UTF8Encoding(false));

            writer.WriteLine($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {ex}");
        }
        catch
        {
            // Không để lỗi ghi log làm dừng chương trình.
        }
    }
}

public static class AppData
{
    public static List<Movie> Movies = new();
    public static List<CinemaRoom> Rooms = new();
    public static List<Screening> Screenings = new();
    public static List<Ticket> Tickets = new();
    public static List<AppUser> Users = new();
    public static void Initialize()
    {
        FileHelper.EnsureDataDir();
        Movies=FileHelper.Read("movies.txt", Movie.FromLine);
        Rooms=FileHelper.Read("rooms.txt", CinemaRoom.FromLine);
        Screenings=FileHelper.Read("screenings.txt", Screening.FromLine);
        Tickets=FileHelper.Read("tickets.txt", Ticket.FromLine);
        Users=FileHelper.Read("users.txt", AppUser.FromLine);
        if (Users.Count==0)
        {
            Users.Add(new AppUser { Username="admin", Password="123", Role="QuanTri" });
            Users.Add(new AppUser { Username="nhanvien", Password="123", Role="NhanVien" });
            SaveUsers();
        }
    }
    public static void SaveMovies()=>FileHelper.Write("movies.txt",Movies);
    public static void SaveRooms()=>FileHelper.Write("rooms.txt",Rooms);
    public static void SaveScreenings()=>FileHelper.Write("screenings.txt",Screenings);
    public static void SaveTickets()=>FileHelper.Write("tickets.txt",Tickets);
    public static void SaveUsers()=>FileHelper.Write("users.txt",Users);
    /// <summary>
    /// Tìm suất chiếu bị trùng giờ trong cùng phòng.
    /// Khoảng chiếm phòng = thời lượng phim + số phút dọn phòng trong App.config.
    /// </summary>
    public static Screening? FindConflict(Screening candidate, Movie movie)
    {
        var candidateStart = candidate.StartTime;
        var candidateEnd = candidateStart.AddMinutes(
            movie.DurationMinutes + AppConfig.CleaningMinutes);

        foreach (var s in Screenings.Where(
            x => x.RoomId == candidate.RoomId && x.Id != candidate.Id))
        {
            var oldMovie = Movies.FirstOrDefault(x => x.Id == s.MovieId);

            if (oldMovie == null)
            {
                // Không tính được giờ kết thúc nếu dữ liệu phim bị thiếu.
                FileHelper.Log(new Exception(
                    $"Không tìm thấy phim {s.MovieId} của suất {s.Id}; " +
                    "bỏ qua suất này khi kiểm tra trùng lịch."));
                continue;
            }

            var oldStart = s.StartTime;
            var oldEnd = oldStart.AddMinutes(
                oldMovie.DurationMinutes + AppConfig.CleaningMinutes);

            // Hai khoảng [start, end) giao nhau.
            if (candidateStart < oldEnd && oldStart < candidateEnd)
                return s;
        }

        return null;
    }

    public static bool HasConflict(Screening candidate, Movie movie)
        => FindConflict(candidate, movie) != null;
    public static bool HasSoldTickets(string screeningId)=>Tickets.Any(t=>t.ScreeningId==screeningId);
    public static string SeatKey(string screeningId,string seat)=>screeningId+"|"+seat;
    public static (Color Back, Color Fore, float FontSize) LoadAppearance()
    {
        try
        {
            using var key=Registry.CurrentUser.OpenSubKey(@"Software\QuanLyRapChieuPhim");
            var back=Color.FromArgb(Convert.ToInt32(key?.GetValue("BackColor","-16777216") ?? "-16777216"));
            var fore=Color.FromArgb(Convert.ToInt32(key?.GetValue("ForeColor","-1") ?? "-1"));
            var size=Convert.ToSingle(key?.GetValue("FontSize","9") ?? "9");
            return (back,fore,size);
        } catch { return (Color.FromArgb(35,39,47),Color.White,9f); }
    }
    public static void SaveAppearance(Color back, Color fore, float size)
    {
        try { using var key=Registry.CurrentUser.CreateSubKey(@"Software\QuanLyRapChieuPhim"); key.SetValue("BackColor",back.ToArgb()); key.SetValue("ForeColor",fore.ToArgb()); key.SetValue("FontSize",size); }
        catch(Exception ex) { FileHelper.Log(ex); MessageBox.Show("Không lưu được Registry: "+ex.Message); }
    }
}
