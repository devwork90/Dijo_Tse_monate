using RestaurantAPI.API.Models.DTO;

namespace RestaurantAPI.Service
{
    public interface IMenuService
    {
        Task<IEnumerable<MenuDto>> GetMenusAsync();

        Task <MenuDto?> GetmenuByIdAsync(Guid menuId);

        Task<MenuDto?> UpdateMenu(Guid menuId, UpdateMenuRequestDto updateMenuRequestDto);

        Task <MenuDto> CreateMenu(AddMenuRequestDto addMenuRequestDto);

        Task <bool> DeleteMenu(Guid menuId);
    }
}
