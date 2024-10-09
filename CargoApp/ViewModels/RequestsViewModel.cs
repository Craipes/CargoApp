namespace CargoApp.ViewModels;

public class RequestsViewModel
{
    public List<CarRequestViewModel> CarRequests { get; set; }
    public List<CargoRequestViewModel> CargoRequests { get; set; }

    public RequestsViewModel(IEnumerable<CarRequestViewModel> carRequests, IEnumerable<CargoRequestViewModel> cargoRequests)
    {
        CarRequests = new(carRequests);
        CargoRequests = new(cargoRequests);
    }
}
