using gamesApi.Data;
using gamesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gamesApi.Routes;

public static class AdminCarRoutes
{
    public static RouteGroupBuilder MapAdminCarRoutes(this RouteGroupBuilder admin)
    {
        var group = admin.MapGroup("/cars");

        group.MapGet("/", async (
            GamesContext db,
            string? brand,
            int? yearFrom,
            int? yearTo,
            decimal? priceFrom,
            decimal? priceTo,
            string? sortBy,
            string? sortDir) =>
        {
            var query = db.Cars
                .Include(c => c.Dealers)
                .AsQueryable()
                .WhereAdminFilters(brand, yearFrom, yearTo, priceFrom, priceTo)
                .OrderAdminList(sortBy, sortDir);

            var cars = await query.ToListAsync();
            return Results.Ok(cars.Select(c => new
            {
                c.Id, c.Brand, c.Model, c.Year, c.Price,
                DealerIds = c.Dealers.Select(d => d.Id).ToList(),
                DealerCount = c.Dealers.Count
            }));
        });

        group.MapPost("/", async ([FromBody] CarRequest req, GamesContext db) =>
        {
            var car = new Car
            {
                Brand = req.Brand,
                Model = req.Model,
                Year = req.Year,
                Price = req.Price
            };

            if (req.DealerIds is { Count: > 0 })
                car.Dealers = await db.Dealers.Where(d => req.DealerIds.Contains(d.Id)).ToListAsync();

            db.Cars.Add(car);
            await db.SaveChangesAsync();
            return Results.Created($"/api/admin/cars/{car.Id}", car);
        });

        group.MapPut("/{id}", async ([FromRoute] int id, [FromBody] CarRequest req, GamesContext db) =>
        {
            var car = await db.Cars.Include(c => c.Dealers).FirstOrDefaultAsync(c => c.Id == id);
            if (car is null)
                return Results.NotFound();

            car.Brand = req.Brand;
            car.Model = req.Model;
            car.Year = req.Year;
            car.Price = req.Price;

            car.Dealers.Clear();
            if (req.DealerIds is { Count: > 0 })
                car.Dealers = await db.Dealers.Where(d => req.DealerIds.Contains(d.Id)).ToListAsync();

            await db.SaveChangesAsync();
            return Results.Ok(car);
        });

        group.MapDelete("/{id}", async (int id, GamesContext db) =>
        {
            var car = await db.Cars.FindAsync(id);
            if (car is null)
                return Results.NotFound();

            db.Cars.Remove(car);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        return group;
    }
}
