namespace CargoApp.Models;

public class BaseResponse
{
    public string UserId { get; set; } = null!;
    public User User { get; set; } = null!;

    [MaxLength(512)] public string? Comment { get; set; }
}
