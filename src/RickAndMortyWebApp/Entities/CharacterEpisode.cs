namespace RickAndMortyWebApp.Entities;

public class CharacterEpisode
{

    public int CharacterId { get; set; }
    public Character Character { get; set; } = null!;
    public string EpisodeUrl { get; set; } = string.Empty;
}
