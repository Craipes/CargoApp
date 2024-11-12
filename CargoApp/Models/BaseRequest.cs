using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace CargoApp.Models;

public abstract class BaseRequest : BaseEntity
{
    [BindNever] [ValidateNever] public required string UserId { get; set; }
    [BindNever] public User? User { get; set; }

    [Display(Name = "Contact name")][MaxLength(64)] public required string ContactName { get; set; }
    [Display(Name = "Contact phone")][Phone] public required string ContactPhoneNumber { get; set; }
    [Display(Name = "Contact email")][EmailAddress] public string? ContactEmail { get; set; }
    [Display(Name = "Departure place")] public required string DeparturePlace { get; set; }
    [Display(Name = "Destination place")] public required string DestinationPlace { get; set; }

    [Display(Name = "Price type")] public PriceType PriceType { get; set; } = PriceType.Negotiable;
    [Display(Name = "Price")][Column(TypeName = "decimal(10, 2)")][Range(0, 10000000)] public decimal? Price { get; set; }
    [Display(Name = "Details")][MaxLength(512)] public string? Details { get; set; }

    [Display(Name = "Request status")] public RequestType RequestType { get; set; }

    [Display(Name = "Adding time")] [BindNever] public DateTime AddTime { get; set; } = DateTime.UtcNow;

    [BindNever] [ValidateNever] [NotMapped] public abstract bool CanBeResponded { get; }
}
