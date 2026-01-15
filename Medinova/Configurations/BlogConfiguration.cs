using Medinova.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medinova.Configurations
{
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(x => x.Tittle).IsRequired().HasMaxLength(512);
            builder.Property(x => x.Description).IsRequired().HasMaxLength(1024);
            builder.Property(x => x.ImagePath).IsRequired();

            builder.HasOne(x => x.Doctor).WithMany(x => x.Blogs).HasForeignKey(x => x.DoctorId).HasPrincipalKey(x => x.Id).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
