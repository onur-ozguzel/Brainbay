using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;
using RickAndMortyWebApp.Models;
using RickAndMortyWebApp.Services;

namespace RickAndMortyWebApp.Pages.Characters
{
    [OutputCache(Duration = 300, Tags = ["Character"])]
    public class DetailsModel(ICharacterService characterService) : PageModel
    {
        public CharacterModel Character { get; set; } = default!;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var character = await characterService.GetCharacter(id.Value);

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
