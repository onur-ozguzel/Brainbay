using System.Net.Http.Json;
using AutoMapper;
using RickAndMortyDataFetcher.Data;
using RickAndMortyDataFetcher.DTOs;
using RickAndMortyDataFetcher.Entities;

namespace RickAndMortyDataFetcher.Services;

public class CharacterService(HttpClient httpClient, IMapper mapper, RickAndMortyDbContext dbContext) : ICharacterService
{
    public async Task GetAndSaveAliveCharactersAsync(CancellationToken cancellationToken = default)
    {
        var characters = await GetAllAliveCharactersAsync(cancellationToken);
        var entities = mapper.Map<List<Character>>(characters);

        await SaveCharactersAsync(entities, cancellationToken);
    }

    private async Task<List<CharacterDto>> GetAllAliveCharactersAsync(CancellationToken cancellationToken)
    {
        var endPoint = "character?status=alive";
        var characters = new List<CharacterDto>();
        ApiResponseDto? response;
        do
        {
            response = await GetAliveCharactersAsync(endPoint, cancellationToken);
            if (response == null) break;

            if (response.Results != null && response.Results.Count != 0)
            {
                characters.AddRange(response.Results);
            }

            if (response.Info.Next != null)
            {
                endPoint = response.Info.Next;
            }
        }
        while (response.Info.Next != null);


        return characters;
    }

    private async Task<ApiResponseDto?> GetAliveCharactersAsync(string endpoint, CancellationToken cancellationToken)
    {
        var response = await SendGetRequestAsync(endpoint, httpClient, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<ApiResponseDto>(cancellationToken: cancellationToken)
               ?? throw new InvalidOperationException("Failed to deserialize the response to ApiResponseDto.");

        return result;
    }

    // for the sake of simplicity
    // - I didn't introduced any repository
    private async Task SaveCharactersAsync(List<Character> characters, CancellationToken cancellationToken)
    {
        dbContext.Characters.AddRange(characters);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    // for the sake of simplicity
    // - I implemented this method to this service
    // - It can be extracted to another class like an HttpHelper class
    public async Task<HttpResponseMessage> SendGetRequestAsync(string endpoint, HttpClient httpClient, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.GetAsync(endpoint, cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync(cancellationToken);
            throw new HttpRequestException(
                $"Request failed with status code {(int)response.StatusCode} ({response.ReasonPhrase}). " +
                $"Response: {errorContent}");
        }

        return response;
    }
}
