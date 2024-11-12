namespace CargoApp.Models;

public class Car : BaseEntity
{
    [Display(Name = "Car type")] public CarType Type { get; set; }
    [Display(Name = "Trailer type")] public TrailerType TrailerType { get; set; }

    [Display(Name = "Max mass")][Column(TypeName = "decimal(8, 1)")][Range(0.5, 1000000, ErrorMessage = "Range Error")][Required(ErrorMessage = "Required Error")] public float MaxMass { get; set; }
    [Display(Name = "Max length")][Column(TypeName = "decimal(5, 2)")][Range(0.1, 100, ErrorMessage = "Range Error")] public float? MaxLength { get; set; }
    [Display(Name = "Max width")][Column(TypeName = "decimal(5, 2)")][Range(0.1, 100, ErrorMessage = "Range Error")] public float? MaxWidth { get; set; }
    [Display(Name = "Max height")][Column(TypeName = "decimal(5, 2)")][Range(0.1, 100, ErrorMessage = "Range Error")] public float? MaxHeight { get; set; }
    [Display(Name = "Max volume")][Column(TypeName = "decimal(9, 4)")][Range(0.001, 50000, ErrorMessage = "Range Error")] public float? MaxVolume { get; set; }

    [Display(Name = "Has GPS?")] public bool AvailableGPS { get; set; }
}
