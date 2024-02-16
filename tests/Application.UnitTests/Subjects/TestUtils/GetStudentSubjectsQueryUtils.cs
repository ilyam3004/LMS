using Application.Features.Subjects.Queries.GetStudentSubjectsQuery;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Subjects.TestUtils;

public static class GetStudentSubjectsQueryUtils
{
    public static GetStudentSubjectsQuery CreateGetStudentSubjectsQuery()
        => new(Constants.Authentication.Token);
}