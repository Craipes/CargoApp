namespace CargoApp.ViewModels;

public class RequestsViewModel
{
    public string Name { get; set; }
    public List<CarRequest> CarRequests { get; set; }
    public List<CargoRequest> CargoRequests { get; set; }

    public RequestsViewModel(string name, IEnumerable<CarRequest> carRequests, IEnumerable<CargoRequest> cargoRequests)
    {
        Name = name;
        CarRequests = new(carRequests);
        CargoRequests = new(cargoRequests);
    }
}
