using Dijo.API.Data;
using Dijo.API.Models.Domain;
using Dijo.API.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dijo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenutItemController : ControllerBase
    {
        private readonly DijoDbContext dbContext;

        public MenutItemController(DijoDbContext dbContext)
        { 
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var menuItems = dbContext.MenuItem.ToList();

            //Map Domain Models to DTO
            var menuItemDto = new List<MenuItemDto>();

            foreach (var menuItem in menuItems)
            {
                menuItemDto.Add(new MenuItemDto
                {
                    Id = menuItem.Id,
                    Name = menuItem.Name,
                    Description = menuItem.Description,
                    Image_url = menuItem.Image_url,
                    Price = menuItem.Price,
                    is_available = menuItem.is_available,
                    updated_at = menuItem.updated_at,
                    created_at = menuItem.created_at ?? DateTime.Now,
                    MenuId = menuItem.MenuId,
                });
            }
            return Ok(menuItemDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddMenuItemDto addMenuItemDto)
        {
            //Map domain Model to Dto
            var menuItemDomainModel = new MenuItem
            {
                Name = addMenuItemDto.Name,
                MenuId = addMenuItemDto.MenuId,
                Image_url = addMenuItemDto.Image_url,
                Description = addMenuItemDto.Description,
                Price = addMenuItemDto.Price,
                is_available = addMenuItemDto.is_available,
                created_at = DateTime.UtcNow,

            };

            dbContext.MenuItem.Add(menuItemDomainModel);
            dbContext.SaveChanges();

            //Map Dto back to Domain Model
            var menuItemDto = new MenuItemDto
            {
                Id = menuItemDomainModel.Id,
                Name = addMenuItemDto.Name,
                MenuId = addMenuItemDto.MenuId,
                Description = addMenuItemDto.Description,
                Price = addMenuItemDto.Price,
                Image_url = addMenuItemDto.Image_url,
                created_at = (DateTime)menuItemDomainModel.created_at,
                is_available = addMenuItemDto.is_available,
            };

            return CreatedAtAction(nameof(GetById), new { id = menuItemDto.Id }, menuItemDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            //Get Data from DB by domain mode
            var menuItem = dbContext.MenuItem.FirstOrDefault(x => x.Id == id);

            if (menuItem == null) 
            {
                return NotFound();

            }

            //Map Domain Model to Dto
            var menuItemDto = new MenuItemDto 
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                Image_url = menuItem.Image_url,
                is_available = menuItem.is_available,
                created_at  = menuItem.created_at ?? DateTime.Now,
                updated_at = menuItem.updated_at,
                MenuId = menuItem.MenuId
            };

            return Ok(menuItemDto);
        }

   
        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdatedMenuItemDto updatedMenuItemDto) 
        {
            //Find the record to update in the DB
            var menuItemDomainModel = dbContext.MenuItem.FirstOrDefault(x => x.Id == id);

            if (menuItemDomainModel == null)
            { 
                return NotFound();
            }

            //Map Domain Model to Dto
            menuItemDomainModel.Name = updatedMenuItemDto.Name;
            menuItemDomainModel.Description = updatedMenuItemDto.Description;
            menuItemDomainModel.is_available = updatedMenuItemDto.is_available;
            menuItemDomainModel.Price = updatedMenuItemDto.Price;
            menuItemDomainModel.Image_url = updatedMenuItemDto.Image_url;
            menuItemDomainModel.updated_at = DateTime.UtcNow;
    

            dbContext.SaveChanges();

            //Map DTO back to Domain Models

            var menuItemDto = new MenuItemDto
            {
                Id = menuItemDomainModel.Id,
                Name = menuItemDomainModel.Name,
                Description = menuItemDomainModel.Description,
                Image_url = menuItemDomainModel.Image_url,
                Price = menuItemDomainModel.Price,
                updated_at = menuItemDomainModel.updated_at,
                created_at = (DateTime)menuItemDomainModel.created_at,
                MenuId = menuItemDomainModel.MenuId,
            };

            return Ok(menuItemDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult Delete([FromRoute] Guid id) 
        {
            var menuItem = dbContext.MenuItem.First(x => x.Id == id);

            if (menuItem == null)
            {
                return NotFound();
            }

            dbContext.Remove(menuItem);
            dbContext.SaveChanges();

        return NoContent();
        }
    }
}