namespace CargoApp.Models;

public class User : IdentityUser
{
    [Display(Name = "Name")][MaxLength(64)] public required string Name { get; set; }
    [Display(Name = "Default phone number")][CorrectPhone] public string? DefaultPhoneNumber { get; set; }

    // Obsolete
    [Column(TypeName = "decimal(3, 2)")][Range(1.00f, 5.00f)] public float Rating { get; set; }
    // Obsolete
    public int ReviewsCount { get; set; }
    public List<Review> ReviewsReceived { get; set; } = [];
    public List<Review> ReviewsSent { get; set; } = [];

    public List<CarRequest> CarRequests { get; set; } = [];
    public List<CargoRequest> CargoRequests { get; set; } = [];
    public List<CarResponse> CarResponses { get; set; } = [];
    public List<CargoResponse> CargoResponses { get; set; } = [];
}
