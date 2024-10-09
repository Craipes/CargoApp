using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Controllers.Api;

[Route("api/[controller]")]
[ApiController]
public class SettlementsController : ControllerBase
{
    private readonly CargoAppContext db;

    public SettlementsController(CargoAppContext cargoAppContext)
    {
        db = cargoAppContext;
    }

    [HttpGet("search")]
    public async Task<IEnumerable<string>?> Get([FromQuery] string? search)
    {
        if (search != null)
        {
            search = search.ToUpperInvariant();
            var result = await db.Settlements
                .AsNoTracking()
                .OrderBy(l => l.City)
                .Where(l => l.IsVisible && l.NormalizedSettlement.Contains(search))
                //.Where(l => l.IsVisible && EF.Functions.Like(l.NormalizedSettlement, $"%{search}%"))
                //.Where(l => l.IsVisible && (l.Region.ToUpper().StartsWith(search) || l.District.ToUpper().StartsWith(search) || l.City.ToUpper().StartsWith(search) || l.CityRegion.ToUpper().StartsWith(search)))
                //.Where(l => l.IsVisible && (l.Region.StartsWith(search) || l.District.StartsWith(search) || l.City.StartsWith(search) || l.CityRegion.StartsWith(search)))
                //.Where(l => l.IsVisible && l.City.StartsWith(search))
                .Take(10)
                .Select(l => l.GetFullName())
                .ToArrayAsync();

            //var result = await db.Settlements
            //    .FromSqlInterpolated($"SELECT TOP 10 * FROM[Settlements] AS[s] WHERE([s].[IsVisible] = CAST(1 AS bit)) AND(({search} LIKE N'') OR(CHARINDEX({search}, [s].[NormalizedSettlement]) > 0)) ORDER BY[s].[City]")                
            //    .Select(l => l.GetFull())
            //    .ToArrayAsync();

            return result;
        }
        return null;
    }
}
