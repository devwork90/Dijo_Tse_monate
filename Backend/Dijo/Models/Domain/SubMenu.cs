using RestaurantAPI.Models.Domain;

namespace RestaurantAPI.API.Models.Domain
{
    public class SubMenu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool is_available { get; set; }
        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; }

        //Foreign Key to Menu (One-tio-Many)
        public Guid MenuId { get; set; }
        public Menu Menu { get; set; }

        //Foreign Key to Restaurant (One-to-One)
        public Guid restaurantId { get; set; }
        public Restaurant? Restaurant { get; set; }

        // Navigation Property for One-to-Many with MenuItem
        public List<MenuItem> MenuItems { get; set; } = new();

    }
}
