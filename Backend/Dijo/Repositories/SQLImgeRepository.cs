using Microsoft.EntityFrameworkCore;
using RestaurantAPI.API.Data;
using RestaurantAPI.Models.Domain;
using RestaurantAPI.Models.DTO;

namespace RestaurantAPI.Repositories
{
    public class SQLImgeRepository : IImageRepository
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly DijoDbContext dbContext;
        private readonly IHttpContextAccessor contextAccessor;

        public SQLImgeRepository(IWebHostEnvironment webHostEnvironment, DijoDbContext dbContext,
            IHttpContextAccessor contextAccessor)
        {
            this.webHostEnvironment = webHostEnvironment;
            this.dbContext = dbContext;
            this.contextAccessor = contextAccessor;
        }

        public async Task<Image?> GetbyIdAsync(Guid id)
        {
            return await dbContext.Images.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Image> UploadImage(Image image)
        {
            var subFolder = image.ImageType.ToString();

            // 1. Folder path ONLY
            var imagesrootPath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", subFolder);

            // 2. Ensure directory exists
            if (!Directory.Exists(imagesrootPath))
            {
                Directory.CreateDirectory(imagesrootPath);
            }

            // 3. File path (filename LAST)
            var fileName = $"{image.FileName}{image.FileExtension}";

            var localFilePath = Path.Combine(imagesrootPath, fileName);

            //Upload Image to local Path
            using (var stream = new FileStream(localFilePath, FileMode.Create))
            {
                await image.File.CopyToAsync(stream);
            }


            var urlFilePath = $"{contextAccessor.HttpContext.Request.Scheme}://{contextAccessor.HttpContext.Request.Host}{contextAccessor.HttpContext.Request.PathBase}/Images/{subFolder}/{image.FileName}{image.FileExtension}";
            image.FilePath = urlFilePath;

            await dbContext.Images.AddAsync(image);
            await  dbContext.SaveChangesAsync();
            return image;
        }
    }
}
