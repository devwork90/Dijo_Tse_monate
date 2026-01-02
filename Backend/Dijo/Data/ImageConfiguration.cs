using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RestaurantAPI.Models.Domain;

namespace RestaurantAPI.Data
{
    public class ImageConfiguration : IEntityTypeConfiguration<Image>
    {
        public void Configure(EntityTypeBuilder<Image> builder) 
        {
            builder.ToTable("Images");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(i => i.FilePath)
                .IsRequired();

            builder.Property(i => i.FileSizeInBytes)
                .IsRequired();
        }
    }
}
