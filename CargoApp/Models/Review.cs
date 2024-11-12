namespace CargoApp.Models;

public class Review
{
    public string ReceiverId { get; set; }
    public User Receiver { get; set; } = null!;

    public string SenderId { get; set; }
    public User Sender { get; set; } = null!;

    [Display(Name = "Points")][Range(1, 5, ErrorMessage = "Range Error")][Required(ErrorMessage = "Required Error")] public int Points { get; set; }
    [Display(Name = "Review")][MaxLength(1024, ErrorMessage = "Max Length Error")] public string? Content { get; set; }

    public Review(string receiverId, string senderId)
    {
        ReceiverId = receiverId;
        SenderId = senderId;
    }
}
