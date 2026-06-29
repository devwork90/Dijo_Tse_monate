using OrderAPI.Models.Domain;
using OrderAPI.Models.DTO;

namespace OrderAPI.Service
{
    public interface ICartService
    {
        Task<CartDisplayDTO> AddCartAsync(AddCartRequestDTO addCartRequest);
        Task<CartDisplayDTO?> GetCartByIdAsync(Guid UserId);
        Task<CartDisplayDTO> AddCartItemAsync(AddCartItemDTO addCartItemRequest);
    }
}
