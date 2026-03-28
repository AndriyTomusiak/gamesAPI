using gamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace gamesApi.Data;

public class GamesContext : DbContext
{
    public GamesContext(DbContextOptions<GamesContext> options) : base(options) { }

    public DbSet<Game> Games => Set<Game>();
}
