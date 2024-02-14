using Application.Features.Subjects.Commands.RemoveSubject;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using NSubstitute;

namespace Application.UnitTests.Subjects.Commands;

public class RemoveSubjectCommandHandlerTests
{
    private readonly RemoveSubjectCommandHandler _sut;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public RemoveSubjectCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _sut = new RemoveSubjectCommandHandler()
    }
}