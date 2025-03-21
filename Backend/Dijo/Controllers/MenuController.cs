using RestaurantAPI.API.Data;
using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        
        private readonly IMenuRepository menuRepository;

        public MenuController(IMenuRepository menuRepository)
        {
            this.menuRepository = menuRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
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

            return Ok(menuDto);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {

            //Get data from DB  via - Domain model
            var menuItem = await menuRepository.GetByIdAsync(id);

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

            };
            return Ok(menuItemDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddMenuRequestDto addMenuRequestDto) {

            if (ModelState.IsValid)
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

                return CreatedAtAction(nameof(GetById), new { id = menuDto.Id }, menuDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMenuRequestDto updateMenuRequestDto)
        {
            if (ModelState.IsValid)
            {

                //Get data from DB  via - Domain model
                var menuDomainModel = new Menu()
                {
                    Name = updateMenuRequestDto.Name,
                    Description = updateMenuRequestDto.Description,
                    is_active = updateMenuRequestDto.is_active,
                };

                menuDomainModel = await menuRepository.UpdateMenuAsync(id, menuDomainModel);

                if (menuDomainModel == null)
                {
                    return NotFound();
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

                return Ok(menuDto);
            }
            else
            {
                return BadRequest(ModelState);
            }
            
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            if (ModelState.IsValid)
            {
                //Get Data from Data via domain Model
                var menuDomainModel = await menuRepository.DeleteMenuAsync(id);

                //Check if data exist
                if (menuDomainModel == null)
                {
                    return NotFound();
                }

                return NoContent();
            }
            else
            { 
                return BadRequest(ModelState);
            }
        }
    }
}