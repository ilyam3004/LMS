using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class SubjectConfigurations : IEntityTypeConfiguration<Subject>
{
    public void Configure(EntityTypeBuilder<Subject> builder)
    {
        builder.HasKey(s => s.SubjectId);

        builder.Property(s => s.Name)
            .HasMaxLength(100);

        builder.Property(s => s.Description)
            .HasMaxLength(400);

        builder.HasOne(s => s.Lecturer)
            .WithMany(l => l.Subjects)
            .HasForeignKey(s => s.LecturerId);
    }
}
