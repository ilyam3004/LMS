using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Features.Tasks.Commands.RemoveTask;
using Application.UnitTests.Tasks.Commands.TestUtils;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.TestUtils.TestConstants;
using NSubstitute.ReturnsExtensions;
using Application.Models.Subjects;
using Domain.Common;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Tasks.Commands;

public class RemoveTaskCommandHandlerTests
{
    private readonly RemoveTaskCommandHandler _sut;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public RemoveTaskCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _sut = new RemoveTaskCommandHandler(_unitOfWork, _jwtTokenReader);
    }

    [Fact]
    public async Task Handler_WhenTaskExists_ShouldRemoveTaskAndReturnLecturerSubject()
    {
        // Arrange
        var command = RemoveTaskCommandUtils.CreateRemoveTaskCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token).Returns(
            Constants.Authentication.UserIdFromToken.ToString());
        
        _unitOfWork.Users.UserExistsById(Constants.Authentication.UserIdFromToken)
            .Returns(true);

        _unitOfWork.Tasks.GetByIdAsync(command.TaskId)
            .Returns(TaskFactory.CreateTask());

        _unitOfWork.Subjects.GetSubjectByIdWithRelations(Constants.Subject.SubjectId)
            .Returns(SubjectFactory.CreateSubject());

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<LecturerSubjectResult>();
        _unitOfWork.Tasks.Received(1).Remove(Arg.Any<Domain.Entities.Task>());
        await _unitOfWork.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenTaskDoesNotExists_ShouldReturnTaskNotFoundError()
    {
        // Arrange
        var command = RemoveTaskCommandUtils.CreateRemoveTaskCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token).Returns(
            Constants.Authentication.UserIdFromToken.ToString());
        
        _unitOfWork.Users.UserExistsById(Constants.Authentication.UserIdFromToken)
            .Returns(true);

        _unitOfWork.Tasks.GetByIdAsync(command.TaskId)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        _unitOfWork.Tasks.Received(0).Remove(Arg.Any<Domain.Entities.Task>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenUserDoesNotExists_ShouldReturnUserNotFoundError()
    {
        // Arrange
        var command = RemoveTaskCommandUtils.CreateRemoveTaskCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token).Returns(
            Constants.Authentication.UserIdFromToken.ToString());

        _unitOfWork.Users.UserExistsById(Constants.Authentication.UserIdFromToken)
            .Returns(false);
        
        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.User.UserNotFound);
        _unitOfWork.Tasks.Received(0).Remove(Arg.Any<Domain.Entities.Task>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }
    
    
    [Fact]
    public async Task Handler_WhenTokenIsInvalid_ShouldReturnInvalidTokenError()
    {
        // Arrange
        var command = RemoveTaskCommandUtils.CreateRemoveTaskCommand();

        _jwtTokenReader.ReadUserIdFromToken(command.Token).ReturnsNull();

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.User.InvalidToken);
        _unitOfWork.Tasks.Received(0).Remove(Arg.Any<Domain.Entities.Task>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }
}