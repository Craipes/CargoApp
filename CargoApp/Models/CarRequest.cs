using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace CargoApp.Models;

public class CarRequest : BaseRequest
{
    [ValidateObjectMembers] public Cargo Cargo { get; set; } = null!;

    [CorrectDepartureTime] public DateTime EarlyDepartureDate { get; set; }
    [CorrectDepartureTime] public DateTime LateDepartureDate { get; set; }
    public bool NeedsGPS { get; set; }

    public List<CarResponse> Responses { get; set; } = [];

    public override bool CanBeResponded => LateDepartureDate >= DateTime.UtcNow.Date.AddHours(MinResponseTimeInHours);
}
