using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CargoApp.ViewModels;
public class UserProfileViewModel
{
    public required string Id { get; set; }
    [Display(Name = "Email")][EmailAddress] public required string Email { get; set; }
    [Display(Name = "Default phone number")][RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")][Phone] public string? Phone { get; set; }

    [Display(Name = "Name")][MaxLength(64)] public required string Name { get; set; }

    [Display(Name = "Rating")][ValidateNever][Range(1.00f, 5.00f)] public double Rating { get; set; }
    [Display(Name = "Reviews received count:")] public int ReviewsReceivedCount { get; set; }
    [Display(Name = "Reviews sent count:")] public int ReviewsSentCount { get; set; }
    [Display(Name = "Car requests count:")] public int CarRequestsCount { get; set; }
    [Display(Name = "Cargo requests count:")] public int CargoRequestsCount { get; set; }
    [Display(Name = "Car responses count:")] public int CarResponsesCount { get; set; }
    [Display(Name = "Cargo responses count:")] public int CargoResponsesCount { get; set; }

    public bool AllowEditing { get; set; }
    public bool CanCreateReview { get; set; }
    public bool WasReviewCreated { get; set; }

    public List<CarRequest> CarRequests { get; set; } = [];
    public List<CargoRequest> CargoRequests { get; set; } = [];
}