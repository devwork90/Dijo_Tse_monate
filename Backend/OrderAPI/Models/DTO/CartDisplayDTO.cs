using OrderAPI.Models.Enums;
using System.Security.Cryptography.Pkcs;

namespace OrderAPI.Models.DTO
{
    public class CartDisplayDTO
    {
        public Guid CartId { get; set; }

        public Guid UserId { get; set; }

        public Guid RestaurantId { get; set; }

        public string RestaurantName { get; set; } = string.Empty;

        public int Status { get; set; }

        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public List<CartItemDisplayDTO> CartItems { get; set; } = new List<CartItemDisplayDTO>();

    }
}
