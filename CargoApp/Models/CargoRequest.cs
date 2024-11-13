using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace CargoApp.Models;

public class CargoRequest : BaseRequest
{
    [ValidateObjectMembers] public Car Car { get; set; } = null!;
    [Display(Name = "Departure time")] [CorrectDepartureTime(CargoAppConstants.MinRequestTimeInHours)] public required DateTime DepartureTime { get; set; }

    public List<CargoResponse> Responses { get; set; } = [];

    public override bool CanBeResponded => DepartureTime >= DateTime.UtcNow.AddHours(CargoAppConstants.MinResponseTimeInHours);
}
