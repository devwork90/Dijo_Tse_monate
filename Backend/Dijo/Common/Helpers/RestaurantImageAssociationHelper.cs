using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.API.Models.DTO;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.Common.Helpers
{
    public static class RestaurantImageAssociationHelper
    {
        public static RestaurantDto ToDto(Restaurant restaurant)
        {
            if (restaurant == null)
                return null!;
            return new RestaurantDto
            {
                Id = restaurant.Id,
                name = restaurant.name,
                description = restaurant.description,
                is_open = restaurant.is_open,
                created_at = restaurant.created_at!.Value,
                updated_at = restaurant.updated_at,
                RestaurantIconImage = restaurant.RestaurantIcon == null
                    ? null
                    : ToImageDto(restaurant.RestaurantIcon)
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
