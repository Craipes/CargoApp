using ApplicationCore.Models;

namespace CargoApp.ViewModels;

public class RequestsViewModel
{
    public List<CarRequestModel> CarRequests { get; set; }
    public List<CargoRequestModel> CargoRequests { get; set; }

    public RequestsViewModel(IEnumerable<CarRequestModel> carRequests, IEnumerable<CargoRequestModel> cargoRequests)
    {
        CarRequests = new(carRequests);
        CargoRequests = new(cargoRequests);
    }
}
