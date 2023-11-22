using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Domain.Abstractions.Results;
using Application.Models;
using Domain.Common;
using MediatR;

namespace Application.Subjects.Commands.RemoveSubject;

public class RemoveSubjectCommandHandler
    : IRequestHandler<RemoveSubjectCommand, Result<List<LecturerSubjectResult>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public RemoveSubjectCommandHandler(IUnitOfWork unitOfWork, 
        IJwtTokenReader jwtTokenGenerator)
    {
        _unitOfWork = unitOfWork;
        _jwtTokenReader = jwtTokenGenerator;
    }
    
    public async Task<Result<List<LecturerSubjectResult>>> Handle(
        RemoveSubjectCommand command,
        CancellationToken cancellationToken)
    {
        if (!await _unitOfWork.Subjects.SubjectExists(command.SubjectId))
            return Errors.Subject.SubjectNotFound;

        var userId = _jwtTokenReader.ReadUserIdFromToken(command.Token);
        if (userId is null)
            return Errors.User.InvalidToken;
        
        var user = await _unitOfWork.Users
            .GetUserByIdWithRelations(Guid.Parse(userId));
        if (user is null)
            return Errors.User.UserNotFound;
        
        _unitOfWork.Subjects.Remove(command.SubjectId);
        await _unitOfWork.SaveChangesAsync();

        return await GetLecturerSubjects(user.Lecturer!.LecturerId);
    }

    private async Task<List<LecturerSubjectResult>> GetLecturerSubjects(Guid lecturerId)
    {
        var lecturerSubjects = await _unitOfWork.Subjects
            .GetLecturerSubjects(lecturerId);
        
        return lecturerSubjects.Select(subject =>
        {
            var groupResults = subject.GroupSubjects.Select(gs =>
            {
                var studentResults = gs.Group.Students.Select(s => 
                    new StudentResult(s)).ToList();
                
                return new GroupResult(gs.Group, studentResults);
            }).ToList();

            return new LecturerSubjectResult(subject, groupResults);
        }).ToList();
    }
}