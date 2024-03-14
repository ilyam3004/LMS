using Application.Common.Interfaces.Persistence;
using Application.Features.Tasks.Queries.GetLecturerTaskDetails;
using Application.UnitTests.Tasks.Queries.TestUtils;
using Domain.Common;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;

namespace Application.UnitTests.Tasks.Queries;

public class GetLecturerTaskDetailsQueryHandlerTests
{
    private readonly GetLecturerTaskDetailsQueryHandler _sut;
    private readonly IUnitOfWork _unitOfWork;

    public GetLecturerTaskDetailsQueryHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _sut = new GetLecturerTaskDetailsQueryHandler(_unitOfWork);
    }

    [Fact]
    public async Task Handler_WhenTaskExists_ShouldReturnLecturerResult()
    {
        //Arrange
        var query = GetLecturerTaskDetailsQueryUtils.CreateGetLecturerTaskDetailsQuery();
        
        _unitOfWork.Tasks.GetTaskByIdWithRelations(query.TaskId)
            .Returns(TaskFactory.CreateTask());

        //Act
        var result = await _sut.Handle(query, default);
        
        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Task.Should().NotBeNull();
    }
    
    
    [Fact]
    public async Task Handler_WhenTaskDoesNotExists_ShouldReturnTaskNotFoundError()
    {
        //Arrange
        var query = GetLecturerTaskDetailsQueryUtils.CreateGetLecturerTaskDetailsQuery();
        
        _unitOfWork.Tasks.GetTaskByIdWithRelations(query.TaskId)
            .ReturnsNull();

        //Act
        var result = await _sut.Handle(query, default);
        
        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.TaskNotFound);
    }
}