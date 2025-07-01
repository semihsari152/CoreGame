using DomainLayer.Common;
using DomainLayer.Entities.Social;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Games
{
    public class GameReview : BaseEntity
    {
        // Temel Bilgiler
        public int UserId { get; set; }
        public int GameId { get; set; }

        // Review İçeriği
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Summary { get; set; } // Kısa özet

        // Puanlama Sistemi
        public decimal OverallRating { get; set; } // 1-10 arası genel puan
        public decimal? GameplayRating { get; set; } // Oynanış
        public decimal? GraphicsRating { get; set; } // Grafik
        public decimal? SoundRating { get; set; } // Ses/Müzik
        public decimal? StoryRating { get; set; } // Hikaye
        public decimal? ValueRating { get; set; } // Fiyat/Performans
        public decimal? DifficultyRating { get; set; } // Zorluk seviyesi

        // Oyun Bilgileri
        public int HoursPlayed { get; set; } = 0;
        public Platform PlayedOnPlatform { get; set; }
        public CompletionStatus CompletionStatus { get; set; }
        public bool IsRecommended { get; set; } = true;

        // Review Özellikleri
        public ReviewType Type { get; set; } = ReviewType.Full;
        public bool ContainsSpoilers { get; set; } = false;
        public bool IsVerifiedPurchase { get; set; } = false;
        public DateTime? PurchaseDate { get; set; }

        // Sosyal ve İstatistik
        public int LikeCount { get; set; } = 0;
        public int DislikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int ViewCount { get; set; } = 0;
        public int HelpfulCount { get; set; } = 0; // "Bu review yararlı mı?" puanı
        public int UnhelpfulCount { get; set; } = 0;

        // İçerik Moderasyonu
        public ReviewStatus Status { get; set; } = ReviewStatus.Published;
        public bool IsFeatured { get; set; } = false; // Öne çıkarılmış review
        public bool IsEditorChoice { get; set; } = false; // Editor seçimi
        public int ReportCount { get; set; } = 0;

        // Metadata
        public string? Tags { get; set; } // Virgülle ayrılmış etiketler
        public string? Pros { get; set; } // JSON array - artılar
        public string? Cons { get; set; } // JSON array - eksiler
        public string? MediaUrls { get; set; } // JSON array - screenshot/video

        // Review Güncelleme
        public bool IsEdited { get; set; } = false;
        public DateTime? EditedDate { get; set; }
        public string? EditReason { get; set; }
        public int EditCount { get; set; } = 0;

        // SEO ve Keşfedilebilirlik
        public string? Slug { get; set; }
        public string? MetaDescription { get; set; }

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual Game Game { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<ReviewHelpful> HelpfulVotes { get; set; } = new List<ReviewHelpful>();
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
        public virtual ICollection<ReviewHistory> ReviewHistories { get; set; } = new List<ReviewHistory>();

        // Computed Properties
        public decimal NetScore => LikeCount - DislikeCount;
        public decimal HelpfulScore => HelpfulCount - UnhelpfulCount;
        public decimal HelpfulPercentage => (HelpfulCount + UnhelpfulCount) > 0
            ? (decimal)HelpfulCount / (HelpfulCount + UnhelpfulCount) * 100 : 0;
    }
}
