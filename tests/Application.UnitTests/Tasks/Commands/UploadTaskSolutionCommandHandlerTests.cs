using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Features.Tasks.Commands.UploadTaskSolution;
using Application.Models.Tasks;
using Application.UnitTests.Tasks.Commands.TestUtils;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.TestUtils.TestConstants;
using Task = System.Threading.Tasks.Task;
using Application.Services;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Tasks.Commands;

public class UploadTaskSolutionCommandHandlerTests
{
    private readonly UploadTaskSolutionCommandHandler _sut;
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IFileUploader _fileUploader;
    private readonly IUnitOfWork _unitOfWork;

    public UploadTaskSolutionCommandHandlerTests()
    {
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        _fileUploader = Substitute.For<IFileUploader>();
        _sut = new UploadTaskSolutionCommandHandler(_unitOfWork,
            _dateTimeProvider,
            _jwtTokenReader,
            _fileUploader);
    }

    [Fact]
    public async Task
        Handler_WhenTokenIsValidAndUserExistsAndStudentTaskExistsAndTaskStatusIsNotRejectedOrUploadedOrAcceptedAndFileLengthIsNotZero_ShouldUploadStudentTaskAndUploadTaskFile()
    {
        //Arrange
        var command = UploadTaskSolutionCommandUtils.CreateUploadTaskSolutionCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .Returns(AuthenticationFactory.CreateStudentUser());

        _unitOfWork.StudentTasks.GetByIdAsync(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithTaskObject(
                status: StudentTaskStatus.NotUploaded));

        _fileUploader.UploadFileAndGetFilePath(command.File)
            .Returns(Constants.File.FileUrl);

        _unitOfWork.Tasks.GetTaskByIdWithRelations(Arg.Any<Guid>())
            .Returns(TaskFactory.CreateTask());

        //Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<StudentTaskResult>();
        _unitOfWork.StudentTasks.Received(1).Update(Arg.Any<StudentTask>());
        await _fileUploader.Received(1).UploadFileAndGetFilePath(command.File);
        await _unitOfWork.Received(1).SaveChangesAsync();
        await _unitOfWork.Tasks.Received(1).GetTaskByIdWithRelations(Arg.Any<Guid>());
    }
    
    [Fact]
    public async Task
        Handler_WhenTokenIsValidAndUserExistsAndStudentTaskExistsAndTaskStatusIsNotRejectedOrNotUploadedOrNotAcceptedButFileLengthIsZero_ShouldReturnFileNotFoundError()
    {
        //Arrange
        var command = UploadTaskSolutionCommandUtils.CreateUploadTaskSolutionCommand(
            fileLength: Constants.File.EmptyFileLength);

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .Returns(AuthenticationFactory.CreateStudentUser());

        _unitOfWork.StudentTasks.GetByIdAsync(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithTaskObject(
                status: StudentTaskStatus.NotUploaded));
        
        //Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.File.FileNotFound);
        await _fileUploader.Received(0).UploadFileAndGetFilePath(command.File);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
    }
    
    [Theory]
    [InlineData(StudentTaskStatus.Rejected)]
    [InlineData(StudentTaskStatus.Uploaded)]
    [InlineData(StudentTaskStatus.Accepted)]
    public async Task
        Handler_WhenTokenIsValidAndUserExistsAndStudentTaskExistsButTaskStatusIsRejectedOrUploadedOrAccepted_ShouldReturnInvalidTaskStatusError(
            StudentTaskStatus status)
    {
        //Arrange
        var command = UploadTaskSolutionCommandUtils.CreateUploadTaskSolutionCommand();

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
        await _fileUploader.Received(0).UploadFileAndGetFilePath(command.File);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
    }
    
    [Fact]
    public async Task
        Handler_WhenTokenIsValidAndUserExistsAndStudentTaskDoesNotExists_ShouldReturnStudentTaskNotFoundError()
    {
        //Arrange
        var command = UploadTaskSolutionCommandUtils.CreateUploadTaskSolutionCommand();

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
        await _fileUploader.Received(0).UploadFileAndGetFilePath(command.File);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
    }
    
    [Fact]
    public async Task Handler_WhenTokenIsValidAndUserDoesNotExists_ShouldReturnUserNotFoundError()
    {
        //Arrange
        var command = UploadTaskSolutionCommandUtils.CreateUploadTaskSolutionCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .ReturnsNull();
        
        //Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.UserNotFound);
        await _fileUploader.Received(0).UploadFileAndGetFilePath(command.File);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
    }
    
    
    [Fact]
    public async Task Handler_WhenTokenIsInvalid_ShouldReturnInvalidTokenError()
    {
        //Arrange
        var command = UploadTaskSolutionCommandUtils.CreateUploadTaskSolutionCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .ReturnsNull();
        
        //Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidToken);
        await _fileUploader.Received(0).UploadFileAndGetFilePath(command.File);
        _unitOfWork.StudentTasks.Received(0).Update(Arg.Any<StudentTask>());
        await _unitOfWork.Received(0).SaveChangesAsync();
        await _unitOfWork.Tasks.Received(0).GetTaskByIdWithRelations(Arg.Any<Guid>());
    }
}