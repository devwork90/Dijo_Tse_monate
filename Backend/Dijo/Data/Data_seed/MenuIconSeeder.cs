using RestaurantAPI.API.Data;

namespace RestaurantAPI.Data.Data_seed
{
    public static class MenuIconSeeder
    {
        public static void Seed(DijoDbContext context) 
        {
            // Guard clause run only once 
            if (context.Menu.Any(m => m.MenuIconImageId != null))
            {
                return;
            }

            var mappings = new Dictionary<Guid, Guid>
            {
                { Guid.Parse("B7272C86-286D-465C-B993-10E177F6F056"), Guid.Parse("25C28484-1567-4318-32C8-08DEB1D91573") },
                { Guid.Parse("89B565B4-BF45-46E1-A798-17DD7DF1FD94"), Guid.Parse("3B117348-F83B-41D9-32CA-08DEB1D91573") },
                { Guid.Parse("58077521-CFC5-41A3-8414-5A5C8A7D4DA3"), Guid.Parse("9492D71E-9075-47DF-32CB-08DEB1D91573") },
                { Guid.Parse("0C5074EC-73D6-40C1-810E-C5782D318FEB"), Guid.Parse("E0D31B84-3383-4DDF-32CD-08DEB1D91573") },
                { Guid.Parse("769AD0E9-11B4-4C4D-955F-1EFA80C04B9B"), Guid.Parse("81A98461-31BC-44D3-AC96-FB2947FB1EBE") },
                { Guid.Parse("C5E708E3-46E5-49FF-BB27-A94CF56FA2FE"), Guid.Parse("8DD86D7D-2B17-4628-32CF-08DEB1D91573") },
                { Guid.Parse("83B89851-D594-48A0-B66B-94BA920A0C70"), Guid.Parse("E6446ADD-B6C5-4CBF-32CC-08DEB1D91573") },
                { Guid.Parse("EC4C5514-AE37-4B7C-AC36-671A3923F6EA"), Guid.Parse("92BDA4D1-AD25-45CE-32CE-08DEB1D91573") }
            };

            foreach (var map in mappings)
            {
                var menu = context.Menu.FirstOrDefault(m => m.Id == map.Key);
                var image = context.Images.FirstOrDefault(i => i.Id == map.Value);
                if (menu != null)
                {
                    menu.MenuIconImageId = map.Value;
                    menu.url_menu_icon = image?.FileName;
                    menu.updated_at = DateTime.UtcNow;
                }

                context.SaveChanges();
            }
        }
    } 
}
