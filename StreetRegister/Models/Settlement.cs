namespace StreetRegister.Models;

public class Settlement
{
    public int Id { get; set; }
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
        NormalizedSettlement = GetFull().ToUpperInvariant();
    }

    public string GetFull()
    {
        string result = "";
        if (Region != string.Empty) result += Region;
        if (District != string.Empty) result += " " + District;
        if (City != string.Empty) result += " " + City;
        if (CityRegion != string.Empty) result += " " + CityRegion;
        return result.TrimStart();
    }
}
