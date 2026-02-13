using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.Common.Helpers
{
    public class MenuItemImageAssociationHelper
    {
        public static MenuItemsDto ToDto(MenuItem menuItem)
        {
            if (menuItem == null)
                return null!;
            return new MenuItemsDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                imageUrl = menuItem.imageUrl,
                is_Available = menuItem.is_Available,
                created_at = menuItem.created_at!.Value,
                updated_at = menuItem.updated_at,
                MenuItemIconImage = menuItem.MenuItemIcon == null
                    ? null
                    : ToImageDto(menuItem.MenuItemIcon)
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
