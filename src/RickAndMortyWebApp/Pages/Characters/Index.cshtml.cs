using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;
using RickAndMortyWebApp.Models;
using RickAndMortyWebApp.Services;

namespace RickAndMortyWebApp.Pages.Characters
{
    [OutputCache(Duration = 300, Tags = ["AliveCharacters"])]
    public class IndexModel(ICharacterService characterService) : PageModel
    {
        public PaginationModel<CharacterListModel>? PaginatedResults { get; set; }

        public async Task OnGetAsync(int currentPage = 1, int pageSize = 10)
        {
            PaginatedResults = await characterService.GetCharacters(currentPage, pageSize);
        }
    }
}
