namespace CargoApp.ViewModels;

public class CarSearchRequestViewModel : BaseSearchRequestViewModel
{
    [Display(Name = "Early departure time")] [CorrectDepartureTime] public DateTime? EarlyDepartureTime { get; set; }
    [Display(Name = "Late departure time")] [CorrectDepartureTime] [LaterThan(nameof(EarlyDepartureTime))] public DateTime? LateDepartureTime { get; set; }
    [Display(Name = "Needs GPS?")] public bool NeedsGPS { get; set; }
}
