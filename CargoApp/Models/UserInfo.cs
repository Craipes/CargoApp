namespace CargoApp.Models;

public class UserInfo
{
    public string Id { get; set; } = null!;
    [MaxLength(64)] public string Name { get; set; } = null!;
    [CorrectPhone] public string? PhoneNumber { get; set; }
    [CorrectPhone] public string? DefaultPhoneNumber { get; set; }

    [Column(TypeName = "decimal(3, 2)")]
    [Range(1.00f, 5.00f)] public float Rating { get; set; }
    public int ReviewsCount { get; set; }
    public List<Review> ReviewsReceived { get; set; } = new();
    public List<Review> ReviewsSent { get; set; } = new();
    public List<Car> DefaultCars { get; set; } = new();

    public List<CarRequest> CarRequests { get; set; } = new();
    public List<CargoRequest> CargoRequests { get; set; } = new();
    public List<CarResponse> CarResponses { get; set; } = new();
    public List<CargoResponse> CargoResponses { get; set; } = new();


    public UserInfo(string id)
    {
        Id = id;
    }
}
