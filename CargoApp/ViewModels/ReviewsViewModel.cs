namespace CargoApp.ViewModels;

public class ReviewsViewModel
{
    public required string? CurrentId { get; set; }
    public required string UserId { get; set; }
    public required string UserName { get; set; }
    public bool IsAdmin { get; set; }
    public List<Review> Reviews { get; set; } = [];
}
