namespace gamesApi.Models;

public class CarRequest
{
    public string Brand { get; set; } = string.Empty;
    public string Model { get; set; } = string.Empty;
    public int Year { get; set; }
    public decimal Price { get; set; }
    public List<int>? DealerIds { get; set; }
}
