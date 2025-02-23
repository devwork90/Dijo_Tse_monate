using Dijo.API.Models.Domain;

namespace Dijo.API.Models.DTO
{
    public class MenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool is_active { get; set; }
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public Guid restaurantId { get; set; }

    }
}