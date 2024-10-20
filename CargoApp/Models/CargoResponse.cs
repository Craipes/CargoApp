using Microsoft.Extensions.Options;

namespace CargoApp.Models;

public class CargoResponse : BaseResponse
{
    public int CargoRequestId { get; set; }
    public CargoRequest CargoRequest { get; set; } = null!;
    [ValidateObjectMembers] public Cargo Cargo { get; set; } = null!;
}
