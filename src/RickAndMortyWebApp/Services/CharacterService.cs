using Microsoft.EntityFrameworkCore;
using RickAndMortyWebApp.Data;
using RickAndMortyWebApp.Entities;

namespace RickAndMortyWebApp.Services
{
    public class CharacterService(RickAndMortyWebAppContext context)
    {
        public async Task<List<Character>> GetCharacters(int pageIndex, int pageSize)
        {
            int skip = pageSize * (pageIndex - 1);
            int take = pageSize;

            return await context.Characters.Include(i => i.CharacterEpisodes).Skip(skip).Take(skip).ToListAsync();
        }
    }
}
