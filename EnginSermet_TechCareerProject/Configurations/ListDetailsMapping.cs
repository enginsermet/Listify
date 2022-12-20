using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EnginSermet_TechCareerProject.Entities;

namespace EnginSermet_TechCareerProject.Configurations
{
    public class ListDetailsMapping : IEntityTypeConfiguration<ListDetail>
    {
        public void Configure(EntityTypeBuilder<ListDetail> builder)
        {
            {
                builder.HasKey(a => new { a.ListId, a.ProductId });

                builder.HasOne(a => a.List)
                    .WithMany(b => b.ListDetails)
                    .HasForeignKey(c => c.ListId);

                builder.HasOne(a => a.Product)
                    .WithMany(b => b.ListDetails)
                    .HasForeignKey(c => c.ProductId);
            }
        }
    }
}
