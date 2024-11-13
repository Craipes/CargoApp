using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace CargoApp.ViewModels;

public class CarRequestViewModel
{
    [BindNever][ValidateNever] public required CarRequest CarRequest { get; set; }
    [BindNever][ValidateNever][Display(Name = "Name")] public string UserName { get; set; } = string.Empty;
    [BindNever][ValidateNever] public List<CarResponse> Responses { get; set; } = [];
    [BindNever][ValidateNever] public bool AllowEditing { get; set; }
    [BindNever][ValidateNever] public bool AllowResponding { get; set; }
    [BindNever][ValidateNever] public string? UserId { get; set; }
    public CarResponse Response { get; set; } = null!;
}