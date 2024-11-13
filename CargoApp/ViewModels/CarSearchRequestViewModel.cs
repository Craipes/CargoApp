namespace CargoApp.ViewModels;

public class CarSearchRequestViewModel
{
    [Display(Name = "Early departure time")] [CorrectDepartureDate] public DateTime? EarlyDepartureTime { get; set; }
    [Display(Name = "Late departure time")] [CorrectDepartureDate] [LaterThan(nameof(EarlyDepartureTime))] public DateTime? LateDepartureTime { get; set; }
    [Display(Name = "Needs GPS?")] public bool NeedsGPS { get; set; }
}
