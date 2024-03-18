using Application.Models.Grades;
using Domain.Entities;
using FluentAssertions;

namespace Application.UnitTests.TestUtils.Extensions;

public static class GradesExtensions
{
    public static void ValidateRetrievedSubjectsData(this List<SubjectGradesResult> result,
        List<Subject> subjects)
    {
        result.Should().HaveCount(subjects.Count);

        result.Zip(subjects, (gradeResult, subject) =>
            {
                gradeResult.SubjectId.Should().NotBeEmpty();
                gradeResult.SubjectName.Should().NotBeNullOrEmpty();
                gradeResult.GroupName.Should().NotBeNullOrEmpty();
                gradeResult.StudentTasks.Should().NotBeNullOrEmpty();
                
                gradeResult.SubjectId.Should().Be(subject.SubjectId);
                gradeResult.SubjectName.Should().Be(subject.Name);
                gradeResult.GroupName.Should().Be(subject.Group.Name);
                
                return 0;
            }
        );
    }
}