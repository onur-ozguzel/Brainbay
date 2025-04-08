using RickAndMortyWebApp.Models;

namespace RickAndMortyWebApp.Services
{
    public interface ICharacterService
    {
        Task<PaginationModel<CharacterListModel>> GetCharacters(int pageIndex, int pageSize);
        Task CreateCharacter(CharacterModel model);
        Task<CharacterModel?> GetCharacter(int id);
    }
}
