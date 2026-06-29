using OrderAPI.Models.DTO;

namespace OrderAPI.Service
{
    public interface IRestaurantService
    {
        Task<MenuItemDTO?> GetMenuItemAsync(Guid menuItemId);
    }
}
