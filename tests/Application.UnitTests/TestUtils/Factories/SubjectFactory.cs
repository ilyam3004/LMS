using Application.UnitTests.TestUtils.TestConstants;
using Task = Domain.Entities.Task;
using Domain.Entities;

namespace Application.UnitTests.TestUtils.Factories;

public static class SubjectFactory
{
    public static List<Subject> CreateSubjects(Guid? groupId = null,
        int subjectsCount = 1,
        List<Task>? tasks = null)
    {
        Guid generatedGroupId = groupId ?? Constants.Group.GroupId;

        return Enumerable.Range(0, subjectsCount)
            .Select(index => CreateSubject(
                groupId: generatedGroupId,
                subjectId: Constants.Subject.SubjectId,
                index: index,
                tasks: tasks))
            .ToList();
    }

    public static Subject CreateSubject(List<Task>? tasks = null,
        Guid? groupId = null,
        Guid? subjectId = null,
        Guid? lecturerId = null,
        int index = 0)
        => new Subject
        {
            SubjectId = subjectId ?? Constants.Subject.SubjectId,
            Name = Constants.Subject.SubjectNameFromGivenIndex(index),
            Description = Constants.Subject.SubjectDescriptionFromGivenIndex(index),
            GroupId = groupId ?? Constants.Group.GroupId,
            LecturerId = lecturerId ?? Constants.Lecturer.LecturerId,
            Tasks = tasks ?? TaskFactory.CreateTasks(subjectId),
            Group = GroupFactory.CreateGroupWithOutSubjects(),
            Lecturer = AuthenticationFactory.CreateLecturerUser().Lecturer
        };

    public static Subject CreateSubjectWithOutTasks(Guid? groupId = null,
        Guid? subjectId = null,
        Guid? lecturerId = null,
        int index = 0)
        => new Subject
        {
            SubjectId = subjectId ?? Constants.Subject.SubjectId,
            Name = Constants.Subject.SubjectNameFromGivenIndex(index),
            Description = Constants.Subject.SubjectDescriptionFromGivenIndex(index),
            GroupId = groupId ?? Constants.Group.GroupId,
            LecturerId = lecturerId ?? Constants.Lecturer.LecturerId,
            Tasks = [],
            Group = GroupFactory.CreateGroupWithOutSubjects(),
            Lecturer = AuthenticationFactory.CreateLecturerUser().Lecturer
        };
}