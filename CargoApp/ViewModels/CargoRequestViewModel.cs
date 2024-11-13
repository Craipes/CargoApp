using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace CargoApp.ViewModels;

public class CargoRequestViewModel
{
    [BindNever][ValidateNever] public required CargoRequest CargoRequest { get; set; }
    [BindNever][ValidateNever][Display(Name = "Name")] public string UserName { get; set; } = string.Empty;
    [BindNever][ValidateNever] public List<CargoResponse> Responses { get; set; } = [];
    [BindNever][ValidateNever] public bool AllowEditing { get; set; }
    [BindNever][ValidateNever] public bool AllowResponding { get; set; }
    [BindNever][ValidateNever] public string? UserId { get; set; }
    public CargoResponse Response { get; set; } = null!;
}