using CargoApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettlementsController : ControllerBase
    {
        //private readonly Func<CargoAppContext, string, IEnumerable<Settlement>> compiledSearch =
        //    EF.CompileQuery((CargoAppContext db, string search) => db.Settlements
        //    .AsNoTracking()
        //    .OrderBy(l => l.City)
        //    .Where(l => l.IsVisible && l.NormalizedSettlement.Contains(search))
        //    .Take(10));

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
                    //.Where(l => l.IsVisible && l.City.StartsWith(search))
                    .Take(10)
                    .Select(l => l.GetFull())
                    .ToArrayAsync();

                //var result = await db.Settlements
                //    .FromSqlInterpolated($"SELECT TOP 10 * FROM[Settlements] AS[s] WHERE([s].[IsVisible] = CAST(1 AS bit)) AND(({search} LIKE N'') OR(CHARINDEX({search}, [s].[NormalizedSettlement]) > 0)) ORDER BY[s].[City]")                
                //    .Select(l => l.GetFull())
                //    .ToArrayAsync();

                return result;

                //var result = compiledSearch.Invoke(db, search);

                //return result
                //    .Select(l => l.GetFull())
                //    .ToArray();
            }
            return null;
        }
    }
}
