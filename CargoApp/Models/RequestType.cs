namespace CargoApp.Models;

public enum RequestType
{
    [Display(Name = "Request Visible")]
    Visible = 0,

    [Display(Name = "Request Closed")]
    Closed = 1000,

    [Display(Name = "Request Hidden")]
    Hidden = 2000,
    [Display(Name = "Request Hidden By Admin")]
    HiddenByAdmin = 2001
}
