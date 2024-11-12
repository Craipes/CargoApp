namespace CargoApp.ViewModels;

public class BaseSearchRequestViewModel
{
    [Display(Name = "Departure place")] public string DeparturePlace { get; set; } = null!;
    [Display(Name = "Destination place")] public string DestinationPlace { get; set; } = null!;   
    [Display(Name = "Trailer type")] public TrailerType TrailerType { get; set; }
    [Display(Name = "Mass")][Column(TypeName = "decimal(8, 3)")][Range(0.001, 100000, MaximumIsExclusive = true)] public float? Mass { get; set; }
    [Display(Name = "Length")][Column(TypeName = "decimal(5, 2)")][Range(0.1, 1000, MaximumIsExclusive = true)] public float? Length { get; set; }
    [Display(Name = "Width")][Column(TypeName = "decimal(5, 2)")][Range(0.1, 1000, MaximumIsExclusive = true)] public float? Width { get; set; }
    [Display(Name = "Height")][Column(TypeName = "decimal(5, 2)")][Range(0.1, 1000, MaximumIsExclusive = true)] public float? Height { get; set; }
    [Display(Name = "Volume")][Column(TypeName = "decimal(9, 4)")][Range(0.001, 100000, MaximumIsExclusive = true)] public float? Volume { get; set; }
}
