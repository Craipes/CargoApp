namespace CargoApp.ViewModels;

public class IndexViewModel
{
    public bool IsCarRequest { get; set; } = false;
    public bool IsCargoRequest { get; set; } = false;
    public CarRequestViewModel CarRequest { get; set; } = new();
    public CargoRequestViewModel CargoRequest { get; set; } = new();
}
