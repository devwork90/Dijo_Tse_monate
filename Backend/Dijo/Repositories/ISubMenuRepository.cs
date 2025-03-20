using RestaurantAPI.API.Models.Domain;

namespace RestaurantAPI.API.Repositories
{
    public interface ISubMenuRepository
    {
        Task<List<SubMenu>> GetAllSubMenusAsync();

        Task<SubMenu?> GetSubMenusByIdAsync(Guid id);

        Task<SubMenu?> UpdateSubMenusAsync(Guid guid, SubMenu item);

        Task<SubMenu> CreateSubMenus(SubMenu item);

        Task<SubMenu?> DeleteSubMenus(Guid id);
    }
}
