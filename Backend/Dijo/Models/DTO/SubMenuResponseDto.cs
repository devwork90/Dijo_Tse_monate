using RestaurantAPI.API.Models.DTO;

namespace RestaurantAPI.Models.DTO
{
    public class SubMenuResponseDto
    {
        public IEnumerable<SubMenuDto>? SubMenus { get; set; } = Enumerable.Empty<SubMenuDto>();
    }
}
