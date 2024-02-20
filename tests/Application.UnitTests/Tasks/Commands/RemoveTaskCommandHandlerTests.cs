using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Features.Tasks.Commands.RemoveTask;
using Application.Models.Subjects;
using Application.UnitTests.Tasks.Commands.TestUtils;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.TestUtils.TestConstants;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Tasks.Commands;

public class RemoveTaskCommandHandlerTests
{
    private readonly RemoveTaskCommandHandler _sut;
    private readonly IUnitOfWork _mockUnitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public RemoveTaskCommandHandlerTests()
    {
        _mockUnitOfWork = Substitute.For<IUnitOfWork>();
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _sut = new RemoveTaskCommandHandler(_mockUnitOfWork, _jwtTokenReader);
    }

    [Fact]
    public async Task Handler_WhenTaskExists_ShouldRemoveTaskAndReturnLecturerSubject()
    {
        // Arrange
        var command = RemoveTaskCommandUtils.CreateRemoveTaskCommand();

        _mockUnitOfWork.Tasks.GetByIdAsync(command.TaskId)
            .Returns(TaskFactory.CreateTask());

        _mockUnitOfWork.Subjects.GetSubjectByIdWithRelations(Constants.Subject.SubjectId)
            .Returns(SubjectFactory.CreateSubject());

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<LecturerSubjectResult>();
        _mockUnitOfWork.Tasks.Received(1).Remove(Arg.Any<Domain.Entities.Task>());
        await _mockUnitOfWork.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenTaskDoesNotExists_ShouldReturnTaskNotFoundError()
    {
        // Arrange
        var command = RemoveTaskCommandUtils.CreateRemoveTaskCommand();

        _mockUnitOfWork.Tasks.GetByIdAsync(command.TaskId)
            .ReturnsNull();
        
        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        _mockUnitOfWork.Tasks.Received(0).Remove(Arg.Any<Domain.Entities.Task>());
        await _mockUnitOfWork.Received(0).SaveChangesAsync();
    }
}