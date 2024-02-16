using Application.Models.Subjects;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.TestUtils.Extensions;

public static class SubjectExtensions
{
    public static void ValidateRetrievedLecturerSubjects(this List<LecturerSubjectResult> result,
        List<Subject> subjects)
    {
        result.Should().NotBeNull();
        result.Count.Should().Be(subjects.Count);

        subjects.Zip(result, (s, r) => new {Subject = s, Result = r})
            .ToList()
            .ForEach(item =>
            {
                item.Result.Subject.SubjectId.Should().Be(item.Subject.SubjectId);
                item.Result.Subject.Name.Should().Be(item.Subject.Name);
                item.Result.Subject.Description.Should().Be(item.Subject.Description);
                item.Result.Subject.GroupId.Should().Be(item.Subject.GroupId);
                item.Result.Subject.LecturerId.Should().Be(item.Subject.LecturerId);
                item.Result.Subject.Tasks.Should().NotBeNull();
                item.Result.Subject.Tasks.Count.Should().Be(item.Subject.Tasks.Count);
                item.Result.Subject.Group.Should().NotBeNull();
            });
    }


    public static void ValidateRetrievedStudentSubjects(this List<StudentSubjectResult> result,
        List<Subject> subjects)
    {
        result.Should().NotBeNull();
        result.Count.Should().Be(subjects.Count);

        subjects.Zip(result, (s, r) => new {Subject = s, Result = r})
            .ToList()
            .ForEach(item =>
            {
                item.Result.Subject.SubjectId.Should().Be(item.Subject.SubjectId);
                item.Result.Subject.Name.Should().Be(item.Subject.Name);
                item.Result.Subject.Description.Should().Be(item.Subject.Description);
                item.Result.Subject.GroupId.Should().Be(item.Subject.GroupId);
                item.Result.Subject.LecturerId.Should().Be(item.Subject.LecturerId);
                item.Result.Subject.Tasks.Should().NotBeNull();
                item.Result.Subject.Tasks.Count.Should().Be(item.Subject.Tasks.Count);
                item.Result.Subject.Group.Should().NotBeNull();
                item.Result.Subject.Lecturer.Should().NotBeNull();
            });
    }
}