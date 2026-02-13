using RestaurantAPI.API.Models.Domain;

namespace RestaurantAPI.Models.Domain
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public  string? imageUrl { get; set; }
        public decimal Price { get; set; }
        public bool is_Available { get; set; }
        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; }

        //Foreign Key for Sub-Menu (One-Many)
        public Guid SubMenuId { get; set; }
        public SubMenu SubMenu { get; set; }

        public Guid? MenuItemIconId { get; set; }

        public Image? MenuItemIcon { get; set; }

        //Foreign Key for restaurant (One-Many)
        public Guid restaurantId { get; set; }
        public Restaurant Restaurant { get; set; }
    }
}
