
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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data for restaurant model

            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Id = Guid.Parse("83b89851-d594-48a0-b66b-94ba920a0c70"),
                    name = "KFC",
                    Address = "2282 Samjokozela Street",
                    description = "It's finger lickin good",
                    logo_url = "KFC.jpg",
                    rating = 3,
                    is_open = true,
                    created_at = new DateTime(2025, 2, 15, 14, 30, 0),
                    updated_at = null,
                },

                new Restaurant()
                {
                    Id = Guid.Parse("c5e708e3-46e5-49ff-bb27-a94cf56fa2fe"),
                    name = "Loza's braai vleis",
                    Address = "40 Fairdale Street",
                    description = "WHere good food is",
                    logo_url = "loza.jpg",
                    rating = 4,
                    is_open = false,
                    created_at =  new DateTime(2025, 2, 27, 18, 15, 0),
                    updated_at = null,
                },

                new Restaurant()
                {
                    Id = Guid.Parse("b7272c86-286d-465c-b993-10e177f6f056"),
                    name = "Kwa Ace",
                    Address = "11 Songwiqi Street",
                    description = "Where good time is found",
                    logo_url = "ace.jpg",
                    rating = 5,
                    is_open = true,
                    created_at = new DateTime(2025, 3, 12, 14, 10, 0),
                    updated_at = null,
                },
            };

            //Seeds restaurants data to the database
            modelBuilder.Entity<Restaurant>().HasData(restaurants);
        }
    }
}
