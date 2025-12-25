using RestaurantAPI.API.Models.DTO;

namespace RestaurantAPI.Models.DTO
{
    public class MenuListResponseDto
    {
        public IEnumerable<MenuDto> MenuList { get; set; } = Enumerable.Empty<MenuDto>();
    }
}
