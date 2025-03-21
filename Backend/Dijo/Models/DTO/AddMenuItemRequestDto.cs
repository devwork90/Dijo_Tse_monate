using RestaurantAPI.API.Models.Domain;

namespace RestaurantAPI.Models.DTO
{
    public class AddMenuItemRequestDto
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public  string imageUrl { get; set; }
        public bool is_Available { get; set; }
        public decimal Price { get; set; }

        public Guid subMenuId { get; set; }
        public Guid restaurantId { get; set; }
    
    }
}
