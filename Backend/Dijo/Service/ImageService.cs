using Microsoft.AspNetCore.Mvc.ModelBinding;
using RestaurantAPI.Exceptions;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;
using RestaurantAPI.Models.Enums;
using RestaurantAPI.Repositories;
namespace RestaurantAPI.Service
{
    public class ImageService : IImageService
    {

        public readonly IImageRepository imageRepository;

        public ImageService(IImageRepository imageRepository) 
        {
            this.imageRepository = imageRepository;
        }

        public async Task<ImageDto?> GetImageByIdAsync(Guid imageId)
        {
            var ImageItem = await imageRepository.GetImageByIdAsync(imageId);

            if (ImageItem == null) { return null; }

            var allowedTyped = new[]
            {
                ImageType.MenuIcon,
                ImageType.MenuItemImage,
                ImageType.RestaurantIcon,

            };

            if (!allowedTyped.Contains(ImageItem.ImageType))
            {
                ValidateRestaurantImageQuery(ImageItem.ImageType);
            }
             
            //Map repository response to Dto
            var imageDto = new ImageDto
            {
                Id = ImageItem.Id,
                File = ImageItem.File,
                FileName = ImageItem.FileName,
                FileExtension = ImageItem.FileExtension,
                FileSizeInBytes = ImageItem.FileSizeInBytes,
                FilePath = ImageItem.FilePath,
                ImageType = ImageItem.ImageType,
                created_at = (DateTime)ImageItem.created_at,
            };

            return imageDto;

        }

        public async Task<ImageDto> UploadImageAsync(ImageUploadRequestDto request)
        {
            ValidationFileUpload(request);

            //Map Dto to a domain model
            var imageDomainModel = new Image

            {
                File = request.File,
                FileExtension = Path.GetExtension(request.File.FileName),
                FileSizeInBytes = request.File.Length,
                FileName = request.FileName,
                ImageType = request.ImageType,
                created_at = DateTime.UtcNow,
            };

            imageDomainModel = await imageRepository.UploadImage(imageDomainModel);

            var imageDto = new ImageDto
            {
                Id = imageDomainModel.Id,
                File = imageDomainModel.File,
                FileName = imageDomainModel.FileName,
                FileSizeInBytes = imageDomainModel.FileSizeInBytes,
                FileExtension = imageDomainModel.FileExtension,
                ImageType = imageDomainModel.ImageType,
                FilePath = imageDomainModel.FilePath,
                created_at = (DateTime)imageDomainModel.created_at
            };

            return imageDto;
        }

        public void ValidationFileUpload(ImageUploadRequestDto imageUploadRequestDto)
        {
            const int MinImageSize = 50 * 1024;
            const int MaxImageSize = 400 * 1024;
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png", ".gif" };

            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadRequestDto.File.FileName)))
            {
                var allowedList = string.Join(",", allowedExtensions);
                throw new UnsupportedFileExtensionException(allowedList);
            }

            //File size validation
            if (imageUploadRequestDto.File.Length < MinImageSize || (imageUploadRequestDto.File.Length > MaxImageSize))
            {
                throw new FileSizeExceededException(MaxImageSize);
            }
        }

        public void ValidateRestaurantImageQuery(ImageType imageType)
        {
            throw new InvalidOperationException("Only MenuIcon images are allowed here.");
        }
    }
}
