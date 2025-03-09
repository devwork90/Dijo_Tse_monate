using System.ComponentModel.DataAnnotations;

namespace Dijo.API.Models.DTO
{
    public class UpdatedMenuItemDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public double Price { get; set; }
        public string? Image_url { get; set; }

        [Required]
        public bool is_available { get; set; }
    }
}
