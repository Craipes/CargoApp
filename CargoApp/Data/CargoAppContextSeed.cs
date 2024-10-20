namespace CargoApp.Data;

public static class CargoAppContextSeed
{
    public static async Task SeedAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        // Seeding User
        const string userEmail = "user@gmail.com";
        var user = await userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            var defaultUser = new User() { Email = userEmail, UserName = userEmail, Name = "User" };
            await userManager.CreateAsync(defaultUser, "password");
        }

        // Seeding Admin
        const string adminEmail = "admin@gmail.com";
        user = await userManager.FindByEmailAsync(adminEmail);
        if (user == null)
        {
            var defaultUser = new User() { Email = adminEmail, UserName = adminEmail, Name = "Admin" };
            await userManager.CreateAsync(defaultUser, "password");
            user = defaultUser;
        }
        if (!await roleManager.RoleExistsAsync(CargoAppConstants.AdminRole))
        {
            await roleManager.CreateAsync(new IdentityRole(CargoAppConstants.AdminRole));
        }
        if (!await userManager.IsInRoleAsync(user, CargoAppConstants.AdminRole))
        {
            await userManager.AddToRoleAsync(user, CargoAppConstants.AdminRole);
        }
    }
}
