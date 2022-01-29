using CargoApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Controllers.Api
{
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
                //search = search.ToUpperInvariant();
                var result = await db.Settlements
                    .AsNoTracking()
                    .OrderBy(l => l.City)
                    .Where(l => l.IsVisible && l.NormalizedSettlement.Contains(search))
                    //.Where(l => l.IsVisible && l.City.StartsWith(search))
                    .Take(10)
                    .Select(l => l.GetFull())
                    .ToArrayAsync();

                return result;
            }
            return null;
        }
    }
}
