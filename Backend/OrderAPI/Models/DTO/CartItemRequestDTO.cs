namespace OrderAPI.Models.DTO
{
    public class CartItemRequestDTO
    {
        public Guid MenuItemId { get; set; }
        public int Quantity { get; set; }
    }
}
