using Dijo.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Dijo.API.Data
{
    public class DijoDbContext: DbContext
    {
        public DijoDbContext(DbContextOptions dbContextOptions):base(dbContextOptions) 
        { 
        
        }

        //Create DB sets

        public DbSet<Menu> Menu { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }
        public DbSet<Restaurant> restaurants { get; set; }
    }
}
