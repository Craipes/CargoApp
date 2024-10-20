using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;

namespace CargoApp.Models;

public class CargoRequest : BaseRequest
{
    [ValidateObjectMembers] public Car Car { get; set; } = null!;
    [CorrectDepartureTime] public required DateTime DepartureTime { get; set; }

    public List<CargoResponse> Responses { get; set; } = [];
}
