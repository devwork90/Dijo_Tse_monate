using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.Service
{
    public interface IRestaurantService
    {
        Task<RestaurantResponseDto>GetAllRestaurantsAsync(string? menuName);

        Task<RestaurantDto?> GetRestaurantbyIdAsync(Guid id);

        Task<RestaurantDto> CreateRestaurantAsync(AddRestaurantRequestDto addRestaurantRequestDto);

        Task<RestaurantDto?> UpdateRestaurantAsync(Guid id, UpdateRestaurantRequestDto UpdateRestaurantRequestDt);

        Task<bool> DeleteRestaurantAsync(Guid id);
        Task<List<GroupedMenuItemsDto>> GetMenuItemsByRestaurantAsync(Guid restaurantId);
    }
}
