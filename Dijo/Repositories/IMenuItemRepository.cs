using Dijo.API.Models.Domain;

namespace Dijo.API.Repositories
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItem>> GetAllMenuItemsAsync();

        Task<MenuItem?> GetMenuItemByIdAsync(Guid id);

        Task<MenuItem?> UpdateMenuItemAsync(Guid guid, MenuItem item);

        Task<MenuItem> CreateMenuItem(MenuItem item);

        Task<MenuItem?> DeleteMenuItem(Guid id);
    }
}
