namespace Dijo.API.Models.Domain
{
    public class MenuItem
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string? Image_url { get; set; }
        public bool is_available { get; set; }
        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; }
        public Guid MenuId { get; set; }

        public Menu Menu { get; set; }
    }
}
