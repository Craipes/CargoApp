namespace CargoApp.ViewModels;

public class SearchViewModel
{
    public bool SearchWasPerformed { get; set; } = false;
    public bool IsCarSearching { get; set; } = false;
    public bool IsCargoSearching { get; set; } = false;
    public BaseSearchRequestViewModel Search { get; set; } = new();
    public CarSearchRequestViewModel CarSearch { get; set; } = new();
    public CargoSearchRequestViewModel CargoSearch { get; set; } = new();
    public List<CargoRequest>? CargoRequests { get; set; }
    public List<CarRequest>? CarRequests { get; set; }
}
