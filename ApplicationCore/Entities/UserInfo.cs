using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entities;

public class UserInfo
{
    public string Id { get; set; }

    //Base user info
    [MaxLength(64)] public string Name { get; set; } = null!;
    [Phone] public string? DefaultPhoneNumber { get; set; }
    public List<CarRequest> CarRequests { get; set; } = new();

    //Driver info
    public List<CargoRequest> CargoRequests { get; set; } = new();
    public int ReviewsCount { get; set; }
    public List<Review> Reviews { get; set; } = new();

    [Column(TypeName = "decimal(3, 2)")]
    [Range(1.00f, 5.00f)] public float Rating { get; set; }

    public List<Car> DefaultCars { get; set; } = new();

    public UserInfo(string id)
    {
        Id = id;
    }
}
