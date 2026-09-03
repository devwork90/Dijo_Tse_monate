using Microsoft.AspNetCore.Http.HttpResults;
using OrderAPI.Data;
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

        public async Task<CartDisplayDTO> AddCartAsync(AddCartRequestDTO addCartRequest, Guid userId)
        {
            var cart = new Cart
            {
                UserId = userId,
                RestaurantId = addCartRequest.RestaurantId,
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

        public async Task<CartDisplayDTO> AddCartItemAsync(AddCartItemDTO addCartItemRequest, Guid userId)
        {
            var activeCart = await cartRepository.GetCartByIdAsync(userId);
            var restaurant = await restaurantService.GetRestaurantByIdAsync(activeCart?.RestaurantId ?? Guid.Empty);
           

            if (activeCart == null)
            {
                activeCart ??= new Cart
                {
                    UserId = userId,
                    RestaurantId = addCartItemRequest.RestaurantId,
                    CreatedAt = DateTime.UtcNow
                };

                await cartRepository.AddCartAsync(activeCart);
            }

            foreach (var item in addCartItemRequest.Items) 
            {
                var menuItem = await restaurantService.GetMenuItemAsync(item.MenuItemId);
                var existingCartItem = await cartRepository.GetByCartandMenuIdAsync(activeCart?.Id ?? Guid.Empty, item.MenuItemId);
                

                if (menuItem == null)
                {
                    throw new Exception($"Menu item with ID {item.MenuItemId} not found.");
                }

                if (existingCartItem == null) 
                {
                    var cartItem = new CartItem
                    {
                        CartId = activeCart?.Id ?? Guid.Empty,
                        MenuItemId = item.MenuItemId,
                        ItemName = menuItem.Name,
                        Quantity = item.Quantity,
                        UnitPrice = menuItem.Price
                    };
                    await cartItemRepository.AddCartItemAsync(cartItem);
                    await cartRepository.UpdateCartTotalAmountAsync(activeCart.Id);
                }
                else
                {
                    existingCartItem.Quantity += item.Quantity;
                    activeCart.TotalAmount = existingCartItem.Quantity * existingCartItem.UnitPrice;
                    await cartRepository.UpdateCartTotalAmountAsync(activeCart.Id);
                    await cartItemRepository.UpdateCartItemAsync(existingCartItem.Id);
                }

            }

            return new CartDisplayDTO
            {
                CartId = activeCart.Id,
                UserId = (Guid)activeCart.UserId,
                RestaurantId = activeCart.RestaurantId,
                RestaurantName = restaurant?.Name ?? string.Empty,
                Quantity = activeCart.TotalItems,
                CreatedAt = activeCart.CreatedAt,
                TotalAmount = activeCart.TotalAmount,
            };
        }

        public async Task<GetCartResponseDTO> DeleteCartItemAsync(Guid cartItemId, Guid userId)
        {
            var existingCartItem = await cartItemRepository.GetCartItemByIdAsync(cartItemId);
            var activeCart = await cartRepository.GetCartByIdAsync(userId);

            if (existingCartItem == null)
            {
                throw new Exception($"Cart item with ID {cartItemId} not found.");
            }

            if (activeCart == null)
            {
                throw new Exception($"Cart for user with ID {userId} not found.");
            }

            await cartItemRepository.DeleteCartItemAsync(cartItemId);
            await cartRepository.UpdateCartTotalAmountAsync(activeCart.Id);

            return new GetCartResponseDTO
            {
                Cart = new CartDisplayDTO
                {
                    CartId = activeCart.Id,
                    UserId = (Guid)activeCart.UserId,
                    RestaurantId = activeCart.RestaurantId,
                    Quantity = activeCart.TotalItems,
                    CreatedAt = activeCart.CreatedAt,
                    TotalAmount = activeCart.TotalAmount,
                }
            };
        }

        public async Task<GetCartResponseDTO?> GetCartByIdAsync(Guid UserId)
        {
            var activeCart = await cartRepository.GetCartByIdAsync(UserId);
            var cartItems = await cartItemRepository.GetCartItems();
            var restaurant = await restaurantService.GetRestaurantByIdAsync(activeCart?.RestaurantId ?? Guid.Empty);

            if (activeCart == null)
            {
                return null;
            }

            var cartResponseDto = new CartDisplayDTO
            {
                CartId = activeCart.Id,
                UserId = (Guid)activeCart.UserId,
                RestaurantId = activeCart.RestaurantId,
                RestaurantName = restaurant?.Name ?? string.Empty,
                Quantity = activeCart.TotalItems,
                CreatedAt = activeCart.CreatedAt,
                TotalAmount = activeCart.TotalAmount,

                CartItems = cartItems.Select(cartItem => new CartItemDisplayDTO
                {
                    Id = cartItem.Id,
                    MenuItemId = cartItem.MenuItemId,
                    ItemName = cartItem.ItemName,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.UnitPrice,
                    TotalPrice = cartItem.Quantity * cartItem.UnitPrice
                }).ToList()
            };

            return new GetCartResponseDTO
            {
                Cart = cartResponseDto
            };
        }

        public async Task<GetCartResponseDTO> PatchCartItemQuantityAsync(Guid id, PatchCartItemQuantityDTO patchCartItemQuantityDTO)
        {
            var existingCartItem = await cartItemRepository.GetCartItemByIdAsync(patchCartItemQuantityDTO.Id);
            var activeCart = await cartRepository.GetCartByIdAsync(id);
            
            var restaurant = await restaurantService.GetRestaurantByIdAsync(activeCart?.RestaurantId ?? Guid.Empty);
            if (existingCartItem == null) 
            {
                throw new Exception($"Menu item with ID {id} not found.");
            }
            existingCartItem.Quantity = patchCartItemQuantityDTO.Quantity;
            
            await cartItemRepository.UpdateCartItemAsync(existingCartItem.Id);

            var remainingItems = await cartItemRepository.GetCartItemByIdAsync(existingCartItem.Id)
                .ContinueWith(task => task.Result != null ? new[] { task.Result } : new CartItem[0]);

            // Check if there are no remaining items in the cart, and if so, delete the cart
            if (!remainingItems.Any())
            {
                
                await cartRepository.DeleteCartAsync((Guid)activeCart.UserId);
               
                return null; // Return null if the cart is deleted
            }

            //Cart still exists, so reload it to get the latest values
            activeCart = await cartRepository.GetCartByIdAsync(id);

            var cartItems = await cartItemRepository.GetCartItems();
            await cartRepository.UpdateCartTotalAmountAsync(activeCart.Id);

            var cartResponseDto = new CartDisplayDTO
            {
                CartId = activeCart.Id,
                UserId = (Guid)activeCart.UserId,
                RestaurantId = activeCart.RestaurantId,
                RestaurantName = restaurant?.Name ?? string.Empty,
                Quantity = activeCart.TotalItems,
                CreatedAt = activeCart.CreatedAt,
                TotalAmount = activeCart.TotalAmount,

                CartItems = cartItems.Select(cartItem => new CartItemDisplayDTO
                {
                    Id = cartItem.Id,
                    MenuItemId = cartItem.MenuItemId,
                    ItemName = cartItem.ItemName,
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.UnitPrice,
                    TotalPrice = cartItem.Quantity * cartItem.UnitPrice
                }).ToList()
            };

            return new GetCartResponseDTO
            {
                Cart = cartResponseDto
            };
        }
    }
}