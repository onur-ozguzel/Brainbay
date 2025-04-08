namespace RickAndMortyDataFetcher.Entities;

/// <summary>
/// NOTE: for simplicity I didn't create a common layer between DataFetcher & WebApp
/// </summary>
public class CharacterEpisode
{
    public int CharacterId { get; set; }
    public Character Character { get; set; } = null!;
    public string EpisodeUrl { get; set; } = string.Empty;
}
