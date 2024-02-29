using Domain.Enums;

namespace Application.UnitTests.TestUtils.TestConstants;

public static partial class Constants
{
    public static class Task
    {
        public static readonly Guid TaskId = Guid.NewGuid();
        public static readonly Guid StudentTaskId = Guid.NewGuid();
        public static readonly Guid TaskCommentId = Guid.NewGuid();

        public const string Title = "Title";
        public const string Description = "Description";
        public const string Comment = "Comment";

        public static readonly DateTime CreatedAt = DateTime.Now;
        public static readonly DateTime? Deadline = DateTime.Now.AddDays(7);
        public static readonly DateTime? ExpiredDeadline = DateTime.Now.AddDays(-7);
        public static readonly DateTime UploadedAt = DateTime.Now;

        public static readonly StudentTaskStatus Status = StudentTaskStatus.Accepted;

        public const string OrdinalFileName = "OrdinalFileName";
        public const string FileUrl = "FileUrl";

        public const int Grade = 5;
        public const int MaxGrade = 10;
        public const int TooHighGrade = 11;

        public static string TaskTitleFromGivenIndex(int index)
            => $"{Title} {index}";

        public static string TaskDescriptionFromGivenIndex(int index)
            => $"{Description} {index}";
        
        public static string CommentFromGivenIndex(int index)
            => $"{Comment} {index}";
    }
}