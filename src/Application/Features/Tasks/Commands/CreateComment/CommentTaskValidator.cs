using FluentValidation;

namespace Application.Features.Tasks.Commands.CreateComment;

public class CommentTaskValidator : AbstractValidator<CommentTaskCommand>
{
    public CommentTaskValidator()
    {
        RuleFor(c => c.Comment)
            .NotEmpty()
            .MaximumLength(300);
    }
}