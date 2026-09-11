using OrderAPI.Models.Domain;
using OrderAPI.Models.DTO;
using OrderAPI.Models.Enums;
using OrderAPI.Repositories;
using System.Reflection.Metadata.Ecma335;

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
            var cartItems = await cartItemRepository.GetCartItems();


            if (existingCart == null)
            {

                return new GetOrderResponseDTO
                {
                    Order = null
                };
            }

            var existingOrder = await orderRepository.GetOrderByUserId(Guid.Parse(userId));

            if (existingOrder != null) 
            {
               if (existingOrder.Status == OrderStatus.PendingPayment)
                {
                    await orderRepository.DeleteOrder();
                    existingOrder = null;
                }
                else
                {
                    return new GetOrderResponseDTO
                    {
                        Order = null
                    };
                }
            }
            var order = await CreateOrderFromCartAsync(existingCart);

            return new GetOrderResponseDTO
            {
                Order = MapToOrderResponseDto(order, existingCart, cartItems)
            };
            
        }

        private OrderResponseDto MapToOrderResponseDto(Order order, Cart cart, IEnumerable<CartItem> cartItems)
        {
            
            return new OrderResponseDto
            {
                OrderId = order.Id,
                RestaurantId = order.RestaurantId,
                TotalAmount = order.TotalAmount,
                Status = order.Status,
                PaymentStatus = order.PaymentStatus,
                TotalItems = order.TotalItems,

                OrderItems = order.OrderItems.Select(cartItems => new OrderItemResponseDto
                {
                    Id = cartItems.Id,
                    MenuItemId = cartItems.MenuItemId,
                    ItemName = cartItems.ItemName,
                    Quantity = cartItems.Quantity,
                    UnitPrice = cartItems.UnitPrice,
                    TotalPrice = cartItems.TotalPrice,
                }).ToList()
            };
        }

        public async Task<Order> CreateOrderFromCartAsync(Cart cart)
        {
            var order = new Order
            {
                UserId = cart.UserId,
                RestaurantId = cart.RestaurantId,
                TotalAmount = cart.TotalAmount,
                Status = OrderStatus.PendingPayment,
                PaymentStatus = PaymentStatus.Pending,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow,
                TotalItems = cart.TotalItems,
            };

            await orderRepository.CreateOrderAsync(order);

            foreach (var item in cart.Items)
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

            return order;
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
