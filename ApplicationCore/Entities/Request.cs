using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public abstract class Request : BaseEntity
{
    public string UserId { get; set; }
    public UserInfo? User { get; set; }

    [Phone] public string ContactPhoneNumber { get; set; }
    [MaxLength(64)] public string ContactName { get; set; }
    public string DeparturePlace { get; set; }
    public string DestinationPlace { get; set; }
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
