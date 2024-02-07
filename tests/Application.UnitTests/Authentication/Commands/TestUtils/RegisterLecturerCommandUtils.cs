using Application.Features.Authentication.Commands.RegisterLecturer;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Authentication.Commands.TestUtils;

public class RegisterLecturerCommandUtils
{
    public static RegisterLecturerCommand CreateRegisterLecturerCommand()
        => new(Constants.Authentication.Email,
            Constants.Authentication.ValidPassword,
            Constants.Authentication.Firstname,
            Constants.Authentication.Lastname,
            Constants.Authentication.Degree,
            Constants.Authentication.Birthday,
            Constants.Authentication.Address);
}