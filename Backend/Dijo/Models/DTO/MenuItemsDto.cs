using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.API.Models.DTO
{
    public class MenuItemsDto
    {
        public Guid Id { get; set; }
        public  string Name { get; set; }
        public string? Description { get; set; }
        public  string imageUrl { get; set; }
        public bool is_Available { get; set; }
        public decimal Price { get; set; }
        public DateTime? created_at { get; set; } 
        public DateTime? updated_at { get; set; }
        public Guid subMenuId { get; set; }
        public Guid restaurantId { get; set; }
        public ImageDto? MenuItemIconImage { get; set; }
    }
}
