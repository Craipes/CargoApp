namespace CargoApp.ViewModels;

public record AllCargoRequestsViewModel(string? Id, string? Name, int Page, int MaxPages, List<CargoRequest> Requests);