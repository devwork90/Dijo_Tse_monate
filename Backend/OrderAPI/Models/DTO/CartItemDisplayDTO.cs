using System.Diagnostics.Contracts;

namespace OrderAPI.Models.DTO
{
    public class CartItemDisplayDTO
    {
        public Guid Id { get; set; }
    
        public Guid MenuItemId { get; set; }

        public string ItemName { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice {  get; set; }
        
    }
}
