using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RickAndMortyWebApp.Data;
using RickAndMortyWebApp.Entities;
using RickAndMortyWebApp.Models;

namespace RickAndMortyWebApp.Pages.Characters
{
    public class DetailsModel : PageModel
    {
        private readonly RickAndMortyWebApp.Data.RickAndMortyWebAppContext _context;

        public DetailsModel(RickAndMortyWebApp.Data.RickAndMortyWebAppContext context)
        {
            _context = context;
        }

        public CharacterModel Character { get; set; } = default!;

        // for the sake of simplicity:
        // - I mapped fields manually
        // - I didn't introduced a service to get the Characters
        // - I didn't introduced a repository
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await _context.Characters.Include(a => a.CharacterEpisodes).Select(s => new CharacterModel
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

            if (character == null)
            {
                return NotFound();
            }
            else
            {
                Character = character;
            }
            return Page();
        }
    }
}
