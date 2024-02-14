using Application.UnitTests.TestUtils.TestConstants;
using Domain.Entities;

namespace Application.UnitTests.TestUtils.Factories;

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

    public static User CreateUserWithoutLectureOrStudentObjects(Guid? userId = null,
        string? email = null,
        string? password = null)
        => new User
        {
            UserId = userId ?? Constants.Authentication.UserId,
            Email = email ?? Constants.Authentication.Email,
            Password = password ?? Constants.Authentication.InvalidPassword
        };

    public static Lecturer CreateLecturer(Guid? userId = null,
        Guid? lecturerId = null)
        => new Lecturer
        {
            LecturerId = lecturerId ?? Constants.Lecturer.LecturerId,
            UserId = userId ?? Constants.Authentication.UserId,
            FullName = Constants.Authentication.FullName,
            Degree = Constants.Lecturer.Degree,
            Birthday = Constants.Authentication.Birthday,
            Address = Constants.Authentication.Address,
        };

    public static Student CreateStudent(Guid userId)
    {
        var groupId = Constants.Group.GroupId;

        return new Student
        {
            StudentId = Constants.Student.StudentId,
            UserId = userId,
            GroupId = groupId,
            FullName = Constants.Authentication.FullName,
            Birthday = Constants.Authentication.Birthday,
            Address = Constants.Authentication.Address,
            Group = GroupFactory.CreateGroupWithSubjects(groupId)
        };
    }
}