using RestaurantAPI.API.Models.Domain;

namespace RestaurantAPI.API.Repositories
{
    public interface ISubMenuRepository
    {
        Task<List<SubMenu>> GetAllMenuItemsAsync();

        Task<SubMenu?> GetMenuItemByIdAsync(Guid id);

        Task<SubMenu?> UpdateMenuItemAsync(Guid guid, SubMenu item);

        Task<SubMenu> CreateMenuItem(SubMenu item);

        Task<SubMenu?> DeleteMenuItem(Guid id);
    }
}
