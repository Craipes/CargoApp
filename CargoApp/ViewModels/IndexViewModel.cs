namespace CargoApp.ViewModels;

public class IndexViewModel
{
    public bool IsCarRequest { get; set; } = false;
    public bool IsCargoRequest { get; set; } = false;
    public CarRequestModel CarRequest { get; set; } = new();
    public CargoRequestModel CargoRequest { get; set; } = new();
}
