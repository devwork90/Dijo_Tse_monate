namespace OrderAPI.Models.Domain
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        
        public Guid OrderId {  get; set; }

        public Guid MenuItemId { get; set; } = Guid.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public Order Order { get; set; } = null!;
    }
}
