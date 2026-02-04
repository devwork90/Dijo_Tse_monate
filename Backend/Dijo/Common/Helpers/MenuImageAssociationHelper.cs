using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.Common.Helpers
{
    public static class MenuImageAssociationHelper
    {
        public static MenuDto ToDto(Menu menu)
        {
            if (menu == null)
                return null!;

            return new MenuDto
            {
                Id = menu.Id,
                Name = menu.Name,
                Description = menu.Description,
                is_active = menu.is_active,
                created_at = menu.created_at!.Value,
                updated_at = menu.updated_at,

                MenuIcon = menu.MenuIconImage == null
                    ? null
                    : ToImageDto(menu.MenuIconImage)
            };
        }

        private static ImageDto ToImageDto(Image image)
        {
            return new ImageDto
            {
                Id = image.Id,
                FilePath = image.FilePath,
                ImageType = image.ImageType
            };
        }
    }
}
