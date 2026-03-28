using System.Text.Json;

namespace gamesApi.Models;

public class CreateGameRequest
{
    public required string Slug { get; set; }
    public required string Genre { get; set; }
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public JsonElement? Config { get; set; }
}
