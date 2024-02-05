namespace Application.UnitTests.TestUtils.TestConstants;

public static partial class Constants
{
    public static class Authentication
    {
        public static readonly Guid UserId = Guid.NewGuid();
        public static readonly Guid StudentId = Guid.NewGuid();
        public static readonly Guid LecturerId = Guid.NewGuid();
        public static readonly Guid UserIdFromToken = Guid.Parse("240cfab7-eb9c-4512-8ebb-85eb7bbb57cb");
        public const string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        public const string Email = "email@gmail.com";
        public const string Password = "password1234";
        public const string FullName = "FullName";
        public const string Degree = "PhD";
        public const string Address = "Address";
        public static readonly DateTime Birthday = new(1999, 1, 1);
    }
}