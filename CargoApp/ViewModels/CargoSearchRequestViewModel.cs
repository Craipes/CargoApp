namespace CargoApp.ViewModels;

public class CargoSearchRequestViewModel : BaseSearchRequestViewModel
{
    [Display(Name = "Departure time")][CorrectDepartureTime] public DateTime? DepartureTime { get; set; }
    [Display(Name = "Has GPS?")] public bool HasGPS { get; set; }
}
