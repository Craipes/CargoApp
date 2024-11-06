using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CargoApp.ViewModels;
public class UserProfileViewModel
{
    public required string Id { get; set; }
    [EmailAddress] public required string Email { get; set; }
    [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")][Phone] public string? Phone { get; set; }

    [MaxLength(64)] public required string Name { get; set; }

    [Column(TypeName = "decimal(3, 2)")]
    [ValidateNever] [Range(1.00f, 5.00f)] public double Rating { get; set; }
    public int ReviewsReceivedCount { get; set; }
    public int ReviewsSentCount { get; set; }
    public int CarRequestsCount { get; set; }
    public int CargoRequestsCount { get; set; }
    public int CarResponsesCount { get; set; }
    public int CargoResponsesCount { get; set; }

    public bool AllowEditing { get; set; }
    public IEnumerable<ReviewViewModel> ReviewsReceived { get; set; } = [];

    public List<CarRequest> CarRequests { get; set; } = [];
    public List<CargoRequest> CargoRequests { get; set; } = [];
}