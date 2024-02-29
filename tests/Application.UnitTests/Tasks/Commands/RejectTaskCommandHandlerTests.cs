using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Application.Features.Tasks.Commands.RejectTask;
using Application.UnitTests.Tasks.Commands.TestUtils;
using Application.Common.Interfaces.Persistence;
using Application.UnitTests.TestUtils.TestConstants;
using NSubstitute;

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
    public async Task Handler_WhenStudentTaskExists_ShouldChangeTheStatusAndReturnLecturerTaskResult()
    {
        // Arrange
        var command = RejectTaskCommandUtils.CreateRejectTaskCommand();

        _unitOfWork.StudentTasks.GetByIdAsync(command.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithTaskObject());

        _unitOfWork.Tasks.GetTaskByIdWithRelations(Constants.Task.TaskId)
            .Returns(TaskFactory.CreateTask());
        // Act

        // Assert
    }
}