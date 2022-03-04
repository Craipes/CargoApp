namespace CargoApp.ViewModels;

public class SearchViewModel
{
    public bool IsCarSearching { get; set; } = false;
    public bool IsCargoSearching { get; set; } = false;
    public SearchModel CarSearch { get; set; } = new();
    public SearchModel CargoSearch { get; set; } = new();
    public List<CargoRequestModel>? CargoRequests { get; set; }
    public List<CarRequestModel>? CarRequests { get; set; }
}
