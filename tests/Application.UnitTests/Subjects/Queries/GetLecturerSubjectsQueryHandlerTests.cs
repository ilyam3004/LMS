using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Task = System.Threading.Tasks.Task;
using NSubstitute.ReturnsExtensions;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Features.Subjects.Queries.GetLecturerSubjects;
using Application.UnitTests.Subjects.TestUtils;
using Application.UnitTests.TestUtils.Extensions;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.TestUtils.TestConstants;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Subjects.Queries;

public class GetLecturerSubjectsQueryHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly GetLecturerSubjectQueryHandler _sut;
    private readonly IJwtTokenReader _jwtTokenReader;

    public GetLecturerSubjectsQueryHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _sut = new GetLecturerSubjectQueryHandler(_unitOfWork, _jwtTokenReader);
    }

    [Theory]
    [MemberData(nameof(ValidRetrieveSubjectsData))]
    public async Task Handler_WhenTokenIsValidAndUserExists_ShouldReturnLecturerSubjects(
        List<Subject> subjects)
    {
        // Arrange
        var query = GetLecturerSubjectsQueryUtils.CreateGetLecturerSubjectsQuery();

        _jwtTokenReader.ReadUserIdFromToken(query.Token).Returns(
            Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserId)
            .Returns(AuthenticationFactory.CreateLecturerUser());

        _unitOfWork.Subjects.GetLecturerSubjects(Constants.Lecturer.LecturerId)
            .Returns(subjects);

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateRetrievedLecturerSubjects(subjects);
    }

    public static IEnumerable<object[]> ValidRetrieveSubjectsData()
    {
        yield return
        [
            SubjectFactory.CreateSubjects()
        ];

        yield return
        [
            SubjectFactory.CreateSubjects(subjectsCount: 4,
                tasks: TaskFactory.CreateTasks(tasksCount: 4))
        ];

        yield return
        [
            SubjectFactory.CreateSubjects(subjectsCount: 8,
                tasks: TaskFactory.CreateTasks(tasksCount: 10))
        ];
    }

    [Fact]
    public async Task Handler_WhenTokenIsInvalid_ShouldReturnInvalidTokenError()
    {
        // Arrange
        var query = GetLecturerSubjectsQueryUtils.CreateGetLecturerSubjectsQuery();

        _jwtTokenReader.ReadUserIdFromToken(query.Token).ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidToken);
    }

    [Fact]
    public async Task Handler_WhenUserNotExists_ShouldReturnUserNotFoundError()
    {
        // Arrange
        var query = GetLecturerSubjectsQueryUtils.CreateGetLecturerSubjectsQuery();

        _jwtTokenReader.ReadUserIdFromToken(query.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserId)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.UserNotFound);
    }
}