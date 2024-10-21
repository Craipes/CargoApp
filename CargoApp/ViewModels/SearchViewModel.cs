namespace CargoApp.ViewModels;

public class SearchViewModel
{
    public bool IsCarSearching { get; set; } = false;
    public bool IsCargoSearching { get; set; } = false;
    public CarSearchRequestViewModel CarSearch { get; set; } = new();
    public BaseSearchRequestViewModel CargoSearch { get; set; } = new();
    public List<CargoRequest>? CargoRequests { get; set; }
    public List<CarRequest>? CarRequests { get; set; }
}
