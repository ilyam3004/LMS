using Application.Features.Authentication.Queries.GetStudentProfile;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Authentication.Queries.TestUtils;

public static class GetStudentProfileQueryUtils
{
    public static GetStudentProfileQuery CreateGetStudentProfileQuery()
        => new GetStudentProfileQuery(Constants.Authentication.Token);
}