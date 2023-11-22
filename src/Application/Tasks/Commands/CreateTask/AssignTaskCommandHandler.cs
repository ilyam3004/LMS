using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Application.Services;
using Domain.Common;
using Domain.Entities;
using MediatR;
using Task = Domain.Entities.Task;

namespace Application.Tasks.Commands.CreateTask;

public class AssignTaskCommandHandler
    : IRequestHandler<AssignTaskCommand, Result<LecturerSubjectResult>>
{
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IDateTimeProvider _dateTimeProvider;
    private readonly IUnitOfWork _unitOfWork;

    public AssignTaskCommandHandler(IJwtTokenReader jwtTokenReader,
        IUnitOfWork unitOfWork, 
        IDateTimeProvider dateTimeProvider)
    {
        _jwtTokenReader = jwtTokenReader;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<Result<LecturerSubjectResult>> Handle(AssignTaskCommand command,
        CancellationToken cancellationToken)
    {
        var userId = _jwtTokenReader.ReadUserIdFromToken(command.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await _unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        var task = new Task
        {
            TaskId = Guid.NewGuid(),
            Title = command.Title,
            Description = command.Description,
            SubjectId = Guid.NewGuid(),
            CreatedAt = _dateTimeProvider.UtcNow,
            Deadline = command.Deadline,
            MaxGrade = command.MaxGrade
        };

        await _unitOfWork.Tasks.AddAsync(task);

        await _unitOfWork.SaveChangesAsync();

        throw new NotImplementedException();
    }

    // private async Task<LecturerSubjectResult> GetSubjectResult(Guid subjectId)
    // {
    //     // var subject = await _unitOfWork.Subjects
    //     //     .GetSubjectById(subjectId);
    //
    //     return new LecturerSubjectResult();
    // }
}