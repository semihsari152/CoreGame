using DomainLayer.Common;
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
    public class BlogPost : BaseEntity
    {
        // Temel Bilgiler
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public string? Excerpt { get; set; }
        public int AuthorId { get; set; }

        // Blog Özellikleri
        public BlogPostType Type { get; set; } = BlogPostType.Article;
        public ContentStatus Status { get; set; } = ContentStatus.Draft;
        public bool IsFeatured { get; set; } = false;
        public bool IsSticky { get; set; } = false;
        public bool AllowComments { get; set; } = true;

        // Yayın Bilgileri
        public DateTime? PublishedDate { get; set; }
        public DateTime? ScheduledDate { get; set; }

        // Sosyal ve İstatistikler
        public int ViewCount { get; set; } = 0;
        public int LikeCount { get; set; } = 0;
        public int DislikeCount { get; set; } = 0;
        public int CommentCount { get; set; } = 0;
        public int ShareCount { get; set; } = 0;

        // İçerik Özellikleri
        public string? FeaturedImageUrl { get; set; }
        public string? VideoUrl { get; set; }
        public string? AudioUrl { get; set; }
        public int? ReadingTime { get; set; } // Dakika cinsinden tahmini okuma süresi

        // Kategoriler ve Etiketler
        public string? Categories { get; set; } // JSON array
        public string? Tags { get; set; } // Virgülle ayrılmış

        // SEO
        public string? Slug { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }

        // İlgili İçerik
        public string? RelatedGameIds { get; set; } // JSON array
        public string? RelatedPostIds { get; set; } // JSON array

        // Moderasyon
        public int? ReviewedBy { get; set; }
        public DateTime? ReviewedDate { get; set; }
        public string? ReviewNotes { get; set; }

        // Navigation Properties
        public virtual User Author { get; set; } = null!;
        public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();
        public virtual ICollection<Like> Likes { get; set; } = new List<Like>();
        public virtual ICollection<Media> MediaFiles { get; set; } = new List<Media>();
        public virtual User? Reviewer { get; set; }
    }
}
