namespace CargoApp.Services;

public class CargoAppService
{
    private readonly CargoAppContext db;

    public CargoAppService(CargoAppContext cargoAppContext)
    {
        db = cargoAppContext;
    }
}
