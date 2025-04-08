using Microsoft.EntityFrameworkCore;
using RickAndMortyWebApp.Entities;

namespace RickAndMortyWebApp.Data
{
    public class RickAndMortyWebAppContext : DbContext
    {
        public RickAndMortyWebAppContext (DbContextOptions<RickAndMortyWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<Character> Characters { get; set; } = default!;

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
}
