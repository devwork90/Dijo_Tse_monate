using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderAPI.Service;

namespace OrderAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService orderService;

        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> CreateOrder()
        {
            //var userIdClaim = (Guid)HttpContext.Items["UserId"]!;
            var createdOrder = await orderService.CreateOrderAsync();
            return CreatedAtAction(nameof(CreateOrder), new { id = createdOrder?.Order?.OrderId }, createdOrder);
        }

    }
        
}
