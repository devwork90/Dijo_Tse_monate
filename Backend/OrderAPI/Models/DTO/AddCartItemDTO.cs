namespace OrderAPI.Models.DTO
{
    public class AddCartItemDTO
    {
        public Guid UserId { get; set; }
        public Guid RestaurantId { get; set; }

        public List<CartItemRequestDTO> Items { get; set; } = new List<CartItemRequestDTO>();
    }
}
