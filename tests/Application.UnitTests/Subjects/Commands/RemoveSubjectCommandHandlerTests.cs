using Application.Features.Subjects.Commands.RemoveSubject;
using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.UnitTests.Subjects.Commands.TestUtils;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.TestUtils.TestConstants;
using Task = System.Threading.Tasks.Task;
using NSubstitute.ReturnsExtensions;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;
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
        _sut = new RemoveSubjectCommandHandler(_unitOfWork, _jwtTokenReader);
    }

    [Fact]
    public async Task Handler_WhenSubjectExistsTokenIsValidAndUserExists_ShouldRemoveSubjectAndReturnLecturerSubjects()
    {
        // Arrange
        var command = RemoveSubjectCommandUtils.CreateRemoveSubjectCommand();

        _unitOfWork.Subjects
            .GetSubjectByIdWithRelations(command.SubjectId)
            .Returns(SubjectFactory.CreateSubject(
                groupId: Constants.Group.GroupId,
                subjectId: command.SubjectId,
                lecturerId: Constants.Lecturer.LecturerId));

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserId)
            .Returns(AuthenticationFactory.CreateLecturerUser(
                lecturer: AuthenticationFactory.CreateLecturer(
                    lecturerId: Constants.Lecturer.LecturerId)));

        _unitOfWork.Subjects.GetLecturerSubjects(Constants.Lecturer.LecturerId)
            .Returns(SubjectFactory.CreateSubjects(subjectsCount: 4));

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        _unitOfWork.Subjects.Received(1).Remove(Arg.Any<Subject>());
        await _unitOfWork.Received(1).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenSubjectNotExists_ShouldReturnSubjectNotFoundError()
    {
        // Arrange
        var command = RemoveSubjectCommandUtils.CreateRemoveSubjectCommand();

        _unitOfWork.Subjects
            .GetSubjectByIdWithRelations(command.SubjectId)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Subject.SubjectNotFound);
        _unitOfWork.Subjects.Received(0).Remove(Arg.Any<Subject>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenSubjectUserIsNotAnOwnerOfTheSubject_ShouldReturnLecturerIsNotAnOwnerError()
    {
        // Arrange
        var command = RemoveSubjectCommandUtils.CreateRemoveSubjectCommand();

        _unitOfWork.Subjects
            .GetSubjectByIdWithRelations(command.SubjectId)
            .Returns(SubjectFactory.CreateSubject(
                groupId: Constants.Group.GroupId,
                subjectId: command.SubjectId));

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Constants.Authentication.UserId)
            .Returns(AuthenticationFactory.CreateLecturerUser(
                lecturer: AuthenticationFactory.CreateLecturer(
                    lecturerId: Constants.Lecturer.AnotherLecturerId)));

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Subject.LecturerNotOwnerOfSubject);
        _unitOfWork.Subjects.Received(0).Remove(Arg.Any<Subject>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenSubjectExistsButTokenIsInvalid_ShouldReturnInvalidTokenError()
    {
        // Arrange
        var command = RemoveSubjectCommandUtils.CreateRemoveSubjectCommand();

        _unitOfWork.Subjects
            .GetSubjectByIdWithRelations(command.SubjectId)
            .Returns(SubjectFactory.CreateSubject(
                groupId: Constants.Group.GroupId,
                subjectId: command.SubjectId,
                lecturerId: Constants.Lecturer.LecturerId));

        _jwtTokenReader.ReadUserIdFromToken(command.Token)
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(command, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.User.InvalidToken);
        _unitOfWork.Subjects.Received(0).Remove(Arg.Any<Subject>());
        await _unitOfWork.Received(0).SaveChangesAsync();
    }
}