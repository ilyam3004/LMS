using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Domain.Entities;

namespace Infrastructure.Persistence.Configurations;

public class GroupConfiguration : IEntityTypeConfiguration<Group>
{
    public void Configure(EntityTypeBuilder<Group> builder)
    {
        builder.HasKey(g => g.GroupId);

        builder.HasMany(g => g.Students);

        builder.Property(g => g.Name)
            .HasMaxLength(100);

        builder.Property(g => g.Department)
            .HasMaxLength(100);


        builder.HasData(
            new Group
            {
                GroupId = Guid.NewGuid(),
                Name = "Group A",
                Department = "Computer Science"
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group B", 
                Department = "Electrical Engineering"
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group C", 
                Department = "Mechanical Engineering"
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group D", 
                Department = "Physics"
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group E", 
                Department = "Mathematics"
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group F", 
                Department = "Chemistry"
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group G", 
                Department = "Biology"
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group H", 
                Department = "Civil Engineering"
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group I", 
                Department = "Environmental Science"
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group J", 
                Department = "Information Technology"
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group K", 
                Department = "Aerospace Engineering"
            }
        );
    }
}