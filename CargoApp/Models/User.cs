﻿namespace CargoApp.Models;

public class User : IdentityUser
{
    [MaxLength(64)] public string Name { get; set; } = null!;
    [CorrectPhone] public string? DefaultPhoneNumber { get; set; }

    [Column(TypeName = "decimal(3, 2)")]
    [Range(1.00f, 5.00f)] public float Rating { get; set; }
    public int ReviewsCount { get; set; }
    public List<Review> ReviewsReceived { get; set; } = [];
    public List<Review> ReviewsSent { get; set; } = [];
    public List<Car> DefaultCars { get; set; } = [];

    public List<CarRequest> CarRequests { get; set; } = [];
    public List<CargoRequest> CargoRequests { get; set; } = [];
    public List<CarResponse> CarResponses { get; set; } = [];
    public List<CargoResponse> CargoResponses { get; set; } = [];
}
