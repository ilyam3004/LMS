namespace Application.UnitTests.TestUtils.TestConstants;

public static partial class Constants
{
    public static class Task
    {
        public static readonly Guid TaskId = Guid.NewGuid();
        public const string TaskTitle = "Title";
        public const string TaskDescription = "Description";
        public static readonly DateTime CreatedAt = DateTime.Now;
        public static readonly DateTime? Deadline = DateTime.Now.AddDays(7);
        public const int MaxGrade = 10;
        
        public static string TaskTitleFromGivenIndex(int index)
            => $"{TaskTitle} {index}";

        public static string TaskDescriptionFromGivenIndex(int index)
            => $"{TaskDescription} {index}";
    }
}