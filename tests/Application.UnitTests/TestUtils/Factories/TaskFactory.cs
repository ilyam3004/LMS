using Application.UnitTests.TestUtils.TestConstants;
using Task = Domain.Entities.Task;
using Domain.Entities;
using Domain.Enums;

namespace Application.UnitTests.TestUtils.Factories;

public static class TaskFactory
{
    public static List<Task> CreateTasks(Guid? subjectId = null,
        int tasksCount = 1)
        => Enumerable.Range(0, tasksCount)
            .Select(index => CreateTask(subjectId: subjectId, index: index))
            .ToList();

    public static Task CreateTask(Guid? subjectId = null,
        DateTime? deadLine = null,
        List<StudentTask>? studentTasks = null,
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
            Deadline = deadLine ?? Constants.Task.NotExpiredDeadline,
            MaxGrade = Constants.Task.MaxGrade,
            StudentTasks = studentTasks ?? CreateStudentTasks(Constants.Task.TaskId),
            Subject = SubjectFactory.CreateSubjectWithOutTasks()
        };
    }


    public static List<StudentTask> CreateStudentTasks(Guid? taskId = null,
        int studentTasksCount = 1)
    {
        Guid generatedTaskId = taskId ?? Constants.Task.TaskId;

        return Enumerable.Range(0, studentTasksCount)
            .Select(_ => CreateStudentTaskWithoutTaskObject(taskId: generatedTaskId))
            .ToList();
    }

    public static StudentTask CreateStudentTaskWithoutTaskObject(Guid? taskId = null,
        StudentTaskStatus status = StudentTaskStatus.Uploaded)
        => new ()
        {
            StudentTaskId = Constants.Task.StudentTaskId,
            TaskId = taskId ?? Constants.Task.TaskId,
            StudentId = Constants.Student.StudentId,
            Grade = Constants.Task.Grade,
            Status = status,
            UploadedAt = Constants.Task.UploadedAt,
            OrdinalFileName = Constants.File.OrdinalFileName,
            FileUrl = Constants.File.FileUrl,
            Comments = CreateStudentTaskComments(studentTaskId: taskId)
        };


    public static StudentTask CreateStudentTaskWithTaskObject(Guid? taskId = null,
        StudentTaskStatus status = StudentTaskStatus.Uploaded, 
        DateTime? deadline = null)
        => new ()
        {
            StudentTaskId = Constants.Task.StudentTaskId,
            TaskId = taskId ?? Constants.Task.TaskId,
            StudentId = Constants.Student.StudentId,
            Grade = Constants.Task.Grade,
            Status = status,
            UploadedAt = Constants.Task.UploadedAt,
            OrdinalFileName = Constants.File.OrdinalFileName,
            FileUrl = Constants.File.FileUrl,
            Comments = CreateStudentTaskComments(studentTaskId: taskId),
            Task = CreateTask(deadLine: deadline)
        };


    private static List<TaskComment> CreateStudentTaskComments(Guid? studentTaskId = null,
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