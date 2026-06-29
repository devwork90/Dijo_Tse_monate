namespace OrderAPI.Models.DTO
{
    public class AddCartItemDTO
    {
        public Guid UserId { get; set; }
        public Guid RestaurantId { get; set; }
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}
