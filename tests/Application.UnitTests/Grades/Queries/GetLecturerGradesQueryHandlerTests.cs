using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Application.UnitTests.Grades.Queries.TestUtils;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.UnitTests.TestUtils.TestConstants;
using Application.UnitTests.TestUtils.Factories;
using Task = System.Threading.Tasks.Task;
using Application.Features.Grades.Queries;
using Application.UnitTests.TestUtils.Extensions;
using NSubstitute.ReturnsExtensions;
using FluentAssertions;
using Domain.Common;
using Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.Grades.Queries;

public class GetLecturerGradesQueryHandlerTests
{
    private readonly GetLecturerGradesQueryHandler _sut;
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IUnitOfWork _unitOfWork;

    public GetLecturerGradesQueryHandlerTests()
    {
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _sut = new GetLecturerGradesQueryHandler(_unitOfWork, _jwtTokenReader);
    }

    [Fact]
    public async Task Handle_WhenTokenIsInvalid_ShouldReturnInvalidTokenError()
    {
        // Arrange
        var query = GetLecturerGradesQueryUtils.CreateGetLecturerGradesQuery();
        _jwtTokenReader.ReadUserIdFromToken(query.Token).ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidToken);
    }
    
    [Fact]
    public async Task Handle_WhenUserDoesNotExists_ShouldReturnUserNotFoundError()
    {
        // Arrange
        var query = GetLecturerGradesQueryUtils.CreateGetLecturerGradesQuery();
        _jwtTokenReader.ReadUserIdFromToken(query.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.UserNotFound);
    }
    
    [Theory]
    [MemberData(nameof(ValidRetrieveSubjectsData))]
    public async Task Handle_WhenTokenIsValidAndUserExists_ShouldReturnLecturerGradesResults(
        List<Subject> subjects)
    {
        // Arrange
        var query = GetLecturerGradesQueryUtils.CreateGetLecturerGradesQuery();
        _jwtTokenReader.ReadUserIdFromToken(query.Token)
            .Returns(Constants.Authentication.UserId.ToString());
    
        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .Returns(AuthenticationFactory.CreateLecturerUser());
    
        _unitOfWork.Subjects.GetLecturerSubjects(Arg.Any<Guid>())
            .Returns(subjects);
        
        // Act
        var result = await _sut.Handle(query, default);
    
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateRetrievedSubjectsData(subjects);
        
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
}