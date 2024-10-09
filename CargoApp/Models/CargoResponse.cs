namespace CargoApp.Models;

public class CargoResponse
{
    public int CargoRequestId { get; set; }
    public CargoRequest? CargoRequest { get; set; }

    //Check
    public string SenderId { get; set; }
    public User? Sender { get; set; }

    [Column(TypeName = "decimal(6, 3)")]
    [Range(0.050f, 100f)] public float? CargoMass { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.10f, 100f)] public float? CargoLength { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.10f, 100f)] public float? CargoWidth { get; set; }

    [Column(TypeName = "decimal(5, 2)")]
    [Range(0.10f, 100f)] public float? CargoHeight { get; set; }

    [Column(TypeName = "decimal(9, 4)")]
    [Range(0.0010f, 50000f)] public float? CargoVolume { get; set; }

    [MaxLength(512)] public string? Comment { get; set; }

    public CargoResponse(int cargoRequestId, string senderId)
    {
        CargoRequestId = cargoRequestId;
        SenderId = senderId;
    }
}
