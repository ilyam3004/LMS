using Application.UnitTests.TestUtils.TestConstants;
using Application.Authentication.Queries.Login;

namespace Application.UnitTests.Authentication.Queries.TestUtils;

public static class LoginQueryUtils
{
    public static LoginQuery CreateLoginQueryWithValidPassword()
        => new LoginQuery(Constants.Authentication.Email, Constants.Authentication.ValidPassword);

    public static LoginQuery CreateLoginQueryWithInvalidPassword()
        => new LoginQuery(Constants.Authentication.Email, Constants.Authentication.InvalidPassword);
}