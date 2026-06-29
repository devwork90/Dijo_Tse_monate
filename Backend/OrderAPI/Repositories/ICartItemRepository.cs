using OrderAPI.Models.Domain;

namespace OrderAPI.Repositories
{
    public interface ICartItemRepository
    {
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task<CartItem> UpdateCartItemAsync(Guid CartId);
        //Task<CartItem> GetCartItemByIdAsync(Guid id);
        //Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(Guid cartId);
        //Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
        //Task DeleteCartItemAsync(Guid id);
    }
}
