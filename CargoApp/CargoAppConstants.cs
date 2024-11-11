namespace CargoApp;

public static class CargoAppConstants
{
    public const string AdminRole = "Admin";

    public const int RequestsPerPage = 4;
    public const int UsersPerPage = 10;
    public const int LatestRequestsMaxCount = 3;
    public const int LatestRequestsMaxDays = 14;

    public const double MaxRequestTimeInHours = 24 * 14;
    public const double MinRequestTimeInHours = 3;
    public const double MinResponseTimeInHours = 0.5;
}
