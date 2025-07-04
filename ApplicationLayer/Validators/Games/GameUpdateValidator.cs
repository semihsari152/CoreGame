using ApplicationLayer.DTOs.Games;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validators.Games
{
    public class GameUpdateValidator : AbstractValidator<GameUpdateDto>
    {
        public GameUpdateValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Game title is required.")
                .MaximumLength(200).WithMessage("Game title cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Game description is required.")
                .MaximumLength(2000).WithMessage("Game description cannot exceed 2000 characters.");

            RuleFor(x => x.ShortDescription)
                .MaximumLength(500).WithMessage("Short description cannot exceed 500 characters.");

            RuleFor(x => x.Publisher)
                .NotEmpty().WithMessage("Publisher is required.")
                .MaximumLength(100).WithMessage("Publisher name cannot exceed 100 characters.");

            RuleFor(x => x.Developer)
                .NotEmpty().WithMessage("Developer is required.")
                .MaximumLength(100).WithMessage("Developer name cannot exceed 100 characters.");

            RuleFor(x => x.Price)
                .GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.")
                .When(x => x.Price.HasValue);

            RuleFor(x => x.DiscountPrice)
                .GreaterThanOrEqualTo(0).WithMessage("Discount price cannot be negative.")
                .LessThan(x => x.Price).WithMessage("Discount price must be less than regular price.")
                .When(x => x.DiscountPrice.HasValue && x.Price.HasValue);

            RuleFor(x => x.DiscountPercentage)
                .InclusiveBetween(0, 100).WithMessage("Discount percentage must be between 0 and 100.");

            RuleFor(x => x.ReleaseDate)
                .LessThanOrEqualTo(DateTime.Now.AddYears(5))
                .WithMessage("Release date cannot be more than 5 years in the future.")
                .When(x => x.ReleaseDate.HasValue);

            RuleFor(x => x.CoverImageUrl)
                .Must(BeValidUrl).WithMessage("Cover image URL is not valid.")
                .When(x => !string.IsNullOrEmpty(x.CoverImageUrl));
        }

        private static bool BeValidUrl(string? url)
        {
            if (string.IsNullOrEmpty(url)) return true;
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
