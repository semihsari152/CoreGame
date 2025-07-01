using DomainLayer.Common;
using DomainLayer.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainLayer.Entities.Forum
{
    public class ForumCategory : BaseEntity
    {
        // Temel Bilgiler
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string? IconUrl { get; set; }
        public string ColorCode { get; set; } = "#007bff";

        // Hiyerarşi (Alt kategoriler için)
        public int? ParentCategoryId { get; set; }
        public int Level { get; set; } = 0;
        public string? Path { get; set; } // Breadcrumb için: "Games/RPG/Skyrim"

        // Görünüm ve Sıralama
        public int DisplayOrder { get; set; } = 0;
        public bool IsActive { get; set; } = true;
        public bool IsVisible { get; set; } = true;
        public bool IsLocked { get; set; } = false;

        // İstatistikler (Cache)
        public int TopicCount { get; set; } = 0;
        public int PostCount { get; set; } = 0;
        public int TotalViews { get; set; } = 0;

        // Son Aktivite
        public DateTime? LastPostDate { get; set; }
        public int? LastPostUserId { get; set; }
        public int? LastTopicId { get; set; }

        // İzinler ve Moderasyon
        public string? AllowedRoles { get; set; } // JSON array - hangi roller görebilir
        public string? PostPermissions { get; set; } // JSON array - kim post atabilir
        public bool RequiresApproval { get; set; } = false;
        public string? ModeratorIds { get; set; } // JSON array - moderatör ID'leri

        // SEO
        public string? Slug { get; set; }
        public string? MetaDescription { get; set; }

        // Navigation Properties
        public virtual ForumCategory? ParentCategory { get; set; }
        public virtual ICollection<ForumCategory> SubCategories { get; set; } = new List<ForumCategory>();
        public virtual ICollection<ForumTopic> Topics { get; set; } = new List<ForumTopic>();
        public virtual User? LastPostUser { get; set; }
        public virtual ForumTopic? LastTopic { get; set; }
    }
}
