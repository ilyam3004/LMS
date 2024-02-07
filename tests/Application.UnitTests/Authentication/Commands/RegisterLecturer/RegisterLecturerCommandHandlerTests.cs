using TaskFactory = Application.UnitTests.TestUtils.Tasks.TaskFactory;
using Application.Features.Authentication.Commands.RegisterLecturer;
using Application.UnitTests.Authentication.Commands.TestUtils;
using Application.UnitTests.TestUtils.Authentication.Extensions;
using Application.UnitTests.TestUtils.Groups;
using Application.UnitTests.TestUtils.Subjects;
using Application.UnitTests.TestUtils.TestConstants;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Task = System.Threading.Tasks.Task;
using NSubstitute.ReturnsExtensions;
using FluentAssertions;
using Domain.Entities;
using NSubstitute;
using Domain.Common;

namespace Application.UnitTests.Authentication.Commands.RegisterLecturer;

public class RegisterLecturerCommandHandlerTests
{
    private readonly RegisterLecturerCommandHandler _sut;
    private readonly IUnitOfWork _mockUnitOfWork;
    private readonly IJwtTokenGenerator _mockJwtTokenGenerator;

    public RegisterLecturerCommandHandlerTests()
    {
        _mockUnitOfWork = Substitute.For<IUnitOfWork>();
        _mockJwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _sut = new RegisterLecturerCommandHandler(_mockUnitOfWork, _mockJwtTokenGenerator);
    }
    
    [Fact]
    public async Task Handler_WhenUserNotExistsByEmail_ShouldCreateTheUserAddUserToTheDatabaseThenCreateTheLecturerAddTheLecturerToTheDatabaseGenerateTheTokenAndReturnResult()
    {
        // Arrange
        var command = RegisterLecturerCommandUtils.CreateRegisterLecturerCommand();

        _mockUnitOfWork.Users.UserExistsByEmail(command.Email).Returns(false);

        _mockJwtTokenGenerator.GenerateToken(Arg.Any<Guid>(),
                Constants.Authentication.FullName,
                Constants.Authentication.Email,
                Constants.Authentication.LecturerRole)
            .Returns(Constants.Authentication.Token);

        // Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateCreatedUser();

        await _mockUnitOfWork.Users.Received(1).AddAsync(Arg.Any<User>());
        await _mockUnitOfWork.GetRepository<Lecturer>().Received(1).AddAsync(Arg.Any<Lecturer>());
        await _mockUnitOfWork.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenUserAlreadyExistsByEmail_ShouldReturnDuplicateEmailError()
    {
        // Arrange
        var command = RegisterLecturerCommandUtils.CreateRegisterLecturerCommand();

        _mockUnitOfWork.Users.UserExistsByEmail(command.Email).Returns(true);

        // Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.User.DuplicateEmail);
    }
}
