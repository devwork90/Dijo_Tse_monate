using OrderAPI.Models.Domain;

namespace OrderAPI.Repositories
{
    public interface ICartItemRepository
    {
        Task<CartItem> AddCartItemAsync(CartItem cartItem);
        Task<Cart> UpdateCartItemAsync(Guid CartId);
        Task<IEnumerable<CartItem>> GetCartItems();
        Task<CartItem> GetCartItemByIdAsync(Guid id);

        Task<CartItem> DeleteCartItemAsync(Guid catItemId);
        //Task<IEnumerable<CartItem>> GetCartItemsByCartIdAsync(Guid cartId);
        //Task<CartItem> UpdateCartItemAsync(CartItem cartItem);
        //Task DeleteCartItemAsync(Guid id);
    }
}
