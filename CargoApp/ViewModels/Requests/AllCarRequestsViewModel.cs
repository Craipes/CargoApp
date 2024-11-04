namespace CargoApp.ViewModels.Requests;

public record AllCarRequestsViewModel(string Id, string Name, int Page, int MaxPages, List<CarRequest> Requests);
