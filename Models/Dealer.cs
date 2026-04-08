using System.Text.Json.Serialization;

namespace gamesApi.Models;

public class Dealer
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    [JsonIgnore]
    public List<Car> Cars { get; set; } = [];
}
