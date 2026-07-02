using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using OrderAPI.Models.Domain;

namespace OrderAPI.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly OrderDbContext orderDbContext;
        private readonly ICartRepository CartRepository;

        public CartItemRepository(OrderDbContext orderDbContext, ICartRepository CartRepository)
        {
            this.orderDbContext = orderDbContext;
            this.CartRepository = CartRepository;
        }

        public async Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            await orderDbContext.CartItem.AddAsync(cartItem);
            await orderDbContext.SaveChangesAsync();
            return cartItem;
        }

        public async Task<CartItem> GetCartItemByIdAsync(Guid id)
        {
            return await orderDbContext.CartItem.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<IEnumerable<CartItem?>> GetCartItems()
        {
            return orderDbContext.CartItem.ToListAsync().ContinueWith(task => task.Result.AsEnumerable());

        }

        public async Task<Cart> UpdateCartItemAsync(Guid id)
        {
            var cartItem = await orderDbContext.CartItem.FirstOrDefaultAsync(x => x.Id == id);
            if (cartItem == null)
            {
                throw new InvalidOperationException("CartItem not found");
            }
            else if(cartItem.Quantity < 1)
            {
               await DeleteCartItemAsync(cartItem.Id);

            }

            await orderDbContext.SaveChangesAsync();
            var userId = cartItem.Cart.UserId;
            var updatedCartItem = await orderDbContext.Cart.FirstOrDefaultAsync(c => c.UserId == userId);
            return updatedCartItem;
        }

        public async Task<CartItem> DeleteCartItemAsync(Guid catItemId)
        {
            var existingCartItem = await orderDbContext.CartItem.FirstOrDefaultAsync(x => x.Id == catItemId);
            if (existingCartItem != null)
            {
                orderDbContext.CartItem.Remove(existingCartItem);
                await orderDbContext.SaveChangesAsync();
            }
            return existingCartItem!;
        }
    }
}
