using RestaurantAPI.API.Models.Domain;
using RestaurantAPI.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantAPI.Models.Domain
{
    public class Image
    {
        public Guid Id { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }

        public string FileName { get; set; } = string.Empty;
        public string FileExtension { get; set; } = string.Empty;
        public long FileSizeInBytes { get; set; }
        public string FilePath { get; set; } = string.Empty;
        public ImageType ImageType {  get; set; }

        // Associations (nullable until linked)
        public Guid? RestaurantId { get; set; }
        public Restaurant?  Restaurant { get; set; }
        public Guid? MenuId { get; set; }
        public Menu? Menu { get; set; }
        public DateTime? created_at { get; set; } = DateTime.UtcNow;
        public DateTime? updated_at { get; set; }
    }
}
