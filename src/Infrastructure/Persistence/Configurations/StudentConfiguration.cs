using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.StudentId);

        builder.Property(s => s.Address)
            .HasMaxLength(200);
        
        builder.Property(s => s.FullName)
            .HasMaxLength(100);
        
        builder.HasOne(s => s.User);
        builder.HasOne(s => s.Group);
    }
}