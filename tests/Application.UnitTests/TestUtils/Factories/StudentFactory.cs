using Application.UnitTests.TestUtils.TestConstants;
using Domain.Entities;

namespace Application.UnitTests.TestUtils.Factories;

public static class StudentFactory
{
    public static List<Student> CreateStudentList(int studentsCount = 1,
        Guid? groupId = null)
        => Enumerable.Range(0, studentsCount).Select(index =>
            CreateStudent(givenIndex: index, groupId: groupId)).ToList();

    public static Student CreateStudent(Guid? studentId = null,
        Guid? userId = null,
        Guid? groupId = null,
        string? fullName = null,
        DateTime? birthday = null,
        string? address = null,
        int givenIndex = 0,
        List<StudentTask>? tasks = null)
        => new()
        {
            StudentId = studentId ?? Constants.Student.StudentId,
            UserId = userId ?? Constants.Authentication.UserId,
            GroupId = groupId ?? Constants.Group.GroupId,
            FullName = fullName ?? Constants.Authentication.FullNameFromGiveIndex(givenIndex),
            Birthday = birthday ?? Constants.Authentication.BirthdayFromGivenIndex(givenIndex),
            Address = address ?? Constants.Authentication.AddressFromGivenIndex(givenIndex),
            Tasks = tasks ?? TaskFactory.CreateStudentTasksWithTaskObject()
        };
}