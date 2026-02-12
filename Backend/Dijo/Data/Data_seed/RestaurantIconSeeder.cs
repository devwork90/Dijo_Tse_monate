using RestaurantAPI.API.Data;
using RestaurantAPI.API.Models.Domain;
using System.Linq;
namespace RestaurantAPI.Data.Data_seed
{
    public class RestaurantIconSeeder
    {
        public static void Seed(DijoDbContext context)
        {
            // Guard clause run only once 
            if (context.Images.Any(i => i.RestaurantId != null))
            {
                return;
            }

            var mappings = new Dictionary<Guid, Guid>
                {
                    { Guid.Parse("1AD4DE6C-F138-41D1-AF4E-1D0D58E34B80"), Guid.Parse("A1B2C3D4-E5F6-7890-1234-56789ABCDE01") },
                    { Guid.Parse("02D2951C-05A7-4768-B1BB-31891EB522CE"), Guid.Parse("B1C2D3E4-F5A6-7890-1234-56789ABCDE02") },
                    { Guid.Parse("0DC1443D-9E35-4940-810C-426F55E04AC7"), Guid.Parse("C1D2E3F4-A5B6-7890-1234-56789ABCDE03") },
                    { Guid.Parse("FBFD640E-03B5-4AEF-95CF-6112708C4CE5"), Guid.Parse("D1E2F3A4-B5C6-7890-1234-56789ABCDE04") },
                    { Guid.Parse("12B88F56-CC9A-4189-810E-6A1510F6045B"), Guid.Parse("E1F2A3B4-C5D6-7890-1234-56789ABCDE05") },
                    { Guid.Parse("93EC51AB-0862-47FE-A2BE-B663C0D36971"), Guid.Parse("F1A2B3C4-D5E6-7890-1234-56789ABCDE06") },
                    { Guid.Parse("6D807253-5081-4928-8CEA-BA0019F457A7"), Guid.Parse("A2B3C4D5-E6F7-7890-1234-56789ABCDE07") },
                    { Guid.Parse("9A9D2D84-E48F-4FF8-B2FA-C45B05421C82"), Guid.Parse("B2C3D4E5-F6A7-7890-1234-56789ABCDE08") },
                    { Guid.Parse("94053C61-D5F8-4D82-A0E8-C6FE2F1D5843"), Guid.Parse("C2D3E4F5-A6B7-7890-1234-56789ABCDE09") },
                    { Guid.Parse("CAF21DBB-ADE5-4F51-8F12-EF9862022BA9"), Guid.Parse("D2E3F4A5-B6C7-7890-1234-56789ABCDE10") },
                };

            // load all restaurant and image ids into memory to avoid multiple database calls in the loop
            var restaurantIds = mappings.Keys.ToList();
            var imageIds = mappings.Values.ToList();

            // Load restaurants and images into dictionaries for quick lookup
            var restuarants = context.Restaurants.Where(r => restaurantIds.Contains(r.Id)).ToDictionary(r => r.Id);
            var images = context.Images.Where(i => imageIds.Contains(i.Id)).ToDictionary(i => i.Id);

            foreach (var map in mappings)
            {
               if (!restuarants.TryGetValue(map.Key, out var restaurant))
                {
                    continue; // skip if restaurant not found
                }

                if (!images.TryGetValue(map.Value, out var image))
                {
                    continue; // skip if image not found
                }

                
                image.RestaurantId = restaurant.Id;
                restaurant.RestaurantIcon = image;
                restaurant.logo_url = image.FilePath;
                restaurant.RestaurantIconId = image.Id;
                image.updated_at = DateTime.UtcNow;
                restaurant.updated_at = DateTime.UtcNow;

            }
            // Save all changes in one batch to optimize performance
            context.SaveChanges();

        }

    }
}
