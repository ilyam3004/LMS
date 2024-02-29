using Application.Features.Tasks.Commands.ReturnTask;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Tasks.Commands.TestUtils;

public static class ReturnTaskCommandUtils
{
    public static ReturnTaskCommand CreateReturnTaskCommand()
        => new(Constants.Task.StudentTaskId);
}