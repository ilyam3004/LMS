using Constants = Application.UnitTests.TestUtils.TestConstants.Constants;
using Application.Features.Tasks.Commands.UploadTaskSolution;
using Microsoft.AspNetCore.Http;

namespace Application.UnitTests.Tasks.Commands.TestUtils;

public static class UploadTaskSolutionCommandUtils
{
    public static UploadTaskSolutionCommand CreateUploadTaskSolutionCommand(
        string fileContent = null,
        Guid? studentTaskId = null, 
        int? fileLength = null)
        => new(GenerateIFormFile(fileContent, fileLength),
            studentTaskId ?? Constants.Task.StudentTaskId,
            Constants.Authentication.Token);

    private static IFormFile GenerateIFormFile(string? fileContent = null,
        int? iFormFileLength = null)
    {
        (var memoryStream, int fileLength) = Constants.Task.GenerateFileContent(
            fileContent ?? Constants.File.FileContent);
        var file = new FormFile(memoryStream, 0, 
            iFormFileLength ?? fileLength,
            Constants.File.FileNameWithoutExtension,
            Constants.File.FileNameWithExtension);
        
        return file;
    }
    
    
    private static IFormFile GenerateEmptyIFormFile()
    {
        (var memoryStream, int fileLength) = Constants.Task.GenerateFileContent(
            Constants.File.FileContent);
        var file = new FormFile(memoryStream, 0, 0,
            Constants.File.FileNameWithoutExtension,
            Constants.File.FileNameWithExtension);
        
        return file;
    }
}