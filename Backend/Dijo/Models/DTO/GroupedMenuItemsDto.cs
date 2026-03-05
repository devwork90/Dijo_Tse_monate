using RestaurantAPI.API.Models.DTO;

namespace RestaurantAPI.Models.DTO
{
    public class GroupedMenuItemsDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string description { get; set; }
        public List<MenuItemsDto> Items { get; set; }
    }
}
