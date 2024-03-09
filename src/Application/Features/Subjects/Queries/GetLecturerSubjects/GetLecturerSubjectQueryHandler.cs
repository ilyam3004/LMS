using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Subjects;
using Domain.Abstractions.Results;
using Task = Domain.Entities.Task;
using Application.Models.Groups;
using Application.Models.Tasks;
using Domain.Entities;
using Domain.Common;
using MediatR;

namespace Application.Features.Subjects.Queries.GetLecturerSubjects;

public class GetLecturerSubjectQueryHandler
    : IRequestHandler<GetLecturerSubjectsQuery, Result<List<LecturerSubjectResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public GetLecturerSubjectQueryHandler(IUnitOfWork unitOfWork,
        IJwtTokenReader jwtTokenReader)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenReader;
    }

    public async Task<Result<List<LecturerSubjectResult>>> Handle(
        GetLecturerSubjectsQuery query,
        CancellationToken cancellationToken)
    {
        var userId = _jwtTokenReader.ReadUserIdFromToken(query.Token);
        if (userId is null)
            return Errors.Authentication.InvalidToken;

        var user = await _unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.Authentication.UserNotFound;

        var lecturerSubjects = await _unitOfWork.Subjects
            .GetLecturerSubjects(user.Lecturer!.LecturerId);

        return lecturerSubjects.Select(MapSubjectToLecturerSubjectResult)
            .ToList();
    }

    private LecturerSubjectResult MapSubjectToLecturerSubjectResult(Subject subject)
    {
        var studentResults = subject.Group.Students.Select(s =>
            new StudentResult(s)).ToList();

        var groupResult = new GroupResult(subject.Group, studentResults);

        var taskResults = MapTasksToLecturerTaskResults(
            subject.Tasks);

        return new LecturerSubjectResult(subject, groupResult, taskResults);
    }

    private List<LecturerTaskResult> MapTasksToLecturerTaskResults(
        List<Task> tasks)
        => tasks.Select(task => new LecturerTaskResult(task)).ToList();
}