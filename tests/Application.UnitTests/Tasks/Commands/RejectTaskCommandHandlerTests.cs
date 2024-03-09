using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Application.Features.Tasks.Commands.RejectTask;
using Application.UnitTests.Tasks.Commands.TestUtils;
using Application.Common.Interfaces.Persistence;
using Application.Models.Tasks;
using Application.UnitTests.TestUtils.TestConstants;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Task = System.Threading.Tasks.Task;

namespace Application.UnitTests.Tasks.Commands;

public class RejectTaskCommandHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly RejectTaskCommandHandler _sut;

    public RejectTaskCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _sut = new RejectTaskCommandHandler(_unitOfWork);
    }

    [Fact]
    public async Task
        Handler_WhenStudentTaskExistsAndStatusIsNotUploadedAndDeadlineIsExpired_ShouldChangeTheStatusAndReturnLecturerTaskResult()
    {
        // Arrange
        var command = RejectTaskCommandUtils.CreateRejectTaskCommand();

        _unitOfWork.StudentTasks.GetByIdAsyncWithRelations(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithTaskObject(
                deadline: Constants.Task.ExpiredDeadline,
                status: StudentTaskStatus.NotUploaded));

        _unitOfWork.Tasks.GetTaskByIdWithRelations(Constants.Task.TaskId)
            .Returns(TaskFactory.CreateTask());

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<LecturerTaskResult>();
        _unitOfWork.StudentTasks.Received(1).Update(Arg.Any<StudentTask>());
        _unitOfWork.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task
        Handler_WhenStudentTaskExistsAndDeadlineIsExpiredButStatusIsNotNotUploaded_ShouldReturnWrongStatusError()
    {
        // Arrange
        var command = RejectTaskCommandUtils.CreateRejectTaskCommand();

        _unitOfWork.StudentTasks.GetByIdAsyncWithRelations(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithTaskObject(
                deadline: Constants.Task.ExpiredDeadline,
                status: StudentTaskStatus.Uploaded));

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.WrongTaskStatus);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        _unitOfWork.Received(0).SaveChangesAsync();
    }


    [Fact]
    public async Task Handler_WhenStudentTaskExistsButDeadlineIsNotExpired_ShouldReturnDeadlineIsNotExpiredError()
    {
        // Arrange
        var command = RejectTaskCommandUtils.CreateRejectTaskCommand();

        _unitOfWork.StudentTasks.GetByIdAsyncWithRelations(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithTaskObject(
                deadline: Constants.Task.NotExpiredDeadline,
                status: StudentTaskStatus.NotUploaded));

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.TaskDeadlineNotExpired);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        _unitOfWork.Received(0).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenStudentTaskDoesNotExists_ShouldReturnStudentTaskNotFound()
    {
        // Arrange
        var command = RejectTaskCommandUtils.CreateRejectTaskCommand();

        _unitOfWork.StudentTasks.GetByIdAsyncWithRelations(command.StudentTaskId)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.StudentTaskNotFound);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        _unitOfWork.Received(0).SaveChangesAsync();
    }
}