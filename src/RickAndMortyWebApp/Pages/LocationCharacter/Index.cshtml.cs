using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.OutputCaching;
using RickAndMortyWebApp.Models;
using RickAndMortyWebApp.Services;

namespace RickAndMortyWebApp.Pages.LocationCharacter
{
    [OutputCache(Duration = 300, Tags = ["AliveCharacters"])]
    public class IndexModel(ICharacterService characterService) : PageModel
    {
        public string LocationName { get; set; } = string.Empty;
        public PaginationModel<LocationCharacterListModel>? PaginatedResults { get; set; }

        public async Task OnGet(string locationName, int currentPage = 1, int pageSize = 10)
        {
            LocationName = locationName;

            PaginatedResults = await characterService.GetCharactersByLocation(currentPage, pageSize, LocationName);
        }
    }
}
