using Application.Authentication.Commands.RegisterStudent;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Authentication.Commands.TestUtils;

public static class RegisterStudentCommandUtils
{
    public static RegisterStudentCommand CreateRegisterStudentCommand()
        => new RegisterStudentCommand(
            Constants.Authentication.Email,
            Constants.Authentication.ValidPassword,
            Constants.Authentication.Firstname,
            Constants.Authentication.Lastname,
            Constants.Group.GroupName,
            Constants.Authentication.Birthday,
            Constants.Authentication.Address);
}