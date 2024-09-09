namespace ApplicationCore.Entities;

public class CargoRequest : Request
{
    public int? CarId { get; set; }
    public Car? Car { get; set; }
    [CorrectDepartureTime] public DateTime DepartureTime { get; set; }

    public List<CargoResponse> Responses { get; set; } = new();

    public CargoRequest(string userId, string contactPhoneNumber, string contactName,
        int departurePlaceId, int destinationPlaceId, decimal? price, string? details,
        int? carId, DateTime departureTime)
        : base(userId, contactPhoneNumber, contactName, departurePlaceId, destinationPlaceId, price, details)
    {
        CarId = carId;
        DepartureTime = departureTime;
    }
}
