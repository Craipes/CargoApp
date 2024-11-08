namespace CargoApp.Models;

public class Car : BaseEntity
{
    public CarType Type { get; set; }
    public TrailerType TrailerType { get; set; }

    [Column(TypeName = "decimal(8, 1)")][Range(0.5, 1000000)] public float MaxMass { get; set; }
    [Column(TypeName = "decimal(5, 2)")][Range(0.1, 100)] public float? MaxLength { get; set; }
    [Column(TypeName = "decimal(5, 2)")][Range(0.1, 100)] public float? MaxWidth { get; set; }
    [Column(TypeName = "decimal(5, 2)")][Range(0.1, 100)] public float? MaxHeight { get; set; }
    [Column(TypeName = "decimal(9, 4)")][Range(0.001, 50000)] public float? MaxVolume { get; set; }

    public bool AvailableGPS { get; set; }
}
