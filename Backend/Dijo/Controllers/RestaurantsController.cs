using RestaurantAPI.API.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using RestaurantAPI.Service;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace RestaurantAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RestaurantsController : ControllerBase
    {
        
        private readonly IRestaurantService restaurantService;

        public RestaurantsController(IRestaurantService restaurantService)
        {
            this.restaurantService = restaurantService;
        }

        //Get all restaurants 
        [HttpGet]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> GetAll([FromQuery] string? menuName)
        {
            var restaurantsList = await restaurantService.GetAllRestaurantsAsync(menuName);
            return Ok(restaurantsList);
        }

        [HttpGet]
        [Route("{id:Guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id) {

            var foundRestaurant = await restaurantService.GetRestaurantbyIdAsync(id);
            if (foundRestaurant == null) { return NotFound(); }
            return Ok(foundRestaurant);
        }

        [HttpGet("menu-items")]
        [Authorize(Roles = "Admin, Customer")]
        public async Task<IActionResult> GetMenuItems(Guid restaurantId)
        {
            var roles = HttpContext.User.Claims
                .Where(c => c.Type == ClaimTypes.Role)
                .Select(c => c.Value)
                .ToList();

            if (roles.Contains("Admin") || roles.Contains("Employee"))
            {
                if (!HttpContext.Items.TryGetValue("RestaurantId", out var restaurantIdObj))
                    return Forbid();

                var scopedRestaurantId = (Guid)restaurantIdObj;

                //Prevent Admin Users from accessing another restaurant menuItem
                if(scopedRestaurantId != restaurantId)
                {
                    return Forbid();
                }
            }
            var items = await restaurantService.GetMenuItemsByRestaurantAsync(restaurantId);
            return Ok(items);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AddRestaurantRequestDto addRestaurantRequestDto)
        {
           if(ModelState.IsValid) 
            {
                var cratedRestaurant = await restaurantService.CreateRestaurantAsync(addRestaurantRequestDto);
                return CreatedAtAction(nameof(GetById), new { id = cratedRestaurant.Id }, cratedRestaurant);
            }
            else { return BadRequest(ModelState); }
        }

        [HttpDelete]
        [Route("{id:Guid}")]
        public async Task<IActionResult> DeleteItem([FromRoute] Guid id) 
        {
            var restaurant = await restaurantService.DeleteRestaurantAsync(id);
            if (!restaurant){ return NotFound();}return NoContent();
        }

        [HttpPut]
        [Route("{id:Guid}")]
        public async Task<IActionResult> Update([FromRoute] Guid id,[FromBody] UpdateRestaurantRequestDto updateRestaurantRequestDto)
        {
           if(ModelState.IsValid) 
            {
                var updatedRestaurant = await restaurantService.UpdateRestaurantAsync(id, updateRestaurantRequestDto);
                return Ok(updatedRestaurant);
            }
            else { return( BadRequest(ModelState)); }
        }
    }
}