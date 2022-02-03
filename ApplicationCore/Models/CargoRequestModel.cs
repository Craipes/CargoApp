using ApplicationCore.Entities;

namespace ApplicationCore.Models;

public class CargoRequestModel : RequestModel
{
    public Car? Car { get; set; }
    public DateTime DepartureTime { get; set; }

    public CargoRequest CreateRequest(string userId, int departurePlaceId, int destinationPlaceId)
    {
        return new CargoRequest(userId, ContactPhoneNumber, ContactName, departurePlaceId, destinationPlaceId,
            Price, Details, Car?.Id, DepartureTime)
        {
            Car = Car
        };
    }

    public static CargoRequestModel FromRequest(CargoRequest request)
    {
        return new CargoRequestModel()
        {
            ContactName = request.ContactName,
            ContactPhoneNumber = request.ContactPhoneNumber,
            DeparturePlace = request.DeparturePlace?.GetFull() ?? "",
            DestinationPlace = request.DestinationPlace?.GetFull() ?? "",
            Price = request.Price,
            Details = request.Details,
            Car = request.Car,
            DepartureTime = request.DepartureTime,
            AddTime = request.AddTime
        };
    }
}
