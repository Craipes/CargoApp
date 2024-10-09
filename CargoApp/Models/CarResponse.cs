namespace CargoApp.Models;

public class CarResponse
{
    public int CarRequestId { get; set; }
    public CarRequest? CarRequest { get; set; }

    //Check
    public string DriverId { get; set; }
    public UserInfo? Driver { get; set; }
    public Car? Car { get; set; }

    [MaxLength(512)] public string? Comment { get; set; }

    public CarResponse(int carRequestId, string driverId)
    {
        CarRequestId = carRequestId;
        DriverId = driverId;
    }
}
