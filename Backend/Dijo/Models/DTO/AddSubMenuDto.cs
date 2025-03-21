using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.API.Models.DTO
{
    public class AddSubMenuDto
    {
        [Required]
        public required string Name { get; set; }
       
        [Required]
        public bool is_available { get; set; }
        public Guid MenuId { get; set; }
        public Guid restaurantId { get; set; }
        

    }
}
