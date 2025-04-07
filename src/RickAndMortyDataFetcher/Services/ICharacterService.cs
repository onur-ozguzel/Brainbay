namespace RickAndMortyDataFetcher.Services;

public interface ICharacterService
{
    Task GetAndSaveAliveCharactersAsync(CancellationToken cancellationToken = default);
}
