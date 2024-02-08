using Application.UnitTests.TestUtils.TestConstants;
using Domain.Entities;

namespace Application.UnitTests.TestUtils.Factories;

public static class GroupFactory
{
    public static List<Group> CreateGroupList(List<Student>? students = null,
        int groupsCount = 1)
        => Enumerable.Range(0, groupsCount).Select(index =>
                CreateGroup(students: students))
            .ToList();

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
            Students = students ?? StudentFactory.CreateStudentList(),
            Subjects = subjects ?? SubjectFactory.CreateSubjects(groupId: generatedGroupId)
        };
    }
}