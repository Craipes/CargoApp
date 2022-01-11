using ApplicationCore.Models;

namespace CargoApp.ViewModels;

public class IndexViewModel
{
    public bool IsCarRequest { get; set; } = true;
    public CarRequestModel CarRequest { get; set; } = new();
    public CargoRequestModel CargoRequest { get; set; } = new();
}
