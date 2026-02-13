using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Models.DTO
{
    public class PatchMenuItemDto
    {
        [Required]
        public Guid MenuItemIconId { get; set; }
    }
}