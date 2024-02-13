using Application.Features.Groups.Queries.GetAllGroups;
using Application.Common.Interfaces.Persistence;
using Application.UnitTests.TestUtils.Extensions;
using Application.UnitTests.TestUtils.Factories;
using Task = System.Threading.Tasks.Task;
using FluentAssertions;
using Domain.Entities;
using NSubstitute;

namespace Application.UnitTests.Groups.Queries;

public class GetAllGetGroupsQueryHandlerTests
{
    private readonly IUnitOfWork _mockUnitOfWork;
    private readonly GetAllGroupsQueryHandler _sut;

    public GetAllGetGroupsQueryHandlerTests()
    {
        _mockUnitOfWork = Substitute.For<IUnitOfWork>();
        _sut = new GetAllGroupsQueryHandler(_mockUnitOfWork);
    }

    [Theory]
    [MemberData(nameof(ValidRetrieveStudentGroupData))]
    public async Task Handler_ShouldReturnGroupResults(List<Group> groups)
    {
        // Arrange 
        var query = new GetAllGroupsQuery();

        _mockUnitOfWork.Groups.GetAllGroupsWithStudents()
            .Returns(groups);

        // Act
        var result = await _sut.Handle(query, default);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateRetrievedGroups(groups);
        await _mockUnitOfWork.Groups.Received(1).GetAllGroupsWithStudents();
    }

    public static IEnumerable<object[]> ValidRetrieveStudentGroupData()
    {
        yield return
        [
            GroupFactory.CreateGroupList()
        ];

        yield return
        [
            GroupFactory.CreateGroupList(groupsCount: 4,
                students: StudentFactory.CreateStudentList(studentsCount: 4))
        ];

        yield return
        [
            GroupFactory.CreateGroupList(groupsCount: 10,
                students: StudentFactory.CreateStudentList(studentsCount: 10))
        ];
    }
}