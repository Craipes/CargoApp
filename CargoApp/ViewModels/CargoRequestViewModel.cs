namespace CargoApp.ViewModels;

public class CargoRequestViewModel
{
    public required CargoRequest CargoRequest { get; set; }
    [Display(Name = "Name")] public string UserName { get; set; } = string.Empty;
    public List<CargoResponse> Responses { get; set; } = [];
    public bool AllowEditing { get; set; }
}