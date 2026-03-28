using System.Text.Json;
using gamesApi.Data;
using gamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace gamesApi.Routes;

public static class AdminRoutes
{
    public static void MapAdminRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/api/admin/games");

        // List all games (for admin panel)
        group.MapGet("/", async (GamesContext db) =>
            Results.Ok(await db.Games.ToListAsync()));

        // Create game
        group.MapPost("/", async (CreateGameRequest request, GamesContext db) =>
        {
            var game = new Game
            {
                Slug = request.Slug,
                Genre = request.Genre,
                Description = request.Description,
                ImageUrl = request.ImageUrl,
                Config = request.Config.HasValue
                    ? JsonDocument.Parse(request.Config.Value.GetRawText())
                    : null
            };

            db.Games.Add(game);
            await db.SaveChangesAsync();
            return Results.Created($"/api/games/{game.Slug}", game);
        });

        // Update game
        group.MapPut("/{id}", async (int id, UpdateGameRequest request, GamesContext db) =>
        {
            var game = await db.Games.FindAsync(id);
            if (game is null)
                return Results.NotFound(new { Message = $"Game with id {id} not found" });

            if (request.Slug is not null) game.Slug = request.Slug;
            if (request.Genre is not null) game.Genre = request.Genre;
            if (request.Description is not null) game.Description = request.Description;
            if (request.ImageUrl is not null) game.ImageUrl = request.ImageUrl;
            if (request.Config.HasValue)
                game.Config = JsonDocument.Parse(request.Config.Value.GetRawText());

            await db.SaveChangesAsync();
            return Results.Ok(game);
        });

        // Delete game (only non-system)
        group.MapDelete("/{id}", async (int id, GamesContext db) =>
        {
            var game = await db.Games.FindAsync(id);
            if (game is null)
                return Results.NotFound(new { Message = $"Game with id {id} not found" });

            if (game.IsSystem)
                return Results.BadRequest(new { Message = "System games cannot be deleted" });

            db.Games.Remove(game);
            await db.SaveChangesAsync();
            return Results.NoContent();
        });

        // Upload image
        app.MapPost("/api/admin/upload", async (IFormFile file, IWebHostEnvironment env) =>
        {
            var allowed = new[] { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowed.Contains(ext))
                return Results.BadRequest(new { Message = "Allowed formats: jpg, jpeg, png, gif, webp" });

            if (file.Length > 5 * 1024 * 1024)
                return Results.BadRequest(new { Message = "Maximum file size: 5 MB" });

            var fileName = $"{Guid.NewGuid()}{ext}";
            var imagesPath = Path.Combine(env.WebRootPath, "images");
            Directory.CreateDirectory(imagesPath);

            var filePath = Path.Combine(imagesPath, fileName);
            await using var stream = File.Create(filePath);
            await file.CopyToAsync(stream);

            return Results.Ok(new { Url = $"/images/{fileName}" });
        }).DisableAntiforgery();
    }
}
