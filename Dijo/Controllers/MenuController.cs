using Dijo.API.Data;
using Dijo.API.Models.Domain;
using Dijo.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Dijo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly DijoDbContext dbContext;

        public MenuController(DijoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetAll() 
        {
            //Get Data from DB by Domain Model
            var menuItems = dbContext.Menu.ToList();

            //Map Model to DTO
            var menuDto = new List<MenuDto>();
            foreach (var item in menuItems)
            {
                menuDto.Add(new MenuDto

                { 
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    is_active = item.is_active,
                    created_at = item.created_at ?? DateTime.UtcNow,
                    updated_at = item.updated_at,
                    restaurantId = item.restaurantId,

                });
            }

            return Ok(menuDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public IActionResult GetById([FromRoute] Guid id) {

            //Get data from DB  via - Domain model
            var menuItem = dbContext.Menu.FirstOrDefault(x => x.Id == id);

            if (menuItem == null)
            {
                return NotFound();
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
                restaurantId = menuItem.restaurantId,
            };
            return Ok(menuItemDto);
        }

        [HttpPost]
        public IActionResult Create([FromBody] AddMenuRequestDto addMenuRequestDto) {

            //Map Dto to Domain Model

            var menuDomainModel = new Menu 
            {
                Name = addMenuRequestDto.Name,
                Description = addMenuRequestDto.Description,
                is_active = addMenuRequestDto.is_active,
                restaurantId = addMenuRequestDto.restaurantId,
                created_at = DateTime.UtcNow,
            };

            //Use domain model to create a new menu item in the DB
            dbContext.Menu.Add(menuDomainModel);
            dbContext.SaveChanges();

            //Map Domain models back to DTO's

            var menuDto = new MenuDto
            {
                Id = menuDomainModel.Id,
                Name = menuDomainModel.Name,
                Description = menuDomainModel.Description,
                is_active = menuDomainModel.is_active,
                created_at = (DateTime)menuDomainModel.created_at,
                restaurantId = menuDomainModel.restaurantId,
            };

            return CreatedAtAction(nameof(GetById), new { id = menuDto.Id }, menuDto);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] UpdateMenuRequestDto updateMenuRequestDto)
        {
            //Get data from DB  via - Domain model
            var menuDomainModel = dbContext.Menu.FirstOrDefault(x => x.Id == id);

            if (menuDomainModel == null) 
            {
                return NotFound();
            }

            //Map Dto Model to Domain Model
            menuDomainModel.Name = updateMenuRequestDto.Name;
            menuDomainModel.Description = updateMenuRequestDto.Description;
            menuDomainModel.is_active = updateMenuRequestDto.is_active;
            menuDomainModel.updated_at = DateTime.UtcNow;

            dbContext.SaveChanges();

            //Map Domain model back to DTO

            var menuDto = new MenuDto
            { 
               Name= menuDomainModel.Name,
               Description= menuDomainModel.Description,
               is_active=menuDomainModel.is_active,
               updated_at = DateTime.UtcNow
            };

            return Ok(menuDto);
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public IActionResult DeleteItem([FromRoute] Guid id)
        {
            //Get Data from Data via domain Model
            var menuDomainModel = dbContext.Menu.FirstOrDefault(y => y.Id == id);

            //Check if data exist
            if (menuDomainModel == null)
            {
                return NotFound();
            }
            dbContext.Remove(menuDomainModel);
            dbContext.SaveChanges();

            return NoContent();
        }
    }
}