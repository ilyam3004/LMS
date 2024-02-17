using System.Runtime.InteropServices.JavaScript;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Features.Tasks.Commands.CreateTask;
using Application.Services;
using Application.UnitTests.Tasks.Commands.TestUtils;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.TestUtils.TestConstants;
using Domain.Common;
using FluentAssertions;
using NSubstitute;
using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;

namespace Application.UnitTests.Tasks.Commands;

public class AssignTaskCommandHandlerTests
{
    private readonly AssignTaskCommandHandler _sut;
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTimeProvider _dateTimeProvider;

    public AssignTaskCommandHandlerTests()
    {
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _dateTimeProvider = Substitute.For<IDateTimeProvider>();
        _sut = new AssignTaskCommandHandler(_jwtTokenReader, _unitOfWork, _dateTimeProvider);
    }

    [Fact]
    public async Task
        Handler_WhenSubjectExists_ShouldAddAddNewTaskAddStudentTaskForEachStudentOfTheGroupAndReturnLecturerSubjectResult()
    {
        // Arrange
        var command = AssignTaskCommandUtils.CreateAssignTaskCommand();

        _unitOfWork.Subjects.SubjectExists(command.SubjectId)
            .Returns(true);

        _unitOfWork.Tasks.GetTaskByIdWithRelations(Arg.Any<Guid>())
            .Returns(TaskFactory.CreateTask());

        _unitOfWork.Subjects.GetSubjectByIdWithRelations(command.SubjectId)
            .Returns(SubjectFactory.CreateSubject());

        _unitOfWork.Groups.GetGroupByIdWithStudents(Constants.Group.GroupId)
            .Returns(GroupFactory.CreateGroupWithOutSubjects());

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        await _unitOfWork.Tasks.Received(1).AddAsync(Arg.Any<Domain.Entities.Task>());
        await _unitOfWork.Received(2).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenSubjectDoesNotExists_ShouldReturnSubjectNotFoundError()
    {
        // Arrange
        var command = AssignTaskCommandUtils.CreateAssignTaskCommand();

        _unitOfWork.Subjects.SubjectExists(command.SubjectId)
            .Returns(false);

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Subject.SubjectNotFound);
        await _unitOfWork.Tasks.Received(0).AddAsync(Arg.Any<Domain.Entities.Task>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }
}