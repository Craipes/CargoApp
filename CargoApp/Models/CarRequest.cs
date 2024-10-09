namespace CargoApp.Models;

public class CarRequest : Request
{
    [Column(TypeName = "decimal(8, 1)")]
    [Range(0.5f, 1000000f)] public float CargoMass { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.10f, 100f)] public float? CargoLength { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.10f, 100f)] public float? CargoWidth { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.10f, 100f)] public float? CargoHeight { get; set; }

    [Column(TypeName = "decimal(9, 4)")]
    [Range(0.0010f, 50000f)] public float CargoVolume { get; set; }

    public List<CarResponse> Responses { get; set; } = new();

    public CarRequest(string userId, string contactPhoneNumber, string contactName,
        int departurePlaceId, int destinationPlaceId, decimal? price, string? details,
        float cargoMass, float cargoVolume, float? cargoLength, float? cargoWidth, float? cargoHeight)
        : base(userId, contactPhoneNumber, contactName, departurePlaceId, destinationPlaceId, price, details)
    {
        CargoMass = cargoMass;
        CargoLength = cargoLength;
        CargoWidth = cargoWidth;
        CargoHeight = cargoHeight;
        CargoVolume = cargoVolume;
    }
}
