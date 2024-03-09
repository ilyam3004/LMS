using Application.Common.Interfaces.Authentication;
using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;
using Task = System.Threading.Tasks.Task;
using Application.Common.Interfaces.Persistence;
using Application.Features.Authentication.Commands.RegisterStudent;
using Application.UnitTests.Authentication.Commands.TestUtils;
using Application.UnitTests.TestUtils.Extensions;
using Application.UnitTests.TestUtils.Factories;
using Application.UnitTests.TestUtils.TestConstants;
using NSubstitute.ReturnsExtensions;
using Domain.Common;
using Domain.Entities;
using FluentAssertions;
using NSubstitute;

namespace Application.UnitTests.Authentication.Commands.RegisterStudent;

public class RegisterStudentCommandHandlerTests
{
    private readonly RegisterStudentCommandHandler _sut;
    private readonly IUnitOfWork _mockUnitOfWork;
    private readonly IJwtTokenGenerator _mockJwtTokenGenerator;

    public RegisterStudentCommandHandlerTests()
    {
        _mockUnitOfWork = Substitute.For<IUnitOfWork>();
        _mockJwtTokenGenerator = Substitute.For<IJwtTokenGenerator>();
        _sut = new RegisterStudentCommandHandler(_mockUnitOfWork, _mockJwtTokenGenerator);
    }

    [Theory]
    [MemberData(nameof(ValidRetrieveStudentGroupData))]
    public async Task Handler_WhenUserNotExistsByEmailAndGroupExistsByName_ShouldAddUserAndStudentToDatabaseCreateStudentTasksAndGenerateTheToken(
        Group group)
    {
        // Arrange
        var command = RegisterStudentCommandUtils.CreateRegisterStudentCommand();

        _mockUnitOfWork.Users.UserExistsByEmail(command.Email).Returns(false);

        _mockUnitOfWork.Groups.GetGroupByName(Constants.Group.GroupName).Returns(group);

        _mockJwtTokenGenerator.GenerateToken(Arg.Any<Guid>(),
                Constants.Authentication.FullName,
                Constants.Authentication.Email,
                Constants.Student.StudentRole)
            .Returns(Constants.Authentication.Token);

        // Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateCreatedUser();

        await _mockUnitOfWork.Users.Received(1).AddAsync(Arg.Any<User>());
        await _mockUnitOfWork.GetRepository<Student>().Received(1)
            .AddAsync(Arg.Any<Student>());
        await _mockUnitOfWork.StudentTasks.Received(CalculateNumberOfAddStudentTaskCalls(group.Subjects))
            .AddAsync(Arg.Any<StudentTask>());
        await _mockUnitOfWork.Received(2).SaveChangesAsync();
    }

    [Fact]
    public async Task Handler_WhenUserAlreadyExistsByEmail_ShouldReturnDuplicateEmailError()
    {
        // Arrange
        var command = RegisterStudentCommandUtils.CreateRegisterStudentCommand();

        _mockUnitOfWork.Users.UserExistsByEmail(command.Email).Returns(true);

        // Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Authentication.DuplicateEmail);
    }

    [Fact]
    public async Task Handler_WhenUserNotExistsByEmailButGroupNotExistsByName_ShouldReturnGroupNotFoundError()
    {
        // Arrange
        var command = RegisterStudentCommandUtils.CreateRegisterStudentCommand();

        _mockUnitOfWork.Users.UserExistsByEmail(command.Email).Returns(false);

        _mockUnitOfWork.Groups.GetGroupByName(command.GroupName).ReturnsNull();

        // Act
        var result = await _sut.Handle(command, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Group.GroupNotFound);
    }

    public static IEnumerable<object[]> ValidRetrieveStudentGroupData()
    {
        yield return [GroupFactory.CreateGroupWithSubjects()];

        yield return
        [
            GroupFactory.CreateGroupWithSubjects(subjects:
                SubjectFactory.CreateSubjects(subjectsCount: 4))
        ];

        yield return
        [
            GroupFactory.CreateGroupWithSubjects(subjects:
                SubjectFactory.CreateSubjects(subjectsCount: 4,
                    tasks: TaskFactory.CreateTasks(tasksCount: 4)))
        ];
    }

    private static int CalculateNumberOfAddStudentTaskCalls(List<Subject> subjects)
        => subjects.Sum(subject => subject.Tasks.Count);
}