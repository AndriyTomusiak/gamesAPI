using gamesApi.Models;

namespace gamesApi.Data;

public class GamesStore
{
    private readonly List<Game> _games = new()
    {
        new Game { Id = 1, Name = "The Witcher 3", Genre = "RPG", Description = "Open world RPG", Price = 29.99m, ImageUrl = "" },
        new Game { Id = 2, Name = "CS2", Genre = "Shooter", Description = "Competitive shooter", Price = 0m, ImageUrl = "" },
        new Game { Id = 3, Name = "Minecraft", Genre = "Sandbox", Description = "Block building game", Price = 19.99m, ImageUrl = "" }
    };

    private int _nextId = 4;

    public List<Game> GetAll() => _games;

    public Game? GetById(int id) => _games.FirstOrDefault(g => g.Id == id);

    public Game Add(CreateGameRequest request)
    {
        var game = new Game
        {
            Id = _nextId++,
            Name = request.Name,
            Genre = request.Genre,
            Description = request.Description,
            Price = request.Price,
            ImageUrl = request.ImageUrl
        };

        _games.Add(game);
        return game;
    }

    public Game? Update(int id, UpdateGameRequest request)
    {
        var game = GetById(id);
        if (game is null) return null;

        if (request.Name is not null) game.Name = request.Name;
        if (request.Genre is not null) game.Genre = request.Genre;
        if (request.Description is not null) game.Description = request.Description;
        if (request.Price.HasValue) game.Price = request.Price.Value;
        if (request.ImageUrl is not null) game.ImageUrl = request.ImageUrl;

        return game;
    }

    public bool Delete(int id)
    {
        var game = GetById(id);
        if (game is null) return false;

        _games.Remove(game);
        return true;
    }
}
