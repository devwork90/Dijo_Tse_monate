using RestaurantAPI.API.Data;
using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Service;
using Microsoft.AspNetCore.Authorization;
using System.Text.Json;

namespace RestaurantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
   
    public class MenuController : ControllerBase
    {
        
        //private readonly IMenuRepository menuRepository;
        private readonly IMenuService menuService;
        private readonly ILogger<MenuController> logger;

        public MenuController(IMenuRepository menuRepository, IMenuService menuService,
            ILogger<MenuController> logger)
        {
            this.menuService = menuService;
            this.logger = logger;
        }

        [HttpGet]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAll() 
        {
            var ListOfMenus = await menuService.GetMenusAsync(); 
            //Create exception
            return Ok(ListOfMenus);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {
            
            var foundMenu = await menuService.GetmenuByIdAsync(id);

            if (foundMenu == null) { return NotFound(); }
            return Ok(foundMenu);
        }

        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Create([FromBody] AddMenuRequestDto addMenuRequestDto) {

            var CreatedMenu = await menuService.CreateMenu(addMenuRequestDto);

            return CreatedAtAction(nameof(GetById), new { id = CreatedMenu.Id }, CreatedMenu);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMenuRequestDto updateMenuRequestDto)
        {
            if (ModelState.IsValid)
            {
                var updatedMenu = await menuService.UpdateMenu(id, updateMenuRequestDto);
                return Ok(updatedMenu);
            }
            else { return BadRequest(ModelState); }

        }

        [HttpDelete]
        [Route("{id:Guid}")]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id)
        {
            var deletedMenu = await menuService.DeleteMenu(id);
            if(!deletedMenu) { return NotFound(); } return NoContent();
        }
    }
}