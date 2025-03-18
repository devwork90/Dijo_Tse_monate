
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
        public DbSet<Restaurant> Restaurants { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Define a Many-to-Many Relationship between Menu <-> Restaurant
            modelBuilder.Entity<Menu>()
                .HasMany(m => m.Restaurants)
                .WithMany(r => r.Menu)
                .UsingEntity(j => j.ToTable("MenuRestaurants"));

            //Dfine a One-to-Many Relationship between Menu -> SubMenu

            modelBuilder.Entity<Menu>()
                .HasMany(m => m.SubMenus)
                .WithOne(s => s.Menu)
                .HasForeignKey(s => s.MenuId)
                .OnDelete(DeleteBehavior.Cascade); //When a Menu is deleted, delete its SubMenus

            //Define a One - to - Many Relationship between Restaurant->SubMenu
            modelBuilder.Entity<Restaurant>()
                .HasMany(r => r.SubMenus)
                .WithOne(s => s.Restaurant)
                .HasForeignKey(s => s.restaurantId)
                .OnDelete(DeleteBehavior.Cascade); //When a Menu is deleted, delete its SubMenus

            //Prevent Restaurant deletion when a Menus is deleted
            modelBuilder.Entity<Menu>()
                .HasMany(m => m.Restaurants)
                .WithMany(r => r.Menu)
                .UsingEntity<Dictionary<string, object>>(
                   "MenuRestaurants",
                    j => j.HasOne<Restaurant>().WithMany().HasForeignKey("restaurantId"),
                    j => j.HasOne<Menu>().WithMany().HasForeignKey("MenuId"),
                    j => j.ToTable("MenuRestaurants")
                );

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
                    Name = "Pizza",
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
