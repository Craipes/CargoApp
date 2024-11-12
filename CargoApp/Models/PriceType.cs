namespace CargoApp.Models;

public enum PriceType
{
    [Display(Name = "Price Negotiable")]
    Negotiable,
    [Display(Name = "Price Fixed")]
    Fixed,
    [Display(Name = "Price Per Kilometer")]
    PerKilometer,
    [Display(Name = "Price Per Ton")]
    PerTon
}
