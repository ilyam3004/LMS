using Application.Features.Tasks.Commands.AcceptTask;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Tasks.Commands.TestUtils;

public static class AcceptTaskCommandUtils
{
    public static AcceptTaskCommand CreateAcceptTaskCommand(
        int grade = Constants.Task.Grade)
        => new (Constants.Task.StudentTaskId, grade);
}