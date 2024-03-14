using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Task = System.Threading.Tasks.Task;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Features.Tasks.Queries.GetStudentTask;
using Application.UnitTests.Tasks.Queries.TestUtils;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.TestUtils.TestConstants;
using Domain.Common;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Tasks.Queries;

public class GetStudentTaskQueryHandlerTests
{
    private readonly GetStudentTaskQueryHandler _sut;
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IUnitOfWork _unitOfWork;

    public GetStudentTaskQueryHandlerTests()
    {
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _sut = new GetStudentTaskQueryHandler(_jwtTokenReader, _unitOfWork);
    }

    [Fact]
    public async Task Handle_WhenTokenIsInvalid_ShouldReturnInvalidTokenError()
    {
        // Arrange
        var query = GetStudentTaskQueryUtils.CreateGetStudentTaskQuery();

        _jwtTokenReader.ReadUserIdFromToken(query.Token).ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidToken);
    }

    [Fact]
    public async Task Handle_WhenUserNotFound_ShouldReturnUserNotFoundError()
    {
        // Arrange
        var query = GetStudentTaskQueryUtils.CreateGetStudentTaskQuery();

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

    [Fact]
    public async Task Handle_WhenTaskNotFound_ShouldReturnTaskNotFoundError()
    {
        // Arrange
        var query = GetStudentTaskQueryUtils.CreateGetStudentTaskQuery();

        _jwtTokenReader.ReadUserIdFromToken(query.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .Returns(AuthenticationFactory.CreateStudentUser());

        _unitOfWork.Tasks.GetTaskByIdWithRelations(query.TaskId)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.TaskNotFound);
    }

    [Fact]
    public async Task Handle_WhenTaskAndUseExists_ShouldReturnStudentTaskResult()
    {
        // Arrange
        var query = GetStudentTaskQueryUtils.CreateGetStudentTaskQuery();

        _jwtTokenReader.ReadUserIdFromToken(query.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .Returns(AuthenticationFactory.CreateStudentUser());

        _unitOfWork.Tasks.GetTaskByIdWithRelations(query.TaskId)
            .Returns(TaskFactory.CreateTask());
        
        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Task.Should().NotBeNull();
        result.Value.UploadedTask.Should().NotBeNull();
    }
}