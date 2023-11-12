using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);
        
        builder.Property(u => u.Email)
            .IsRequired()
            .HasMaxLength(319);

        builder.Property(u => u.Password)
            .IsRequired()
            .HasMaxLength(255);

        builder.HasOne(u => u.Student);

        builder.HasOne(u => u.Lecturer);
    }
}