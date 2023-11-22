using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Task = Domain.Entities.Task;

namespace Infrastructure.Persistence.Configurations;

public class TaskConfiguration : IEntityTypeConfiguration<Task>
{
    public void Configure(EntityTypeBuilder<Task> builder)
    {
        builder.HasKey(s => s.TaskId);

        builder.Property(s => s.Title)
            .HasMaxLength(100);

        builder.Property(s => s.Description)
            .HasMaxLength(2000);

        builder.HasOne(t => t.Subject)
            .WithMany(s => s.Tasks)
            .HasForeignKey(t => t.SubjectId);
    }
}