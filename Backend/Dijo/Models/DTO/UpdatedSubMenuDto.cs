using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.API.Models.DTO
{
    public class UpdatedSubMenuDto
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public bool is_available { get; set; }
    }
}
