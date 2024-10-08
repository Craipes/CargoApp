﻿namespace CargoApp.Models;

public class Settlement : BaseEntity
{
    public string Region { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string CityRegion { get; set; } = string.Empty;
    public string NormalizedSettlement { get; set; } = string.Empty;
    public bool IsVisible { get; set; } = true;

    public Settlement() { }

    public Settlement(string region, string district, string city, string cityRegion, bool isVisible = true)
    {
        Region = region;
        District = district;
        City = city;
        CityRegion = cityRegion;
        IsVisible = isVisible;
        NormalizedSettlement = GetFullName().ToUpperInvariant();
    }

    public string GetFullName()
    {
        string result = Region;
        if (District != string.Empty) result += " " + District;
        if (City != string.Empty) result += " " + City;
        if (CityRegion != string.Empty) result += " " + CityRegion;
        return result.TrimStart();
    }
}
