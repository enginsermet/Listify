using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;
using EnginSermet_TechCareerProject.Entities;

namespace EnginSermet_TechCareerProject.Configurations
{
    public class AppUserMapping : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id);

            builder.Property(a => a.UserName)
                .IsRequired(true)
            .HasMaxLength(20);

            builder.Property(a => a.PasswordHash)
                .IsRequired(true);

            builder.Property(a => a.PasswordSalt)
                .IsRequired(true);
        }
    }
}
