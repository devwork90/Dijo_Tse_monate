using RestaurantAPI.API.Models.DTO;

namespace RestaurantAPI.Models.DTO
{
    public class RestaurantMenuItemsDto
    {
       public RestaurantDto Restaurant { get; set; }
        //public List<SubMenuDto> SubMenus { get; set; }
        public List<GroupedMenuItemsDto> Menu { get; set; }
    }
}
