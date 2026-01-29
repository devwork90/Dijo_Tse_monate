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
                { Guid.Parse("B7272C86-286D-465C-B993-10E177F6F056"), Guid.Parse("DFB0EB59-8AC5-431A-A0A2-B7A5A11AB130") },
                { Guid.Parse("89B565B4-BF45-46E1-A798-17DD7DF1FD94"), Guid.Parse("08FDDB0B-C5F0-4350-93FF-08DE5E5F95AE") },
                { Guid.Parse("58077521-CFC5-41A3-8414-5A5C8A7D4DA3"), Guid.Parse("98E2BD9B-2F01-4E40-AEAD-0C06A9D266CB") },
                { Guid.Parse("0C5074EC-73D6-40C1-810E-C5782D318FEB"), Guid.Parse("B9C52EC7-517E-4DE4-8A38-4151D9D674B0") },
                { Guid.Parse("769AD0E9-11B4-4C4D-955F-1EFA80C04B9B"), Guid.Parse("81A98461-31BC-44D3-AC96-FB2947FB1EBE") },
                { Guid.Parse("C5E708E3-46E5-49FF-BB27-A94CF56FA2FE"), Guid.Parse("2D089945-D41A-46D4-B165-F4635C078835") },
                { Guid.Parse("83B89851-D594-48A0-B66B-94BA920A0C70"), Guid.Parse("C374D2C3-4CF8-47C0-BC0B-4244296DC7AD") },
                { Guid.Parse("EC4C5514-AE37-4B7C-AC36-671A3923F6EA"), Guid.Parse("B95753B9-8E2F-4D3E-A505-A3AC7AA47214") }
            };

            foreach (var map in mappings)
            {
                var menu = context.Menu.FirstOrDefault(m => m.Id == map.Key);
                var image = context.Images.FirstOrDefault(i => i.Id == map.Value);
                if (menu != null)
                {
                    menu.MenuIconImageId = map.Value;
                    menu.url_menu_icon = image.FilePath;
                    menu.updated_at = DateTime.UtcNow;
                }

                context.SaveChanges();
            }
        }
    } 
}
