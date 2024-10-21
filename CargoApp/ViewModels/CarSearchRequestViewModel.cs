namespace CargoApp.ViewModels;

public class CarSearchRequestViewModel : BaseSearchRequestViewModel
{
    [CorrectDepartureTime] public DateTime? LateDepartureTime { get; set; }
}
