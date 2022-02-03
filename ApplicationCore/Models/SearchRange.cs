using System.ComponentModel.DataAnnotations;

namespace ApplicationCore.Models;

public enum SearchRange
{
    [Display(Name = "Точний")] Exact,
    [Display(Name = "Місто")] City,
    [Display(Name = "Район")] District,
    [Display(Name = "Область")] Region
}
