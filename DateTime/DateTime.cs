// See https://aka.ms/new-console-template for more information

Console.WriteLine("TÍNH SỐ NGÀY BẠN ĐÃ SỐNG");
Console.Write("Nhập ngày sinh của bạn (định dạng dd/MM/yyyy: ");
string? input = Console.ReadLine();

if (!DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime birthDate))
{
    Console.WriteLine("Ngày sinh không hợp lệ! Vui lòng nhập lại đúng định dạng dd/MM/yyyy");
    return;
}

DateTime today =  DateTime.Now;

TimeSpan songduoc = today - birthDate;
double soNgay =  songduoc.TotalDays;

Console.WriteLine($"Bạn sinh ngày: {birthDate:dd/MM/yyyy}");
Console.WriteLine($"Hôm nay là: {today:dd/MM/yyyy}");
Console.WriteLine($"Bạn đã sống được khoảng: {Math.Floor(soNgay)} ngày");
Console.WriteLine($"Tức là khoảng {Math.Floor(soNgay / 365)} năm và {Math.Floor((soNgay % 365) / 30)} tháng");