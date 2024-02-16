using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models.Groups;
using Application.Models.Subjects;
using Application.Models.Tasks;
using Domain.Abstractions.Results;
using Domain.Common;
using Domain.Entities;
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
            return Errors.User.InvalidToken;

        var user = await _unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        var lecturerSubjects = await _unitOfWork.Subjects
            .GetLecturerSubjects(user.Lecturer!.LecturerId);

        return lecturerSubjects.Select(TransformSubjectToLecturerSubjectResult)
            .ToList();
    }

    private LecturerSubjectResult TransformSubjectToLecturerSubjectResult(Subject subject)
    {
        var studentResults = subject.Group.Students.Select(s =>
            new StudentResult(s)).ToList();

        var groupResult = new GroupResult(subject.Group, studentResults);

        var taskResults = subject.Tasks.Select(task =>
            new LecturerTaskResult(task)).ToList();

        return new LecturerSubjectResult(subject, groupResult, taskResults);
    }
}