using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Models;
using Domain.Abstractions.Results;
using Domain.Common;
using MediatR;

namespace Application.Subjects.Queries.GetStudentSubjectsQuery;

public class GetStudentSubjectsQueryHandler
    : IRequestHandler<GetStudentSubjectsQuery, Result<List<StudentSubjectResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;
    
    public GetStudentSubjectsQueryHandler(IUnitOfWork unitOfWork, 
        IJwtTokenReader jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenGenerator;
    }
    
    public async Task<Result<List<StudentSubjectResult>>> Handle(
        GetStudentSubjectsQuery query,
        CancellationToken cancellationToken)
    {
        var userId = _jwtTokenReader.ReadUserIdFromToken(query.Token);
        if (userId is null)
            return Errors.User.InvalidToken;

        var user = await _unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;

        var studentSubjects = await _unitOfWork.Subjects
            .GetStudentSubjects(user.Student!.GroupId);

        return studentSubjects.Select(subject =>
            new StudentSubjectResult(subject, subject.Lecturer.FullName))
            .ToList();
    }
}