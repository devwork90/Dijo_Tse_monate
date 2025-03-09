using System.ComponentModel.DataAnnotations;

namespace Dijo.API.Models.DTO
{
    public class AddRestaurantRequestDto
    {
        [Required]
        public string name { get; set; }

        [Required]
        public string Address { get; set; }
        public string description { get; set; }

        public string logo_url { get; set; }
        public int rating { get; set; }

        [Required]
        public bool is_open { get; set; }

    }
}
