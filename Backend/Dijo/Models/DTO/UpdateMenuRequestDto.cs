using RestaurantAPI.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.API.Models.DTO
{
    public class UpdateMenuRequestDto
    {
        [Required]
        public string Name { get; set; }

        //[MinLength(20, ErrorMessage = "Description has to be minimum of 20 characters")]
        //[MaxLength(20 - 50, ErrorMessage = "Description has to to be a maximum of 20 - 50 characters")]
        public string Description { get; set; }

        [Required]
        public bool is_active { get; set; }

        //public string? url_menu_icon { get; set; }
        public Guid? MenuIconImageId { get; set; }
    }
}
