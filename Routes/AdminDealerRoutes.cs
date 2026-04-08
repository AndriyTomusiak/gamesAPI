using gamesApi.Data;
using gamesApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace gamesApi.Routes;

public static class AdminDealerRoutes
{
    public static RouteGroupBuilder MapAdminDealerRoutes(this RouteGroupBuilder admin)
    {
        var group = admin.MapGroup("/dealers");

        group.MapGet("/", async (
            GamesContext db,
            string? brand,
            string? city) =>
        {
            var query = db.Dealers.Include(d => d.Cars).AsQueryable();

            if (!string.IsNullOrEmpty(brand))
                query = query.Where(d => d.Brand == brand);

            if (!string.IsNullOrEmpty(city))
                query = query.Where(d => d.City == city);

            var dealers = await query.ToListAsync();
            return Results.Ok(dealers.Select(d => new
            {
                d.Id, d.Name, d.Brand, d.City, d.Phone,
                CarIds = d.Cars.Select(c => c.Id).ToList(),
                CarCount = d.Cars.Count
            }));
        });

        group.MapPut("/{id}", async ([FromRoute] int id, [FromBody] DealerRequest req, GamesContext db) =>
        {
            var dealer = await db.Dealers.Include(d => d.Cars).FirstOrDefaultAsync(d => d.Id == id);
            if (dealer is null)
                return Results.NotFound();

            dealer.Name = req.Name;
            dealer.Brand = req.Brand;
            dealer.City = req.City;
            dealer.Phone = req.Phone;

            dealer.Cars.Clear();
            if (req.CarIds is { Count: > 0 })
                dealer.Cars = await db.Cars.Where(c => req.CarIds.Contains(c.Id)).ToListAsync();

            await db.SaveChangesAsync();
            return Results.Ok(dealer);
        });

        group.MapDelete("/{id}", async (int id, GamesContext db) =>
        {
            var dealer = await db.Dealers.FindAsync(id);
            if (dealer is null)
                return Results.NotFound();

            db.Dealers.Remove(dealer);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        return group;
    }
}

public class DealerRequest
{
    public string Name { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public List<int>? CarIds { get; set; }
}
