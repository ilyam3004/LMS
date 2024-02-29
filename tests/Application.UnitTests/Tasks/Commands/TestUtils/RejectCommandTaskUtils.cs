using Application.Features.Tasks.Commands.RejectTask;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Tasks.Commands.TestUtils;

public static class RejectTaskCommandUtils
{
    public static RejectTaskCommand CreateRejectTaskCommand()
        => new (Constants.Task.StudentTaskId);
}