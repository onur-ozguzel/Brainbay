using System.ComponentModel.DataAnnotations;

namespace RickAndMortyWebApp.Models
{
    public class LocationCharacterListModel
    {
        public int Id { get; set; }
        
        public string Name { get; set; } = string.Empty;
        
        public string Species { get; set; } = string.Empty;
        
        public string Type { get; set; } = string.Empty;
        
        public string Gender { get; set; } = string.Empty;
        
        [Display(Name = "Origin Name")]
        public string OriginName { get; set; } = string.Empty;
        
        public string Image { get; set; } = string.Empty;
    }
}
