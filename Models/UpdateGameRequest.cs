using System.Text.Json;

namespace gamesApi.Models;

public class UpdateGameRequest
{
    public string? Slug { get; set; }
    public string? Genre { get; set; }
    public string? Description { get; set; }
    public string? ImageUrl { get; set; }
    public JsonElement? Config { get; set; }
}
