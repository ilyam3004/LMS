using Domain.Enums;

namespace Application.UnitTests.TestUtils.TestConstants;

public static partial class Constants
{
    public static class Task
    {
        public static readonly Guid TaskId = Guid.NewGuid();
        public static readonly Guid StudentTaskId = Guid.NewGuid();
        public static readonly Guid TaskCommentId = Guid.NewGuid();

        public const string TaskTitle = "Title";
        public const string TaskDescription = "Description";
        public const string Comment = "Comment";

        public static readonly DateTime CreatedAt = DateTime.Now;
        public static readonly DateTime? Deadline = DateTime.Now.AddDays(7);
        public static readonly DateTime UploadedAt = DateTime.Now;

        public static readonly StudentTaskStatus Status = StudentTaskStatus.Accepted;

        public const string OrdinalFileName = "OrdinalFileName";
        public const string FileUrl = "FileUrl";

        public const int Grade = 5;
        public const int MaxGrade = 10;

        public static string TaskTitleFromGivenIndex(int index)
            => $"{TaskTitle} {index}";

        public static string TaskDescriptionFromGivenIndex(int index)
            => $"{TaskDescription} {index}";
        
        public static string CommentFromGivenIndex(int index)
            => $"{Comment} {index}";
    }
}