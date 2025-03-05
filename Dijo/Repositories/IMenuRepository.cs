using Dijo.API.Models.Domain;

namespace Dijo.API.Repositories
{
    public interface IMenuRepository
    {
        Task<List<Menu>> GetAllMenusAsync();

        Task<Menu?> GetByIdAsync(Guid id);

        Task<Menu?> UpdateMenuAsync(Guid id, Menu? menu);

        Task<Menu> CreateMenuAsync(Menu menu);

        Task<Menu> DeleteMenuAsync(Guid id);
    }
}
