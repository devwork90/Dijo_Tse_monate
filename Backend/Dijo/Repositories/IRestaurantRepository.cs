using RestaurantAPI.API.Models.Domain;

namespace RestaurantAPI.API.Repositories
{
    public interface IRestaurantRepository
    {
        Task<List<Restaurant>> GetAllAsync();

        Task<Restaurant?> GetRestaurantbyIdAsync(Guid guid);

        Task<Restaurant> CreateRestaurantAsync(Restaurant restaurant);

        Task<Restaurant?> UpdateRestaurantAsync(Guid id, Restaurant restaurant);

        Task<Restaurant?> DeleteRestaurantAsync(Guid id);
    }
}
