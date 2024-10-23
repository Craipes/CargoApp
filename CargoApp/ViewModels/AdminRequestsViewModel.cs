namespace CargoApp.ViewModels;

public class AdminRequestsViewModel
{
    public List<AdminCarRequestViewModel> CarRequests { get; set; } = [];
    public List<AdminCargoRequestViewModel> CargoRequests { get; set; } = [];
}
