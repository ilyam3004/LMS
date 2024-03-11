using Application.Features.Tasks.Commands.RemoveUploadedSolution;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Tasks.Commands.TestUtils;

public static class RemoveUploadedSolutionCommandUtils
{
    public static RemoveUploadedSolutionCommand CreateRemoveUploadedSolutionCommand()
        => new(Constants.Task.StudentTaskId, Constants.Authentication.Token);
}