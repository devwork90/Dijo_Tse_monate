using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.Service
{
    public interface IMenuService
    {
        Task<MenuListResponseDto> GetMenusAsync();

        Task <MenuDto?> GetmenuByIdAsync(Guid menuId);

        Task<MenuDto?> UpdateMenu(Guid menuId, UpdateMenuRequestDto updateMenuRequestDto);

        Task <MenuDto> CreateMenu(AddMenuRequestDto addMenuRequestDto);

        Task <bool> DeleteMenu(Guid menuId);
    }
}
