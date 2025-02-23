using Dijo.API.Models.Domain;

namespace Dijo.API.Models.DTO
{
    public class AddMenuRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool is_active { get; set; }
        public Guid restaurantId { get; set; }

    }
}
