namespace CargoApp.Models;

public class Car : BaseEntity
{
    public CarType Type { get; set; }
    public TrailerType TrailerType { get; set; }

    [Column(TypeName = "decimal(8, 1)")][Range(0.5f, 1000000f)] public float MaxMass { get; set; }
    [Column(TypeName = "decimal(5, 2)")][Range(0.10f, 100f)] public float? MaxLength { get; set; }
    [Column(TypeName = "decimal(5, 2)")][Range(0.10f, 100f)] public float? MaxWidth { get; set; }
    [Column(TypeName = "decimal(5, 2)")][Range(0.10f, 100f)] public float? MaxHeight { get; set; }
    [Column(TypeName = "decimal(9, 4)")][Range(0.0010f, 50000f)] public float? MaxVolume { get; set; }

    public bool AvailableGPS { get; set; }
}
