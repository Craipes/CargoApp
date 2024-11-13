using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.Options;

namespace CargoApp.Models;

public class CarResponse : BaseResponse
{
    public int CarRequestId { get; set; }
    [ValidateNever] public CarRequest CarRequest { get; set; } = null!;
    [ValidateObjectMembers] public Car Car { get; set; } = null!;
}
