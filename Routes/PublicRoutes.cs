using gamesApi.Data;

namespace gamesApi.Routes;

public static class PublicRoutes
{
    public static void MapPublicRoutes(this WebApplication app)
    {
        var group = app.MapGroup("/api/games");

        group.MapGet("/", (GamesStore store) =>
        {
            return Results.Ok(store.GetAll());
        });

        group.MapGet("/{id}", (int id, GamesStore store) =>
        {
            var game = store.GetById(id);
            return game is not null
                ? Results.Ok(game)
                : Results.NotFound(new { Message = $"Game with id {id} not found" });
        });
    }
}
