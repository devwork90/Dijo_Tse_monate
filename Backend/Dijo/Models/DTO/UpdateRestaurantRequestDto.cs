using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.API.Models.DTO
{
    public class UpdateRestaurantRequestDto
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string Address { get; set; }
        public string description { get; set; }

        public Guid? RestaurantIconId { get; set; }
    }
}
