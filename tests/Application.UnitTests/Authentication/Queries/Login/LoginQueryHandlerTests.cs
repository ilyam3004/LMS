using Application.UnitTests.Authentication.Queries.TestUtils;
using Application.UnitTests.TestUtils.Authentication;
using Application.UnitTests.TestUtils.Authentication.Extensions;
using Application.UnitTests.TestUtils.TestConstants;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Authentication.Queries.Login;
using NSubstitute.ReturnsExtensions;
using NSubstitute;
using FluentAssertions;
using Domain.Common;

namespace Application.UnitTests.Authentication.Queries.Login;

public class LoginQueryHandlerTests
{
    private readonly IJwtTokenGenerator _mockJwtTokenGenerator;
    private readonly IUnitOfWork _mockUnitOfWork;
    private readonly LoginQueryHandler _sut;

    public LoginQueryHandlerTests()
    {
        _mockJwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _mockUnitOfWork = Substitute.For<IUnitOfWork>();
        _sut = new LoginQueryHandler(_mockUnitOfWork, _mockJwtTokenGenerator);
    }

    [Fact]
    public async Task
        Handle_WhenUserExistsAndPasswordIsCorrectAndUserIsStudent_ShouldGenerateTokenAndReturnAuthenticationResult()
    {
        // Arrange
        var query = LoginQueryUtils.CreateLoginQueryWithValidPassword();

        _mockUnitOfWork.Users.GetUserByEmail(query.Email)
            .Returns(AuthenticationFactory.CreateStudentUser(
                password: Constants.Authentication.HashForValidPassword));

        _mockJwtTokenGenerator.GenerateToken(Constants.Authentication.UserId,
                Constants.Authentication.FullName,
                Constants.Authentication.Email,
                Constants.Authentication.StudentRole)
            .Returns(Constants.Authentication.Token);

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateRetrievedStudentUser();
        await _mockUnitOfWork.Users.Received(1).GetUserByEmail(query.Email);
        _mockJwtTokenGenerator.Received(1).GenerateToken(Constants.Authentication.UserId,
            Constants.Authentication.FullName,
            Constants.Authentication.Email,
            Constants.Authentication.StudentRole);
    }

    [Fact]
    public async Task
        Handler_WhenUserExistsAndPasswordIsCorrectAndUserIsLecturer_ShouldGenerateTokenAndReturnAuthenticationResult()
    {
        // Arrange
        var query = LoginQueryUtils.CreateLoginQueryWithValidPassword();

        _mockUnitOfWork.Users.GetUserByEmail(query.Email)
            .Returns(AuthenticationFactory.CreateLecturerUser(
                password: Constants.Authentication.HashForValidPassword));

        _mockJwtTokenGenerator.GenerateToken(Constants.Authentication.UserId,
                Constants.Authentication.FullName,
                Constants.Authentication.Email,
                Constants.Authentication.LecturerRole)
            .Returns(Constants.Authentication.Token);

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateRetrievedLecturerUser();
        await _mockUnitOfWork.Users.Received(1).GetUserByEmail(query.Email);
        _mockJwtTokenGenerator.Received(1).GenerateToken(Constants.Authentication.UserId,
            Constants.Authentication.FullName,
            Constants.Authentication.Email,
            Constants.Authentication.LecturerRole);
    }

    [Fact]
    public async Task Handler_UserByGivenEmailIsNotExists_ShouldReturnUserNotFoundError()
    {
        // Arrange
        var query = LoginQueryUtils.CreateLoginQueryWithValidPassword();

        _mockUnitOfWork.Users.GetUserByEmail(query.Email)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.User.UserNotFound);
        await _mockUnitOfWork.Users.Received(1).GetUserByEmail(query.Email);
        _mockJwtTokenGenerator.Received(0).GenerateToken(Constants.Authentication.UserId,
            Constants.Authentication.FullName,
            Constants.Authentication.Email,
            Constants.Authentication.LecturerRole);
    }

    [Fact]
    public async Task Handler_WhenUserExistsByGivenEmailButPasswordIsIncorrect_ShouldReturnInvalidCredentialsError()
    {
        // Arrange
        var query = LoginQueryUtils.CreateLoginQueryWithInvalidPassword();

        _mockUnitOfWork.Users.GetUserByEmail(query.Email)
            .Returns(AuthenticationFactory.CreateLecturerUser(
                password: Constants.Authentication.HashForValidPassword));

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.User.InvalidCredentials);
        await _mockUnitOfWork.Users.Received(1).GetUserByEmail(query.Email);
        _mockJwtTokenGenerator.Received(0).GenerateToken(Constants.Authentication.UserId,
            Constants.Authentication.FullName,
            Constants.Authentication.Email,
            Constants.Authentication.LecturerRole);
    }

    [Fact]
    public async Task Handler_WhenUserExistsByGivenEmailAndPasswordIsCorrectButStudentAndLecturerObjectsAreNull_ShouldReturnUserNotFound()
    {
        // Arrange
        var query = LoginQueryUtils.CreateLoginQueryWithValidPassword();

        _mockUnitOfWork.Users.GetUserByEmail(query.Email)
            .Returns(AuthenticationFactory.CreateUserWithoutLectureOrStudentObjects(
                password: Constants.Authentication.HashForValidPassword));

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.User.UserNotFound);
        await _mockUnitOfWork.Users.Received(1).GetUserByEmail(query.Email);
        _mockJwtTokenGenerator.Received(0).GenerateToken(Constants.Authentication.UserId,
            Constants.Authentication.FullName,
            Constants.Authentication.Email,
            Constants.Authentication.LecturerRole);
    }
}