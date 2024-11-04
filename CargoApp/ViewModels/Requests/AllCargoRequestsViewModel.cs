namespace CargoApp.ViewModels.Requests;

public record AllCargoRequestsViewModel(string Id, string Name, int Page, int MaxPages, List<CargoRequest> Requests);