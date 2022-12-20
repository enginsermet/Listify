using EnginSermet_TechCareerProject.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EnginSermet_TechCareerProject.Configurations
{
    public class CategoryMapping : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasMany(a => a.Products)
                .WithOne(a => a.Category)
                .HasForeignKey(a => a.CategoryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasKey(a => a.CategoryId);

            builder.Property(a => a.CategoryName)
                .IsRequired(true)
            .HasMaxLength(20);

            builder.Property(a => a.Picture)
                .HasColumnType("image");

        }
    }
}