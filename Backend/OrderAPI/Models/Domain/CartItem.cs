namespace OrderAPI.Models.Domain
{
    public class CartItem
    {
        public Guid Id { get; set; }
        public Guid CartId { get; set; }

        public Guid MenuItemId { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice => Quantity * UnitPrice;

        public string? ItemName { get; set; }

        public Cart Cart { get; set; } = null!;
    }
}
