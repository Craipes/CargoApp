namespace CargoApp.ViewModels;

public class BaseSearchRequestViewModel
{
    public string DeparturePlace { get; set; } = null!;
    public string DestinationPlace { get; set; } = null!;
    [CorrectDepartureTime] public DateTime? DepartureTime { get; set; }
    public TrailerType TrailerType { get; set; }
    [Column(TypeName = "decimal(8, 3)")][Range(0.001f, 100000f, MaximumIsExclusive = true)] public float? Mass { get; set; }
    [Column(TypeName = "decimal(5, 2)")][Range(0.10f, 1000f, MaximumIsExclusive = true)] public float? Length { get; set; }
    [Column(TypeName = "decimal(5, 2)")][Range(0.10f, 1000f, MaximumIsExclusive = true)] public float? Width { get; set; }
    [Column(TypeName = "decimal(5, 2)")][Range(0.10f, 1000f, MaximumIsExclusive = true)] public float? Height { get; set; }
    [Column(TypeName = "decimal(9, 4)")][Range(0.0010f, 100000f, MaximumIsExclusive = true)] public float? Volume { get; set; }
    public bool GPS { get; set; }
}
