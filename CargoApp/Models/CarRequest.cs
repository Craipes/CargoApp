using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace CargoApp.Models;

public class CarRequest : BaseRequest
{
    [ValidateObjectMembers] public Cargo Cargo { get; set; } = null!;

    [Display(Name = "Early departure time")][DisplayFormat(DataFormatString = "{0:D}")][CorrectDepartureTime] public DateTime EarlyDepartureDate { get; set; }
    [Display(Name = "Late departure time")][DisplayFormat(DataFormatString = "{0:D}")][CorrectDepartureTime][LaterThan(nameof(EarlyDepartureDate))] public DateTime LateDepartureDate { get; set; }
    [Display(Name = "Needs GPS?")] public bool NeedsGPS { get; set; }

    public List<CarResponse> Responses { get; set; } = [];

    public override bool CanBeResponded => LateDepartureDate >= DateTime.UtcNow.Date.AddHours(CargoAppConstants.MinResponseTimeInHours);
}
