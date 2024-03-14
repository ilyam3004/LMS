using Application.Features.Tasks.Queries.GetStudentTask;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Tasks.Queries.TestUtils;

public class GetStudentTaskQueryUtils
{
    public static GetStudentTaskQuery CreateGetStudentTaskQuery()
        => new (Constants.Task.TaskId, Constants.Authentication.Token);
}