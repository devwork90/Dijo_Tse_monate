using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.API.Data;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;
using RestaurantAPI.Repositories;
using RestaurantAPI.Service;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RestaurantAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            this.menuItemService = menuItemService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddMenuItemRequestDto addMenuItemRequestDto)
        {

            if(ModelState.IsValid)
            {
               var createdMenuItem = await menuItemService.CreateMenuItem(addMenuItemRequestDto);

                return CreatedAtAction(nameof(GetById), new { id = createdMenuItem.Id }, createdMenuItem);
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
            var foundMenuItem = await menuItemService.GetMenuItemByIdAsync(id);
            if (foundMenuItem == null) { return NotFound(); }
            return Ok(foundMenuItem);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() 
        {
            //Get Data from the Database through the dbContext
            var availableMenuItems = await menuItemService.GetAllMenuItemsAsync();
            return Ok(availableMenuItems);
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateMenuItemDto updateMenuItemDto )
        {
            if(ModelState.IsValid) 
            {
               var UpdatedMenuItem = await menuItemService.UpdateMenuItem(id, updateMenuItemDto); 
                if (UpdatedMenuItem == null) { return NotFound(); }
                return Ok(UpdatedMenuItem);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete]
        [Route("{id:Guid}")]

        public async Task<IActionResult> Delete([FromRoute] Guid id)
        {
            var existingMenuItem = await menuItemService.DeleteMenuItem(id);
            if (existingMenuItem == null){return NotFound();}
            return NoContent();

        }
    }
}
