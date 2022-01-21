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
                var result = db.Localities
                    .AsNoTracking()
                    .Where(l => (l.Region + " " + l.District + " " +
                    l.City + " " + l.CityRegion + " " + l.StreetName).Contains(search))
                    //.Where(l => l.City.Contains(search))
                    .Take(10)
                    .Select(l => l.GetFull());

                return result;
            }
            return null;
        }
    }
}
