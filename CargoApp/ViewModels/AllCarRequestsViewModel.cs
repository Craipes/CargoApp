namespace CargoApp.ViewModels;

public record AllCarRequestsViewModel(string? Id, string? Name, int Page, int MaxPages, List<CarRequest> Requests);
