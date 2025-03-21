using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.API.Models.DTO
{
    public class AddSubMenuDto
    {
        [Required]
        public required string Name { get; set; }
       
        [Required]
        public bool is_available { get; set; }

        [Required]
        public Guid MenuId { get; set; }

        [Required]
        public Guid restaurantId { get; set; }
        

    }
}
