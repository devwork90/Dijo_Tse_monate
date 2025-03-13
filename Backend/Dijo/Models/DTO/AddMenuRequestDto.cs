using RestaurantAPI.API.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.API.Models.DTO
{
    public class AddMenuRequestDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public bool is_active { get; set; }
       

    }
}
