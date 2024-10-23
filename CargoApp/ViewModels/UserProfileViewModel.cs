using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CargoApp.ViewModels;
public class UserProfileViewModel
{
    public required string Id { get; set; }
    [EmailAddress] public required string Email { get; set; }
    [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")][Phone] public string? Phone { get; set; }

    [MaxLength(64)] public required string Name { get; set; }
    //[CorrectPhone] public string? DefaultPhoneNumber { get; set; }

    [Column(TypeName = "decimal(3, 2)")]
    [ValidateNever] [Range(1.00f, 5.00f)] public double Rating { get; set; }
    public int ReviewsReceivedCount { get; set; }
    public int ReviewsSentCount { get; set; }

    [NotMapped] public bool AllowEditing { get; set; }
    [NotMapped] public IEnumerable<ReviewViewModel> ReviewsReceived { get; set; } = [];
}