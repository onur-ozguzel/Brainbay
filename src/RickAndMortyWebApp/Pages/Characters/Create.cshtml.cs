using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;
using RickAndMortyWebApp.Models;
using RickAndMortyWebApp.Services;

namespace RickAndMortyWebApp.Pages.Characters
{
    public class CreateModel(ICharacterService characterService, IOutputCacheStore cacheStore) : PageModel
    {
        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public CharacterModel Character { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await characterService.CreateCharacter(Character);

            // NOTE: Invalidating the cache might not be the responsibility of this page
            // but I left it like this for the simplicity. A better approach might be 
            // implementing events
            await cacheStore.EvictByTagAsync("AliveCharacters", default);

            return RedirectToPage("./Index");
        }
    }
}
