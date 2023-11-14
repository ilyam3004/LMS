using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class LecturerConfiguration : IEntityTypeConfiguration<Lecturer>
{
    public void Configure(EntityTypeBuilder<Lecturer> builder)
    {
        builder.HasKey(l => l.LecturerId);

        builder.Property(l => l.FullName)
            .HasMaxLength(100);

        builder.Property(l => l.Degree)
            .IsRequired(false);

        builder.Property(l => l.Address)
            .HasMaxLength(200);

        builder.HasOne(l => l.User);

        builder.HasMany(l => l.Subjects)
            .WithOne(s => s.Lecturer);
    }
}