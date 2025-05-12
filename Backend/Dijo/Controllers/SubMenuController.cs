using RestaurantAPI.API.Data;
using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.API.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Service;
using Microsoft.AspNetCore.Http.HttpResults;

namespace RestaurantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubMenuController : ControllerBase
    {
 
        private readonly ISubMenuRepository menuRepository;
        private readonly ISubMenuService subMenuService;
        public SubMenuController(ISubMenuRepository SubmenuItemRepository, ISubMenuService subMenuService)
        { 
            this.menuRepository = SubmenuItemRepository;
            this.subMenuService = subMenuService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var allSubmenus = await subMenuService.GetAllSubMenusAsync();
            return Ok(allSubmenus);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddSubMenuDto addSubMenuDto)
        {
            if(ModelState.IsValid) 
            {       
                var createdSubMenu = await subMenuService.CreateSubMenus(addSubMenuDto);
                return CreatedAtAction(nameof(GetById), new { id = createdSubMenu.Id }, createdSubMenu);
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
            var foundSubMenuItem = await subMenuService.GetSubMenusByIdAsync(id);
            if (foundSubMenuItem == null) { return BadRequest(); }
            return Ok(foundSubMenuItem);
        }

   
        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdatedSubMenuDto updatedMenuItemDto) 
        {

           if (ModelState.IsValid) 
            {
                var updatedSubMenuItem = await subMenuService.UpdateSubMenusAsync(id, updatedMenuItemDto);
                if (updatedSubMenuItem == null) { return  BadRequest(); }
                return Ok(updatedSubMenuItem);
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
            var menuItem = await subMenuService.DeleteSubMenus(id);
            if (!menuItem){ return NotFound();}return NoContent();
        }
    }
}