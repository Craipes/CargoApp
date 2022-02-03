namespace ApplicationCore.Models;

public class SearchModel
{
    public string DeparturePlace { get; set; } = null!;
    public string DestinationPlace { get; set; } = null!;
    public SearchRange SearchRange { get; set; }
}
