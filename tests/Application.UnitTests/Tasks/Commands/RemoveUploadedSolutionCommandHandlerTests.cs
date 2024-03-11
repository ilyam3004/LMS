using Application.Features.Tasks.Commands.RemoveUploadedSolution;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Services;
using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Application.UnitTests.Tasks.Commands.TestUtils;
using Application.UnitTests.TestUtils.Extensions;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.TestUtils.TestConstants;
using Domain.Common;
using Task = System.Threading.Tasks.Task;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Tasks.Commands;

public class RemoveUploadedSolutionCommandHandlerTests
{
    private readonly RemoveUploadedSolutionCommandHandler _sut;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IFileManager _fileManager;

    public RemoveUploadedSolutionCommandHandlerTests()
    {
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _fileManager = Substitute.For<IFileManager>();
        _sut = new RemoveUploadedSolutionCommandHandler(
            _unitOfWork, _jwtTokenReader, _fileManager);
    }


    [Fact]
    public async Task
        Handler_WhenTokenIsValidAndUserExistsAndStudentTaskExistsAndTaskStatusIsUploadedAndFileExists_ShouldChangeStudentTaskAndRemoveUploadedSolution()
    {
        //Arrange
        var command = RemoveUploadedSolutionCommandUtils
            .CreateRemoveUploadedSolutionCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .Returns(AuthenticationFactory.CreateStudentUser());

        _unitOfWork.StudentTasks.GetByIdAsync(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithTaskObject(
                status: StudentTaskStatus.Uploaded));

        _fileManager.FileExists(Constants.File.FileUrl)
            .Returns(true);

        _unitOfWork.Tasks.GetTaskByIdWithRelations(Arg.Any<Guid>())
            .Returns(TaskFactory.CreateTask(
                studentTasks:
                [
                    TaskFactory.CreateStudentTaskWithoutTaskObject(
                        status: StudentTaskStatus.NotUploaded)
                ]));

        //Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateRetrieveStudentTask();
        _unitOfWork.StudentTasks.Received(1).Update(Arg.Any<StudentTask>());
        await _fileManager.Received(1).RemoveFile(Constants.File.FileUrl);
        await _unitOfWork.Received(1).SaveChangesAsync();
        await _unitOfWork.Tasks.Received(1).GetTaskByIdWithRelations(Arg.Any<Guid>());
    }

    [Fact]
    public async Task
        Handler_WhenTokenIsValidAndUserExistsAndStudentTaskExistsAndTaskStatusIsUploadedButFileDoesNotExists_ShouldReturnFileNotFoundError()
    {
        //Arrange
        var command = RemoveUploadedSolutionCommandUtils
            .CreateRemoveUploadedSolutionCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .Returns(AuthenticationFactory.CreateStudentUser());

        _unitOfWork.StudentTasks.GetByIdAsync(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithTaskObject(
                status: StudentTaskStatus.Uploaded));

        _fileManager.FileExists(Constants.File.FileUrl)
            .Returns(false);

        //Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.File.FileNotFound);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _fileManager.Received(0).RemoveFile(Constants.File.FileUrl);
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
    }

    [Theory]
    [InlineData(StudentTaskStatus.Rejected)]
    [InlineData(StudentTaskStatus.NotUploaded)]
    [InlineData(StudentTaskStatus.Accepted)]
    [InlineData(StudentTaskStatus.Returned)]
    public async Task Handler_WhenTokenIsValidAndUserExistsAndStudentTaskExistsButTaskStatusIsNotUploaded_ShouldReturnWrongTaskStatusError(
            StudentTaskStatus status)
    {
        //Arrange
        var command = RemoveUploadedSolutionCommandUtils
            .CreateRemoveUploadedSolutionCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .Returns(AuthenticationFactory.CreateStudentUser());

        _unitOfWork.StudentTasks.GetByIdAsync(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithTaskObject(
                status: status));

        //Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.WrongTaskStatus);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _fileManager.Received(0).RemoveFile(Constants.File.FileUrl);
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
    }
    
    [Fact]
    public async Task Handler_WhenTokenIsValidAndUserExistsButStudentTaskDoesNotExists_ShouldReturnStudentTaskNotFoundError()
    {
        //Arrange
        var command = RemoveUploadedSolutionCommandUtils
            .CreateRemoveUploadedSolutionCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .Returns(AuthenticationFactory.CreateStudentUser());

        _unitOfWork.StudentTasks.GetByIdAsync(command.StudentTaskId)
            .ReturnsNull();
        
        //Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.StudentTaskNotFound);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _fileManager.Received(0).RemoveFile(Constants.File.FileUrl);
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
    }
    
    
    [Fact]
    public async Task Handler_WhenTokenIsValidButUserDoesNotExists_ShouldReturnUserNotFoundError()
    {
        //Arrange
        var command = RemoveUploadedSolutionCommandUtils
            .CreateRemoveUploadedSolutionCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .ReturnsNull();

        //Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.UserNotFound);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _fileManager.Received(0).RemoveFile(Constants.File.FileUrl);
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
    }
    
    [Fact]
    public async Task Handler_WhenTokenIsInvalid_ShouldReturnInvalidTokenError()
    {
        //Arrange
        var command = RemoveUploadedSolutionCommandUtils
            .CreateRemoveUploadedSolutionCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .ReturnsNull();
        
        //Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidToken);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _fileManager.Received(0).RemoveFile(Constants.File.FileUrl);
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
    }
}