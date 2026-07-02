using OrderAPI.Models.Domain;
using OrderAPI.Models.DTO;

namespace OrderAPI.Service
{
    public interface ICartService
    {
        Task<CartDisplayDTO> AddCartAsync(AddCartRequestDTO addCartRequest);
        Task<GetCartResponseDTO?> GetCartByIdAsync(Guid UserId);
        Task<CartDisplayDTO> AddCartItemAsync(AddCartItemDTO addCartItemRequest);
        Task<GetCartResponseDTO> PatchCartItemQuantityAsync(Guid id, PatchCartItemQuantityDTO patchCartItemQuantityDTO);

        Task<GetCartResponseDTO> DeleteCartItemAsync(Guid cartItemId, Guid userId);
    }
}
 