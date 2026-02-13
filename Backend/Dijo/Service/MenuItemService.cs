using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.API.Repositories;
using RestaurantAPI.Common.Helpers;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;
using RestaurantAPI.Models.Enums;
using RestaurantAPI.Repositories;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;

namespace RestaurantAPI.Service
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository menuItemRepository;
        private readonly ISubMenuRepository subMenuRepository;
        private readonly IImageRepository imageRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository, ISubMenuRepository subMenuRepository, IImageRepository imageRepository)
        {
            this.menuItemRepository = menuItemRepository;
            this.subMenuRepository = subMenuRepository;
            this.imageRepository = imageRepository;
        }

        public async Task<MenuItemsDto> CreateMenuItem(AddMenuItemRequestDto addMenuItemRequestDto)
        {

            var submenu = await subMenuRepository.GetSubMenusByIdAsync(addMenuItemRequestDto.subMenuId);
            
            if (submenu == null)
            {
                throw new Exception("SubMenu does not exist");
            }

            if (submenu.restaurantId != addMenuItemRequestDto.restaurantId)
            {
                throw new Exception("SubMenu does not belong to the specified restaurant");
            }

            //Map DTOs to Domain Model
            var menuItemModel = new MenuItem
            {
                Name = addMenuItemRequestDto.Name,
                Description = addMenuItemRequestDto.Description,
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

        public async Task<MenuItemsResponseDto> GetAllMenuItemsAsync()
        {
            //Get Data from the Database through the dbContext
            var availableMenuItems = await menuItemRepository.GetAllMenuItemsAsync();

            var menuItems = availableMenuItems.Select(item => new MenuItemsDto
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price = item.Price,
                    is_Available = item.is_Available,
                    created_at = item.created_at ?? DateTime.UtcNow,
                    updated_at = item.updated_at,
                    subMenuId = item.SubMenuId,
                    restaurantId = item.restaurantId,
                    MenuItemIconImage = item.MenuItemIcon == null
                    ? null
                    : new ImageDto
                    {
                        Id = item.MenuItemIcon.Id,
                        FileName = item.MenuItemIcon.FileName,
                        FileExtension = item.MenuItemIcon.FileExtension,
                        FileSizeInBytes = item.MenuItemIcon.FileSizeInBytes,
                        FilePath = item.MenuItemIcon.FilePath,
                        ImageType = item.MenuItemIcon.ImageType,
                        created_at = (DateTime)item.created_at
                    }
                });

            //Return DTOs back to client
            return new MenuItemsResponseDto
            {
                MenuItems = menuItems,
            };
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
                created_at = menuItem.created_at ?? DateTime.UtcNow,
                updated_at = menuItem.updated_at,
                subMenuId = menuItem.SubMenuId,
                restaurantId = menuItem.restaurantId,

            };

            //Return DTOs back to client
            return menuItemDto;
        }

        public async Task<MenuItemsDto?> PatchMenuItem(Guid id, PatchMenuItemDto patchMenuItemDto)
        {
           var menuItem = await menuItemRepository.GetMenuItemByIdAsync (id);

            if (menuItem == null)
            {
                return null;
            }

            var image = await imageRepository.GetImageByIdAsync(patchMenuItemDto.MenuItemIconId);
            if (image == null){ throw new Exception("Image is not found"); }

            if (image.ImageType != ImageType.MenuItemIcon) throw new ValidationException("Only MenuIcon images allowed");
            image.MenuItemId = menuItem.Id;
            menuItem.MenuItemIcon = image;
            menuItem.MenuItemIconId = image.MenuId;
            menuItem.SubMenuId = menuItem.SubMenuId;
            menuItem.restaurantId = menuItem.restaurantId;
            menuItem.updated_at = DateTime.UtcNow;

            //Persist the changes to the database
            await menuItemRepository.UpdateMenuItem(id, menuItem);

            //Reload the updated menu item to get the latest data
            var updatedMenuItem = await menuItemRepository.GetMenuItemByIdAsync(id).ConfigureAwait(false);

            return MenuItemImageAssociationHelper.ToDto(updatedMenuItem);

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
