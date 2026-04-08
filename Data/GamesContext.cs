using gamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace gamesApi.Data;

public class GamesContext : DbContext
{
    public GamesContext(DbContextOptions<GamesContext> options) : base(options) { }

    public DbSet<Car> Cars => Set<Car>();
    public DbSet<Dealer> Dealers => Set<Dealer>();

}
