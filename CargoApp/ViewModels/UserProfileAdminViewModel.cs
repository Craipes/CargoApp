using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CargoApp.ViewModels;

public class UserProfileAdminViewModel
{
    public required string Id { get; set; }
    [EmailAddress] public required string Email { get; set; }
    [MaxLength(64)] public required string Name { get; set; }

    [ValidateNever][Range(1.00f, 5.00f)] public double Rating { get; set; }
    public int ReviewsReceivedCount { get; set; }
    public int ReviewsSentCount { get; set; }
    public int CarRequestsCount { get; set; }
    public int CargoRequestsCount { get; set; }
    public int CarResponsesCount { get; set; }
    public int CargoResponsesCount { get; set; }
}
