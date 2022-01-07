using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Entities;

public class Review : BaseEntity
{
    public string ReceiverId { get; set; }
    public UserInfo? Receiver { get; set; }

    //Check
    public string SenderId { get; set; }
    public UserInfo? Sender { get; set; }

    [Range(1, 5)] public int Points { get; set; }
    [MaxLength(1024)] public string? Content { get; set; }

    public Review(string receiverId, string senderId)
    {
        ReceiverId = receiverId;
        SenderId = senderId;
    }
}
