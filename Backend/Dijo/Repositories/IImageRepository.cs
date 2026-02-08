using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.Repositories
{
    public interface IImageRepository
    {
        Task<Image> UploadImage(Image image);
        Task<Image?>GetImageByIdAsync(Guid id);

        Task<Image?> DeleteImageAsync(Guid id);
    }
}
