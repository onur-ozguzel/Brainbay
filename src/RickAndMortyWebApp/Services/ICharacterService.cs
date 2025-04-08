using RickAndMortyWebApp.Entities;

namespace RickAndMortyWebApp.Services
{
    public interface ICharacterService
    {
        Task<List<Character>> GetCharacters(int pageIndex, int pageSize);
    }
}
