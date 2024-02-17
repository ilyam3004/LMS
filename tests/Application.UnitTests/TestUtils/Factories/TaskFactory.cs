using Application.UnitTests.TestUtils.TestConstants;
using Task = Domain.Entities.Task;
using Domain.Entities;

namespace Application.UnitTests.TestUtils.Factories;

public static class TaskFactory
{
    public static List<Task> CreateTasks(Guid? subjectId = null,
        int tasksCount = 1)
    {
        Guid generatedSubjectId = subjectId ?? Constants.Subject.SubjectId;

        return Enumerable.Range(0, tasksCount)
            .Select(index => CreateTask(subjectId, index))
            .ToList();
    }

    public static Task CreateTask(Guid? subjectId = null,
        int index = 0)
    {
        Guid generatedSubjectId = subjectId ?? Constants.Subject.SubjectId;

        return new Task
        {
            TaskId = Constants.Task.TaskId,
            Title = Constants.Task.TaskTitleFromGivenIndex(index),
            Description = Constants.Task.TaskDescriptionFromGivenIndex(index),
            SubjectId = generatedSubjectId,
            CreatedAt = Constants.Task.CreatedAt,
            Deadline = Constants.Task.Deadline,
            MaxGrade = Constants.Task.MaxGrade,
            StudentTasks = CreateStudentTasks(Constants.Task.TaskId),
            Subject = SubjectFactory.CreateSubjectWithOutTasks()
        };
    }


    public static List<StudentTask> CreateStudentTasks(Guid? taskId = null,
        int tasksCount = 1)
    {
        Guid generatedTaskId = taskId ?? Constants.Task.TaskId;

        return Enumerable.Range(0, tasksCount)
            .Select(index =>
            {
                Guid generatedStudentTaskId = taskId ?? Constants.Task.TaskId;

                return new StudentTask
                {
                    StudentTaskId = generatedStudentTaskId,
                    TaskId = generatedTaskId,
                    StudentId = Constants.Student.StudentId,
                    Grade = Constants.Task.Grade,
                    Status = Constants.Task.Status,
                    UploadedAt = Constants.Task.UploadedAt,
                    OrdinalFileName = Constants.Task.OrdinalFileName,
                    FileUrl = Constants.Task.FileUrl,
                    Comments = CreateStudentTaskComments(studentTaskId: generatedTaskId)
                };
            }).ToList();
    }

    public static List<TaskComment> CreateStudentTaskComments(Guid? studentTaskId = null,
        int commentsCount = 1)
    {
        Guid generatedStudentTaskId = studentTaskId ?? Constants.Task.StudentTaskId;

        return Enumerable.Range(0, commentsCount)
            .Select(index => new TaskComment
            {
                TaskCommentId = Constants.Task.TaskCommentId,
                StudentTaskId = generatedStudentTaskId,
                Comment = Constants.Task.CommentFromGivenIndex(index),
                CreatedAt = Constants.Task.CreatedAt,
                UserId = Constants.Authentication.UserId,
                User = AuthenticationFactory.CreateUserWithoutLectureOrStudentObjects()
            }).ToList();
    }
}