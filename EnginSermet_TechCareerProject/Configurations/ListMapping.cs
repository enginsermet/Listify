using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EnginSermet_TechCareerProject.Entities;
using System.Reflection.Emit;

namespace EnginSermet_TechCareerProject.Configurations
{
    public class ListMapping : IEntityTypeConfiguration<List>
    {
        public void Configure(EntityTypeBuilder<List> builder)
        {

            builder.HasKey(a => a.ListId);

            builder.Property(a => a.UserId)
            .IsRequired(true)
            .HasColumnType("nvarchar(450)");

            builder.Property(a => a.ListName)
                .IsRequired(true)
            .HasMaxLength(36);

        }
    }
}
