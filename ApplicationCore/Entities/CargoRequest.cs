namespace ApplicationCore.Entities;

public class CargoRequest : Request
{
    public Car? Car { get; set; }
    public DateTime DepartureTime { get; set; }

    public List<CargoResponse> Responses { get; set; } = new();

    public CargoRequest(string userId, string contactPhoneNumber, string contactName,
        int departurePlaceId, int destinationPlaceId, decimal? price, string? details) 
        : base(userId, contactPhoneNumber, contactName, departurePlaceId, destinationPlaceId, price, details)
    {
    }
}
