using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.Service
{
    public interface IMenuItemService
    {
        Task<MenuItemsResponseDto> GetAllMenuItemsAsync();

        Task<MenuItemsDto?> GetMenuItemByIdAsync(Guid id);

        Task<MenuItemsDto?> UpdateMenuItem(Guid id, UpdateMenuItemDto updateMenuItemDto);

        Task<MenuItemsDto?> PatchMenuItem(Guid id, PatchMenuItemDto patchMenuItemDto);

        Task<MenuItemsDto> CreateMenuItem(AddMenuItemRequestDto addMenuItemRequestDto);

        Task<bool?> DeleteMenuItem(Guid id);
    }
}
