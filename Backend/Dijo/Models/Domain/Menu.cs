namespace RestaurantAPI.API.Models.Domain
{
    public class Menu
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public bool is_active {  get; set; }
        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; }

        public ICollection<SubMenu> SubMenus { get; set; } // Ensure this exists
        public ICollection<Restaurant> Restaurants { get; set; }
    }
}
