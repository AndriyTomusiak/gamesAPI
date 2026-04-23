namespace gamesApi.Routes;

public static class MapRoutes
{
    private const int PinCount = 6000;
    private const int ExtraFieldCount = 96; // id, lat, lng, title + 96 = 100 полів

    /// <summary>Демо-піни по території України (фіксований seed для стабільних координат).</summary>
    private static readonly Lazy<IReadOnlyList<Dictionary<string, object?>>> Pins = new(BuildPins);

    private static List<Dictionary<string, object?>> BuildPins()
    {
        const double latMin = 44.4;
        const double latMax = 52.4;
        const double lngMin = 22.1;
        const double lngMax = 40.2;

        var rng = new Random(42);
        var pins = new List<Dictionary<string, object?>>(PinCount);

        for (var i = 0; i < PinCount; i++)
        {
            var lat = latMin + rng.NextDouble() * (latMax - latMin);
            var lng = lngMin + rng.NextDouble() * (lngMax - lngMin);
            var pin = new Dictionary<string, object?>(100)
            {
                ["id"] = i + 1,
                ["lat"] = lat,
                ["lng"] = lng,
                ["title"] = $"Точка {i + 1}"
            };

            for (var e = 1; e <= ExtraFieldCount; e++)
            {
                var tag = i * 1000 + e;
                pin[$"extra{e:D2}"] = tag % 7 == 0
                    ? Math.Round(rng.NextDouble() * 1000, 4)
                    : tag % 5 == 0
                        ? rng.Next()
                        : $"v{i + 1}-{e}";
            }

            pins.Add(pin);
        }

        return pins;
    }

    public static void MapMapRoutes(this WebApplication app)
    {
        app.MapGet("/api/map/pins", () => Results.Ok(Pins.Value));
    }
}
