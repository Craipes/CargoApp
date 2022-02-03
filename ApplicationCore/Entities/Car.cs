using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

public class Car : BaseEntity
{
    public string? DriverId { get; set; }
    public UserInfo? Driver { get; set; }

    [MaxLength(24)] public string? NumberPlate { get; set; }
    [MaxLength(32)] public string? Brand { get; set; }
    [MaxLength(64)] public string? Model { get; set; }
    [MaxLength(32)] public string? Color { get; set; }

    [Column(TypeName = "decimal(8, 1)")]
    [Range(0.5f, 1000000f)] public float MaxMass { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.10f, 100f)] public float? MaxLength { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.10f, 100f)] public float? MaxWidth { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.10f, 100f)] public float? MaxHeight { get; set; }

    [Column(TypeName = "decimal(9, 4)")]
    [Range(0.0010f, 50000f)] public float? MaxVolume { get; set; }
}
