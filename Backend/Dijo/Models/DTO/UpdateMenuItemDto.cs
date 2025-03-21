using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models.DTO
{
    public class UpdateMenuItemDto
    {
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? imageUrl { get; set; }

        [Required]
        public bool is_Available { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
