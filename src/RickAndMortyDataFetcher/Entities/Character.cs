namespace RickAndMortyDataFetcher.Entities;

/// <summary>
/// NOTE: for simplicity I didn't create a common layer between DataFetcher & WebApp
/// </summary>
public class Character
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public string OriginName { get; set; } = string.Empty;
    public string OriginUrl { get; set; } = string.Empty;
    public string LocationName { get; set; } = string.Empty;
    public string LocationUrl { get; set; } = string.Empty;
    public string Image { get; set; } = string.Empty;
    public List<CharacterEpisode>? CharacterEpisodes { get; set; }
    public string Url { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}
