using System.Text.Json;
using gamesApi.Models;
using Microsoft.EntityFrameworkCore;

namespace gamesApi.Data;

public class GamesContext : DbContext
{
    public GamesContext(DbContextOptions<GamesContext> options) : base(options) { }

    public DbSet<Game> Games => Set<Game>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>()
            .Property(g => g.Config)
            .HasColumnType("TEXT")
            .HasConversion(
                v => v != null ? v.RootElement.GetRawText() : "{}",
                v => JsonDocument.Parse(v, default)
            );

        modelBuilder.Entity<Game>().HasData(new
        {
            Id = 1,
            Slug = "magicMemory",
            Genre = "Logic",
            Description = "",
            ImageUrl = "",
            IsSystem = true,
            Config = JsonDocument.Parse("{\"items\":[],\"cardBack\":\"\",\"hints\":5,\"title\":\"Magic Memory\"}")
        });
    }
}
