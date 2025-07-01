using DomainLayer.Common;
using DomainLayer.Entities.Games;
using DomainLayer.Entities.Social;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Content
{
    public class Guide : BaseEntity
    {
        // Temel Bilgiler
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Summary { get; set; }
        public string? Introduction { get; set; }
        public int AuthorId { get; set; }

        // Guide Özellikleri
        public GuideType Type { get; set; } = GuideType.Walkthrough;
        public DifficultyLevel Difficulty { get; set; } = DifficultyLevel.Beginner;
        public int EstimatedTime { get; set; } = 0; // Dakika cinsinden
        public decimal? EstimatedCost { get; set; } // İhtiyaç duyulan oyun içi para vs.

        // İlgili Oyun
        public int? GameId { get; set; }
        public string? GameVersion { get; set; } // Hangi versiyon için geçerli
        public Enums.Platform? TargetPlatform { get; set; } // Hangi platform için

        // İçerik Durumu
        public ContentStatus Status { get; set; } = ContentStatus.Draft;
        public bool IsFeatured { get; set; } = false;
        public bool IsOfficial { get; set; } = false; // Resmi rehber mi
        public bool RequiresUpdate { get; set; } = false; // Güncelleme gerekiyor mu

        // Sosyal ve İstatistikler
        public int ViewCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public int DislikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int BookmarkCount { get; set; } = 0;
        public int ShareCount { get; set; } = 0;

        // Kalite ve Değerlendirme
        public decimal AverageRating { get; set; } = 0;
        public int RatingCount { get; set; } = 0;
        public int HelpfulCount { get; set; } = 0;
        public int UnhelpfulCount { get; set; } = 0;

        // İçerik Özellikleri
        public bool ContainsSpoilers { get; set; } = false;
        public bool IsVideoGuide { get; set; } = false;
        public string? VideoUrl { get; set; }
        public int? VideoDuration { get; set; } // Saniye
        public string? ThumbnailUrl { get; set; }

        // Kategoriler ve Etiketler
        public string? Categories { get; set; } // JSON array
        public string? Tags { get; set; } // Virgülle ayrılmış
        public string? Prerequisites { get; set; } // JSON array - önkoşullar

        // SEO ve Keşfedilebilirlik
        public string? Slug { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }

        // Versiyonlama ve Güncelleme
        public int Version { get; set; } = 1;
        public DateTime? LastUpdatedContent { get; set; }
        public string? UpdateNotes { get; set; }
        public bool IsLatestVersion { get; set; } = true;

        // Moderasyon
        public int? ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string? ReviewNotes { get; set; }

        // Navigation Properties
        public virtual User Author { get; set; } = null!;
        public virtual Game? Game { get; set; }
        public virtual ICollection<GuideStep> GuideSteps { get; set; } = new List<GuideStep>();
        public virtual ICollection<GuideRating> GuideRatings { get; set; } = new List<GuideRating>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Media> MediaFiles { get; set; } = new List<Media>();
        public virtual ICollection<GuideBookmark> Bookmarks { get; set; } = new List<GuideBookmark>();
        public virtual User? Reviewer { get; set; }

        // Computed Properties
        public decimal NetScore => LikeCount - DislikeCount;
        public decimal HelpfulPercentage => (HelpfulCount + UnhelpfulCount) > 0
            ? (decimal)HelpfulCount / (HelpfulCount + UnhelpfulCount) * 100 : 0;
    }
}
