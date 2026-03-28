using System.Text.Json;
using gamesApi.Data;
using Microsoft.EntityFrameworkCore;

namespace gamesApi.Routes;

public static class PublicRoutes
{
    public static void MapPublicRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/api/games");

        group.MapGet("/", async (GamesContext db) =>
        {
            var games = await db.Games.ToListAsync();
            var result = new Dictionary<string, object>();
            foreach (var g in games)
            {
                result[g.Slug] = new
                {
                    g.Id,
                    g.Slug,
                    g.Genre,
                    g.Description,
                    g.ImageUrl,
                    Config = g.Config?.RootElement
                };
            }
            return Results.Ok(result);
        });

        group.MapGet("/{slug}", async (string slug, GamesContext db) =>
        {
            var game = await db.Games.FirstOrDefaultAsync(g => g.Slug == slug);
            if (game is null)
                return Results.NotFound(new { Message = $"Game '{slug}' not found" });

            return Results.Ok(new
            {
                game.Id,
                game.Slug,
                game.Genre,
                game.Description,
                game.ImageUrl,
                Config = game.Config?.RootElement
            });
        });
    }
}
