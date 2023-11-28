using Application.Tasks.Commands.CreateTask;
using Contracts.Requests.Tasks;
using Contracts.Responses.Tasks;
using Application.Models.Tasks;
using Domain.Entities;
using Mapster;

namespace Api.Common.Mapping;

public class TaskMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<AssignTaskRequest, AssignTaskCommand>()
            .Map(dest => dest.Title, src => src.Title)
            .Map(dest => dest.Description, src => src.Description)
            .Map(dest => dest.SubjectId, src => src.SubjectId)
            .Map(dest => dest.Deadline, src => src.Deadline)
            .Map(dest => dest.MaxGrade, src => src.MaxGrade);

        config.NewConfig<StudentTask, UploadedStudentTaskResponse>()
            .Map(dest => dest.StudentTaskId, src => src.StudentTaskId)
            .Map(dest => dest.TaskId, src => src.TaskId)
            .Map(dest => dest.FileUrl, src => src.FileUrl)
            .Map(dest => dest.UploadedAt, src => src.UploadedAt)
            .Map(dest => dest.Grade, src => src.Grade)
            .Map(dest => dest.Student, src => src.Student)
            .Map(dest => dest.Status, src => src.Status)
            .Map(dest => dest.Comments, src => src.Comments);

        config.NewConfig<LecturerTaskResult, LecturerTaskResponse>()
            .Map(dest => dest.TaskId, src => src.Task.TaskId)
            .Map(dest => dest.Title, src => src.Task.Title)
            .Map(dest => dest.Description, src => src.Task.Description)
            .Map(dest => dest.CreatedAt, src => src.Task.CreatedAt)
            .Map(dest => dest.Deadline, src => src.Task.Deadline)
            .Map(dest => dest.MaxGrade, src => src.Task.MaxGrade)
            .Map(dest => dest.GroupName, src => src.Task.Subject.Group.Name)
            .Map(dest => dest.StudentTasks, src => src.Task.StudentTasks);

        config.NewConfig<StudentTaskResult, StudentTaskResponse>()
            .Map(dest => dest.TaskId, src => src.Task.TaskId)
            .Map(dest => dest.Title, src => src.Task.Title)
            .Map(dest => dest.Description, src => src.Task.Description)
            .Map(dest => dest.Deadline, src => src.Task.Deadline)
            .Map(dest => dest.CreatedAt, src => src.Task.CreatedAt)
            .Map(dest => dest.MaxGrade, src => src.Task.MaxGrade)
            .Map(dest => dest.LecturerName, src => src.Task.Subject.Lecturer.FullName)
            .Map(dest => dest.UploadedTask, src => src.UploadedTask);

        config.NewConfig<UploadedTaskResult, UploadedStudentTaskResponse>()
            .Map(dest => dest.StudentTaskId, src => src.Task.StudentTaskId)
            .Map(dest => dest.TaskId, src => src.Task.TaskId)
            .Map(dest => dest.FileUrl, src => src.Task.FileUrl)
            .Map(dest => dest.UploadedAt, src => src.Task.UploadedAt)
            .Map(dest => dest.Grade, src => src.Task.Grade)
            .Map(dest => dest.Student, src => src.Task.Student)
            .Map(dest => dest.Status, src => src.Task.Status)
            .Map(dest => dest.Comments, src => src.Task.Comments);

        config.NewConfig<TaskComment, TaskCommentResponse>()
            .Map(dest => dest.TaskCommentId, src => src.TaskCommentId)
            .Map(dest => dest.Comment, src => src.Comment)
            .Map(dest => dest.CreatedAt, src => src.CreatedAt)
            .Map(dest => dest.UserId, src => src.UserId)
            .Map(dest => dest.Username, src =>
                src.User.Student != null
                    ? src.User.Student.FullName
                    : src.User.Lecturer!.FullName);
    }
}