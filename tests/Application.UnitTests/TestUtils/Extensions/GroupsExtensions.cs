using Application.Models.Groups;
using FluentAssertions;
using Domain.Entities;

namespace Application.UnitTests.TestUtils.Extensions;

public static class GroupsExtensions
{
    public static void ValidateRetrievedGroups(this List<GroupResult> result, List<Group> groups)
    {
        result.Should().NotBeNull();
        result.Count.Should().Be(groups.Count);

        groups.Zip(result, (g, r) => new {Group = g, Result = r})
            .ToList()
            .ForEach(item =>
            {
                item.Result.Group.GroupId.Should().Be(item.Group.GroupId);
                item.Result.Group.Name.Should().Be(item.Group.Name);
                item.Result.Group.Course.Should().Be(item.Group.Course);
                item.Result.Group.Department.Should().Be(item.Group.Department);
                item.Result.Group.Students.Should().NotBeNull();
                item.Result.Group.Students.Count.Should().Be(item.Group.Students.Count);
                item.Result.Group.Subjects.Should().NotBeNull();
                item.Result.Group.Subjects.Count.Should().Be(item.Group.Subjects.Count);
            });
    }
}