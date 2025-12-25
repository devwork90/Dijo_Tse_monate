using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.Service
{
    public interface ISubMenuService
    {
        Task<SubMenuResponseDto> GetAllSubMenusAsync();

        Task<SubMenuDto?> GetSubMenusByIdAsync(Guid id);

        Task<SubMenuDto?> UpdateSubMenusAsync(Guid id, UpdatedSubMenuDto updatedSubMenuDto);

        Task<SubMenuDto> CreateSubMenus(AddSubMenuDto addSubMenuDto);

        Task<bool> DeleteSubMenus(Guid id);
    }
}
