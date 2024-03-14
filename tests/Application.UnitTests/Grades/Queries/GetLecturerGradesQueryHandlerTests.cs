using Application.Common.Interfaces.Authentication;
using Application.Common.Interfaces.Persistence;
using Application.Features.Grades.Queries;
using Application.Models.Grades;
using Application.UnitTests.Grades.Queries.TestUtils;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.TestUtils.TestConstants;
using Domain.Common;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using Task = System.Threading.Tasks.Task;

namespace Application.UnitTests.Grades.Queries;

public class GetLecturerGradesQueryHandlerTests
{
    private readonly GetLecturerGradesQueryHandler _sut;
    private readonly IJwtTokenReader _jwtTokenReader;
    private readonly IUnitOfWork _unitOfWork;

    public GetLecturerGradesQueryHandlerTests()
    {
        _jwtTokenReader = Substitute.For<IJwtTokenReader>();
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _sut = new GetLecturerGradesQueryHandler(_unitOfWork, _jwtTokenReader);
    }

    [Fact]
    public async Task Handle_WhenTokenIsInvalid_ShouldReturnInvalidTokenError()
    {
        // Arrange
        var query = GetLecturerGradesQueryUtils.CreateGetLecturerGradesQuery();
        _jwtTokenReader.ReadUserIdFromToken(query.Token).ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.InvalidToken);
    }
    
    [Fact]
    public async Task Handle_WhenUserDoesNotExists_ShouldReturnUserNotFoundError()
    {
        // Arrange
        var query = GetLecturerGradesQueryUtils.CreateGetLecturerGradesQuery();
        _jwtTokenReader.ReadUserIdFromToken(query.Token)
            .Returns(Constants.Authentication.UserId.ToString());

        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .ReturnsNull();

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.UserNotFound);
    }
    
    [Fact]
    public async Task Handle_WhenTokenIsValidAndUserExists_ShouldReturnLecturerGradesResults()
    {
        // Arrange
        var query = GetLecturerGradesQueryUtils.CreateGetLecturerGradesQuery();
        _jwtTokenReader.ReadUserIdFromToken(query.Token)
            .Returns(Constants.Authentication.UserId.ToString());
    
        _unitOfWork.Users.GetUserByIdWithRelations(Arg.Any<Guid>())
            .Returns(AuthenticationFactory.CreateLecturerUser());
    
        _unitOfWork.Subjects.GetLecturerSubjects(Arg.Any<Guid>())
            .Returns(SubjectFactory.CreateSubjects());
        
        // Act
        var result = await _sut.Handle(query, default);
    
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().BeOfType<List<SubjectGradesResult>>();
    }
    
    //
    // [Fact]
    // public async Task Handle_WhenNoSubjectsForUser_ShouldReturnEmptyResult()
    // {
    //     // Arrange
    //     var query = GetLecturerGradesQueryUtils.CreateGetLecturerGradesQuery("valid_token");
    //     var userId = Constants.Authentication.UserId;
    //     _jwtTokenReader.ReadUserIdFromToken(query.Token).Returns(userId.ToString());
    //     _unitOfWork.Users.GetUserByIdWithRelations(userId).Returns(new User());
    //     _unitOfWork.Subjects.GetLecturerSubjects(Arg.Any<Guid>()).Returns(new List<Subject>());
    //
    //     // Act
    //     var result = await _sut.Handle(query, default);
    //
    //     // Assert
    //     result.IsSuccess.Should().BeTrue();
    //     result.Value.Should().BeEmpty();
    // }
    //
    // [Fact]
    // public async Task Handle_WhenSubjectsExistForUser_ShouldReturnSubjectGradesResult()
    // {
    //     // Arrange
    //     var query = GetLecturerGradesQueryUtils.CreateGetLecturerGradesQuery("valid_token");
    //     var userId = Constants.Authentication.UserId;
    //     _jwtTokenReader.ReadUserIdFromToken(query.Token).Returns(userId.ToString());
    //     var user = new User { Lecturer = new Lecturer { LecturerId = Guid.NewGuid() } };
    //     var subject = new Subject { SubjectId = Guid.NewGuid(), Name = "Subject", Group = new Group { Name = "Group" } };
    //     var student = new Student { StudentId = Guid.NewGuid(), FullName = "Student" };
    //     var task = new Domain.Entities.Task { TaskId = Guid.NewGuid(), SubjectId = subject.SubjectId };
    //     var uploadedTask = new UploadedTask { Task = task, Grade = 80 };
    //     var studentTaskResult = new StudentTaskResult(task, uploadedTask);
    //     student.Tasks.Add(uploadedTask);
    //     subject.Group.Students.Add(student);
    //     subject.Tasks.Add(task);
    //     _unitOfWork.Users.GetUserByIdWithRelations(userId).Returns(user);
    //     _unitOfWork.Subjects.GetLecturerSubjects(user.Lecturer!.LecturerId).Returns(new List<Subject> { subject });
    //
    //     // Act
    //     var result = await _sut.Handle(query, default);
    //
    //     // Assert
    //     result.IsSuccess.Should().BeTrue();
    //     result.Value.Should().NotBeEmpty();
    //     result.Value.First().SubjectId.Should().Be(subject.SubjectId);
    //     result.Value.First().SubjectName.Should().Be(subject.Name);
    //     result.Value.First().GroupName.Should().Be(subject.Group.Name);
    //     result.Value.First().StudentGradesResults.Should().NotBeEmpty();
    //     result.Value.First().StudentGradesResults.First().StudentId.Should().Be(student.StudentId);
    //     result.Value.First().StudentGradesResults.First().StudentName.Should().Be(student.FullName);
    //     result.Value.First().StudentGradesResults.First().TotalGrade.Should().Be(uploadedTask.Grade);
    //     result.Value.First().StudentGradesResults.First().AverageGrade.Should().BeApproximately(80, 0.001);
    //     result.Value.First().StudentGradesResults.First().StudentTasksResults.Should().NotBeEmpty();
    //     result.Value.First().StudentGradesResults.First().StudentTasksResults.First().Task.Should().NotBeNull();
    //     result.Value.First().StudentGradesResults.First().StudentTasksResults.First().UploadedTask.Should().NotBeNull();
    // }
}