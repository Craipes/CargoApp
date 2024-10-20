using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CargoApp.Models;

public abstract class BaseRequest : BaseEntity
{
    public const double MaxRequestTimeInHours = 24 * 14;
    public const double MinRequestTimeInHours = 3;
    public const double MinResponseTimeInHours = 0.5;

    [BindNever] [ValidateNever] public required string UserId { get; set; }
    [BindNever] public User? User { get; set; }

    [MaxLength(64)] public required string ContactName { get; set; }
    [Phone] public required string ContactPhoneNumber { get; set; }
    [EmailAddress] public string? ContactEmail { get; set; }
    public required string DeparturePlace { get; set; }
    public required string DestinationPlace { get; set; }

    public PriceType PriceType { get; set; } = PriceType.Negotiable;
    [DataType(DataType.Currency)][Column(TypeName = "decimal(10, 2)")][Range(0, 10000000)] public decimal? Price { get; set; }
    [MaxLength(512)] public string? Details { get; set; }

    [BindNever] public DateTime AddTime { get; set; } = DateTime.UtcNow;

    [NotMapped] public bool IsExpired => AddTime.AddHours(MinResponseTimeInHours) > DateTime.UtcNow;
}
