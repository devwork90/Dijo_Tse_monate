using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models.DTO
{
    public class ImageDto
    {
        public Guid Id { get; set; }
        public IFormFile File { get; set; }

        public string FileName { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public long FileSizeInBytes { get; set; }
        public string FilPath { get; set; } = string.Empty; 
        public DateTime created_at { get; set; }
        public DateTime? updated_at { get; set; }
    }
}
