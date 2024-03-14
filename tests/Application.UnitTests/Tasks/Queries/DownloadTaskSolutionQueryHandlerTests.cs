using System.Net.Mime;
using Application.Common.Interfaces.Persistence;
using Application.Features.Tasks.Queries.DownloadTaskSolution;
using Application.Services;
using Application.UnitTests.Tasks.Queries.TestUtils;
using Application.UnitTests.TestUtils.Extensions;
using Application.UnitTests.TestUtils.TestConstants;
using Domain.Abstractions.Errors;
using Domain.Common;
using FluentAssertions;
using Microsoft.AspNetCore.Http.Features;
using NSubstitute;
using NSubstitute.ReturnsExtensions;
using TaskFactory = Application.UnitTests.TestUtils.Factories.TaskFactory;

namespace Application.UnitTests.Tasks.Queries;

public class DownloadTaskSolutionQueryHandlerTests {
    private readonly DownloadTaskSolutionQueryHandler _sut;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IFileManager _fileManager;

    public DownloadTaskSolutionQueryHandlerTests()
    {
        _unitOfWork = Substitute.For<IUnitOfWork>();
        _fileManager = Substitute.For<IFileManager>();
        _sut = new DownloadTaskSolutionQueryHandler(_unitOfWork,
            _fileManager);
    }

    [Fact]
    public async Task Handler_WhenStudentTaskExistsAndFileExistsByUrlAndOrdinalFileNameIsNotNull_ShouldReadFileAsArrayOfBytesAndReturnDownloadTaskResult()
    {
        //Arrange
        var query = DownloadTaskSolutionQueryUtils.CreateDownloadTaskSolutionQuery();
        
        _unitOfWork.StudentTasks.GetByIdAsync(query.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithoutTaskObject(
                uploadedAt: Constants.Task.UploadedAt,
                fileUrl: Constants.File.FileUrl,
                ordinalFileName: Constants.File.OrdinalFileName));

        _fileManager.FileExists(Constants.File.FileUrl)
            .Returns(true);

        _fileManager.ReadFileAsArrayOfBytes(Constants.File.FileUrl)
            .Returns(Constants.File.FileContentAsArrayOfBytes);

        _fileManager.GetContentType(Constants.File.OrdinalFileName)
            .Returns(Constants.File.ContentType);

        //Act
        var result = await _sut.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.ValidateDownloadStudentTaskSolutionFile();
    }
    
    
    [Fact]
    public async Task Handler_WhenStudentTaskExistsAndFileExistsByUrlButOrdinalNameIsNull_ShouldReturnOrdinalFileNameNotFoundError()
    {
        //Arrange
        var query = DownloadTaskSolutionQueryUtils.CreateDownloadTaskSolutionQuery();
        
        _unitOfWork.StudentTasks.GetByIdAsync(query.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithoutTaskObject(
                uploadedAt: Constants.Task.UploadedAt,
                fileUrl: Constants.File.FileUrl,
                ordinalFileName: null));

        _fileManager.FileExists(Constants.File.FileUrl)
            .Returns(true);

        //Act
        var result = await _sut.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.File.OrdinalFileNameNotFound);
        await _fileManager.Received(0).ReadFileAsArrayOfBytes(Arg.Any<string>());
        _fileManager.Received(0).GetContentType(Arg.Any<string>());
    }
    
    
    [Fact]
    public async Task Handler_WhenStudentTaskExistsButFileDoesNotExistsByUrl_ShouldReturnFileNotFoundError()
    {
        //Arrange
        var query = DownloadTaskSolutionQueryUtils.CreateDownloadTaskSolutionQuery();
        
        _unitOfWork.StudentTasks.GetByIdAsync(query.StudentTaskId)
            .Returns(TaskFactory.CreateStudentTaskWithoutTaskObject(
                uploadedAt: Constants.Task.UploadedAt,
                fileUrl: Constants.File.FileUrl,
                ordinalFileName: Constants.File.OrdinalFileName));

        _fileManager.FileExists(Constants.File.FileUrl)
            .Returns(false);

        //Act
        var result = await _sut.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.File.FileNotFound);
        await _fileManager.Received(0).ReadFileAsArrayOfBytes(Arg.Any<string>());
        _fileManager.Received(0).GetContentType(Arg.Any<string>());
    }
    
    
    [Fact]
    public async Task Handler_WhenStudentTaskDoesNotExists_ShouldStudentTaskNotFoundError()
    {
        //Arrange
        var query = DownloadTaskSolutionQueryUtils.CreateDownloadTaskSolutionQuery();
        
        _unitOfWork.StudentTasks.GetByIdAsync(query.StudentTaskId)
            .ReturnsNull();

        //Act
        var result = await _sut.Handle(query, default);

        //Assert
        result.IsSuccess.Should().BeFalse();
        result.Errors.Should().ContainEquivalentOf(Errors.Task.StudentTaskNotFound);
        await _fileManager.Received(0).ReadFileAsArrayOfBytes(Arg.Any<string>());
        _fileManager.Received(0).GetContentType(Arg.Any<string>());
    }
}