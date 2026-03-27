namespace gamesApi.Models;

public class CreateGameRequest
{
    public required string Name { get; set; }
    public required string Genre { get; set; }
    public string Description { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public string ImageUrl { get; set; } = string.Empty;
}
