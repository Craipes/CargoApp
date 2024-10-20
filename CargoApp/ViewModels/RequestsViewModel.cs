namespace CargoApp.ViewModels;

public class RequestsViewModel
{
    public List<CarRequest> CarRequests { get; set; }
    public List<CargoRequest> CargoRequests { get; set; }

    public RequestsViewModel(IEnumerable<CarRequest> carRequests, IEnumerable<CargoRequest> cargoRequests)
    {
        CarRequests = new(carRequests);
        CargoRequests = new(cargoRequests);
    }
}
