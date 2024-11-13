using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CargoApp.Models;

public abstract class BaseRequestResponse : BaseEntity
{
    [BindNever][ValidateNever] public required string UserId { get; set; }
    [BindNever] public User? User { get; set; }

    [Display(Name = "Contact name")][MaxLength(64, ErrorMessage = "Max Length Error")][Required(ErrorMessage = "Required Error")] public required string ContactName { get; set; }
    [Display(Name = "Contact phone")][Phone(ErrorMessage = "Phone Error")][Required(ErrorMessage = "Required Error")] public required string ContactPhoneNumber { get; set; }
    [Display(Name = "Contact email")][EmailAddress(ErrorMessage = "Email Error")] public string? ContactEmail { get; set; }

    [Display(Name = "Details")][MaxLength(512, ErrorMessage = "Max Length Error")] public string? Details { get; set; }

    [Display(Name = "Adding time")][BindNever] public DateTime AddTime { get; set; } = DateTime.UtcNow;
}
