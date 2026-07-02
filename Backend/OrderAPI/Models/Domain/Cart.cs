using OrderAPI.Models.Enums;
using System.Numerics;

namespace OrderAPI.Models.Domain
{
    public class Cart
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }

        public Guid RestaurantId { get; set; }

        public int TotalItems { get; set; } = 0;

        public CartStatus Status { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt {  get; set; }

        public decimal TotalAmount { get; set; } = 0m;

        public ICollection<CartItem>? Items { get; set; } = new List<CartItem>();
    }
}
