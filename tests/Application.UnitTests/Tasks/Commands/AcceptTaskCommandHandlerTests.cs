using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Application.Features.Tasks.Commands.AcceptTask;
using Application.UnitTests.Tasks.Commands.TestUtils;
using Application.Common.Interfaces.Persistence;
using Application.UnitTests.TestUtils.TestConstants;
using Task = System.Threading.Tasks.Task;
using NSubstitute.ReturnsExtensions;
using Application.Models.Tasks;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Tasks.Commands;

public class AcceptTaskCommandHandlerTests
{
    private readonly AcceptTaskCommandHandler _sut;
    private readonly IUnitOfWork _unitOfWork;

    public AcceptTaskCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _sut = new AcceptTaskCommandHandler(_unitOfWork);
    }

    [Fact]
    public async Task Handler_WhenStudentTaskExists_ShouldChangeTheStatusOfTheStudentTask()
    {
        // Arrange
        var command = AcceptTaskCommandUtils.CreateAcceptTaskCommand();

        _unitOfWork.StudentTasks.GetByIdAsyncWithRelations(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithTaskObject());

        _unitOfWork.Tasks.GetTaskByIdWithRelations(Constants.Task.TaskId)
            .Returns(TaskFactory.CreateTask());

        // Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<LecturerTaskResult>();
        _unitOfWork.StudentTasks.Received(1).Update(Arg.Any<StudentTask>());
        await _unitOfWork.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenStudentTaskDoesNotExists_ShouldReturnStudentTaskNotFoundError()
    {
        // Arrange
        var command = AcceptTaskCommandUtils.CreateAcceptTaskCommand();

        _unitOfWork.StudentTasks.GetByIdAsyncWithRelations(command.StudentTaskId)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.StudentTaskNotFound);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }


    [Fact]
    public async Task Handler_WhenStudentTaskNotUploaded_ShouldReturnStudentTaskNotUploadedError()
    {
        // Arrange
        var command = AcceptTaskCommandUtils.CreateAcceptTaskCommand();

        _unitOfWork.StudentTasks.GetByIdAsyncWithRelations(command.StudentTaskId)
            .Returns(TaskFactory
                .CreateStudentTaskWithTaskObject(
                    status: StudentTaskStatus.NotUploaded));

        // Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.StudentTaskNotUploaded);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenGradeTooHigh_ShouldReturnGradeTooHighError()
    {
        var command = AcceptTaskCommandUtils.CreateAcceptTaskCommand(
            grade: Constants.Task.TooHighGrade);

        _unitOfWork.StudentTasks.GetByIdAsyncWithRelations(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithTaskObject());

        // Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.GradeTooHigh);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }
}