using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.API.Data;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;
using RestaurantAPI.Repositories;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly DijoDbContext dbContext;
        private readonly IMenuItemRepository menuItemRepository;

        public MenuItemController(DijoDbContext dbContext, IMenuItemRepository menuItemRepository)
        {
            this.dbContext = dbContext;
            this.menuItemRepository = menuItemRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddMenuItemRequestDto addMenuItemRequestDto)
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

            return CreatedAtAction(nameof(GetById), new { id = menuItemsDto.Id }, menuItemsDto );
        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            //Get Data from the Database through the dbContext
            var menuItem = await menuItemRepository.GetMenuItemByIdAsync(id);

            if (menuItem == null) 
            {
                return NotFound();
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
            return Ok(menuItemDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            //Get Data from the Database through the dbContext
            var availableMenuItems = await menuItemRepository.GetAllMenuItemsAsync();

            //Map Domain Models to the DTOs
            var menuItemDto = new List<MenuItemsDto>();
            foreach (var item in availableMenuItems) 
            {
                menuItemDto.Add(new MenuItemsDto
                {
                    Id=item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Price=item.Price,
                    is_Available=item.is_Available,
                    imageUrl = item.imageUrl,
                    created_at = item.created_at ?? DateTime.UtcNow,
                    updated_at = item.updated_at,
                    subMenuId = item.SubMenuId,
                    restaurantId = item.restaurantId,
                });
            }

            //Return DTOs back to client
            return Ok(menuItemDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMenuItemDto updateMenuItemDto )
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
                return NotFound();
            }

            var menuItemDto = new MenuItemsDto
            {
                Id = menuItemDomainModel.Id,
                Name = menuItemDomainModel.Name,
                Description = menuItemDomainModel.Description,
                Price = menuItemDomainModel.Price,
                imageUrl = menuItemDomainModel.imageUrl,
                restaurantId= menuItemDomainModel.restaurantId,
                subMenuId = menuItemDomainModel.SubMenuId,
                updated_at = menuItemDomainModel.updated_at,
                created_at = menuItemDomainModel.created_at,

            };

            //Return DTOs back to client
            return Ok(menuItemDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var existingMenuItem = await menuItemRepository.DeleteMenuItem(id);

            if (existingMenuItem == null)
            {
                return NotFound();

            }

            return NoContent();

        }
    }
}
