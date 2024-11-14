using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CargoApp.Models;

public class Review
{
    [ValidateNever] public required string ReceiverId { get; set; }
    [ValidateNever][Display(Name = "Receiver")] public User Receiver { get; set; } = null!;

    [ValidateNever] public required string SenderId { get; set; }
    [ValidateNever][Display(Name = "Sender")] public User Sender { get; set; } = null!;

    [ValidateNever][BindNever][Display(Name = "Adding time")] public DateTime AddTime { get; set; }

    [Display(Name = "Points")][Range(1, 5, ErrorMessage = "Range Error")][Required(ErrorMessage = "Required Error")] public int Points { get; set; }
    [Display(Name = "Review")][MaxLength(1024, ErrorMessage = "Max Length Error")] public string? Content { get; set; }
}
