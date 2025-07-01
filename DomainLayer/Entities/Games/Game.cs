using DomainLayer.Common;
using DomainLayer.Entities.Social;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DomainLayer.Entities.Games
{
    public class Game : BaseEntity
    {
        // Temel Bilgiler
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ShortDescription { get; set; } = string.Empty;

        // Yayıncı ve Geliştirici
        public string Publisher { get; set; } = string.Empty;
        public string Developer { get; set; } = string.Empty;

        // Tarihler
        public DateTime? ReleaseDate { get; set; }
        public DateTime? EarlyAccessDate { get; set; }

        // Fiyat Bilgileri
        public decimal? Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public int DiscountPercentage { get; set; } = 0;
        public bool IsFree { get; set; } = false;

        // Medya
        public string? CoverImageUrl { get; set; }
        public string? HeaderImageUrl { get; set; }
        public string? BackgroundImageUrl { get; set; }
        public string? TrailerUrl { get; set; }

        // Platform ve Kategori
        public Platform SupportedPlatforms { get; set; } // Flags enum
        public GameStatus Status { get; set; }

        // Derecelendirme ve İstatistikler
        public decimal AverageRating { get; set; } = 0;
        public int TotalRatings { get; set; } = 0;
        public int ViewCount { get; set; } = 0;
        public int DownloadCount { get; set; } = 0;

        // Sistem Gereksinimleri (JSON olarak saklayacağız)
        public string? MinimumRequirements { get; set; }
        public string? RecommendedRequirements { get; set; }

        // SEO ve Meta
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? Tags { get; set; } // Virgülle ayrılmış

        // External API Bilgileri
        public string? SteamAppId { get; set; }
        public string? EpicStoreId { get; set; }
        public string? IgdbId { get; set; }

        // Navigation Properties (İlişkiler)
        public virtual ICollection<GameCategory> GameCategories { get; set; } = new List<GameCategory>();
        public virtual ICollection<GamePlatform> GamePlatforms { get; set; } = new List<GamePlatform>();
        public virtual ICollection<GameTag> GameTags { get; set; } = new List<GameTag>();
        public virtual ICollection<GameReview> GameReviews { get; set; } = new List<GameReview>();
        public virtual ICollection<GameImage> GameImages { get; set; } = new List<GameImage>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<UserGameList> UserGameLists { get; set; } = new List<UserGameList>();
    }
}
