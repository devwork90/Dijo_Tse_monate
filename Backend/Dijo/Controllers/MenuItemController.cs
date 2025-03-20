using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.API.Data;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly DijoDbContext dbContext;

        public MenuItemController(DijoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddMenuItemRequestDto addMenuItemRequestDto)
        {

            //DTO to Domain Model
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
            await dbContext.MenuItems.AddAsync(menuItemModel);
            await dbContext.SaveChangesAsync();

            //Map Domain Model back to DTO
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
            var menuItem = await dbContext.MenuItems.FirstOrDefaultAsync(x => x.Id == id);

            if (menuItem == null) 
            {
                return NotFound();
            }

            //Map domain model to Dto

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

            return Ok(menuItemDto);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            //Get Data from the Database
            var mennuItems = await dbContext.MenuItems.ToListAsync();

            //Map menuItems model to the Dto
            var menuItemDto = new List<MenuItemsDto>();
            foreach (var item in mennuItems) 
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

            return Ok(menuItemDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMenuItemDto updateMenuItemDto )
        {

            var existingMenuItem = await dbContext.MenuItems.FirstOrDefaultAsync(x => x.Id == id);

            if (existingMenuItem == null)
            {
                return NotFound();
            }

            //Map Dto to Domain Model

            existingMenuItem.Name = updateMenuItemDto.Name;
            existingMenuItem.Description = updateMenuItemDto.Description;
            existingMenuItem.Price = updateMenuItemDto.Price;
            existingMenuItem.imageUrl = updateMenuItemDto.imageUrl;
            existingMenuItem.updated_at = DateTime.UtcNow;
              
            
            await dbContext.SaveChangesAsync();

            var menuItemDto = new MenuItemsDto
            {
                Id = existingMenuItem.Id,
                Name = existingMenuItem.Name,
                Description = existingMenuItem.Description,
                Price = existingMenuItem.Price,
                imageUrl = existingMenuItem.imageUrl,
                restaurantId=existingMenuItem.restaurantId,
                subMenuId = existingMenuItem.SubMenuId,
                updated_at = existingMenuItem.updated_at,
                created_at =existingMenuItem.created_at,


            };

            return Ok(menuItemDto);

        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var existingMenuItem = await dbContext.MenuItems.FirstOrDefaultAsync(x => x.Id == id);

            if (existingMenuItem == null)
            {
                return NotFound();

            }

            dbContext.Remove(existingMenuItem);
            await dbContext.SaveChangesAsync();

            return NoContent();

        }
    }
}
