using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using RickAndMortyWebApp.Data;
using RickAndMortyWebApp.Models;

namespace RickAndMortyWebApp.Pages.Characters
{
    public class IndexModel(RickAndMortyWebAppContext Context) : PageModel
    {
        public IList<CharacterListModel> Characters { get; set; } = default!;

        public async Task OnGetAsync()
        {
            // for the sake of simplicity:
            // - I mapped fields manually
            // - I didn't introduced a service to get the Characters
            // - I didn't introduced a repository
            Characters = await Context.Characters.Select(s => new CharacterListModel
            {
                Gender = s.Gender,
                Id = s.Id,
                Image = s.Image,
                LocationName = s.LocationName,
                Name = s.Name,
                OriginName = s.OriginName,
                Species = s.Species,
                Type = s.Type
            }).ToListAsync();
        }
    }
}
