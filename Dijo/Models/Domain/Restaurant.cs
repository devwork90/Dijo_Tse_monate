using System.ComponentModel.DataAnnotations.Schema;

namespace Dijo.API.Models.Domain
{
    public class Restaurant
    {
        public Guid Id { get; set; }
        public string name { get; set; }
        public string Address { get; set; }
        public string description { get; set; }
        public string logo_url { get; set; }
        public int rating { get; set; }
        public bool is_open { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get;set; }

    }
}
