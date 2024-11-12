namespace CargoApp.Models;

public enum TrailerType
{
    [Display(Name = "Trailer Any")]
    Any,
    [Display(Name = "Trailer Closed")]
    Closed,
    [Display(Name = "Trailer Open")]
    Open,
    [Display(Name = "Trailer Container")]
    Container,
    [Display(Name = "Trailer Tilted")]
    Tilted,
    [Display(Name = "Trailer Refrigerator")]
    Refrigerator,
    [Display(Name = "Trailer Isothermal Van")]
    IsothermalVan,
    [Display(Name = "Trailer Minibus")]
    Minibus,
    [Display(Name = "Trailer Flatbed")]
    Flatbed,
    [Display(Name = "Trailer Dump Truck")]
    DumpTruck,
    [Display(Name = "Trailer Crane")]
    Crane,
    [Display(Name = "Trailer Car Carrier")]
    CarCarrier,
    [Display(Name = "Trailer Tank")]
    Tank,
    [Display(Name = "Trailer Timber Truck")]
    TimberTruck,
    [Display(Name = "Trailer Truck-Tractor")]
    TruckTractor
}
