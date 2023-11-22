using Application.Models;
using Domain.Abstractions.Results;
using MediatR;

namespace Application.Tasks.Commands.RemoveTask;

public class RemoveTaskCommandHandler 
    : IRequestHandler<RemoveTaskCommand, Result<LecturerSubjectResult>>
{
    public Task<Result<LecturerSubjectResult>> Handle(
        RemoveTaskCommand command, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}