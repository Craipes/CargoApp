using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;

namespace CargoApp.Models;

public abstract class BaseRequest : BaseRequestResponse
{
    [Display(Name = "Departure place")][Required(ErrorMessage = "Required Error")] public required string DeparturePlace { get; set; }
    [Display(Name = "Destination place")][Required(ErrorMessage = "Required Error")] public required string DestinationPlace { get; set; }

    [Display(Name = "Price type")] public PriceType PriceType { get; set; } = PriceType.Negotiable;
    [Display(Name = "Price")][Column(TypeName = "decimal(10, 2)")][Range(0, 10000000, ErrorMessage = "Range Error")][Required(ErrorMessage = "Required Error")] public decimal? Price { get; set; }

    [Display(Name = "Request status")] public RequestType RequestType { get; set; }

    [BindNever] [ValidateNever] [NotMapped] public abstract bool CanBeResponded { get; }
}
