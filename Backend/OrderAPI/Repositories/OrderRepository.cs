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
    }
}
