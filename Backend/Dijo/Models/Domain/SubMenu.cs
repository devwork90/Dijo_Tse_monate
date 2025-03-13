namespace RestaurantAPI.API.Models.Domain
{
    public class SubMenu
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool is_available { get; set; }
        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; }
        public Guid MenuId { get; set; }
        public Guid restaurantId { get; set; }

        public Menu Menu { get; set; }
        public Restaurant Restaurant { get; set; }

        public ICollection<SubMenu  > SubMenus { get; set; } // Ensure this exists
    }
}
