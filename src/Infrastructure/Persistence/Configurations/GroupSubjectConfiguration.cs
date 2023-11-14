using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

public class GroupSubjectConfiguration : IEntityTypeConfiguration<GroupSubject>
{
    public void Configure(EntityTypeBuilder<GroupSubject> builder)
    {
        builder.HasKey(gs => new { gs.SubjectId, gs.GroupId });

        builder.HasOne(gs => gs.Subject)
            .WithMany(s => s.GroupSubjects)
            .HasForeignKey(gs => gs.SubjectId);

        builder.HasOne(gs => gs.Group)
            .WithMany(g => g.GroupSubjects)
            .HasForeignKey(gs => gs.GroupId);
    }
}