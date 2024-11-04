namespace CargoApp;

public static class DateTimeExtensions
{
    public static DateTime RoundToMinutes(this DateTime dateTime) => new(dateTime.Year, dateTime.Month, dateTime.Day, dateTime.Hour, dateTime.Minute, 0, 0, dateTime.Kind);
}
