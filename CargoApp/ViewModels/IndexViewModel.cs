namespace CargoApp.ViewModels;

public class IndexViewModel
{
    public bool IsCarRequest { get; set; } = false;
    public bool IsCargoRequest { get; set; } = false;
    public CarRequest CarRequest { get; set; } = null!;
    public CargoRequest CargoRequest { get; set; } = null!;
}
