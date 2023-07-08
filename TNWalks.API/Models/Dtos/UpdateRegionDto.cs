
using System.ComponentModel.DataAnnotations;

namespace TNWalks.API.Models.Dtos
{
    public class UpdateRegionDto
    {
        [Required]
        [MinLength(3, ErrorMessage = "Code must be 3 characters")]
        [MaxLength(3, ErrorMessage = "Code must be 3 characters")]
        public string Code { get; set; }
        
        [Required]
        [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }
        
        public string? RegionImageUrl { get; set; }
    }
}