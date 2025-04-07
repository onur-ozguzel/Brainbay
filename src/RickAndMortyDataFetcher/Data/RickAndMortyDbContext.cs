using Microsoft.EntityFrameworkCore;
using RickAndMortyDataFetcher.Entities;

namespace RickAndMortyDataFetcher.Data;

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
