using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;
using RestaurantAPI.Repositories;

namespace RestaurantAPI.Service
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            this.menuItemRepository = menuItemRepository;
        }

        public async Task<MenuItemsDto> CreateMenuItem(AddMenuItemRequestDto addMenuItemRequestDto)
        {
            //Map DTOs to Domain Model
            var menuItemModel = new MenuItem
            {
                Name = addMenuItemRequestDto.Name,
                Description = addMenuItemRequestDto.Description,
                imageUrl = addMenuItemRequestDto.imageUrl,
                Price = addMenuItemRequestDto.Price,
                is_Available = addMenuItemRequestDto.is_Available,
                restaurantId = addMenuItemRequestDto.restaurantId,
                SubMenuId = addMenuItemRequestDto.subMenuId,
                created_at = DateTime.UtcNow

            };

            //Use menuItemRepository to save data to DB layer
            await menuItemRepository.CreateMenuItem(menuItemModel);

            //Map Domain Model to DTOs
            var menuItemsDto = new MenuItemsDto
            {
                Id = menuItemModel.Id,
                Name = menuItemModel.Name,
                Description = menuItemModel.Description,
                imageUrl = menuItemModel.imageUrl,
                Price = menuItemModel.Price,
                is_Available = menuItemModel.is_Available,
                restaurantId = menuItemModel.restaurantId,
                subMenuId = menuItemModel.SubMenuId,
                created_at = menuItemModel.created_at

            };

            return menuItemsDto;
        }

        public async Task<bool?> DeleteMenuItem(Guid id)
        {
            var existingMenuItem = await menuItemRepository.DeleteMenuItem(id);
            if (existingMenuItem == null) { return false; }
            return true;
        }

        public async Task<List<MenuItemsDto>> GetAllMenuItemsAsync()
        {
            //Get Data from the Database through the dbContext
            var availableMenuItems = await menuItemRepository.GetAllMenuItemsAsync();

            //Map Domain Models to the DTOs
            var menuItemDto = new List<MenuItemsDto>();
            foreach (var item in availableMenuItems)
            {
                menuItemDto.Add(new MenuItemsDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    is_Available = item.is_Available,
                    imageUrl = item.imageUrl,
                    created_at = item.created_at ?? DateTime.UtcNow,
                    updated_at = item.updated_at,
                    subMenuId = item.SubMenuId,
                    restaurantId = item.restaurantId,
                });
            }

            //Return DTOs back to client
            return menuItemDto;
        }

        public async Task<MenuItemsDto?> GetMenuItemByIdAsync(Guid id)
        {
            //Get Data from the Database through the dbContext
            var menuItem = await menuItemRepository.GetMenuItemByIdAsync(id);

            if (menuItem == null)
            {
                return null;
            }

            // Map Domain Models to DTOs
            var menuItemDto = new MenuItemsDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                is_Available = menuItem.is_Available,
                Price = menuItem.Price,
                imageUrl = menuItem.imageUrl,
                created_at = menuItem.created_at ?? DateTime.UtcNow,
                updated_at = menuItem.updated_at,
                subMenuId = menuItem.SubMenuId,
                restaurantId = menuItem.restaurantId,

            };

            //Return DTOs back to client
            return menuItemDto;
        }

        public async Task<MenuItemsDto?> UpdateMenuItem(Guid id, UpdateMenuItemDto updateMenuItemDto)
        {
            //Map Dto to Domain Model
            var menuItemDomainModel = new MenuItem
            {
                Name = updateMenuItemDto.Name,
                Description = updateMenuItemDto.Description,
                Price = updateMenuItemDto.Price,
                is_Available = updateMenuItemDto.is_Available,
                imageUrl = updateMenuItemDto.imageUrl,
            };


            menuItemDomainModel = await menuItemRepository.UpdateMenuItem(id, menuItemDomainModel);

            //Check if the item exists
            if (menuItemDomainModel == null)
            {
                return null;
            }

            var menuItemDto = new MenuItemsDto
            {
                Id = menuItemDomainModel.Id,
                Name = menuItemDomainModel.Name,
                Description = menuItemDomainModel.Description,
                Price = menuItemDomainModel.Price,
                imageUrl = menuItemDomainModel.imageUrl,
                restaurantId = menuItemDomainModel.restaurantId,
                subMenuId = menuItemDomainModel.SubMenuId,
                updated_at = menuItemDomainModel.updated_at,
                created_at = menuItemDomainModel.created_at,

            };

            //Return DTOs back to client
            return menuItemDto;
        }
    }
}
