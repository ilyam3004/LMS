using Application.Features.Tasks.Commands.CreateComment;
using Application.Features.Tasks.Commands.CreateTask;
using Application.Features.Tasks.Commands.UploadTaskSolution;
using Application.Models.Tasks;
using Domain.Entities;
using Api.Protos;
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

        config.NewConfig<StudentTask, UploadedTaskResponse>()
            .Map(dest => dest.StudentTaskId, src => src.StudentTaskId)
            .Map(dest => dest.TaskId, src => src.TaskId)
            .Map(dest => dest.FileName, src => src.OrdinalFileName)
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

        config.NewConfig<UploadedTaskResult, UploadedTaskResponse>()
            .Map(dest => dest.StudentTaskId, src => src.Task.StudentTaskId)
            .Map(dest => dest.TaskId, src => src.Task.TaskId)
            .Map(dest => dest.FileName, src => src.Task.OrdinalFileName)
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
        
        config.NewConfig<StudentTasksResult, StudentTasksResponse>()
            .Map(dest => dest.StudentId, src => src.StudentId)
            .Map(dest => dest.FullName, src => src.FullName)
            .Map(dest => dest.TotalGrade, src => src.TotalGrade)
            .Map(dest => dest.AverageGrade, src => src.AverageGrade)
            .Map(dest => dest.Tasks, src => src.Tasks);

        config.NewConfig<(UploadTaskSolutionRequest, string), UploadTaskSolutionCommand>()
            .Map(dest => dest.FileName, src => src.Item1.FileName)
            .Map(dest => dest.FileContent, src => src.Item1.File.ToByteArray())
            .Map(dest => dest.Token, src => src.Item2)
            .Map(dest => dest.StudentTaskId, src => Guid.Parse(src.Item1.StudentTaskId));
        
        config.NewConfig<(CommentTaskRequest, string), CommentTaskCommand>()
            .Map(dest => dest.Comment, src => src.Item1.Comment)
            .Map(dest => dest.Token, src => src.Item2)
            .Map(dest => dest.StudentTaskId, src => Guid.Parse(src.Item1.StudentTaskId));
    }
}