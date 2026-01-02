using RestaurantAPI.Models.Domain;

namespace RestaurantAPI.API.Models.Domain
{
    public class Menu
    {

        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public bool is_active {  get; set; }

        public string? url_menu_icon { get; set; } 
        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; }
        public Guid? MenuIconImageId { get; set; }
        public Image? MenuIconImage {  get; set; }
        //Specifies One-to-Many with SubMenu
        public ICollection<SubMenu> SubMenus { get; set; } = new List<SubMenu>();

        //Specifies Many-to-Many with Restaurant
        public ICollection<Restaurant> Restaurants { get; set; } = new List<Restaurant>();

        public ICollection<Image> Images { get; set; } = new LinkedList<Image>();
    }
}
