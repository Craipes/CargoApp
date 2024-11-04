using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace CargoApp.Models;

public abstract class BaseRequest : BaseEntity
{
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

    [BindNever] [ValidateNever] [NotMapped] public abstract bool CanBeResponded { get; }
}
