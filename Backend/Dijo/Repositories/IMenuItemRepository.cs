using RestaurantAPI.Models.Domain;

namespace RestaurantAPI.Repositories
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItem>> GetAllMenuItemsAsync();

        Task<MenuItem?> GetMenuItemByIdAsync(Guid guid);

        Task<MenuItem?> UpdateMenuItem(Guid giud, MenuItem menuItem);

        Task<MenuItem> CreateMenuItem(MenuItem menuItem);

        Task<MenuItem?> DeleteMenuItem(Guid giud);
    }
}
