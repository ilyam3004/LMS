using Application.UnitTests.TestUtils.TestConstants;
using Domain.Entities;

namespace Application.UnitTests.TestUtils.Authentication;

public static class AuthenticationFactory
{
    public static User CreateLecturerUser(Guid? userId = null,
        string? email = null,
        string? password = null,
        Lecturer? lecturer = null)
    {
        Guid generatedUserId = userId ?? Constants.Authentication.UserId;

        return new User
        {
            UserId = generatedUserId,
            Email = email ?? Constants.Authentication.Email,
            Password = password ?? Constants.Authentication.ValidPassword,
            Lecturer = lecturer ?? CreateLecturer(generatedUserId)
        };
    }

    public static User CreateStudentUser(Guid? userId = null,
        string? email = null,
        string? password = null,
        Student? student = null)
    {
        Guid generatedUserId = userId ?? Constants.Authentication.UserId;
        
        return new User
        {
            UserId = generatedUserId,
            Email = email ?? Constants.Authentication.Email,
            Password = password ?? Constants.Authentication.ValidPassword,
            Student = student ?? CreateStudent(generatedUserId)
        };
    }

    public static Lecturer CreateLecturer(Guid userId)
        => new Lecturer
        {
            LecturerId = Constants.Authentication.LecturerId,
            UserId = userId,
            FullName = Constants.Authentication.FullName,
            Degree = Constants.Authentication.Degree,
            Birthday = Constants.Authentication.Birthday,
            Address = Constants.Authentication.Address,
        };

    public static Student CreateStudent(Guid userId)
    {
        var groupId = Constants.Group.GroupId;

        return new Student
        {
            StudentId = Constants.Authentication.StudentId,
            UserId = userId,
            GroupId = groupId,
            FullName = Constants.Authentication.FullName,
            Birthday = Constants.Authentication.Birthday,
            Address = Constants.Authentication.Address,
            Group = CreateGroup(groupId)
        };
    }

    public static Group CreateGroup(Guid groupId)
        => new Group
        {
            GroupId = groupId,
            Name = Constants.Group.GroupName,
            Department = Constants.Group.Department,
            Course = Constants.Group.Course
        };
}