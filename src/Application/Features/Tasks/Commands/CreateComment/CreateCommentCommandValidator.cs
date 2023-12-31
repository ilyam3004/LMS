﻿using FluentValidation;

namespace Application.Features.Tasks.Commands.CreateComment;

public class CreateCommentCommandValidator : AbstractValidator<CreateCommentCommand>
{
    public CreateCommentCommandValidator()
    {
        RuleFor(c => c.Comment)
            .NotEmpty()
            .MaximumLength(300);
    }
}