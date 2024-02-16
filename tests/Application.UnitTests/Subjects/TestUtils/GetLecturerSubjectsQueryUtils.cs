using Application.Features.Subjects.Queries.GetLecturerSubjects;
using Application.UnitTests.TestUtils.TestConstants;

namespace Application.UnitTests.Subjects.TestUtils;

public static class GetLecturerSubjectsQueryUtils
{
    public static GetLecturerSubjectsQuery CreateGetLecturerSubjectsQuery()
        => new GetLecturerSubjectsQuery(Constants.Authentication.Token);
}