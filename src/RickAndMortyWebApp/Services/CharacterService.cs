using Microsoft.EntityFrameworkCore;
using RickAndMortyWebApp.Data;
using RickAndMortyWebApp.Entities;
using RickAndMortyWebApp.Models;

namespace RickAndMortyWebApp.Services
{
    public class CharacterService(RickAndMortyWebAppContext context) : ICharacterService
    {
        /// <summary>
        // for the sake of simplicity
        /// - I did pager calculation in this service
        ///   Other patterns like Specification can be used for this purpose
        /// - I didn't create repository
        /// - I mapped fields manually
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public async Task<PaginationModel<CharacterListModel>> GetCharacters(int pageIndex, int pageSize)
        {
            var totalCount = await context.Characters.CountAsync();
            int skip = pageSize * (pageIndex - 1);
            var dbResults = await context.Characters.Include(i => i.CharacterEpisodes).Skip(skip).Take(pageSize).ToListAsync();


            return new PaginationModel<CharacterListModel>(pageIndex, pageSize, totalCount, [.. dbResults.Select(s => new CharacterListModel
            {
                Gender = s.Gender,
                Id = s.Id,
                Image = s.Image,
                LocationName = s.LocationName,
                Name = s.Name,
                OriginName = s.OriginName,
                Species = s.Species,
                Type = s.Type
            })]);
        }

        /// <summary>
        /// for the sake of simplicity
        ///  - I mapped fields manually
        ///  - I didn't create repository
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<CharacterModel?> GetCharacter(int id)
        {
            var character = await context.Characters.Include(a => a.CharacterEpisodes).Select(s => new CharacterModel
            {
                Created = s.Created,
                Episodes = s.CharacterEpisodes != null ? s.CharacterEpisodes.Select(s => s.EpisodeUrl).ToList() : null,
                Gender = s.Gender,
                Id = s.Id,
                Image = s.Image,
                LocationName = s.LocationName,
                LocationUrl = s.LocationUrl,
                Name = s.Name,
                OriginName = s.OriginName,
                OriginUrl = s.OriginUrl,
                Species = s.Species,
                Status = s.Status,
                Type = s.Type,
                Url = s.Url
            }).FirstOrDefaultAsync(m => m.Id == id);

            return character;
        }

        /// <summary>
        /// for the sake of simplicity
        ///  - I mapped fields manually
        ///  - I didn't create repository
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task CreateCharacter(CharacterModel model)
        {
            var entity = new Character
            {
                CharacterEpisodes = model.Episodes?.Select(s => new CharacterEpisode { EpisodeUrl = s }).ToList(),
                Created = model.Created.ToUniversalTime(),
                Gender = model.Gender,
                Image = model.Image,
                LocationName = model.LocationName,
                LocationUrl = model.LocationUrl,
                Name = model.Name,
                OriginName = model.OriginName,
                OriginUrl = model.OriginUrl,
                Species = model.Species,
                Status = model.Status,
                Type = model.Type,
                Url = model.Url,
                Id = context.Characters.Max(c => c.Id) + 1
            };

            context.Characters.Add(entity);
            await context.SaveChangesAsync();
        }
    }
}
