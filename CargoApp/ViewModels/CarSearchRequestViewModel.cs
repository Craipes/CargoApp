namespace CargoApp.ViewModels;

public class CarSearchRequestViewModel : BaseSearchRequestViewModel
{
    [CorrectDepartureTime] [LaterThan(nameof(DepartureTime))] public DateTime? LateDepartureTime { get; set; }
}
