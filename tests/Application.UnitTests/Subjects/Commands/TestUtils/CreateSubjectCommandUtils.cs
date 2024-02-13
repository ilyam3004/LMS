using Application.Features.Subjects.Commands.CreateSubject;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Subjects.Commands.TestUtils;

public static class CreateSubjectCommandUtils
{
    public static CreateSubjectCommand CreateSubjectCommand(
        string subjectName = Constants.Subject.SubjectName)
        => new CreateSubjectCommand(subjectName,
            Constants.Subject.SubjectDescription,
            Constants.Group.GroupName,
            Constants.Authentication.Token);
}