using RickAndMortyWebApp.Entities;
using System.ComponentModel.DataAnnotations;

namespace RickAndMortyWebApp.Models
{
    public class CharacterModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        public string Status { get; set; } = string.Empty;
        
        [Required]
        public string Species { get; set; } = string.Empty;
        
        [Required]
        public string Type { get; set; } = string.Empty;
        
        [Required]
        public string Gender { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Origin Name")]
        public string OriginName { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Origin Url")]
        public string OriginUrl { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Location Name")]
        public string LocationName { get; set; } = string.Empty;
        
        [Required]
        [Display(Name = "Location Url")]
        public string LocationUrl { get; set; } = string.Empty;
        
        [Required]
        public string Image { get; set; } = string.Empty;
        
        public List<string>? Episodes { get; set; }
        
        [Required]
        public string Url { get; set; } = string.Empty;
        
        [Required]
        public DateTime Created { get; set; }
    }
}
