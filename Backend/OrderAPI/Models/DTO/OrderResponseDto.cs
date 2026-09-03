using OrderAPI.Models.Domain;
using OrderAPI.Models.Enums;

namespace OrderAPI.Models.DTO
{
    public class OrderResponseDto
    {
        public Guid OrderId { get; set; }
        public Guid RestaurantId { get; set; } = Guid.Empty;
        public decimal TotalAmount { get; set; }
        public OrderStatus Status { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public int TotalItems { get; set; }
        public List<OrderItemResponseDto> OrderItems { get; set; } = new List<OrderItemResponseDto>();
    }
}
