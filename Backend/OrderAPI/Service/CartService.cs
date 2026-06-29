using Microsoft.AspNetCore.Http.HttpResults;
using OrderAPI.Models.Domain;
using OrderAPI.Models.DTO;
using OrderAPI.Models.Enums;
using OrderAPI.Repositories;

namespace OrderAPI.Service
{
    public class CartService: ICartService
    {
        private readonly ICartRepository cartRepository;
        private readonly ICartItemRepository cartItemRepository;
        private readonly IRestaurantService restaurantService;

        public CartService(ICartRepository cartRepository, IRestaurantService restaurantService, ICartItemRepository cartItemRepository)
        {
            this.cartRepository = cartRepository;
            this.restaurantService = restaurantService;
            this.cartItemRepository = cartItemRepository;
        }

        public async Task<CartDisplayDTO> AddCartAsync(AddCartRequestDTO addCartRequest)
        {
            var cart = new Cart
            {
                UserId = addCartRequest.UserId,
                RestaurantId = addCartRequest.RestaurantId,
                Status = CartStatus.Active,
                CreatedAt = DateTime.UtcNow
            };
            var savedCart = await cartRepository.AddCartAsync(cart);

            var cartResponseDto = new CartDisplayDTO
            {
                CartId = savedCart.Id,
                RestaurantId = savedCart.RestaurantId,
                CreatedAt = savedCart.CreatedAt,
                TotalAmount = savedCart.TotalAmount
            };
            return cartResponseDto;
        }

        public async Task<CartDisplayDTO> AddCartItemAsync(AddCartItemDTO addCartItemRequest)
        {
            var activeCart = await cartRepository.GetCartByIdAsync(addCartItemRequest.UserId);
            var menuItem = await restaurantService.GetMenuItemAsync(addCartItemRequest.MenuItemId);
            var existingCartItem = await cartRepository.GetByCartandMenuIdAsync(activeCart?.Id ?? Guid.Empty, addCartItemRequest.MenuItemId);
            
            // Check for active existing cart
            if (activeCart == null)
            {
                activeCart ??= new Cart
                {
                    UserId = addCartItemRequest.UserId,
                    RestaurantId = addCartItemRequest.RestaurantId,
                    Status = CartStatus.Active,
                    CreatedAt = DateTime.UtcNow
                };

                await cartRepository.AddCartAsync(activeCart);
            }

            if (menuItem == null)
            {
                throw new Exception("Menu item not found.");
            }

            if (existingCartItem == null)
            {
                var cartItem = new CartItem
                {
                    CartId = activeCart.Id,
                    MenuItemId = addCartItemRequest.MenuItemId,
                    Quantity = addCartItemRequest.Quantity,
                    UnitPrice = menuItem.Price,
                    ItemName = menuItem.Name
                };
                await cartRepository.AddCartItemAsync(cartItem);
                await cartRepository.UpdateCartTotalAmountAsync(activeCart.Id);

            }
            else
            {
                existingCartItem.Quantity += addCartItemRequest.Quantity;
                activeCart.TotalAmount = existingCartItem.Quantity * existingCartItem.UnitPrice;
                await cartRepository.UpdateCartTotalAmountAsync(activeCart.Id);
                await cartItemRepository.UpdateCartItemAsync(existingCartItem.Id);
            }

            return new CartDisplayDTO
            {
                CartId = activeCart.Id,
                RestaurantId = activeCart.RestaurantId,
                CreatedAt = activeCart.CreatedAt,
                TotalAmount = activeCart.TotalAmount
            };
        }

        public async Task<CartDisplayDTO?> GetCartByIdAsync(Guid UserId)
        {
            var activeCart = await cartRepository.GetCartByIdAsync(UserId);

            if (activeCart == null)
            {
                return null;
            }

            var cartResponseDto = new CartDisplayDTO
            {
                CartId = activeCart.Id,
                RestaurantId = activeCart.RestaurantId,
                CreatedAt = activeCart.CreatedAt,
                TotalAmount = activeCart.TotalAmount
            };
            return cartResponseDto;
        }
    }
}