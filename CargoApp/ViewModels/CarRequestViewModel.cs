namespace CargoApp.ViewModels;

public class CarRequestViewModel
{
    public required CarRequest CarRequest { get; set; }
    [Display(Name = "Name")] public string UserName { get; set; } = string.Empty;
    public List<CarResponse> Responses { get; set; } = [];
    public bool AllowEditing { get; set; }
}