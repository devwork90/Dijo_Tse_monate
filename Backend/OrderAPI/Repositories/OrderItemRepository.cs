using OrderAPI.Data;
using OrderAPI.Models.Domain;

namespace OrderAPI.Repositories
{
    public class OrderItemRepository : IOrderItemRepository
    {
        readonly OrderDbContext orderDbContext;
        public OrderItemRepository(OrderDbContext orderDbContext)
        {
            this.orderDbContext = orderDbContext;
        }
        public async Task<OrderItem> AddOrderItem(OrderItem orderItem)
        {
            await orderDbContext.OrderItem.AddAsync(orderItem);
            await orderDbContext.SaveChangesAsync();
            return orderItem;
        }
    }
}
