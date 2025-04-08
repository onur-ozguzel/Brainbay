namespace RickAndMortyDataFetcher.DTOs;

public class ApiResponseDto
{
    public PageInfoDto Info { get; set; } = default!;
    public List<CharacterDto>? Results { get; set; }
}
