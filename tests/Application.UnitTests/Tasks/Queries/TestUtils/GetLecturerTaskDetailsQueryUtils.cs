using Application.Features.Tasks.Queries.GetLecturerTaskDetails;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Tasks.Queries.TestUtils;

public static class GetLecturerTaskDetailsQueryUtils
{
    public static GetLecturerTaskDetailsQuery CreateGetLecturerTaskDetailsQuery()
        => new (Constants.Task.TaskId);
}