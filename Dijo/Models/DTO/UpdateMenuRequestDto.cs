using System.ComponentModel.DataAnnotations;

namespace Dijo.API.Models.DTO
{
    public class UpdateMenuRequestDto
    {
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required]
        public bool is_active { get; set; }
    }
}
