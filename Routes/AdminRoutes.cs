using gamesApi.Data;
using gamesApi.Models;

namespace gamesApi.Routes;

public static class AdminRoutes
{
    public static void MapAdminRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/api/admin/games");

        // Create game
        group.MapPost("/", async (CreateGameRequest request, GamesContext db) =>
        {
            var game = new Game
            {
                Name = request.Name,
                Genre = request.Genre,
                Description = request.Description,
                Price = request.Price,
                ImageUrl = request.ImageUrl
            };

            db.Games.Add(game);
            await db.SaveChangesAsync();
            return Results.Created($"/api/games/{game.Id}", game);
        });

        // Update game (partial update)
        group.MapPut("/{id}", async (int id, UpdateGameRequest request, GamesContext db) =>
        {
            var game = await db.Games.FindAsync(id);
            if (game is null)
                return Results.NotFound(new { Message = $"Game with id {id} not found" });

            if (request.Name is not null) game.Name = request.Name;
            if (request.Genre is not null) game.Genre = request.Genre;
            if (request.Description is not null) game.Description = request.Description;
            if (request.Price.HasValue) game.Price = request.Price.Value;
            if (request.ImageUrl is not null) game.ImageUrl = request.ImageUrl;

            await db.SaveChangesAsync();
            return Results.Ok(game);
        });

        // Delete game
        group.MapDelete("/{id}", async (int id, GamesContext db) =>
        {
            var game = await db.Games.FindAsync(id);
            if (game is null)
                return Results.NotFound(new { Message = $"Game with id {id} not found" });

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
