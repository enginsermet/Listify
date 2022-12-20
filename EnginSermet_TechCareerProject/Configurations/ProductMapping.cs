using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EnginSermet_TechCareerProject.Entities;

namespace EnginSermet_TechCareerProject.Configurations
{
    public class ProductMapping : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {

            builder.HasKey(a => a.ProductId);

            builder.HasOne(a => a.Category)
                .WithMany(a => a.Products)
                .HasForeignKey(a => a.CategoryId);

            builder.Property(a => a.ProductName)
                .IsRequired(true)
            .HasMaxLength(36);

            builder.Property(a => a.UnitPrice)
                .HasColumnType("money")
                .HasDefaultValueSql("(0)");



        }
    }
}
