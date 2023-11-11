using ErrorOr;

namespace Domain.Common;

public static class Errors
{
    public static class User
    {
        public static Error DuplicateEmail => Error.Conflict("User.DuplicateEmail",
            "User with the same email already exists");

        public static Error UserNotFound => Error.NotFound("User.UserNotFound",
            description: "User not found");
    }
    
    public static class Group
    {
        public static Error NotFound => Error.NotFound("Group.NotFound",
            description: "Group not found");
    }
}