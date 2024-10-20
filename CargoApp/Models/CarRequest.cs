using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CargoApp.Models;

public class CarRequest : BaseRequest
{
    [BindNever] public int CargoId { get; set; }
    public Cargo Cargo { get; set; } = null!;

    [CorrectDepartureTime] public DateTime EarlyDepartureDate { get; set; }
    [CorrectDepartureTime] public DateTime LateDepartureDate { get; set; }
    public bool? NeedsGPS { get; set; }

    public List<CarResponse> Responses { get; set; } = [];
}
