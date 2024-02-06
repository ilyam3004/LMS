using Application.UnitTests.TestUtils.TestConstants;
using Application.Models.Authentication;
using FluentAssertions;

namespace Application.UnitTests.TestUtils.Authentication.Extensions;

public static class AuthenticationsExtensions
{
    public static void ValidateRetrievedStudentProfile(this ProfileResult result)
    {
        result.User.UserId.Should().Be(Constants.Authentication.UserIdFromToken);
        result.User.Email.Should().Be(Constants.Authentication.Email);
        result.User.Password.Should().Be(Constants.Authentication.ValidPassword);

        result.User.Student.Should().NotBeNull();
        result.User.Lecturer.Should().BeNull();

        result.User.Student!.StudentId.Should().Be(Constants.Authentication.StudentId);
        result.User.Student!.GroupId.Should().Be(Constants.Group.GroupId);
        result.User.Student!.FullName.Should().Be(Constants.Authentication.FullName);
        result.User.Student!.Address.Should().Be(Constants.Authentication.Address);
        result.User.Student!.Birthday.Should().Be(Constants.Authentication.Birthday);

        result.User.Student!.Group.Should().NotBeNull();
        result.User.Student!.Group!.GroupId.Should().Be(Constants.Group.GroupId);
        result.User.Student!.Group!.Name.Should().Be(Constants.Group.GroupName);
        result.User.Student!.Group!.Course.Should().Be(Constants.Group.Course);
        result.User.Student!.Group.Department.Should().Be(Constants.Group.Department);
    }

    public static void ValidateRetrievedLecturerProfile(this ProfileResult result)
    {
        result.User.UserId.Should().Be(Constants.Authentication.UserIdFromToken);
        result.User.Email.Should().Be(Constants.Authentication.Email);
        result.User.Password.Should().Be(Constants.Authentication.ValidPassword);

        result.User.Student.Should().BeNull();
        result.User.Lecturer.Should().NotBeNull();

        result.User.Lecturer!.LecturerId.Should().Be(Constants.Authentication.LecturerId);
        result.User.Lecturer!.FullName.Should().Be(Constants.Authentication.FullName);
        result.User.Lecturer!.Address.Should().Be(Constants.Authentication.Address);
        result.User.Lecturer!.Degree.Should().Be(Constants.Authentication.Degree);
        result.User.Lecturer!.Birthday.Should().Be(Constants.Authentication.Birthday);
    }

    public static void ValidateRetrievedStudentUser(this AuthenticationResult result)
    {
        result.User.UserId.Should().Be(Constants.Authentication.UserId);
        result.User.Email.Should().Be(Constants.Authentication.Email);

        result.User.Student.Should().NotBeNull();
        result.User.Lecturer.Should().BeNull();

        result.User.Student!.StudentId.Should().Be(Constants.Authentication.StudentId);
        result.User.Student!.GroupId.Should().Be(Constants.Group.GroupId);
        result.User.Student!.FullName.Should().Be(Constants.Authentication.FullName);
        result.User.Student!.Address.Should().Be(Constants.Authentication.Address);
        result.User.Student!.Birthday.Should().Be(Constants.Authentication.Birthday);
    }


    public static void ValidateRetrievedLecturerUser(this AuthenticationResult result)
    {
        result.User.UserId.Should().Be(Constants.Authentication.UserId);
        result.User.Email.Should().Be(Constants.Authentication.Email);

        result.User.Student.Should().BeNull();
        result.User.Lecturer.Should().NotBeNull();

        result.User.Lecturer!.LecturerId.Should().Be(Constants.Authentication.LecturerId);
        result.User.Lecturer!.FullName.Should().Be(Constants.Authentication.FullName);
        result.User.Lecturer!.Address.Should().Be(Constants.Authentication.Address);
        result.User.Lecturer!.Birthday.Should().Be(Constants.Authentication.Birthday);
    }
}