namespace Dijo.API.Models.DTO
{
    public class RestaurantDto
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string Address { get; set; }
        public string description { get; set; }

        public string logo_url { get; set; }
        public int rating { get; set; }
        public bool is_open { get; set; }

        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set;}

    }
}
