namespace CargoApp.ViewModels.Requests;

public record RequestsViewModel(string Id, string Name, List<CarRequest> CarRequests, List<CargoRequest> CargoRequests);
