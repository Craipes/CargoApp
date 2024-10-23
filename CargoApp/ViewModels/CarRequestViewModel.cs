namespace CargoApp.ViewModels;

public class CarRequestViewModel
{
    public required CarRequest CarRequest { get; set; }
    public string UserName { get; set; } = string.Empty;
    public List<CarResponse> Responses { get; set; } = [];
}