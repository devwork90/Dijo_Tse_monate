using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.API.Repositories;
using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.Service
{
    public class SubMenuService: ISubMenuService
    {
        readonly ISubMenuRepository subMenuRepository;
        public SubMenuService(ISubMenuRepository subMenuRepository) 
        { 
            this.subMenuRepository = subMenuRepository;
        }

        public async Task<SubMenuDto> CreateSubMenus(AddSubMenuDto addSubMenuDto)
        {
            //Map domain Model to Dto
            var menuItemDomainModel = new SubMenu
            {
                Name = addSubMenuDto.Name,
                MenuId = addSubMenuDto.MenuId,
                restaurantId = addSubMenuDto.restaurantId,
                is_available = addSubMenuDto.is_available,
                created_at = DateTime.UtcNow,

            };

            await subMenuRepository.CreateSubMenus(menuItemDomainModel);

            //Map Dto back to Domain Model
            var subMenuDto = new SubMenuDto
            {
                Id = menuItemDomainModel.Id,
                Name = addSubMenuDto.Name,
                MenuId = addSubMenuDto.MenuId,
                restaurantId = addSubMenuDto.restaurantId,
                created_at = (DateTime)menuItemDomainModel.created_at,
                is_available = addSubMenuDto.is_available,
            };

            return subMenuDto;
        }

        public async Task<bool> DeleteSubMenus(Guid id)
        {
            var menuItem = await subMenuRepository.DeleteSubMenus(id);

            if (menuItem == null)
            {
                return false;
            }

            return true;
        }

        public async Task<List<SubMenuDto>> GetAllSubMenusAsync()
        {
            var SubmenuItems = await subMenuRepository.GetAllSubMenusAsync();

            //Map Domain Models to DTO
            var menuItemDto = new List<SubMenuDto>();

            foreach (var SubmenuItem in SubmenuItems)
            {
                menuItemDto.Add(new SubMenuDto
                {
                    Id = SubmenuItem.Id,
                    Name = SubmenuItem.Name,
                    is_available = SubmenuItem.is_available,
                    updated_at = SubmenuItem.updated_at,
                    created_at = SubmenuItem.created_at ?? DateTime.Now,
                    MenuId = SubmenuItem.MenuId,
                    restaurantId = SubmenuItem.restaurantId,
                });
            }
            return menuItemDto;
        }

        public async Task<SubMenuDto?> GetSubMenusByIdAsync(Guid id)
        {
            //Get Data from DB by domain mode
            var menuItem = await subMenuRepository.GetSubMenusByIdAsync(id);

            if (menuItem == null)
            {
                return null;

            }

            //Map Domain Model to Dto
            var menuItemDto = new SubMenuDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                is_available = menuItem.is_available,
                created_at = menuItem.created_at ?? DateTime.Now,
                updated_at = menuItem.updated_at,
                MenuId = menuItem.MenuId,
                restaurantId = menuItem.restaurantId
            };

            return menuItemDto;
        }

        public async Task<SubMenuDto?> UpdateSubMenusAsync(Guid id, UpdatedSubMenuDto updatedSubMenuDto)
        {
                //Map Domain Model to Dto
                var menuItemDomainModel = new SubMenu();

                menuItemDomainModel.Name = updatedSubMenuDto.Name;
                menuItemDomainModel.is_available = updatedSubMenuDto.is_available;



                //Find the record to update in the DB
                menuItemDomainModel = await subMenuRepository.UpdateSubMenusAsync(id, menuItemDomainModel);

                if (menuItemDomainModel == null)
                {
                    return null;
                }

                //Map DTO back to Domain Models

                var menuItemDto = new SubMenuDto
                {
                    Id = menuItemDomainModel.Id,
                    Name = menuItemDomainModel.Name,
                    created_at = (DateTime)menuItemDomainModel.created_at,
                    updated_at = menuItemDomainModel.updated_at,
                    MenuId = menuItemDomainModel.MenuId,
                    restaurantId = menuItemDomainModel.restaurantId,

                };

                return menuItemDto;
            }
       }
}
