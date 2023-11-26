using System.Security.Cryptography;
using Domain.Abstractions.Errors;

namespace Domain.Common;

public static class Errors
{
    public static class User
    {
        public static Error InvalidToken => Error.Unauthorized("User.InvalidToken",
            description: "Invalid token");
        
        public static Error InvalidPassword => Error.Unauthorized("User.InvalidPassword",
            description: "Invalid email or password");
        
        public static Error DuplicateEmail => Error.Conflict("User.DuplicateEmail",
            "User with the same email already exists");

        public static Error UserNotFound => Error.NotFound("User.UserNotFound",
            description: "User not found");
    }

    public static class File
    {
        public static Error FileNotFound => Error.NotFound("File.FileNotFound",
            description: "File not found");
    }

    public static class Group
    {
        public static Error NotFound => Error.NotFound("Group.NotFound",
            description: "Group not found");
    }

    public static class Subject
    {
        public static Error SubjectAlreadyExists => Error.Conflict("Subject.SubjectAlreadyExists",
            description: "Subject with the same name already exists in this group");
        
        public static Error SubjectNotFound => Error.NotFound("Subject.SubjectNotFound",
            description: "Subject not found");
    }
    
    public static class Task
    {
        public static Error TaskNotFound => Error.NotFound("Task.TaskNotFound",
            description: "Task not found");

        public static Error StudentTaskNotFound => Error.NotFound("Task.StudentTaskNotFound",
            description: "Uploaded Student task not found");
        
        public static Error TaskNotUploaded => Error.Conflict("Task.TaskNotUploaded",
            description: "Task is not uploaded");
        
        public static Error GradeTooHigh => Error.Conflict("Task.GradeTooHigh",
            description: "Grade is too high");
    }
}