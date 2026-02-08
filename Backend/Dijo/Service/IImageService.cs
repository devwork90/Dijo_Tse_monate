using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.Service
{
    public interface IImageService
    {
        Task<ImageDto> UploadImageAsync(ImageUploadRequestDto imageUploadRequestDto);

        Task<ImageDto?> GetImageByIdAsync(Guid imageId);

        Task<bool> DeleteImageAsync(Guid imageId);
    }
}
