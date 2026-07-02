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

        //[HttpPost]
        //public async Task<IActionResult> CreateCart([FromBody] AddCartRequestDTO addCartRequest)
        //{
        //    var createdCart = await cartService.AddCartAsync(addCartRequest);
        //    return CreatedAtAction(nameof(CreateCart), new { id = createdCart.CartId }, createdCart);
        //}

        [HttpPost]
        public async Task<IActionResult> AddCartItem([FromBody] AddCartItemDTO addCartItemRequest)
        {
            var addedCartItem = await cartService.AddCartItemAsync(addCartItemRequest);
            return CreatedAtAction(nameof(AddCartItem), new { id = addedCartItem.CartId }, addedCartItem);
        }

        [HttpPatch]
        [Route("{id:Guid}")]
        public async Task<IActionResult> PatchCartItemQuantity([FromRoute] Guid id, [FromBody] PatchCartItemQuantityDTO patchCartItemQuantity)
        {
            var updatedCartItem = await cartService.PatchCartItemQuantityAsync(id, patchCartItemQuantity);
            return Ok(updatedCartItem);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCartItem([FromQuery] Guid cartItemId, [FromQuery] Guid userId)
        {
            var updatedCart = await cartService.DeleteCartItemAsync(cartItemId, userId);
            return Ok(updatedCart);
        }
    }
}   
