using Application.UnitTests.Authentication.Queries.TestUtils;
using Application.Authentication.Queries.GetStudentProfile;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.UnitTests.TestUtils.Authentication;
using Application.UnitTests.TestUtils.Authentication.Extensions;
using Application.UnitTests.TestUtils.TestConstants;
using NSubstitute.ReturnsExtensions;
using FluentAssertions;
using Domain.Common;
using NSubstitute;

namespace Application.UnitTests.Authentication.Queries.GetStudentProfile;

public class GetStudentProfileQueryHandlerTests
{
    private readonly GetStudentProfileQueryHandler _sut;
    private readonly IUnitOfWork _mockUnitOfWork;
    private readonly IJwtTokenReader _mockJwtTokenReader;

    public GetStudentProfileQueryHandlerTests()
    {
        _mockUnitOfWork = Substitute.For<IUnitOfWork>();
        _mockJwtTokenReader = Substitute.For<IJwtTokenReader>();
        _sut = new GetStudentProfileQueryHandler(_mockUnitOfWork, _mockJwtTokenReader);
    }

    [Fact]
    public async Task Handler_WhenTheTokenIsValidAndUserExists_ShouldReturnTheStudentProfileResult()
    {
        // Arrange
        var query = GetStudentProfileQueryUtils.CreateGetStudentProfileQuery();
        
        _mockJwtTokenReader.ReadUserIdFromToken(query.Token)
            .Returns(Constants.Authentication.UserIdFromToken.ToString());

        _mockUnitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserIdFromToken)
            .Returns(AuthenticationFactory.CreateStudentUser(
                userId: Constants.Authentication.UserIdFromToken));
        
        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateRetrievedStudentProfile();
        _mockUnitOfWork.Users.Received(1)
            .GetUserByIdWithRelations(Constants.Authentication.UserIdFromToken);
    }

    [Fact]
    public async Task Handler_WhenTokenIsInvalid_ShouldReturnInvalidTokenError()
    {
        // Arrange
        var query = GetStudentProfileQueryUtils.CreateGetStudentProfileQuery();

        _mockJwtTokenReader.ReadUserIdFromToken(query.Token)
            .ReturnsNull();
        
        // Act
        var result = await _sut.Handle(query, default);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.User.InvalidToken);
        _mockUnitOfWork.Users.DidNotReceiveWithAnyArgs()
            .GetUserByIdWithRelations(default);
    }

    [Fact]
    public async Task Handler_ShouldReturnUserNotFound_WhenUserDoesNotExists()
    {
        // Arrange
        var query = GetStudentProfileQueryUtils.CreateGetStudentProfileQuery();

        _mockJwtTokenReader.ReadUserIdFromToken(Constants.Authentication.Token)
            .Returns(Constants.Authentication.UserIdFromToken.ToString());

        _mockUnitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserIdFromToken)
            .ReturnsNull();

        //Act
        var result = await _sut.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.User.UserNotFound);
        _mockUnitOfWork.Users.Received(1)
            .GetUserByIdWithRelations(Constants.Authentication.UserIdFromToken);
    }
}