using gamesApi.Data;
using Microsoft.EntityFrameworkCore;

namespace gamesApi.Routes;

public static class PublicRoutes
{
    public static void MapPublicRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/api/games");

        group.MapGet("/", async (GamesContext db) =>
            Results.Ok(await db.Games.ToListAsync()));

        group.MapGet("/{id}", async (int id, GamesContext db) =>
            await db.Games.FindAsync(id) is { } game
                ? Results.Ok(game)
                : Results.NotFound(new { Message = $"Game with id {id} not found" }));
    }
}
