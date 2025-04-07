namespace RickAndMortyDataFetcher.DTOs;

public class CharacterDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public string Species { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public OriginDto? Origin { get; set; }
    public LocationDto? Location { get; set; }
    public string Image { get; set; } = string.Empty;
    public List<string> Episode { get; set; } = null!;
    public string Url { get; set; } = string.Empty;
    public DateTime Created { get; set; }
}
