using Application.UnitTests.TestUtils.TestConstants;
using Application.UnitTests.TestUtils.Subjects;
using Domain.Entities;

namespace Application.UnitTests.TestUtils.Groups;

public static class GroupFactory
{
    public static Group CreateGroup(Guid? groupId = null,
        string? groupName = null,
        int? course = null,
        string? department = null,
        List<Student>? students = null,
        List<Subject>? subjects = null)
    {
        Guid generatedGroupId = groupId ?? Constants.Group.GroupId;

        return new Group
        {
            GroupId = generatedGroupId,
            Name = groupName ?? Constants.Group.GroupName,
            Course = course ?? Constants.Group.Course,
            Department = department ?? Constants.Group.Department,
            Students = students ?? [],
            Subjects = subjects ?? SubjectFactory.CreateSubjects(groupId: generatedGroupId)
        };
    }
}