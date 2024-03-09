using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Task = System.Threading.Tasks.Task;
using Application.Features.Subjects.Queries.GetStudentSubjectsQuery;
using Application.UnitTests.TestUtils.TestConstants;
using Application.Common.Interfaces.Authentication;
using Application.UnitTests.TestUtils.Extensions;
using Application.Common.Interfaces.Persistence;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.Subjects.TestUtils;
using NSubstitute.ReturnsExtensions;
using FluentAssertions;
using Domain.Entities;
using Domain.Common;
using NSubstitute;

namespace Application.UnitTests.Subjects.Queries;

public class GetStudentSubjectsQueryHandlerTests
{
    private readonly GetStudentSubjectsQueryHandler _sut;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public GetStudentSubjectsQueryHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _sut = new GetStudentSubjectsQueryHandler(_unitOfWork, _jwtTokenReader);
    }

    [Theory]
    [MemberData(nameof(ValidRetrieveSubjectsData))]
    public async Task Handler_WhenTokenIsValidAndUserExists_ShouldReturnStudentSubjects(
        List<Subject> subjects)
    {
        // Arrange
        var query = GetStudentSubjectsQueryUtils.CreateGetStudentSubjectsQuery();

        _jwtTokenReader.ReadUserIdFromToken(query.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserId)
            .Returns(AuthenticationFactory.CreateStudentUser());

        _unitOfWork.Subjects.GetStudentSubjectsWithRelations(Constants.Group.GroupId)
            .Returns(subjects);

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateRetrievedStudentSubjects(subjects);
        await _unitOfWork.Subjects.Received(1)
            .GetStudentSubjectsWithRelations(Arg.Any<Guid>());
    }

    public static IEnumerable<object[]> ValidRetrieveSubjectsData()
    {
        yield return
        [
            SubjectFactory.CreateSubjects()
        ];

        yield return
        [
            SubjectFactory.CreateSubjects(
                tasks: TaskFactory.CreateTasks(tasksCount: 0))
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
        var query = GetStudentSubjectsQueryUtils.CreateGetStudentSubjectsQuery();

        _jwtTokenReader.ReadUserIdFromToken(query.Token)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidToken);
        await _unitOfWork.Subjects.Received(0)
            .GetStudentSubjectsWithRelations(Arg.Any<Guid>());
    }

    [Fact]
    public async Task Handler_WhenTokenIsValidButUserDoesNotExists_ShouldReturnUserNotFoundError()
    {
        // Arrange
        var query = GetStudentSubjectsQueryUtils.CreateGetStudentSubjectsQuery();

        _jwtTokenReader.ReadUserIdFromToken(query.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserId)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.UserNotFound);
        await _unitOfWork.Subjects.Received(0)
            .GetStudentSubjectsWithRelations(Arg.Any<Guid>());
    }
}