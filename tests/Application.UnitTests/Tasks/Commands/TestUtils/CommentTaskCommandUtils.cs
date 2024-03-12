using Application.Features.Tasks.Commands.CreateComment;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Tasks.Commands.TestUtils;

public static class CommentTaskCommandUtils
{
    public static CommentTaskCommand CreateCommentTaskCommand()
        => new(Constants.Task.Comment,
            Constants.Task.StudentTaskId,
            Constants.Authentication.Token);
}