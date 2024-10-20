using CargoApp.Models;

namespace CargoApp;

public static class SearchQueryHelper
{
    //public static async Task<IQueryable<T>?> Search<T>(this IQueryable<T> query, CargoAppContext db,
    //    SearchRequestViewModel model, int count = 10) where T : BaseRequest
    //{
    //    return await query.Search(db, model.DeparturePlace.ToUpper(),
    //        model.DestinationPlace.ToUpper(), model.SearchRange, count);
    //}

    //public static async Task<IQueryable<T>?> Search<T>(this IQueryable<T> query, CargoAppContext db,
    //    string departurePlaceName, string destinationPlaceName, SearchRange range, int count = 10) where T : BaseRequest
    //{
    //    var result = await db.SearchPlaces(departurePlaceName, destinationPlaceName);
    //    if (result != null)
    //    {
    //        (var departurePlace, var destinationPlace) = result.Value;
    //        return query.Search(departurePlace, destinationPlace, range, count);
    //    }
    //    return null;
    //}

    //public static IQueryable<T> Search<T>(this IQueryable<T> query, Settlement departurePlace,
    //    Settlement destinationPlace, SearchRange range, int count = 10) where T : BaseRequest
    //{
    //    query = query
    //        .AsNoTracking()
    //        .Where(r => r.AddTime.AddHours(BaseRequest.DefaultExpirationTimeInHours) > DateTime.UtcNow)
    //        .Where(r => r.DestinationPlaceId == destinationPlace.Id);
    //    query = range switch
    //    {
    //        SearchRange.Exact => query.Where(r => r.DeparturePlaceId == departurePlace.Id),
    //        SearchRange.City => query.Where(r => r.DeparturePlace!.City == departurePlace.City),
    //        SearchRange.District => query.Where(r => r.DeparturePlace!.District == departurePlace.District),
    //        SearchRange.Region => query.Where(r => r.DeparturePlace!.Region == departurePlace.Region),
    //        _ => query
    //    };
    //    return query
    //        .Include(r => r.DeparturePlace)
    //        .Include(r => r.DestinationPlace)
    //        .Take(count);
    //}

    //public static async Task<(Settlement departurePlace, Settlement destinationPlace)?> SearchPlaces(this CargoAppContext db,
    //    string departurePlaceName, string destinationPlaceName)
    //{
    //    var departurePlace = await db.Settlements
    //        .AsNoTracking()
    //        .FirstOrDefaultAsync(s => s.NormalizedSettlement == departurePlaceName.ToUpper());
    //    if (departurePlace != null)
    //    {
    //        var destinationPlace = await db.Settlements
    //            .AsNoTracking()
    //            .FirstOrDefaultAsync(s => s.NormalizedSettlement == destinationPlaceName.ToUpper());
    //        if (destinationPlace != null)
    //        {
    //            return (departurePlace, destinationPlace);
    //        }
    //    }
    //    return null;
    //}
}
