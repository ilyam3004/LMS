using System.Runtime.InteropServices.JavaScript;
using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Application.Common.Interfaces.Persistence;
using Application.Features.Tasks.Commands.ReturnTask;
using Application.UnitTests.Tasks.Commands.TestUtils;
using Application.UnitTests.TestUtils.TestConstants;
using Task = System.Threading.Tasks.Task;
using NSubstitute.ReturnsExtensions;
using FluentAssertions;
using Domain.Entities;
using Domain.Common;
using Domain.Enums;
using NSubstitute;

namespace Application.UnitTests.Tasks.Commands;

public class ReturnTaskCommandHandlerTests
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ReturnTaskCommandHandler _sut;

    public ReturnTaskCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _sut = new ReturnTaskCommandHandler(_unitOfWork);
    }

    [Fact]
    public async Task
        Handler_WhenStudentTaskExists_ShouldChangeTheStatusAndReloadFileDataAndGradeAndReturnLecturerTaskResult()
    {
        // Arrange
        var command = ReturnTaskCommandUtils.CreateReturnTaskCommand();

        _unitOfWork.StudentTasks.GetByIdAsync(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithoutTaskObject());

        _unitOfWork.Tasks.GetTaskByIdWithRelations(Constants.Task.TaskId)
            .Returns(TaskFactory.CreateTask());

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _unitOfWork.StudentTasks.Received(1).Update(Arg.Any<StudentTask>());
        _unitOfWork.Received(1).SaveChangesAsync();
    }


    [Fact]
    public async Task Handler_WhenStudentTaskDoesNotExists_ShouldReturnStudentTaskNotFoundError()
    {
        // Arrange
        var command = ReturnTaskCommandUtils.CreateReturnTaskCommand();

        _unitOfWork.StudentTasks.GetByIdAsync(command.StudentTaskId)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.StudentTaskNotFound);
        _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        _unitOfWork.Received(0).SaveChangesAsync();
    }


    [Fact]
    public async Task Handler_WhenStudentTaskIsNotUploaded_ShouldReturnTaskNotUploadedError()
    {
        // Arrange
        var command = ReturnTaskCommandUtils.CreateReturnTaskCommand();

        _unitOfWork.StudentTasks.GetByIdAsync(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithoutTaskObject(
                status: StudentTaskStatus.NotUploaded));

        _unitOfWork.Tasks.GetTaskByIdWithRelations(Constants.Task.TaskId)
            .Returns(TaskFactory.CreateTask());

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.StudentTaskNotUploaded);
        _unitOfWork.Tasks.Received(1).GetTaskByIdWithRelations(Arg.Any<Guid>());
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        _unitOfWork.Received(0).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenTaskNotExists_ShouldReturnTaskNotFoundError()
    {
        // Arrange
        var command = ReturnTaskCommandUtils.CreateReturnTaskCommand();

        _unitOfWork.StudentTasks.GetByIdAsync(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithoutTaskObject());

        _unitOfWork.Tasks.GetTaskByIdWithRelations(Constants.Task.TaskId)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.TaskNotFound);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        _unitOfWork.Received(0).SaveChangesAsync();
    }
}