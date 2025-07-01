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
    public class ForumPost : BaseEntity
    {
        // Temel Bilgiler
        public string Content { get; set; } = string.Empty;
        public int UserId { get; set; }
        public int TopicId { get; set; }

        // Hierarchical Posts (Nested replies)
        public int? ParentPostId { get; set; }
        public int Level { get; set; } = 0;

        // Post Özellikleri
        public PostType Type { get; set; } = PostType.Reply;
        public bool IsAnswer { get; set; } = false; // Soru-cevap için
        public bool IsAcceptedAnswer { get; set; } = false;
        public bool IsBestAnswer { get; set; } = false;

        // Sosyal Etkileşim
        public int LikeCount { get; set; } = 0;
        public int DislikeCount { get; set; } = 0;
        public int ReplyCount { get; set; } = 0;
        public int ReportCount { get; set; } = 0;

        // İçerik Özellikleri
        public bool ContainsCode { get; set; } = false;
        public bool ContainsSpoilers { get; set; } = false;
        public string? AttachedFiles { get; set; } // JSON array
        public string? QuotedPostIds { get; set; } // JSON array - alıntılanan postlar

        // Moderasyon
        public PostStatus Status { get; set; } = PostStatus.Published;
        public bool IsEdited { get; set; } = false;
        public DateTime? EditedDate { get; set; }
        public string? EditReason { get; set; }
        public int EditCount { get; set; } = 0;

        // Reputation ve Puan
        public int HelpfulCount { get; set; } = 0;
        public int UnhelpfulCount { get; set; } = 0;
        public decimal ReputationChange { get; set; } = 0; // Bu post kullanıcıya ne puan kazandırdı

        // Navigation Properties
        public virtual User User { get; set; } = null!;
        public virtual ForumTopic Topic { get; set; } = null!;
        public virtual ForumPost? ParentPost { get; set; }
        public virtual ICollection<ForumPost> Replies { get; set; } = new List<ForumPost>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<PostHelpful> HelpfulVotes { get; set; } = new List<PostHelpful>();
        public virtual ICollection<Report> Reports { get; set; } = new List<Report>();
        public virtual ICollection<PostHistory> PostHistories { get; set; } = new List<PostHistory>();

        // Computed Properties
        public decimal NetScore => LikeCount - DislikeCount;
        public decimal HelpfulScore => HelpfulCount - UnhelpfulCount;
    }
}
