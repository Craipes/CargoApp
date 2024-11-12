namespace CargoApp.Models;

public enum CarType
{
    [Display(Name = "Car Any")]
    Any,
    [Display(Name = "Car Truck")]
    Truck,
    [Display(Name = "Car Semitrailer")]
    SemiTrailer,
    [Display(Name = "Car Trailer")]
    Trailer
}