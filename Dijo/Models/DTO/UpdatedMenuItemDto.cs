namespace Dijo.API.Models.DTO
{
    public class UpdatedMenuItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string? Image_url { get; set; }
        public bool is_available { get; set; }
    }
}
