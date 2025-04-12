
using RestaurantAPI.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using RestaurantAPI.Models.Domain;

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

        public DbSet<MenuItem> MenuItems { get; set; }
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

            //Define a One-to-Many relationship between SubMenu->MenuItem
            modelBuilder.Entity<MenuItem>()
                .HasOne(mi => mi.SubMenu)
                .WithMany(sm => sm.MenuItems)
                .HasForeignKey(mi => mi.SubMenuId)
                .OnDelete(DeleteBehavior.Cascade);//When a SubMenu is deleted, delete its menuItem

            //Define a One-to-Many relation between Restaurant->MenuItem
            modelBuilder.Entity<MenuItem>()
                .HasOne(mi => mi.Restaurant)
                .WithMany()
                .HasForeignKey(mi => mi.restaurantId)
                .OnDelete(DeleteBehavior.Restrict); //When a Restaurant is deleted, delete its MenuItem
            
            //Explicitly defining Price field as decimal
            {
                modelBuilder.Entity<MenuItem>()
                    .Property(p => p.Price)
                    .HasPrecision(18, 2); // (precision, scale)

                //base.OnModelCreating(modelBuilder);
            }

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

                new Menu()
                {
                    Id = Guid.Parse("769ad0e9-11b4-4c4d-955f-1efa80c04b9b"),
                    Name = "Burger",
                    Description = "All your Burger Menu",
                    is_active= true,
                    created_at =  new DateTime(2025, 4, 10, 20, 15, 0),
                    updated_at = null,
                },
            };

            var restaurants = new List<Restaurant>()
            {
                new Restaurant()
                {
                    Id = Guid.Parse("93ec51ab-0862-47fe-a2be-b663c0d36971"),
                    name = "Kuilsriver's Nandos",
                    description = "Chicken, Burgers, Chicken Wings, Fast Food, Light Meals, Portuguese, Salad, Spicy, Dessert, South African",
                    Address = "101 Voortreker Kuilsriver",
                    logo_url = "nandos.jpg",
                    rating = 3,
                    created_at= new DateTime(2025, 4,10, 20, 15,0),
                    updated_at = null
                },

                new Restaurant()
                {
                    Id = Guid.Parse("9a9d2d84-e48f-4ff8-b2fa-c45b05421c82"),
                    name = "Pedros Zevenwacht",
                    description = "Burgers, Chicken, Portuguese, Salad, Wraps, Kids, Bowls",
                    Address = "102 Zevenwacht ",
                    logo_url = "pedros.jpg",
                    rating = 3,
                    created_at= new DateTime(2025, 4,10, 20, 15,0),
                    updated_at = null
                },

                 new Restaurant()
                {
                    Id = Guid.Parse("0dc1443d-9e35-4940-810c-426f55e04ac7"),
                    name = "KFC Soneike",
                    description = "Chicken, Chicken Wings, Burgers, Wraps, Fast Food, Dessert, American, Spicy, Light Meals, Milkshake",
                    Address = "04 Soneike ",
                    logo_url = "kfc.jpg",
                    rating = 3,
                    created_at= new DateTime(2025, 4,10, 20, 15,0),
                    updated_at = null
                },

                 new Restaurant()
                {
                    Id = Guid.Parse("1ad4de6c-f138-41d1-af4e-1d0d58e34b80"),
                    name = "Hungry Lion Kuils River",
                    description = "Chicken, Burgers, Chicken Wings, Fast Food, Spicy",
                    Address = "07 Vanreibeek Kuilsriver ",
                    logo_url = "hungryLion.jpg",
                    rating = 3,
                    created_at= new DateTime(2025, 4,10, 20, 15,0),
                    updated_at = null
                },

                 new Restaurant()
                {
                    Id = Guid.Parse("caf21dbb-ade5-4f51-8f12-ef9862022ba9"),
                    name = "Burger King Zevenwacht (Drive-thru)",
                    description = "Burgers, Chicken Wings, American, Fast Food, Plant-Based, Milkshake, Chicken",
                    Address = "07 Zevenwacht",
                    logo_url = "BurgerKing.jpg",
                    rating = 3,
                    created_at= new DateTime(2025, 4,10, 20, 15,0),
                    updated_at = null
                },

                 new Restaurant()
                 {
                    Id = Guid.Parse("94053c61-d5f8-4d82-a0e8-c6fe2f1d5843"),
                    name = "McDonald's Haasendal",
                    description = "American, Breakfast, Burgers, Wraps, Cafe, Dessert, Fast Food, Milkshake, Plant-Based, Salad",
                    Address = "07 Haasendal",
                    logo_url = "McDonald .jpg",
                    rating = 3,
                    created_at= new DateTime(2025, 4,10, 20, 15,0),
                    updated_at = null
                 },

                 new Restaurant()
                 {
                    Id = Guid.Parse("12b88f56-cc9a-4189-810e-6a1510f6045b"),
                    name = "Steers Total Stikland",
                    description = "Burgers, Chicken, Fries, Salad, Dessert, Kids, Plant-Based, Healthy, Ribs, Milkshake",
                    Address = "07 Stikland",
                    logo_url = "Steers.jpg",
                    rating = 3,
                    created_at= new DateTime(2025, 4,10, 20, 15,0),
                    updated_at = null
                 },

                 new Restaurant()
                 {
                    Id = Guid.Parse("02d2951c-05a7-4768-b1bb-31891eb522ce"),
                    name = "Roman's Pizza Soneike",
                    description = "Pizza, Pasta, Salad, Dessert, Mediterranean, Fruit, Fast Food, American, Non-Alcoholic",
                    Address = "07 Soneike",
                    logo_url = "Romans.jpg",
                    rating = 3,
                    created_at= new DateTime(2025, 4,10, 20, 15,0),
                    updated_at = null
                 },

                 new Restaurant()
                 {
                    Id = Guid.Parse("fbfd640e-03b5-4aef-95cf-6112708c4ce5"),
                    name = "Debonairs Kuilsriver",
                    description = "Pizza, Fast Food, Italian, Chicken Wings, Dessert, Vegetarian",
                    Address = "07 Kuilsriver",
                    logo_url = "debonairs.jpg",
                    rating = 3,
                    created_at= new DateTime(2025, 4,10, 20, 15,0),
                    updated_at = null
                 },

                 new Restaurant()
                 {
                    Id = Guid.Parse("6d807253-5081-4928-8cea-ba0019f457a7"),
                    name = "Spur Reno",
                    description = "Steakhouse, Grill, Burgers, Ribs, Chicken, Seafood, Chicken Wings, Dessert, Milkshake, Salad",
                    Address = "22 Kuilsriver Reno",
                    logo_url = "spur.jpg",
                    rating = 3,
                    created_at= new DateTime(2025, 4,10, 20, 15,0),
                    updated_at = null
                 }
            };

            //Seeds Menu data to the database
            modelBuilder.Entity<Menu>().HasData(menus);

            //Seeds Restaurant data to the database
            modelBuilder.Entity<Restaurant>().HasData(restaurants);
        }
    }
}
