using Application.Features.Tasks.Commands.RemoveTask;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Tasks.Commands.TestUtils;

public static class RemoveTaskCommandUtils
{
    public static RemoveTaskCommand CreateRemoveTaskCommand()
        => new RemoveTaskCommand(Constants.Task.TaskId);
}