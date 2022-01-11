using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public class RequestModel
{
    [Phone] public string ContactPhoneNumber { get; set; } = null!;
    [MaxLength(64)] public string ContactName { get; set; } = null!;
    public string DeparturePlace { get; set; } = null!;
    public string DestinationPlace { get; set; } = null!;

    [DataType(DataType.Currency)]
    [Range(0, 10000000)] public decimal? Price { get; set; }
    [MaxLength(512)] public string? Details { get; set; }
}
