using OrderAPI.Models.Domain;

namespace OrderAPI.Repositories
{
    public interface IOrderItemRepository
    {
        Task<OrderItem> AddOrderItem(OrderItem orderItem);
    }
}
