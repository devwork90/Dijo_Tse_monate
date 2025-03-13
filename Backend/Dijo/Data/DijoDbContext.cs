
using RestaurantAPI.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace RestaurantAPI.API.Data
{
    public class DijoDbContext: DbContext
    {
        public DijoDbContext(DbContextOptions dbContextOptions)
            :base(dbContextOptions) 
        { 
        
        }

        //Create DB sets

        public DbSet<Menu> Menu { get; set; }
        public DbSet<SubMenu> SubMenu { get; set; }
        public DbSet<Restaurant> restaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure SubMenu relationships
            modelBuilder.Entity<SubMenu>()
                .HasOne(sm => sm.Menu)
                .WithMany(m => m.SubMenus) // Ensure this matches the navigation property name in `Menu`
                .HasForeignKey(sm => sm.MenuId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevents cascading delete

            modelBuilder.Entity<SubMenu>()
                .HasOne(sm => sm.Restaurant)
                .WithMany(r => r.SubMenus) // Ensure this matches the navigation property name in `Restaurant`
                .HasForeignKey(sm => sm.restaurantId)
                .OnDelete(DeleteBehavior.Restrict);  // Prevents cascading delete

            // Seed data for Menu model
            var menus = new List<Menu>()
            {
                new Menu()
                {
                    Id = Guid.Parse("83b89851-d594-48a0-b66b-94ba920a0c70"),
                    Name = "Chicken",
                    Description = "All your chicken menus",
                    is_active = true,
                    created_at = new DateTime(2025, 2, 15, 14, 30, 0),
                    updated_at = null,
                },

                new Menu()
                {
                    Id = Guid.Parse("c5e708e3-46e5-49ff-bb27-a94cf56fa2fe"),
                    Name = "Grilled",
                    Description = "All your shisa nyama grills",
                    is_active= true,
                    created_at =  new DateTime(2025, 2, 27, 18, 15, 0),
                    updated_at = null,
                },

                new Menu()
                {
                    Id = Guid.Parse("b7272c86-286d-465c-b993-10e177f6f056"),
                    Name = "Piza",
                    Description = "All your Piza Menu",
                    is_active= true,
                    created_at =  new DateTime(2025, 3, 10, 20, 15, 0),
                    updated_at = null,
                },
            };

            //Seeds restaurants data to the database
            modelBuilder.Entity<Menu>().HasData(menus);
        }
    }
}
