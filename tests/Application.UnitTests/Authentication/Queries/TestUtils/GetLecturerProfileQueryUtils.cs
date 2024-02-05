using Application.Authentication.Queries.GetLecturerProfile;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Authentication.Queries.TestUtils;

public static class GetLecturerProfileQueryUtils
{
    public static GetLecturerProfileQuery CreateGetLecturerProfileQuery()
        => new GetLecturerProfileQuery(Constants.Authentication.Token);
}