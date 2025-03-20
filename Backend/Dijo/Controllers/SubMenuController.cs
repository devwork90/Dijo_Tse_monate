using RestaurantAPI.API.Data;
using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubMenuController : ControllerBase
    {
 
        private readonly ISubMenuRepository menuRepository;

        public SubMenuController(ISubMenuRepository SubmenuItemRepository)
        { 
            this.menuRepository = SubmenuItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var SubmenuItems = await menuRepository.GetAllSubMenusAsync();

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
            return Ok(menuItemDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddSubMenuDto addMenuItemDto)
        {
            if(ModelState.IsValid) 
            {
                //Map domain Model to Dto
                var menuItemDomainModel = new SubMenu
                {
                    Name = addMenuItemDto.Name,
                    MenuId = addMenuItemDto.MenuId,
                    restaurantId = addMenuItemDto.restaurantId,
                    is_available = addMenuItemDto.is_available,
                    created_at = DateTime.UtcNow,

                };

                await menuRepository.CreateSubMenus(menuItemDomainModel);

                //Map Dto back to Domain Model
                var subMenuDto = new SubMenuDto
                {
                    Id = menuItemDomainModel.Id,
                    Name = addMenuItemDto.Name,
                    MenuId = addMenuItemDto.MenuId,
                    restaurantId = addMenuItemDto.restaurantId,
                    created_at = (DateTime)menuItemDomainModel.created_at,
                    is_available = addMenuItemDto.is_available,
                };

                return CreatedAtAction(nameof(GetById), new { id = subMenuDto.Id }, subMenuDto);
            }
            else 
            {
                return BadRequest(ModelState);
            }
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Data from DB by domain mode
            var menuItem = await menuRepository.GetSubMenusByIdAsync(id);

            if (menuItem == null) 
            {
                return NotFound();

            }

            //Map Domain Model to Dto
            var menuItemDto = new SubMenuDto 
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                is_available = menuItem.is_available,
                created_at  = menuItem.created_at ?? DateTime.Now,
                updated_at = menuItem.updated_at,
                MenuId = menuItem.MenuId,
                restaurantId = menuItem.restaurantId
            };

            return Ok(menuItemDto);
        }

   
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatedSubMenuDto updatedMenuItemDto) 
        {

           if (ModelState.IsValid) 
            {
                //Map Domain Model to Dto
                var menuItemDomainModel = new SubMenu();

                menuItemDomainModel.Name = updatedMenuItemDto.Name;
                menuItemDomainModel.is_available = updatedMenuItemDto.is_available;



                //Find the record to update in the DB
                menuItemDomainModel = await menuRepository.UpdateSubMenusAsync(id, menuItemDomainModel);

                if (menuItemDomainModel == null)
                {
                    return NotFound();
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

                return Ok(menuItemDto);
            }
            else 
            { 
                return BadRequest(ModelState ); 
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id) 
        {
            var menuItem = await menuRepository.DeleteSubMenus(id);

            if (menuItem == null)
            {
                return NotFound();
            }

            

        return NoContent();
        }
    }
}