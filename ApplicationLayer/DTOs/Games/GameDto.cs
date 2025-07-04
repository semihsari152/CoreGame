using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.DTOs.Games
{
    public class GameDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public DateTime? ReleaseDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int DiscountPercentage { get; set; }
        public bool IsFree { get; set; }
        public string? CoverImageUrl { get; set; }
        public string? HeaderImageUrl { get; set; }
        public decimal AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public int ViewCount { get; set; }
        public string Status { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
    }

    public class GameListDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int DiscountPercentage { get; set; }
        public string? CoverImageUrl { get; set; }
        public decimal AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public List<string> Categories { get; set; } = new();
        public List<string> Platforms { get; set; } = new();
    }

    public class GameDetailDto : GameDto
    {
        public string? BackgroundImageUrl { get; set; }
        public string? TrailerUrl { get; set; }
        public string? MinimumRequirements { get; set; }
        public string? RecommendedRequirements { get; set; }
        public List<string> Screenshots { get; set; } = new();
        public List<CategoryDto> Categories { get; set; } = new();
        public List<PlatformDto> Platforms { get; set; } = new();
        public List<string> Tags { get; set; } = new();
    }

    public class GameCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public DateTime? ReleaseDate { get; set; }
        public decimal? Price { get; set; }
        public string? CoverImageUrl { get; set; }
        public List<int> CategoryIds { get; set; } = new();
        public List<int> PlatformIds { get; set; } = new();
    }

    public class GameUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;
        public string Publisher { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;
        public DateTime? ReleaseDate { get; set; }
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int DiscountPercentage { get; set; }
        public string? CoverImageUrl { get; set; }
    }

    public class GameStatsDto
    {
        public int GameId { get; set; }
        public int ViewCount { get; set; }
        public decimal AverageRating { get; set; }
        public int TotalRatings { get; set; }
        public int ReviewCount { get; set; }
        public int CommentCount { get; set; }
    }

    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public string ColorCode { get; set; } = string.Empty;
    }

    public class PlatformDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string DisplayName { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
    }
}
