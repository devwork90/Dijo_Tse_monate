
using Microsoft.AspNetCore.Http.HttpResults;
using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.API.Repositories;
using RestaurantAPI.Common.Helpers;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;
using RestaurantAPI.Models.Enums;
using RestaurantAPI.Repositories;
using System.ComponentModel.DataAnnotations;

namespace RestaurantAPI.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository menuRepository;
        private readonly IImageRepository imageRepository;

        public MenuService(IMenuRepository menuRepository,
            IImageRepository imageRepository)
        {
            this.menuRepository = menuRepository;
            this.imageRepository = imageRepository;
        }

        public async Task<MenuDto> CreateMenu(AddMenuRequestDto addMenuRequestDto)
        {

            //Map Dto to Domain Model
            var menuDomainModel = new Menu
            {
                Name = addMenuRequestDto.Name,
                Description = addMenuRequestDto.Description,
                is_active = addMenuRequestDto.is_active,
                created_at = DateTime.UtcNow,
            };

            menuDomainModel = await menuRepository.CreateMenuAsync(menuDomainModel);

            //Map Domain models back to DTO's
            var menuDto = new MenuDto
            {
                Id = menuDomainModel.Id,
                Name = menuDomainModel.Name,
                Description = menuDomainModel.Description,
                is_active = menuDomainModel.is_active,
                created_at = (DateTime)menuDomainModel.created_at,

            };

            return menuDto;
        }

        public async Task<MenuDto?> GetmenuByIdAsync(Guid menuId)
        {
            var existingMenu = await menuRepository.GetByIdAsync(menuId);

            if (existingMenu == null) { return null; }

            var menuItemDto = new MenuDto
            {
                Id = existingMenu.Id,
                Name = existingMenu.Name,
                Description = existingMenu.Description,
                is_active = existingMenu.is_active,
                created_at = existingMenu.created_at ?? DateTime.Now,
                updated_at = existingMenu.updated_at,
                MenuIcon = existingMenu.MenuIconImage == null
                ? null
                : new ImageDto
                {
                    Id = existingMenu.MenuIconImage.Id,
                    FileName = existingMenu.MenuIconImage.FileName,
                    FileExtension = existingMenu.MenuIconImage.FileExtension,
                    FileSizeInBytes = existingMenu.MenuIconImage.FileSizeInBytes,
                    FilePath = existingMenu.MenuIconImage.FilePath,
                    ImageType = existingMenu.MenuIconImage.ImageType,
                    created_at = (DateTime)existingMenu.created_at

                }

            };

            return menuItemDto;
        }

        public async Task<MenuListResponseDto> GetMenusAsync()
        {
            //Get Data from DB by Domain Model
            var menuItems = await menuRepository.GetAllMenusAsync();

            //Map Model to DTO
            var menus = menuItems.Select(item => new MenuDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                is_active = item.is_active,
                created_at = item.created_at ?? DateTime.Now,
                MenuIcon = item.MenuIconImage == null
                ? null
                : new ImageDto
                {
                    Id = item.MenuIconImage.Id,
                    FileName = item.MenuIconImage.FileName,
                    FileExtension = item.MenuIconImage.FileExtension,
                    FileSizeInBytes = item.MenuIconImage.FileSizeInBytes,
                    FilePath = item.MenuIconImage.FilePath,
                    ImageType = item.MenuIconImage.ImageType,
                    created_at = (DateTime)item.created_at
                }
            });

            return new MenuListResponseDto
            {
                MenuList = menus
            };
           
        }

        public async Task<MenuDto?> GetMenuByIdAsync(Guid menuId)
        {
            //Get data from DB  via - Domain model
            var menuItem = await menuRepository.GetByIdAsync(menuId);

            if (menuItem == null)
            {
                return null;
            }

            //Map domain models to DTO

            var menuItemDto = new MenuDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                is_active = menuItem.is_active,
                created_at = menuItem.created_at ?? DateTime.Now,
                updated_at = menuItem.updated_at,
            };

            return menuItemDto;
        }

        public async Task<MenuDto?> UpdateMenu(Guid menuId, UpdateMenuRequestDto updateMenuRequestDto)
        {

            //Get existing Menu
            var menu = await menuRepository.GetByIdAsync(menuId);
            
            if (menu == null) return null;

            //Update scalar fiels
            menu.Name = updateMenuRequestDto.Name;
            menu.Description = updateMenuRequestDto.Description;
            menu.is_active = updateMenuRequestDto.is_active;
            menu.updated_at = DateTime.UtcNow;
            //menu.url_menu_icon = updateMenuRequestDto.url_menu_icon;


            //Optional image association
            if (updateMenuRequestDto.MenuIconImageId.HasValue)
            {
                var image = await imageRepository.GetImageByIdAsync(updateMenuRequestDto.MenuIconImageId.Value);

                if (image == null) throw new ValidationException("Image us not found");

                if (image.ImageType != ImageType.MenuIcon) throw new ValidationException("Only MenuIcon images allowed");
                menu.url_menu_icon = image.FilePath;
                menu.MenuIconImageId = image.Id;
            }

            //Persist changes
            await menuRepository.UpdateMenuAsync(menuId, menu);

            //Relode menu WITH Image
            var updatedMenu = await menuRepository.GetByIdAsync(menuId).ConfigureAwait(false);

            return MenuImageAssociationHelper.ToDto(updatedMenu);

        }

        public async Task<bool> DeleteMenu(Guid menuId)
        {
            //Get Data from Data via domain Model
            var menuDomainModel = await menuRepository.DeleteMenuAsync(menuId);

            //Check if data exist
            if (menuDomainModel == null)
            {
                return false;
            }
            return true;
        }
    }
}
