using Application.UnitTests.TestUtils.TestConstants;
using Task = Domain.Entities.Task;

namespace Application.UnitTests.TestUtils.Factories;

public static class TaskFactory
{
    public static List<Task> CreateTasks(Guid? subjectId = null,
        int tasksCount = 1)
    {
        Guid generatedSubjectId = subjectId ?? Constants.Subject.SubjectId;
        
        return Enumerable.Range(0, tasksCount)
            .Select(index => new Task
            {
                TaskId = Constants.Subject.SubjectId,
                Title = Constants.Task.TaskTitleFromGivenIndex(index),
                Description = Constants.Task.TaskDescriptionFromGivenIndex(index),
                SubjectId = generatedSubjectId,
                CreatedAt = Constants.Task.CreatedAt,
                Deadline = Constants.Task.Deadline,
                MaxGrade = Constants.Task.MaxGrade
            }).ToList();
    }
}