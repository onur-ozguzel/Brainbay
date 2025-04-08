using Microsoft.EntityFrameworkCore;
using RickAndMortyDataFetcher.Entities;

namespace RickAndMortyDataFetcher.Data;

/// <summary>
/// NOTE: for simplicity I didn't create a common layer between DataFetcher & WebApp for the database objects
/// </summary>
/// <param name="options"></param>
public class RickAndMortyDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Character> Characters { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CharacterEpisode>()
            .HasKey(ce => new { ce.CharacterId, ce.EpisodeUrl });

        modelBuilder.Entity<CharacterEpisode>()
            .HasOne(ce => ce.Character)
            .WithMany(c => c.CharacterEpisodes)
            .HasForeignKey(ce => ce.CharacterId);
    }
}
