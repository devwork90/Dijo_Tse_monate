using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Models.DTO;
using OrderAPI.Service;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : Controller
    {
        private readonly ICartService cartService;

        public CartController(ICartService cartService) 
        {
            this.cartService = cartService;
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetCartById(Guid userId)
        {
            var cartResponse = await cartService.GetCartByIdAsync(userId);
            if (cartResponse == null)
            {
                return NotFound();
            }
            return Ok(cartResponse);
        }

        [Authorize(Roles = "Customer")]
        [HttpPost]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartItemDTO addCartItemRequest)
        {
            var userIdClaim = (Guid)HttpContext.Items["UserId"]!;
            var addedCartItem = await cartService.AddCartItemAsync(addCartItemRequest, userIdClaim);
            return CreatedAtAction(nameof(AddCartItem), new { id = addedCartItem.CartId }, addedCartItem);
        }

        [Authorize(Roles = "Customer")]
        [HttpPatch]

        public async Task<IActionResult> PatchCartItemQuantity([FromBody] PatchCartItemQuantityDTO patchCartItemQuantity)
        {
            var userIdClaim = (Guid)HttpContext.Items["UserId"]!;
            var updatedCartItem = await cartService.PatchCartItemQuantityAsync(userIdClaim, patchCartItemQuantity);
            return Ok(updatedCartItem);
        }

        [Authorize(Roles = "Customer")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem([FromQuery] Guid cartItemId)
        {
            var userId = (Guid)(Guid)HttpContext.Items["UserId"]!;
            var updatedCart = await cartService.DeleteCartItemAsync(cartItemId, userId);
            return Ok(updatedCart);
        }
    }
}   
