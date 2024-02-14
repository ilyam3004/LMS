using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Task = System.Threading.Tasks.Task;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Features.Subjects.Commands.CreateSubject;
using Application.UnitTests.Subjects.Commands.TestUtils;
using Application.UnitTests.TestUtils.Extensions;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.TestUtils.TestConstants;
using Domain.Abstractions.Errors;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Application.UnitTests.Subjects.Commands;

public class CreateSubjectCommandHandlerTests
{
    private readonly CreateSubjectCommandHandler _sut;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtTokenReader _jwtTokenReader;

    public CreateSubjectCommandHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _sut = new CreateSubjectCommandHandler(_unitOfWork, _jwtTokenReader);
    }

    [Theory]
    [MemberData(nameof(ValidRetrieveSubjectsData))]
    public async Task
        Handler_WhenGroupAndUserExistsAndSubjectWithTheSameNameDoesNotExist_ShouldCreateSubjectAndReturnLecturerSubjects(
            List<Subject> subjects)
    {
        //Arrange
        var command = CreateSubjectCommandUtils.CreateCreateSubjectCommand();

        _unitOfWork.Groups.GetGroupByName(command.GroupName)
            .Returns(GroupFactory.CreateGroupWithSubjects(subjects: subjects));

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication
                .UserIdFromToken.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(
                Constants.Authentication.UserIdFromToken)
            .Returns(AuthenticationFactory.CreateLecturerUser());

        _unitOfWork.Subjects.GetLecturerSubjects(Constants.Lecturer.LecturerId)
            .Returns(subjects);

        // Act 
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateRetrievedSubjects(subjects);
        await _unitOfWork.Subjects.Received(1).AddAsync(Arg.Any<Subject>());
        await _unitOfWork.Received(1).SaveChangesAsync();
    }

    public static IEnumerable<object[]> ValidRetrieveSubjectsData()
    {
        yield return
        [
            SubjectFactory.CreateSubjects()
        ];

        yield return
        [
            SubjectFactory.CreateSubjects(subjectsCount: 4,
                tasks: TaskFactory.CreateTasks(tasksCount: 4))
        ];

        yield return
        [
            SubjectFactory.CreateSubjects(subjectsCount: 8,
                tasks: TaskFactory.CreateTasks(tasksCount: 10))
        ];
    }

    [Fact]
    public async Task Handler_WhenGroupIsNotExists_ShouldReturnGroupNotFoundError()
    {
        //Arrange
        var command = CreateSubjectCommandUtils.CreateCreateSubjectCommand();

        _unitOfWork.Groups.GetGroupByName(command.GroupName)
            .ReturnsNull();

        // Act 
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Group.GroupNotFound);
        await _unitOfWork.Subjects.Received(0).AddAsync(Arg.Any<Subject>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenGroupExistsAndUserNotExists_ShouldReturnUserNotFound()
    {
        //Arrange
        var command = CreateSubjectCommandUtils.CreateCreateSubjectCommand();

        _unitOfWork.Groups.GetGroupByName(command.GroupName)
            .Returns(GroupFactory.CreateGroupWithSubjects());

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication
                .UserIdFromToken.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(
                Constants.Authentication.UserIdFromToken)
            .ReturnsNull();

        // Act 
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.User.UserNotFound);
        await _unitOfWork.Subjects.Received(0).AddAsync(Arg.Any<Subject>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }

    [Fact]
    public async Task
        Handler_WhenGroupExistsButSubjectWithTheSameNameAlreadyExistsInGroup_ShouldReturnSubjectAlreadyExists()
    {
        //Arrange
        var command = CreateSubjectCommandUtils
            .CreateCreateSubjectCommand(subjectName:
                Constants.Subject.SubjectNameFromGivenIndex(0));

        _unitOfWork.Groups.GetGroupByName(command.GroupName)
            .Returns(GroupFactory.CreateGroupWithSubjects());

        // Act 
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Subject.SubjectAlreadyExists);
        await _unitOfWork.Subjects.Received(0).AddAsync(Arg.Any<Subject>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenTokenIsInvalid_ShouldReturnInvalidTokenError()
    {
        //Arrange
        var command = CreateSubjectCommandUtils.CreateCreateSubjectCommand();

        _unitOfWork.Groups.GetGroupByName(command.GroupName)
            .Returns(GroupFactory.CreateGroupWithSubjects());

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .ReturnsNull();

        // Act 
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.User.InvalidToken);
        await _unitOfWork.Subjects.Received(0).AddAsync(Arg.Any<Subject>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }
}