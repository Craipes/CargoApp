namespace CargoApp.ViewModels;

public record UserProfilesAdminViewModel(List<UserProfileAdminViewModel> Profiles, string? Search, int Page, int MaxPages);
