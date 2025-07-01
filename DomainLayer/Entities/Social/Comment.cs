using DomainLayer.Common;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Social
{
    public class Comment : BaseEntity
    {
        // Temel Bilgiler
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }

        // Hierarchical Comment System (Nested Comments)
        public int? ParentCommentId { get; set; }
        public int Level { get; set; } = 0; // 0 = ana yorum, 1+ = cevap seviyeleri

        // Polymorphic Comment System - Hangi entity'ye yorum yapıldığı
        public CommentableType CommentableType { get; set; }
        public int CommentableId { get; set; }

        // İstatistikler
        public int LikeCount { get; set; } = 0;
        public int DislikeCount { get; set; } = 0;
        public int ReplyCount { get; set; } = 0;
        public int ReportCount { get; set; } = 0;

        // Durum ve Moderasyon
        public CommentStatus Status { get; set; } = CommentStatus.Published;
        public bool IsEdited { get; set; } = false;
        public DateTime? EditedDate { get; set; }
        public bool IsPinned { get; set; } = false;
        public bool IsSpoiler { get; set; } = false;

        // Moderasyon
        public string? ModerationReason { get; set; }
        public int? ModeratedBy { get; set; }
        public DateTime? ModeratedDate { get; set; }

        // İçerik Özellikleri
        public bool ContainsMedia { get; set; } = false;
        public string? AttachedMediaUrls { get; set; } // JSON array
        public string? Mentions { get; set; } // JSON array - @username'ler

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual Comment? ParentComment { get; set; }
        public virtual ICollection<Comment> Replies { get; set; } = new List<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
        public virtual ICollection<CommentHistory> CommentHistories { get; set; } = new List<CommentHistory>();

        // Computed Properties
        public int NetScore => LikeCount - DislikeCount;
        public bool HasReplies => ReplyCount > 0;
    }
}
