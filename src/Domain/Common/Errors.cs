using Domain.Abstractions.Errors;

namespace Domain.Common;

public static class Errors
{
    public static class Authentication
    {
        public static Error InvalidToken => Error.Unauthorized("User.InvalidToken",
            description: "Invalid token");
        
        public static Error InvalidCredentials => Error.Unauthorized("User.InvalidPassword",
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
        
        public static Error OrdinalFileNameNotFound => Error.NotFound("File.OrdinalFileNameNotFound",
            description: "Ordinal file name not found, because task status not uploaded");
    }

    public static class Group
    {
        public static Error GroupNotFound => Error.NotFound("Group.NotFound",
            description: "Group not found");
    }

    public static class Subject
    {
        public static Error SubjectAlreadyExists => Error.Conflict("Subject.SubjectAlreadyExists",
            description: "Subject with the same name already exists in this group");
        
        public static Error SubjectNotFound => Error.NotFound("Subject.SubjectNotFound",
            description: "Subject not found");

        public static Error LecturerNotOwnerOfSubject => Error.Unauthorized("Subject.LecturerNotOwnerOfSubject",
            description: "You are not an owner of this subject");
    }
    
    public static class Task
    {
        public static Error TaskNotFound => Error.NotFound("Task.TaskNotFound",
            description: "Task not found");

        public static Error StudentTaskNotFound => Error.NotFound("Task.StudentTaskNotFound",
            description: "Uploaded Student task not found");
        
        public static Error StudentTaskNotUploaded => Error.Conflict("Task.StudentTaskNotUploaded",
            description: "Student Task is not uploaded");
        
        public static Error GradeTooHigh => Error.Conflict("Task.GradeTooHigh",
            description: "Grade is too high");
        
        public static Error WrongTaskStatus => Error.Conflict("Task.WrongTaskStatus",
            description: "You can't remove this task. Check the task status");
        
        public static Error InvalidTaskStatusToRemoveSolution => Error.Conflict("Task.InvalidTaskStatusToRemoveSolution",
            description: "You can't remove solution. Check the task status");

        public static Error TaskDeadlineNotExpired => Error.Conflict("Task.TaskDeadlineNotExpired",
            description: "You cannot reject this task. Task deadline not expired");
    }
}