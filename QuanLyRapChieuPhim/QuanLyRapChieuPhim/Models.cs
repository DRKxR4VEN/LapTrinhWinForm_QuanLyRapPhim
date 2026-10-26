using System.Globalization;

namespace QuanLyRapChieuPhim;

public class Movie
{
    public string Id { get; set; } = "";
    public string Title { get; set; } = "";
    public string Genre { get; set; } = "";
    public int DurationMinutes { get; set; }
    public string PosterPath { get; set; } = "";
    public override string ToString() => string.Join(";", Id, Title, Genre, DurationMinutes, PosterPath);
    public static Movie FromLine(string s) { var p=s.Split(';'); return new Movie { Id=p[0], Title=p[1], Genre=p[2], DurationMinutes=int.Parse(p[3]), PosterPath=p.Length>4?p[4]:"" }; }
}

public class CinemaRoom
{
    public string Id { get; set; } = "";
    public string Name { get; set; } = "";
    public int Rows { get; set; } = 5;
    public int SeatsPerRow { get; set; } = 8;
    public int VipRows { get; set; } = 1;
    public int Capacity => Rows * SeatsPerRow;
    public override string ToString() => string.Join(";", Id, Name, Rows, SeatsPerRow, VipRows);
    public static CinemaRoom FromLine(string s) { var p=s.Split(';'); return new CinemaRoom { Id=p[0], Name=p[1], Rows=int.Parse(p[2]), SeatsPerRow=int.Parse(p[3]), VipRows=int.Parse(p[4]) }; }
}

public class Screening
{
    public string Id { get; set; } = "";
    public string MovieId { get; set; } = "";
    public string RoomId { get; set; } = "";
    public DateTime StartTime { get; set; }
    public string MovieTitle { get; set; } = "";
    public string RoomName { get; set; } = "";
    public DateTime EndTime(int duration) => StartTime.AddMinutes(duration + AppConfig.CleaningMinutes);
    public string Display => $"{MovieTitle} | {RoomName} | {StartTime:HH:mm dd/MM/yyyy}";
    public override string ToString() => string.Join(";", Id, MovieId, RoomId, StartTime.ToString("O"), MovieTitle, RoomName);
    public static Screening FromLine(string s) { var p=s.Split(';'); return new Screening { Id=p[0], MovieId=p[1], RoomId=p[2], StartTime=DateTime.Parse(p[3], null, DateTimeStyles.RoundtripKind), MovieTitle=p.Length>4?p[4]:"", RoomName=p.Length>5?p[5]:"" }; }
}

public class Ticket
{
    public string Id { get; set; } = "";
    public string ScreeningId { get; set; } = "";
    public string MovieTitle { get; set; } = "";
    public string RoomName { get; set; } = "";
    public DateTime ShowTime { get; set; }
    public string Seat { get; set; } = "";
    public bool IsVip { get; set; }
    public string CustomerType { get; set; } = "Người lớn";
    public decimal Price { get; set; }
    public DateTime SoldAt { get; set; } = DateTime.Now;
    public override string ToString() => string.Join(";", Id, ScreeningId, MovieTitle, RoomName, ShowTime.ToString("O"), Seat, IsVip, CustomerType, Price.ToString(CultureInfo.InvariantCulture), SoldAt.ToString("O"));
    public static Ticket FromLine(string s) { var p=s.Split(';'); return new Ticket { Id=p[0], ScreeningId=p[1], MovieTitle=p[2], RoomName=p[3], ShowTime=DateTime.Parse(p[4], null, DateTimeStyles.RoundtripKind), Seat=p[5], IsVip=bool.Parse(p[6]), CustomerType=p[7], Price=decimal.Parse(p[8], CultureInfo.InvariantCulture), SoldAt=DateTime.Parse(p[9], null, DateTimeStyles.RoundtripKind) }; }
}

public class AppUser
{
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
    public string Role { get; set; } = "NhanVien";
    public override string ToString() => $"{Username};{Password};{Role}";
    public static AppUser FromLine(string s) { var p=s.Split(';'); return new AppUser { Username=p[0], Password=p[1], Role=p[2] }; }
}
