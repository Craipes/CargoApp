using Microsoft.Extensions.Options;

namespace CargoApp.Models;

public class CarResponse : BaseResponse
{
    public int CarRequestId { get; set; }
    public CarRequest CarRequest { get; set; } = null!;
    [ValidateObjectMembers] public Car Car { get; set; } = null!;
}
