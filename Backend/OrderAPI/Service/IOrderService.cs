using OrderAPI.Models.DTO;

namespace OrderAPI.Service
{
    public interface IOrderService
    {
        Task<GetOrderResponseDTO> CreateOrderAsync();

        Task<GetOrderResponseDTO> GetOrderByIdAsync();
    }
}
