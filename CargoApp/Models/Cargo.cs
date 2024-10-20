namespace CargoApp.Models;

public class Cargo
{
    public TrailerType TrailerType { get; set; }

    [Column(TypeName = "decimal(8, 3)")][Range(0.001f, 100000f, MaximumIsExclusive = true)] public float Mass { get; set; }
    [Column(TypeName = "decimal(5, 2)")][Range(0.10f, 1000f, MaximumIsExclusive = true)] public float? Length { get; set; }
    [Column(TypeName = "decimal(5, 2)")][Range(0.10f, 1000f, MaximumIsExclusive = true)] public float? Width { get; set; }
    [Column(TypeName = "decimal(5, 2)")][Range(0.10f, 1000f, MaximumIsExclusive = true)] public float? Height { get; set; }
    [Column(TypeName = "decimal(9, 4)")][Range(0.0010f, 100000f, MaximumIsExclusive = true)] public float? Volume { get; set; }
    [MaxLength(512)] public string? Description { get; set; }
}
