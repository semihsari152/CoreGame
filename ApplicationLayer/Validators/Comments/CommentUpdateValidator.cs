using ApplicationLayer.DTOs.Comments;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validators.Comments
{
    public class CommentUpdateValidator : AbstractValidator<CommentUpdateDto>
    {
        public CommentUpdateValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Comment content is required.")
                .MinimumLength(3).WithMessage("Comment must be at least 3 characters.")
                .MaximumLength(2000).WithMessage("Comment cannot exceed 2000 characters.");
        }
    }
}
