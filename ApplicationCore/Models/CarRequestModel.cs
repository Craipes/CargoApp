namespace ApplicationCore.Models;

public class CarRequestModel : RequestModel
{
    [Range(0.5f, 1000000f)] public float CargoMass { get; set; }

    [Range(0.10f, 100f)] public float? CargoLength { get; set; }

    [Range(0.10f, 100f)] public float? CargoWidth { get; set; }

    [Range(0.10f, 100f)] public float? CargoHeight { get; set; }

    [Range(0.0010f, 50000f)] public float CargoVolume { get; set; }

    public CarRequest CreateRequest(string userId, int departurePlaceId, int destinationPlaceId)
    {
        return new CarRequest(userId, ContactPhoneNumber, ContactName, departurePlaceId, destinationPlaceId,
            Price, Details, CargoMass, CargoVolume, CargoLength, CargoWidth, CargoHeight);
    }

    public static CarRequestModel FromRequest(CarRequest request)
    {
        return new CarRequestModel()
        {
            ContactName = request.ContactName,
            ContactPhoneNumber = request.ContactPhoneNumber,
            DeparturePlace = request.DeparturePlace?.GetFullName() ?? "",
            DestinationPlace = request.DestinationPlace?.GetFullName() ?? "",
            Price = request.Price,
            Details = request.Details,
            CargoMass = request.CargoMass,
            CargoVolume = request.CargoVolume,
            CargoLength = request.CargoLength,
            CargoWidth = request.CargoWidth,
            CargoHeight = request.CargoHeight,
            AddTime = request.AddTime,
            IsExpired = request.IsExpired
        };
    }
}
