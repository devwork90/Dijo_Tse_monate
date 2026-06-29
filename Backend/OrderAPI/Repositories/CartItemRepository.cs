using Microsoft.EntityFrameworkCore;
using OrderAPI.Data;
using OrderAPI.Models.Domain;

namespace OrderAPI.Repositories
{
    public class CartItemRepository : ICartItemRepository
    {
        private readonly OrderDbContext orderDbContext;

        public CartItemRepository(OrderDbContext orderDbContext)
        {
            this.orderDbContext = orderDbContext;
        }

        public Task<CartItem> AddCartItemAsync(CartItem cartItem)
        {
            throw new NotImplementedException();
        }

        public async Task<CartItem> UpdateCartItemAsync(Guid id)
        {
            var cartItem = await orderDbContext.CartItem.FirstOrDefaultAsync(x => x.Id == id);
            if (cartItem == null)
            {
                throw new InvalidOperationException("CartItem not found");
            }
            else
            {
               
                await orderDbContext.SaveChangesAsync();
            }
            return cartItem;
        }
    }
}
