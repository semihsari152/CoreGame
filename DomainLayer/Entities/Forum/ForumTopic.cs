using DomainLayer.Common;
using DomainLayer.Entities.Social;
using DomainLayer.Entities.Users;
using DomainLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Forum
{
    public class ForumTopic : BaseEntity
    {
        // Temel Bilgiler
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty; // İlk post içeriği
        public int UserId { get; set; }
        public int CategoryId { get; set; }

        // Topic Özellikleri
        public TopicType Type { get; set; } = TopicType.Discussion;
        public TopicStatus Status { get; set; } = TopicStatus.Open;
        public TopicPriority Priority { get; set; } = TopicPriority.Normal;

        // Moderasyon ve Durum
        public bool IsSticky { get; set; } = false; // Sabitlenmiş
        public bool IsLocked { get; set; } = false;
        public bool IsClosed { get; set; } = false;
        public bool IsApproved { get; set; } = true;
        public bool IsFeatured { get; set; } = false;
        public bool IsAnnouncement { get; set; } = false;

        // İstatistikler
        public int ViewCount { get; set; } = 0;
        public int PostCount { get; set; } = 0; // Reply sayısı
        public int LikeCount { get; set; } = 0;
        public int FollowCount { get; set; } = 0; // Kaç kişi takip ediyor

        // Son Aktivite
        public DateTime? LastPostDate { get; set; }
        public int? LastPostUserId { get; set; }
        public int? LastPostId { get; set; }

        // Soru-Cevap Özelliği
        public bool HasAcceptedAnswer { get; set; } = false;
        public int? AcceptedAnswerId { get; set; }
        public int? BestAnswerSelectedBy { get; set; }
        public DateTime? BestAnswerSelectedDate { get; set; }

        // İçerik Özellikleri
        public string? Tags { get; set; } // Virgülle ayrılmış etiketler
        public bool ContainsSpoilers { get; set; } = false;
        public string? AttachedFiles { get; set; } // JSON array
        public string? RelatedGameIds { get; set; } // JSON array - ilgili oyunlar

        // SEO ve Keşfedilebilirlik
        public string? Slug { get; set; }
        public string? MetaDescription { get; set; }

        // Moderasyon
        public string? ModerationNotes { get; set; }
        public int? ModeratedBy { get; set; }
        public DateTime? ModeratedDate { get; set; }

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual ForumCategory Category { get; set; } = null!;
        public virtual ICollection<ForumPost> Posts { get; set; } = new List<ForumPost>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<TopicFollow> Followers { get; set; } = new List<TopicFollow>();
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual User? LastPostUser { get; set; }
        public virtual ForumPost? LastPost { get; set; }
        public virtual ForumPost? AcceptedAnswer { get; set; }
        public virtual User? BestAnswerSelector { get; set; }
        public virtual User? Moderator { get; set; }
    }
}
