using Application.Features.Tasks.Queries.DownloadTaskSolution;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Tasks.Queries.TestUtils;

public static class DownloadTaskSolutionQueryUtils
{
    public static DownloadTaskSolutionQuery CreateDownloadTaskSolutionQuery()
        => new (Constants.Task.StudentTaskId);
}