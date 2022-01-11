using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

public abstract class Request : BaseEntity
{
    public string UserId { get; set; }
    public UserInfo? User { get; set; }

    [Phone] public string ContactPhoneNumber { get; set; }
    [MaxLength(64)] public string ContactName { get; set; }
    public string DeparturePlace { get; set; }
    public string DestinationPlace { get; set; }

    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(10, 2)")]
    [Range(0, 10000000)] public decimal? Price { get; set; }
    [MaxLength(512)] public string? Details { get; set; }

    public Request(string userId, string contactPhoneNumber, string contactName,
        string departurePlace, string destinationPlace)
    {
        UserId = userId;
        ContactPhoneNumber = contactPhoneNumber;
        ContactName = contactName;
        DeparturePlace = departurePlace;
        DestinationPlace = destinationPlace;
    }
}
