using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using OrderAPI.Models.Domain;

namespace OrderAPI.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        public readonly OrderDbContext orderDbContext;
        public OrderRepository(OrderDbContext orderDbContext)
        {
            this.orderDbContext = orderDbContext;
        }
        public async Task<Order> CreateOrderAsync(Order order)
        {
            await orderDbContext.Order.AddAsync(order);
            await orderDbContext.SaveChangesAsync();
            return order;
        }

        public async Task<Order?> GetOrderByUserId(Guid UserId)
        {
            return await orderDbContext.Order.FirstOrDefaultAsync(o => o.UserId == UserId);
        }
    }
}
