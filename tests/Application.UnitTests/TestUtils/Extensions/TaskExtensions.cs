using Application.Models.Tasks;
using Domain.Enums;
using FluentAssertions;

namespace Application.UnitTests.TestUtils.Extensions;

public static class TaskExtensions
{
    public static void ValidateRetrieveStudentTask(this StudentTaskResult result)
    {
        result.Should().NotBeNull();
        
        result.Task.Should().NotBeNull();
        
        result.UploadedTask.Should().NotBeNull();
        result.UploadedTask.StudentTaskId.Should().Be(result.UploadedTask.StudentTaskId);
        result.UploadedTask.StudentId.Should().Be(result.UploadedTask.StudentId);
        result.UploadedTask.TaskId.Should().Be(result.UploadedTask.TaskId);
        result.UploadedTask.FileUrl.Should().BeNull();
        result.UploadedTask.OrdinalFileName.Should().BeNull();
        result.UploadedTask.Status.Should().Be(StudentTaskStatus.NotUploaded);
        result.UploadedTask.UploadedAt.Should().BeNull();
    }

    public static void ValidateDownloadStudentTaskSolutionFile(
        this DownloadTaskResult result)
    {
        result.FileContent.Should().NotBeNullOrEmpty();
        result.ContentType.Should().NotBeNullOrEmpty();
        result.FileName.Should().NotBeNullOrEmpty();
    }
}