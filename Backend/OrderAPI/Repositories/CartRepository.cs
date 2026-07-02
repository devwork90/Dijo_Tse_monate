using OrderAPI.Data;
using OrderAPI.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace OrderAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly OrderDbContext orderDbContext;
        public CartRepository(OrderDbContext orderDbContext)
        {
            this.orderDbContext = orderDbContext;
        }
        public async Task<Cart> AddCartAsync(Cart cart)
        {
            await orderDbContext.Cart.AddAsync(cart);
            await orderDbContext.SaveChangesAsync();
            return cart;
        }

        public async Task<CartItem?> AddCartItemAsync(CartItem cartItem)
        {
            await orderDbContext.CartItem.AddAsync(cartItem);
            await orderDbContext.SaveChangesAsync();
            return cartItem;
        }

        public async Task<Cart> DeleteCartAsync(Guid UserId)
        {
            var cart = await orderDbContext.Cart.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (cart != null)
            {
                orderDbContext.Cart.Remove(cart);
                await orderDbContext.SaveChangesAsync();
            }
            return cart!;
        }

        public async Task<CartItem?> GetByCartandMenuIdAsync(Guid CartId, Guid menuId)
        {
            return await orderDbContext.CartItem.FirstOrDefaultAsync(x => x.CartId == CartId && x.MenuItemId == menuId);
        }

        public async Task<Cart?> GetCartByIdAsync(Guid UserId)
        {
            return await orderDbContext.Cart.FirstOrDefaultAsync(x => x.UserId == UserId);
        }

        public async Task<Cart?> UpdateCartTotalAmountAsync(Guid cartId)
        {
                var cart = await orderDbContext.Cart.Include(c => c.Items).FirstOrDefaultAsync(c => c.Id == cartId);
                if (cart != null)
                {
                    cart.TotalAmount = cart.Items.Sum(ci => ci.UnitPrice * ci.Quantity);
                    cart.TotalItems = cart.Items.Sum(ci => ci.Quantity);
                    await orderDbContext.SaveChangesAsync();
                }
                return cart;
        }
    }
}
