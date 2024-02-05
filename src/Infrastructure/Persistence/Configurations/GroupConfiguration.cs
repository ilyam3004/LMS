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

        builder.HasMany(g => g.Subjects)
            .WithOne(s => s.Group);

        builder.HasData(
            new Group
            {
                GroupId = Guid.NewGuid(),
                Name = "Group A",
                Department = "Computer Science",
                Course = 1
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group B", 
                Department = "Electrical Engineering",
                Course = 2
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group C", 
                Department = "Mechanical Engineering",
                Course = 3
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group D", 
                Department = "Physics",
                Course = 4
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group E", 
                Department = "Mathematics",
                Course = 5
            },
            new Group
            {
                GroupId = Guid.NewGuid(), 
                Name = "Group F", 
                Department = "Chemistry",
                Course = 6
            }
        );
    }
}