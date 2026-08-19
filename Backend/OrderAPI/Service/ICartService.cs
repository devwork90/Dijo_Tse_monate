using OrderAPI.Models.Domain;
using OrderAPI.Models.DTO;

namespace OrderAPI.Service
{
    public interface ICartService
    {
        Task<CartDisplayDTO> AddCartAsync(AddCartRequestDTO addCartRequest, Guid userId);
        Task<GetCartResponseDTO?> GetCartByIdAsync(Guid userId);
        Task<CartDisplayDTO> AddCartItemAsync(AddCartItemDTO addCartItemRequest, Guid userId);
        Task<GetCartResponseDTO> PatchCartItemQuantityAsync(Guid id, PatchCartItemQuantityDTO patchCartItemQuantityDTO);

        Task<GetCartResponseDTO> DeleteCartItemAsync(Guid cartItemId, Guid userId);
    }
}
 