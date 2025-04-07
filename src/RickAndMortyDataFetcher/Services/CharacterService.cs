using System.Net.Http.Json;
using AutoMapper;
using RickAndMortyDataFetcher.Data;
using RickAndMortyDataFetcher.DTOs;
using RickAndMortyDataFetcher.Entities;
using RickAndMortyDataFetcher.Helpers;

namespace RickAndMortyDataFetcher.Services;

public class CharacterService(HttpClient HttpClient, IMapper Mapper, RickAndMortyDbContext DbContext, IHttpHelper HttpHelper) : ICharacterService
{
    public async Task GetAndSaveAliveCharactersAsync(CancellationToken cancellationToken = default)
    {
        var apiResult = await GetAliveCharactersAsync(cancellationToken);
        var entities = Mapper.Map<List<Character>>(apiResult.Results);

        await SaveCharactersAsync(entities, cancellationToken);
    }

    private async Task<ApiResponseDto> GetAliveCharactersAsync(CancellationToken cancellationToken)
    {
        var response = await HttpHelper.SendGetRequestAsync("character?status=alive", HttpClient, cancellationToken);
        var result = await response.Content.ReadFromJsonAsync<ApiResponseDto>(cancellationToken: cancellationToken)
               ?? throw new InvalidOperationException("Failed to deserialize the response to ApiResponseDto.");

        return result;
    }

    private async Task SaveCharactersAsync(List<Character> characters, CancellationToken cancellationToken)
    {
        DbContext.Characters.AddRange(characters);
        await DbContext.SaveChangesAsync(cancellationToken);
    }
}
