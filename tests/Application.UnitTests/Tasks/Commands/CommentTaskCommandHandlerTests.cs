using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Application.Features.Tasks.Commands.CreateComment;
using Application.UnitTests.Tasks.Commands.TestUtils;
using Application.UnitTests.TestUtils.TestConstants;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Tasks;
using Application.Services;
using Application.UnitTests.TestUtils.Factories;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Task = System.Threading.Tasks.Task;

namespace Application.UnitTests.Tasks.Commands;

public class CommentTaskCommandHandlerTests
{
    private readonly CommentTaskCommandHandler _sut;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IDateTimeProvider _dateTimeProvider;

    public CommentTaskCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        _sut = new CommentTaskCommandHandler(_unitOfWork,
            _jwtTokenReader,
            _dateTimeProvider);
    }

    [Fact]
    public async Task
        Handler_WhenTokenIsValidAndUserExistsAndStudentTaskExists_ShouldAddCommentAndReturnUploadedTaskResult()
    {
        //Arrange
        var command = CommentTaskCommandUtils.CreateCommentTaskCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserId)
            .Returns(AuthenticationFactory.CreateLecturerUser());

        _unitOfWork.StudentTasks.GetByIdAsyncWithRelations(
            command.StudentTaskId).Returns(TaskFactory.CreateStudentTaskWithoutTaskObject());

        _dateTimeProvider.UtcNow.Returns(DateTime.Now);

        _unitOfWork.Tasks.GetTaskByIdWithRelations(Arg.Any<Guid>())
            .Returns(TaskFactory.CreateTask());

        //Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<UploadedTaskResult>();
        await _unitOfWork.Tasks.Received(1)
            .GetTaskByIdWithRelations(Arg.Any<Guid>());
        await _unitOfWork.GetRepository<TaskComment>().Received(1)
            .AddAsync(Arg.Any<TaskComment>());
        await _unitOfWork.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task
        Handler_WhenTokenIsValidAndUserExistsButStudentTaskDoesNotExists_ShouldReturnStudentTaskNotFoundError()
    {
        // Arrange
        var command = CommentTaskCommandUtils.CreateCommentTaskCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserId)
            .Returns(AuthenticationFactory.CreateLecturerUser());

        _unitOfWork.StudentTasks.GetByIdAsyncWithRelations(command.StudentTaskId)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.StudentTaskNotFound);
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.GetRepository<TaskComment>().Received(0)
            .AddAsync(Arg.Any<TaskComment>());
    }

    [Fact]
    public async Task Handler_WhenTokenIsValidAndUserDoesNotExists_ShouldReturnUserNotFoundError()
    {
        // Arrange
        var command = CommentTaskCommandUtils.CreateCommentTaskCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserId)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.UserNotFound);
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.GetRepository<TaskComment>().Received(0)
            .AddAsync(Arg.Any<TaskComment>());
    }

    [Fact]
    public async Task Handler_WhenTokenIsInvalid_ShouldReturnInvalidTokenError()
    {
        // Arrange
        var command = CommentTaskCommandUtils.CreateCommentTaskCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidToken);
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.GetRepository<TaskComment>()
            .Received(0).AddAsync(Arg.Any<TaskComment>()); 
    }
}