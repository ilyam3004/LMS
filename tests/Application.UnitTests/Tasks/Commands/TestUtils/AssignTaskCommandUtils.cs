using Application.Features.Tasks.Commands.CreateTask;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Tasks.Commands.TestUtils;

public static class AssignTaskCommandUtils
{
    public static AssignTaskCommand CreateAssignTaskCommand()
        => new(Constants.Task.Title,
            Constants.Task.Description,
            Constants.Subject.SubjectId,
            Constants.Task.Deadline,
            Constants.Task.MaxGrade);
}