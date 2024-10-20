namespace CargoApp.Models;

public class CarResponse : BaseResponse
{
    public int CarRequestId { get; set; }
    public CarRequest CarRequest { get; set; } = null!;

    public int CarId { get; set; }
    public Car Car { get; set; } = null!;
}
