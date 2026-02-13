using RestaurantAPI.Models.Enums;

namespace RestaurantAPI.Common.Helpers
{
    public class ImagePathHelper
    {
        public static string GetSubFolder(ImageType imageType)
        {
            return imageType switch
            {
                ImageType.MenuIcon => "MenuIcon",
                ImageType.RestaurantIcon => "RestaurantIcon",
                ImageType.MenuItemIcon => "MenuItemImage",
                _ => throw new ArgumentOutOfRangeException(nameof(imageType))
            };
        }
    }
}
