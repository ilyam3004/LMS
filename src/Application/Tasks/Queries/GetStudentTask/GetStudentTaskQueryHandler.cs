using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models;
using Domain.Abstractions.Results;
using Domain.Common;
using MediatR;

namespace Application.Tasks.Queries.GetStudentTask;

public class GetStudentTaskQueryHandler
    : IRequestHandler<GetStudentTaskQuery, Result<StudentTaskResult>>
{
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IUnitOfWork _unitOfWork;

    public GetStudentTaskQueryHandler(IJwtTokenReader jwtTokenReader,
        IUnitOfWork unitOfWork)
    {
        _jwtTokenReader = jwtTokenReader;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<StudentTaskResult>> Handle(GetStudentTaskQuery query,
        CancellationToken cancellationToken)
    {
        var userId = _jwtTokenReader.ReadUserIdFromToken(query.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await _unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));
        
        if (user is null)
            return Errors.User.UserNotFound;

        var task = await _unitOfWork.Tasks.GetTaskByIdWithLecturerRelation(query.TaskId);
        
        if(task is null)
            return Errors.Task.TaskNotFound;

        var studentTask = task.StudentTasks.FirstOrDefault(studentTask =>
            studentTask.StudentId == user.Student!.StudentId);

        return new StudentTaskResult(task, studentTask);
    }
}