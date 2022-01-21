using CargoApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalitiesController : ControllerBase
    {
        private readonly CargoAppContext db;

        public LocalitiesController(CargoAppContext cargoAppContext)
        {
            db = cargoAppContext;
        }

        [HttpGet]
        public IEnumerable<string>? Get([FromQuery] string? search)
        {
            if (search != null)
            {
                var result = db.Settlements
                    .AsNoTracking()
                    .Where(l => (l.Region + " " + l.District + " " +
                    l.City + " " + l.CityRegion).Contains(search))
                    //.Where(l => (l.Region.StartsWith(search)) || l.District.StartsWith(search)
                    //    || l.City.StartsWith(search) || l.CityRegion.StartsWith(search))
                    .Take(10)
                    .Select(l => l.GetFull());

                return result;
            }
            return null;
        }
    }
}
