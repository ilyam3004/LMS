using Application.Features.Grades.Queries;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Grades.Queries.TestUtils;

public static class GetLecturerGradesQueryUtils
{
    public static GetLecturerGradesQuery CreateGetLecturerGradesQuery()
        => new (Constants.Authentication.Token);
}