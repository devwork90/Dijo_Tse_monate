namespace RestaurantAPI.API.Models.DTO
{
    public class SubMenuDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool is_available { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }
        public Guid MenuId { get; set; }
        public Guid restaurantId { get; set; }
    }
}
