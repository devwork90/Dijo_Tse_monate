
using Microsoft.AspNetCore.Http.HttpResults;
using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.API.Repositories;
using RestaurantAPI.Models.Domain;

namespace RestaurantAPI.Service
{
    public class MenuService : IMenuService
    {
        private readonly IMenuRepository menuRepository;

        public MenuService(IMenuRepository menuRepository)
        {
            this.menuRepository = menuRepository;
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

            };

            return menuItemDto;
        }

        public async Task<IEnumerable<MenuDto>> GetMenusAsync()
        {
            //Get Data from DB by Domain Model
            var menuItems = await menuRepository.GetAllMenusAsync();

            //Map Model to DTO
            var menuDto = new List<MenuDto>();
            foreach (var item in menuItems)
            {
#pragma warning disable CS8601 // Possible null reference assignment.
                menuDto.Add(new MenuDto

                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    is_active = item.is_active,
                    created_at = item.created_at ?? DateTime.UtcNow,
                    updated_at = item.updated_at,

                });
#pragma warning restore CS8601 // Possible null reference assignment.
            }

            return menuDto;
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

            //Get data from DB  via - Domain model
            var menuDomainModel = new Menu()
            {
                Name = updateMenuRequestDto.Name,
                Description = updateMenuRequestDto.Description,
                is_active = updateMenuRequestDto.is_active,
            };

            menuDomainModel = await menuRepository.UpdateMenuAsync(menuId, menuDomainModel);

            if (menuDomainModel == null)
            {
                return null;
            }

            //Map Dto Model to Domain Model
            menuDomainModel.Name = updateMenuRequestDto.Name;
            menuDomainModel.Description = updateMenuRequestDto.Description;
            menuDomainModel.is_active = updateMenuRequestDto.is_active;

            //Map Domain model back to DTO

            var menuDto = new MenuDto
            {
                Id = menuDomainModel.Id,
                Name = menuDomainModel.Name,
                Description = menuDomainModel.Description,
                is_active = menuDomainModel.is_active,
                updated_at = menuDomainModel.updated_at,
                created_at = (DateTime)menuDomainModel.created_at,

            };

            return menuDto;
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
