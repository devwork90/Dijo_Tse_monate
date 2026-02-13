using RestaurantAPI.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models.DTO
{
    public class AddMenuItemRequestDto
    {
        [Required]
        public required string Name { get; set; }
        public string? Description { get; set; }

        [Required]
        public bool is_Available { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public Guid subMenuId { get; set; }

        [Required]
        public Guid restaurantId { get; set; }
    
    }
}
