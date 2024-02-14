namespace Application.UnitTests.TestUtils.TestConstants;

public static partial class Constants
{
    public static class Lecturer
    {
        public static readonly Guid LecturerId = Guid.NewGuid();
        public static readonly Guid AnotherLecturerId = Guid.NewGuid();
        public const string LecturerRole = "Lecturer";
        public const string Degree = "PhD";
    }
}