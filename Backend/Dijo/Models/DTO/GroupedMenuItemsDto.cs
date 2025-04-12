using RestaurantAPI.API.Models.DTO;

namespace RestaurantAPI.Models.DTO
{
    public class GroupedMenuItemsDto
    {
        public Guid SubMenuId { get; set; }
        public string SubMenuName { get; set; }
        public List<MenuItemsDto> Items { get; set; }
    }
}
