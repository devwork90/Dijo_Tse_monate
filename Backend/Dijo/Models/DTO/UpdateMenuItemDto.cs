namespace RestaurantAPI.Models.DTO
{
    public class UpdateMenuItemDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string imageUrl { get; set; }
        public bool is_Available { get; set; }
        public decimal Price { get; set; }
    }
}
