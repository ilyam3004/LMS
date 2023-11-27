using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

public class TaskCommentConfiguration : IEntityTypeConfiguration<TaskComment>
{
    public void Configure(EntityTypeBuilder<TaskComment> builder)
    {
        builder.HasKey(e => e.TaskCommentId);
        
        builder.Property(e => e.Comment)
            .HasMaxLength(300);
        
        builder.HasOne(e => e.StudentTask)
            .WithMany(t => t.Comments)
            .HasForeignKey(e => e.StudentTaskId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(e => e.User);
    }
}