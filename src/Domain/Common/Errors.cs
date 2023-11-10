using ErrorOr;

namespace Domain.Common;

public static class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict(
            description: "User with the same email already exists");

        public static Error UserNotFound => Error.NotFound(
            description: "User not found");
    }
}