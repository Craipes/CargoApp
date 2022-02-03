using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

public abstract class Request : BaseEntity
{
    public string UserId { get; set; }
    public UserInfo? User { get; set; }

    [Phone] public string ContactPhoneNumber { get; set; }
    [MaxLength(64)] public string ContactName { get; set; }
    public int DeparturePlaceId { get; set; }
    public Settlement? DeparturePlace { get; set; }
    public int DestinationPlaceId { get; set; }
    public Settlement? DestinationPlace { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(10, 2)")]
    [Range(0, 10000000)] public decimal? Price { get; set; }
    [MaxLength(512)] public string? Details { get; set; }

    public DateTime AddTime { get; set; } = DateTime.UtcNow;

    public Request(string userId, string contactPhoneNumber, string contactName,
        int departurePlaceId, int destinationPlaceId, decimal? price, string? details)
    {
        UserId = userId;
        ContactPhoneNumber = contactPhoneNumber;
        ContactName = contactName;
        DeparturePlaceId = departurePlaceId;
        DestinationPlaceId = destinationPlaceId;
        Price = price;
        Details = details;
    }
}
