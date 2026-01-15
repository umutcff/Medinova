using Medinova.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Medinova.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.Property(x => x.FullName).IsRequired().HasMaxLength(128);
            builder.Property(x => x.ImagePath).IsRequired();
        }
    }
}
