using OrderAPI.Models.Domain;

namespace OrderAPI.Repositories
{
    public interface ICartRepository
    {
        Task<Cart> AddCartAsync(Cart cart);
        Task<Cart?> GetCartByIdAsync(Guid UserId);
        Task<CartItem?> GetByCartandMenuIdAsync(Guid CartId, Guid menuId);
        Task<CartItem?> AddCartItemAsync(CartItem cartItem);
        Task<Cart> UpdateCartTotalAmountAsync(Guid cartId);



    }
}
