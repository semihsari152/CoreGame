using ApplicationLayer.DTOs.Comments;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validators.Comments
{
    public class CommentCreateValidator : AbstractValidator<CommentCreateDto>
    {
        public CommentCreateValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Comment content is required.")
                .MinimumLength(3).WithMessage("Comment must be at least 3 characters.")
                .MaximumLength(2000).WithMessage("Comment cannot exceed 2000 characters.");

            RuleFor(x => x.CommentableType)
                .NotEmpty().WithMessage("Commentable type is required.")
                .Must(BeValidCommentableType).WithMessage("Invalid commentable type.");

            RuleFor(x => x.CommentableId)
                .GreaterThan(0).WithMessage("Commentable ID must be greater than 0.");

            RuleFor(x => x.ParentCommentId)
                .GreaterThan(0).WithMessage("Parent comment ID must be greater than 0.")
                .When(x => x.ParentCommentId.HasValue);
        }

        private static bool BeValidCommentableType(string commentableType)
        {
            var validTypes = new[] { "Game", "GameReview", "Guide", "BlogPost", "ForumTopic", "ForumPost", "User" };
            return validTypes.Contains(commentableType);
        }
    }
}
