using ApplicationLayer.DTOs.Games;
using DomainLayer.Interfaces.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Validators.Games
{
    public class GameCreateValidator : AbstractValidator<GameCreateDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GameCreateValidator(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Game title is required.")
                .MaximumLength(200).WithMessage("Game title cannot exceed 200 characters.")
                .MustAsync(BeUniqueTitle).WithMessage("A game with this title already exists.");

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

            RuleFor(x => x.ReleaseDate)
                .LessThanOrEqualTo(DateTime.Now.AddYears(5))
                .WithMessage("Release date cannot be more than 5 years in the future.")
                .When(x => x.ReleaseDate.HasValue);

            //RuleFor(x => x.CategoryIds)
            //    .NotEmpty().WithMessage("At least one category must be selected.")
            //    .Must(x => x.Count <= 5).WithMessage("Cannot select more than 5 categories.");

            //RuleFor(x => x.PlatformIds)
            //    .NotEmpty().WithMessage("At least one platform must be selected.")
            //    .Must(x => x.Count <= 10).WithMessage("Cannot select more than 10 platforms.");

            RuleFor(x => x.CoverImageUrl)
                .Must(BeValidUrl).WithMessage("Cover image URL is not valid.")
                .When(x => !string.IsNullOrEmpty(x.CoverImageUrl));
        }

        private async Task<bool> BeUniqueTitle(string title, CancellationToken cancellationToken)
        {
            return !await _unitOfWork.Games.ExistsAsync(g => g.Title.ToLower() == title.ToLower());
        }

        private static bool BeValidUrl(string? url)
        {
            if (string.IsNullOrEmpty(url)) return true;
            return Uri.TryCreate(url, UriKind.Absolute, out _);
        }
    }
}
