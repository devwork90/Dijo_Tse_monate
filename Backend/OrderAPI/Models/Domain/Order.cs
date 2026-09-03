using OrderAPI.Models.Enums;

namespace OrderAPI.Models.Domain
{
    public class Order
    {
        public Guid Id { get; set; }

        public Guid? UserId { get; set; }

        public Guid RestaurantId { get; set; }

        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; } = 0m;

        public PaymentStatus PaymentStatus { get; set; }

        public string? PaymentReference { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime? UpdatedAt { get; set; }

        public int TotalItems { get; set; } = 0;

        public ICollection<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    }
}
