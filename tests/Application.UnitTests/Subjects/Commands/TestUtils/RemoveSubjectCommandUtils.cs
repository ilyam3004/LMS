using Application.Features.Subjects.Commands.RemoveSubject;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Subjects.Commands.TestUtils;

public static class RemoveSubjectCommandUtils
{
    public static RemoveSubjectCommand CreateRemoveSubjectCommand()
        => new(Constants.Subject.SubjectId,
            Constants.Authentication.Token);
}