﻿namespace CargoApp.ViewModels;

public abstract class RequestViewModel
{
    [Phone] public string ContactPhoneNumber { get; set; } = null!;
    [Required][MaxLength(64)] public string ContactName { get; set; } = null!;
    [Required] public string DeparturePlace { get; set; } = null!;
    [Required] public string DestinationPlace { get; set; } = null!;

    [DataType(DataType.Currency)]
    [Range(0, 10000000)] public decimal? Price { get; set; }
    [MaxLength(512)] public string? Details { get; set; }

    public DateTime? AddTime { get; set; } = null;
    public bool IsExpired { get; set; }
}
