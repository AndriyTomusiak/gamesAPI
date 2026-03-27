using gamesApi.Data;
using gamesApi.Models;

namespace gamesApi.Routes;

public static class AdminRoutes
{
    public static void MapAdminRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/api/admin/games");

        // Створити гру
        group.MapPost("/", (CreateGameRequest request, GamesStore store) =>
        {
            var created = store.Add(request);
            return Results.Created($"/api/games/{created.Id}", created);
        });

        // Оновити гру (часткове оновлення)
        group.MapPut("/{id}", (int id, UpdateGameRequest request, GamesStore store) =>
        {
            var game = store.Update(id, request);
            return game is not null
                ? Results.Ok(game)
                : Results.NotFound(new { Message = $"Game with id {id} not found" });
        });

        // Видалити гру
        group.MapDelete("/{id}", (int id, GamesStore store) =>
        {
            return store.Delete(id)
                ? Results.NoContent()
                : Results.NotFound(new { Message = $"Game with id {id} not found" });
        });
    }
}
