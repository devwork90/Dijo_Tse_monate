namespace RestaurantAPI.Models.DTO
{
    public class RestaurantCategoryDto
    {
        public Guid Id { get; set; }
        public string RestaurantName { get; set; }
        public string MenuName { get; set; }
        public string SubMenuName { get; set; }
        public Guid SubMenuId { get; set; }
    }
}
