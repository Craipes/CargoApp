namespace StreetRegister.Models;

public class Locality
{
    public int Id { get; set; }
    public string Region { get; set; } = string.Empty;
    public string District { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string CityRegion { get; set; } = string.Empty;
    public string StreetName { get; set; } = string.Empty;
    public bool IsVisible { get; set; } = true;

    public Locality() { }

    public Locality(string region, string district, string city, string cityRegion, string streetName)
    {
        Region = region;
        District = district;
        City = city;
        CityRegion = cityRegion;
        StreetName = streetName;
    }
}
