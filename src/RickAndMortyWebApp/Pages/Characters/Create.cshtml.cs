using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using RickAndMortyWebApp.Data;
using RickAndMortyWebApp.Entities;
using RickAndMortyWebApp.Models;

namespace RickAndMortyWebApp.Pages.Characters
{
    public class CreateModel(RickAndMortyWebAppContext Context) : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CharacterModel Character { get; set; } = default!;


        // for the sake of simplicity:
        // - I mapped fields manually
        // - I didn't introduced a service to get the Characters
        // - I didn't introduced a repository
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var entity = new Character
            {
                CharacterEpisodes = Character.Episodes?.Select(s => new CharacterEpisode { EpisodeUrl = s }).ToList(),
                Created = Character.Created.ToUniversalTime(),
                Gender = Character.Gender,
                Image = Character.Image,
                LocationName = Character.LocationName,
                LocationUrl = Character.LocationUrl,
                Name = Character.Name,
                OriginName = Character.OriginName,
                OriginUrl = Character.OriginUrl,
                Species = Character.Species,
                Status = Character.Status,
                Type = Character.Type,
                Url = Character.Url,
                Id = Context.Characters.Max(c => c.Id) + 1
            };

            Context.Characters.Add(entity);
            await Context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
