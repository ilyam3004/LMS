namespace Application.UnitTests.TestUtils.TestConstants;

public static partial class Constants
{
   public static class Subject
   {
      public static readonly Guid SubjectId = Guid.NewGuid();
      public const string SubjectName = "Subject";
      public const string SubjectDescription = "Description";

      public static string SubjectNameFromGivenIndex(int index)
         => $"{SubjectName} {index}";

      public static string SubjectDescriptionFromGivenIndex(int index)
         => $"{SubjectDescription} {index}";
   }
}