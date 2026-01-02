using RestaurantAPI.Models.Domain;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.API.Models.Domain
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

        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get;set; }

        //Specifies Many-to-Many with Menu
        public ICollection<Menu> Menu { get; set; } = new List<Menu>();

        //public List<SubMenu> SubMenus { get; set; } = new();
        public ICollection<SubMenu> SubMenus { get; set; }

        public ICollection<Image> Images { get; set; } = new LinkedList<Image>();
    }
}
