using System.Text.Json;

namespace gamesApi.Models;

public class Game
{
    public int Id { get; set; }
    public string Slug { get; set; } = string.Empty;
    public string Genre { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public bool IsSystem { get; set; }
    public JsonDocument? Config { get; set; }
}
