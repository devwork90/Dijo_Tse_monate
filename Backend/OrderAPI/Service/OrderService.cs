using OrderAPI.Models.Domain;
using OrderAPI.Models.DTO;
using OrderAPI.Models.Enums;
using OrderAPI.Repositories;

namespace OrderAPI.Service
{
    public class OrderService: IOrderService
    {
        private readonly IOrderRepository orderRepository;
        private readonly ICartRepository cartRepository;
        private readonly ICartItemRepository cartItemRepository;
        private readonly IOrderItemRepository orderItemRepository;
        public OrderService(IOrderRepository orderRepository, ICartRepository cartRepository, ICartItemRepository cartItemRepository, IOrderItemRepository orderItemRepository)
        {
            this.orderRepository = orderRepository;
            this.cartRepository = cartRepository;
            this.cartItemRepository = cartItemRepository;
            this.orderItemRepository = orderItemRepository;
        }

        public async Task<GetOrderResponseDTO> CreateOrderAsync()
        {
            var userId = "B218DDD6-3861-4641-9C03-1F93A618BB87";
            var existingCart = await cartRepository.GetCartByIdAsync(Guid.Parse(userId));
            //var existingCartItem = await cartItemRepository.GetCartItemByIdAsync(existingCart.Items.First().Id);
            var cartItems = await cartItemRepository.GetCartItems();

            if (!existingCart.Items.Any())
            {
                throw new Exception("Cannot create order from empty cart");
            }

            var order = new Order
            {
                UserId = existingCart.UserId,
                RestaurantId = existingCart.RestaurantId,
                TotalAmount = existingCart.TotalAmount,
                Status = OrderStatus.PendingPayment,
                PaymentStatus = PaymentStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TotalItems = existingCart.TotalItems,
            };

            await orderRepository.CreateOrderAsync(order);

            foreach (var item in existingCart.Items)
            {
                var orderItem = new OrderItem
                {
                    OrderId = order.Id,
                    MenuItemId = item.MenuItemId,
                    ItemName = item.ItemName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice,
                    CreatedAt = DateTime.UtcNow
                };
                await orderItemRepository.AddOrderItem(orderItem);
            }
            

            var orderResponseDto = new OrderResponseDto
            {
                OrderId = order.Id,
                RestaurantId = order.RestaurantId,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                PaymentStatus = order.PaymentStatus,
                TotalItems = order.TotalItems,

                OrderItems = cartItems.Select(item => new OrderItemResponseDto
                {
                    Id = item.Id,
                    ItemName = item.ItemName,
                    MenuItemId = item.MenuItemId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice,
                }).ToList()
            };

            return new GetOrderResponseDTO
            {
                Order = orderResponseDto
            };
        }

        public async Task<GetOrderResponseDTO> GetOrderByIdAsync()
        {
            var userId = "B218DDD6-3861-4641-9C03-1F93A618BB87";
            var existingOrder = await orderRepository.GetOrderByUserId(Guid.Parse(userId));
            var existingOrderItems = await orderItemRepository.GetAllOrderItems();

            if (existingOrder == null)
            {
                throw new Exception("Order not found");
            }

            var orderResponseDto = new OrderResponseDto
            {
                OrderId = existingOrder.Id,
                RestaurantId = existingOrder.RestaurantId,
                TotalAmount = existingOrder.TotalAmount,
                Status = existingOrder.Status,
                PaymentStatus = existingOrder.PaymentStatus,
                TotalItems = existingOrder.TotalItems,
                OrderItems = existingOrderItems.Select(item => new OrderItemResponseDto
                {
                    Id = item.Id,
                    MenuItemId = item.MenuItemId,
                    ItemName = item.ItemName,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    TotalPrice = item.TotalPrice,
                }).ToList()
            }; return new GetOrderResponseDTO
            {
                Order = orderResponseDto
            };
        }
    }
}
