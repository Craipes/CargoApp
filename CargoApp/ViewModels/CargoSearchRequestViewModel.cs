namespace CargoApp.ViewModels;

public class CargoSearchRequestViewModel
{
    [Display(Name = "Departure time")][CorrectDepartureTime(CargoAppConstants.MinResponseTimeInHours)] public DateTime? DepartureTime { get; set; }
    [Display(Name = "Has GPS?")] public bool HasGPS { get; set; }
}
