using OrderAPI.Models.Domain;

namespace OrderAPI.Repositories
{
    public interface IOrderRepository
    {
        Task<Order> CreateOrderAsync(Order order);

        Task<Order?> GetOrderByUserId(Guid UserId);
    }
}
