namespace Application.UnitTests.TestUtils.TestConstants;

public static partial class Constants
{
    public static class Authentication
    {
        public static readonly Guid UserIdFromToken = Guid.Parse("240cfab7-eb9c-4512-8ebb-85eb7bbb57cb");
        public static readonly Guid UserId = Guid.NewGuid();
        
        public const string Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
        
        public const string Email = "email@gmail.com";
        public const string ValidPassword = "ValidPassword1234";
        public const string InvalidPassword = "InvalidPassword1234";
        public const string HashForValidPassword = "$2a$11$Yu08L6.6p0fhpgXnEHxnb.tN8QUoFYkVK/mq5mjm8k6XIJp0K1jiy";

        public const string Firstname = "Firstname";
        public const string Lastname = "Lastname";
        public const string FullName = $"{Firstname} {Lastname}";
        
        public const string Address = "Address";
        public static readonly DateTime Birthday = new(1999, 1, 1);
        
        public static string FullNameFromGiveIndex(int index)
            => $"{FullName} {index}";
        
        public static string AddressFromGivenIndex(int index)
            => $"{Address} {index}";
        
        public static DateTime BirthdayFromGivenIndex(int index)
            => Birthday.AddYears(index);
    }
}