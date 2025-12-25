using RestaurantAPI.API.Models.DTO;

namespace RestaurantAPI.Models.DTO
{
    public class MenuItemsResponseDto
    {
        public IEnumerable<MenuItemsDto>? MenuItems { get; set; } = Enumerable.Empty<MenuItemsDto>();
    }
}
