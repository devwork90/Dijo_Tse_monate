using RestaurantAPI.API.Models.DTO;

namespace RestaurantAPI.Models.DTO
{
    public class RestaurantResponseDto
    {
        internal List<RestaurantDto> restaurants;

        public List<RestaurantDto>? Restaurants { get; set;}

        //public List<RestaurantCategoryDto>? Categories { get; set;}
    }
}
