using Application.Authentication.Queries.GetLecturerProfile;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.UnitTests.Authentication.Queries.TestUtils;
using Application.UnitTests.TestUtils.Extensions;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.TestUtils.TestConstants;
using NSubstitute.ReturnsExtensions;
using FluentAssertions;
using Domain.Common;
using NSubstitute;

namespace Application.UnitTests.Authentication.Queries.GetLecturerProfile;

public class GetLecturerProfileQueryHandlerTests
{
    private readonly GetLecturerProfileQueryHandler _sut;
    private readonly IUnitOfWork _mockUnitOfWork;
    private readonly IJwtTokenReader _mockJwtTokenReader;

    public GetLecturerProfileQueryHandlerTests()
    {
        _mockUnitOfWork = Substitute.For<IUnitOfWork>();
        _mockJwtTokenReader = Substitute.For<IJwtTokenReader>();
        _sut = new GetLecturerProfileQueryHandler(_mockUnitOfWork, _mockJwtTokenReader);
    }

    [Fact]
    public async Task Handler_WhenUserExistsAndTokenIsValid_ShouldReturnLecturerProfileResult()
    {
        // Arrange
        var query = GetLecturerProfileQueryUtils.CreateGetLecturerProfileQuery();

        _mockJwtTokenReader.ReadUserIdFromToken(Constants.Authentication.Token)
            .Returns(Constants.Authentication.UserIdFromToken.ToString());

        _mockUnitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserIdFromToken)
            .Returns(AuthenticationFactory.CreateLecturerUser(
                Constants.Authentication.UserIdFromToken));

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateRetrievedLecturerProfile();
        await _mockUnitOfWork.Users.Received(1)
            .GetUserByIdWithRelations(Constants.Authentication.UserIdFromToken);
    }

    [Fact]
    public async Task Handler_WhenTokenIsInvalid_ShouldReturnInvalidTokenError()
    {
        // Arrange
        var query = GetLecturerProfileQueryUtils.CreateGetLecturerProfileQuery();

        _mockJwtTokenReader.ReadUserIdFromToken(query.Token)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidToken);
        await _mockUnitOfWork.Users.DidNotReceiveWithAnyArgs()
            .GetUserByIdWithRelations(default);
    }

    [Fact]
    public async Task Handler_ShouldReturnUserNotFound_WhenUserDoesNotExists()
    {
        // Arrange
        var query = GetLecturerProfileQueryUtils.CreateGetLecturerProfileQuery();

        _mockJwtTokenReader.ReadUserIdFromToken(Constants.Authentication.Token)
            .Returns(Constants.Authentication.UserIdFromToken.ToString());

        _mockUnitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserIdFromToken)
            .ReturnsNull();

        //Act
        var result = await _sut.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.UserNotFound);
        await _mockUnitOfWork.Users.Received(1)
            .GetUserByIdWithRelations(Constants.Authentication.UserIdFromToken);
    }
}