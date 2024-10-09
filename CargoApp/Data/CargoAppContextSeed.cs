using CargoApp.Models;

namespace CargoApp.Data;

public static class CargoAppContextSeed
{
    public static async Task SeedUsersAsync(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
        var user = await userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == "+380989973045");
        if (user == null)
        {
            var defaultUser = new User() { PhoneNumber = "+380989973045", UserName = "Default user" };
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

    public static async Task RecreateSettlements(CargoAppContext context, string localitiesPath)
    {
        XmlDataExtractor extractor = new(localitiesPath);
        context.Database.ExecuteSqlRaw("TRUNCATE TABLE SETTLEMENTS");
        context.Settlements.RemoveRange(context.Settlements);
        context.Settlements.AddRange(extractor.RunAndGet());
        //extractor.RunAndSave("D://UA_DB_SETTLEMENTS.xml");
        await context.SaveChangesAsync();
        Console.WriteLine($"Settlements count: {context.Settlements.Count()}");
    }
}
