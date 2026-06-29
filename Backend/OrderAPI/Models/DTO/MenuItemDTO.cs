namespace OrderAPI.Models.DTO
{
    public class MenuItemDTO
    {
        public Guid Id { get; set; }

        public Guid RestaurantId { get; set; } = Guid.NewGuid();

        public string Name { get; set; } = string.Empty;

        public decimal Price { get; set; }

        public bool is_Available { get; set; }
    }
}
 