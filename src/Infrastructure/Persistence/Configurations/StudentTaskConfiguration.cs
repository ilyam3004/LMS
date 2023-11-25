using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

public class StudentTaskConfiguration : IEntityTypeConfiguration<StudentTask>
{
    public void Configure(EntityTypeBuilder<StudentTask> builder)
    {
        builder.HasKey(t => t.StudentTaskId);

        builder.Property(t => t.FileUrl)
            .IsRequired(false);

        builder.Property(t => t.UploadedAt)
            .IsRequired(false);

        builder.HasOne(t => t.Student)
            .WithMany(s => s.Tasks)
            .HasForeignKey(t => t.StudentId);

        builder.HasOne(t => t.Task)
            .WithMany(t => t.StudentTasks)
            .HasForeignKey(t => t.TaskId);

        builder.Property(t => t.Status)
            .IsRequired();
    }
}