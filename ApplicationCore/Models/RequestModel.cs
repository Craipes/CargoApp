using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public abstract class RequestModel
{
    [Phone] public string ContactPhoneNumber { get; set; } = null!;
    [MaxLength(64)] public string ContactName { get; set; } = null!;
    [Required] public string DeparturePlace { get; set; } = null!;
    [Required] public string DestinationPlace { get; set; } = null!;

    [DataType(DataType.Currency)]
    [Range(0, 10000000)] public decimal? Price { get; set; }
    [MaxLength(512)] public string? Details { get; set; }

    public DateTime? AddTime { get; set; } = null;
}
