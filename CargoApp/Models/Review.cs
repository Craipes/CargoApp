namespace CargoApp.Models;

public class Review
{
    public string ReceiverId { get; set; }
    public User Receiver { get; set; } = null!;

    public string SenderId { get; set; }
    public User Sender { get; set; } = null!;

    [Range(1, 5)] public int Points { get; set; }
    [MaxLength(1024)] public string? Content { get; set; }

    public Review(string receiverId, string senderId)
    {
        ReceiverId = receiverId;
        SenderId = senderId;
    }
}
