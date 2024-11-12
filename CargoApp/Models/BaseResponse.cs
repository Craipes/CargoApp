namespace CargoApp.Models;

public class BaseResponse
{
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    [Display(Name = "Comment")][MaxLength(512, ErrorMessage = "Max Length Error")] public string? Comment { get; set; }
}
