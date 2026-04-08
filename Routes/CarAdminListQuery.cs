using gamesApi.Models;

namespace gamesApi.Routes;

internal static class CarAdminListQuery
{
    public static IQueryable<Car> WhereAdminFilters(
        this IQueryable<Car> query,
        string? brand,
        int? yearFrom,
        int? yearTo,
        decimal? priceFrom,
        decimal? priceTo)
    {
        if (!string.IsNullOrEmpty(brand))
            query = query.Where(c => c.Brand == brand);

        if (yearFrom.HasValue)
            query = query.Where(c => c.Year >= yearFrom.Value);

        if (yearTo.HasValue)
            query = query.Where(c => c.Year <= yearTo.Value);

        if (priceFrom.HasValue)
            query = query.Where(c => c.Price >= priceFrom.Value);

        if (priceTo.HasValue)
            query = query.Where(c => c.Price <= priceTo.Value);

        return query;
    }

    public static IQueryable<Car> OrderAdminList(this IQueryable<Car> query, string? sortBy, string? sortDir)
    {
        var key = (sortBy ?? "id").ToLowerInvariant();
        var desc = string.Equals(sortDir, "desc", StringComparison.OrdinalIgnoreCase);

        return key switch
        {
            "brand" => desc
                ? query.OrderByDescending(c => c.Brand).ThenBy(c => c.Id)
                : query.OrderBy(c => c.Brand).ThenBy(c => c.Id),
            "model" => desc
                ? query.OrderByDescending(c => c.Model).ThenBy(c => c.Id)
                : query.OrderBy(c => c.Model).ThenBy(c => c.Id),
            "year" => desc
                ? query.OrderByDescending(c => c.Year).ThenBy(c => c.Id)
                : query.OrderBy(c => c.Year).ThenBy(c => c.Id),
            "price" => desc
                ? query.OrderByDescending(c => c.Price).ThenBy(c => c.Id)
                : query.OrderBy(c => c.Price).ThenBy(c => c.Id),
            "dealercount" or "dealers" => desc
                ? query.OrderByDescending(c => c.Dealers.Count).ThenBy(c => c.Id)
                : query.OrderBy(c => c.Dealers.Count).ThenBy(c => c.Id),
            _ => desc
                ? query.OrderByDescending(c => c.Id)
                : query.OrderBy(c => c.Id)
        };
    }
}
